using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        /// Instance of the <see cref="Connection" /> for making the
        /// resolution requests.
        /// </summary>
        private readonly Connection connection;
        
        /// <summary>
        /// Instance of the <see cref="IHttpContextAccessor" /> for
        /// accessing the http context.
        /// </summary>
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="Resolver" /> class.
        /// </summary>
        /// <param name="httpClientFactory">The <see cref="IHttpClientFactory" /> to initialize the instance with. Injected from service context when being called from service.</param>
        /// <param name="httpContextAccessor">The <see cref="IHttpContextAccessor" /> to initialize the instance with. Injected from service context when being called from service.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="httpClientFactory"> is null.</exception>
        public IdentifiersController([FromServices] IHttpClientFactory httpClientFactory, [FromServices] IHttpContextAccessor httpContextAccessor)
        {
            // Set the private instances
            this.connection = new Connection(httpClientFactory.IsNull("connection"));
            this.httpContextAccessor = httpContextAccessor.IsNull("httpContextAccessor");
        }

        /// <summary>
        /// Checks if <paramref name="initialState"/> has
        /// a value and if so appends as a query
        /// string parameter to the identifier.
        /// </summary>
        /// <param name="identifier">The identifier to which to add the state.</param>
        /// <param name="initialState">The initialState to add the identifier if not null.</param>
        /// <returns>A string containing the identifier with state appended if exists in the request.</returns>
        public static string PrepareIdentifier(string identifier, string initialState = null)
        {
            identifier = identifier.IsPopulated("identifier");

            // Check if ION inital state has a value
            if (initialState != null && !string.IsNullOrWhiteSpace(initialState)) 
            {
                identifier = $"{identifier}?{IdentifiersController.IonInitialStateKey}={initialState}";
            }

            return identifier;
        }

        /// <summary>
        /// GET method for resolving ION identifiers
        /// </summary>
        /// <param name="identifier">The identifier to resolve.</param>
        /// <param name="initialState">The ion initial state if included in the request.</param>
        /// <returns>JSON document for the resolved identifier.</returns>
        /// <exception>Returns 400 BadRequest if <paramref name="identifier"> specifies an unsupported methdod.</exception>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ResponseCache(Duration=360)] // Cache the response for 5 mins
        [Produces("application/json")]
        [Route( "/1.0/identifiers/{identifier}" )]
        [HttpGet]
        public async Task<IActionResult> Get(string identifier, [FromQuery(Name = IonInitialStateKey)] string initialState = null)
        {   
            if (!Resolver.IsSupported(identifier))
            {
                
                Error unsupportedIdentifier = new Error
                {
                    Message = "The specified DID method is not supported. Only ION (https://github.com/decentralized-identity/ion) based identifiers can be resolved.",
                    Type = Error.Types.RequestResolveIdentifier,
                    Code = Error.Codes.UnsupportedDidMethod,
                    CorrelationId = CorrelationIdentifier.Get(this.httpContextAccessor)
                };

                return BadRequest(unsupportedIdentifier);
            }

            JObject document;
            try 
            {
                using (Resolver resolver = new Resolver(this.connection))
                {
                    document = await resolver.Resolve(IdentifiersController.PrepareIdentifier(identifier, initialState));
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
                    CorrelationId =  CorrelationIdentifier.Get(this.httpContextAccessor)
                };

                return new ObjectResult(connectionError)
                {
                    StatusCode = (int)connectionException.StatusCode
                }; 
            }
            
            return Json(document);
        }
    }
}
