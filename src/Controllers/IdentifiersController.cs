using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json.Linq;

namespace IdentityOverlayNetwork.Controllers
{
    /// <summary>
    /// Controller for exposing GET for resolving
    /// identifiers.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [Microsoft.AspNetCore.Mvc.Infrastructure.DefaultStatusCode(400)]
    public class IdentifiersController : Controller
    {
        /// <summary>
        /// Constant for the ION initial state querystring
        /// parameter key.
        /// </summary>
        private const string IonInitialStateKey = "-ion-initial-state";

        /// <summary>
        /// Instance of the <see cref="connection" /> for making the
        /// resolution requests.
        /// </summary>
        private readonly Connection connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="Resolver" /> class.
        /// </summary>
        /// <param name="httpClientFactory">The <see cref="IHttpClientFactory" /> to initialize the instance with. Injecteed from service context when being called from servvice.null</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="httpClientFactory"> is null.</exception>
        //public IdentifiersController([FromServices] Connection connection) 
        public IdentifiersController([FromServices] IHttpClientFactory httpClientFactory)
        {
            // Set the private instance
            this.connection = new Connection(httpClientFactory.IsNull("connection"));
        }

        /// <summary>
        /// GET method for resolving ION identifiers
        /// </summary>
        /// <param name="identifier">The identifier to resolve.</param>
        /// <returns>JSON document for the resolved identifier.</returns>
        /// <exception>Returns 400 BadRequest if <paramref name="identifier"> specifies an unsupported methdod.</exception>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ResponseCache(Duration=360)] // Cache the response for 5 mins
        [Produces("application/json")]
        [Route( "/1.0/identifiers/{identifier}" )]
        [HttpGet]
        public async Task<IActionResult> Get(string identifier)
        {   
            if (!Resolver.IsSupported(identifier))
            {
                
                Error unsupportedIdentifier = new Error
                {
                    Message = "The specified DID method is not supported. Only ION (https://github.com/decentralized-identity/ion) based identifiers can be resolved.",
                    Type = Error.Types.RequestResolveIdentifier,
                    Code = Error.Codes.UnsupportedDidMethod,
                    CorrelationId = this.GetCorrelationId()
                };

                return BadRequest(unsupportedIdentifier);
            }

            JObject document;
            try 
            {
                using (Resolver resolver = new Resolver(this.connection))
                {
                    document = await resolver.Resolve(this.PrepareIdentifier(identifier));
                }
            }
            catch (ConnectionException connectionException) 
            {
                // If we get a 404, just return a vanilla
                // not found
                if (connectionException.StatusCode == HttpStatusCode.NotFound)
                {
                    return NotFound();
                }

                // For exceptions that are not 404,
                // wrap in an error and return
                Error connectionError = new Error
                {
                    Message = connectionException.ReasonPhrase,
                    Type = Error.Types.RequestResolveIdentifier,
                    Code = Error.Codes.RemoteServiceError,
                    CorrelationId = this.GetCorrelationId(),
                };

                return new ObjectResult(connectionError)
                {
                    StatusCode = (int)connectionException.StatusCode
                }; 
            }
            
            return Json(document);
        }

        /// <summary>
        /// Checks to see if the original request included
        /// initial state and if so appends as a query
        /// string parameter to the identifier.
        /// </summary>
        /// <param name="identifier">The identifier to which to add the state.</param>
        /// <returns>A string containing the identifier with state appended if exists in the request.</returns>
        public string PrepareIdentifier(string identifier)
        {
            identifier = identifier.IsPopulated("identifier");

            // Check if ION inital state included in the
            // query. If so ensure that we include in
            // the resolve.
            StringValues initialState = string.Empty;
            if (Request != null && Request.Query.TryGetValue(IdentifiersController.IonInitialStateKey, out initialState)) 
            {
                identifier = $"{identifier}?{IdentifiersController.IonInitialStateKey}={initialState}";
            }

            return identifier;
        }

        /// <summary>
        /// Gets the correlation id to use in errors.null If HttpContext exists
        /// uses the TraceIdentifier from that otherwise returns a <see cref="Guid" />
        /// </summary>
        /// <returns>A string conatning the correlation id.</returns>
        private string GetCorrelationId() 
        {
            return (HttpContext != null && !string.IsNullOrWhiteSpace(HttpContext.TraceIdentifier)) ? HttpContext.TraceIdentifier : Guid.NewGuid().ToString();
        }
    }
}
