using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace uni_resolver_driver_ion.Controllers
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
        [Produces("application/json")]
        [Route( "/1.0/identifiers/{identifier}" )]
        [HttpGet]
        public async Task<ActionResult<JObject>> Get(string identifier)
        {   
            if (!Identifier.IsSupported(identifier)){
                return BadRequest("The specified DID method is not supported. Only ION (https://github.com/decentralized-identity/ion) based identifiers can be resolved.");
            }

            JObject document = await Identifier.Resolve(identifier);
            return Ok(document);
        }
    }
}
