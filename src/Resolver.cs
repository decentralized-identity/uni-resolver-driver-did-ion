using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace IdentityOverlayNetwork
{
    /// <summary>
    /// Class providing methods for validating and resolving
    /// identifiers
    /// </summary>
    public class Resolver
    {
        /// <summary>
        /// Regular expression for matching DID methods supported
        /// by the driver.
        /// </summary>
        public static readonly Regex SupportedMethods = new Regex("^did:(ion):(\\S*)$", MatchOptions, TimeSpan.FromMilliseconds(100)); 

        /// <summary>
        /// Constant declaring options for
        /// the DID supported methods regular expression.
        /// </summary>
        private const RegexOptions MatchOptions = RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline;

        /// <summary>
        /// Instance of the <see cref="Connection" /> for making the
        /// resolution requests.
        /// </summary>
        private readonly Connection Connection;

        /// <summary>
        /// Uses the regular expression to check if 
        /// <paramref name="identifier"> is a supported
        /// method.
        /// </summary>
        /// <param name="identifier">The identifier to check.</param>
        /// <returns>True if <paramref name="identifier"> is supported, otherwise false.</returns>
        public static bool IsSupported(string identifier) {

            // Only do work if passed an identifier
            if (string.IsNullOrWhiteSpace(identifier)) {
                return false;
            }

            return SupportedMethods.IsMatch(identifier);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Resolver" /> class.
        /// </summary>
        public Resolver() {
            // Use the static application connection
            this.Connection = Program.Connection;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Resolver" /> class.
        /// </summary>
        /// <param name="connection">The <see cref="Connection" /> to initialize the instance with.</param>
        public Resolver(Connection connection) {
            if (connection == null) {
                throw new ArgumentNullException("connection");
            }

            // Set the private instance
            this.Connection = connection;
        }

        /// <summary>
        /// Method providing a shim to the Microsoft's beta DID discovery
        /// service. TODO Look at pointing this to the Microsoft ION node
        /// when ready.
        /// </summary>
        /// <param name="identifier">The identifier to resolve.</param>
        /// <returns>A string containing the resolved identifier document.</returns>
        public async Task<JObject> Resolve(string identifier) {
            
            if (String.IsNullOrWhiteSpace(identifier)) {
                throw new ArgumentNullException("identifier");
            }

            // Define your base url //TODO configurable
            string baseURL = $"https://beta.discover.did.microsoft.com/1.0/identifiers/{identifier}";

            JObject jsonDocument = null;

            using (HttpContent httpContent = await this.Connection.GetAsync(baseURL))
            {
                // Read the document from the content
                string document = await httpContent.ReadAsStringAsync();

                if (!string.IsNullOrWhiteSpace(document)) {
                    jsonDocument = JObject.Parse(document);
                }
            }

            return jsonDocument;
        } 
    }
}
