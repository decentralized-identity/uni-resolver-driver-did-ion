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
        /// GET method for resolving ION identifiers.
        /// </summary>
        /// <param name="identifier">The identifier to resolve.</param>
        /// <returns>JSON document for the resolved identifier.</returns>
        /// <exception>Returns 400 BadRequest if <paramref name="identifier"> specifies an unsupported methdod.</exception>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ResponseCache(Duration=60)] // Cache the response for 1 min
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
                    CorrelationId = CorrelationIdentifier.Get(this.httpContextAccessor)
                };

                return BadRequest(unsupportedIdentifier);
            }

            JObject document;
            try 
            {
                using (Resolver resolver = new Resolver(this.connection))
                {
                    document = await resolver.Resolve(identifier);
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
