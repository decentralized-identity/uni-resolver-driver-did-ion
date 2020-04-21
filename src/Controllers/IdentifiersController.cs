using System.Threading.Tasks;
using System.Net;
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
    public class IdentifiersController : ControllerBase
    {
        /// <summary>
        /// GET method for resolving ION identifiers
        /// </summary>
        /// <param name="identifier">The identifier to resolve.</param>
        /// <returns>JSON document for the resolved identifier.</returns>
        /// <exception>Returns 400 BadRequest if <paramref name="identifier"> specifies an unsupported methdod.</exception>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [Route( "/1.0/identifiers/{identifier}" )]
        [HttpGet]
        public async Task<ActionResult<JObject>> Get(string identifier)
        {   
            if (!Resolver.IsSupported(identifier)){
                return BadRequest("The specified DID method is not supported. Only ION (https://github.com/decentralized-identity/ion) based identifiers can be resolved.");
            }

            JObject document;
            try {
                using (Resolver resolver = new Resolver()){
                    document = await resolver.Resolve(identifier);
                }
            }
            catch (ConnectionException connectionException) {
                // Have defaulted to BadRequest, but more
                // cases can be added to return specific
                // responses if needed.
                switch (connectionException.StatusCode) {
                    case HttpStatusCode.NotFound:
                        return NotFound();
                    default:
                        return BadRequest();
                }
            }
            
            return Ok(document);
        }
    }
}
