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
    public static class Identifier
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
        /// Method providing a shim to the Microsoft's beta DID discovery
        /// service. TODO Look at pointing this to the Microsoft ION node
        /// when ready.
        /// </summary>
        /// <param name="identifier">The identifier to resolve.</param>
        /// <returns>A string containing the resolved identifier document.</returns>
        public static async Task<JObject> Resolve(string identifier) {
            
            if (String.IsNullOrWhiteSpace(identifier)) {
                throw new ArgumentNullException("identifier");
            }

            // Define your base url //TODO configurable
            string baseURL = $"https://beta.discover.did.microsoft.com/1.0/identifiers/{identifier}";

            JObject jsonDocument = null;

            // Create an http client, wrap in using to
            // ensure clean up
            using (HttpClient httpClient = new HttpClient())
            {
                // Set the timeout //TODO configurable
                httpClient.Timeout = TimeSpan.FromSeconds(10);

                // Await the response from the request
                using (HttpResponseMessage responseMessage = await httpClient.GetAsync(baseURL))
                {
                    // Check if we have got an OK back, if not
                    // throw
                    if (!responseMessage.IsSuccessStatusCode) {
                        throw new HttpRequestException(responseMessage.ReasonPhrase);
                    }

                    using (HttpContent httpContent = responseMessage.Content)
                    {
                        // Read the document from the content
                        string document = await httpContent.ReadAsStringAsync();

                        if (!string.IsNullOrWhiteSpace(document)) {
                            jsonDocument = JObject.Parse(document);
                        }
                    }
                }
            }

            return jsonDocument;
        } 
    }
}
