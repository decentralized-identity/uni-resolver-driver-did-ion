using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace IdentityOverlayNetwork
{
    /// <summary>
    /// Connection class for fetching data from
    /// remote servers.
    /// </summary>
    public class Connection
    {
        /// <summary>
        /// Private instance of the <see cref="HttpClient" /> 
        /// </summary>
        private readonly HttpClient httpClient;

        /// <summary>
        /// Initializes an instance of the <see cref="Connection" /> class.
        /// </summary>
        /// <param name="httpClient">The <see cref="HttpClient" /> to initialize the instance.</param>
        public Connection(HttpClient httpClient) {
            if (httpClient == null) {
                throw new ArgumentNullException("httpClient");
            }

            // Set the private instance
            this.httpClient = httpClient;
            
            // Set the timeout //TODO configurable
            this.httpClient.Timeout = TimeSpan.FromSeconds(10);
        }

        /// <summary>
        /// Initializes an instance of the <see cref="Connection" /> class.
        /// </summary>
        /// <param name="httpMessageHandler">The <see cref="HttpMessageHandler" /> to initialize the instance.</param>
        public Connection(HttpMessageHandler httpMessageHandler) {
            if (httpMessageHandler == null) {
                throw new ArgumentNullException("httpMessageHandler");
            }

            // Set the private instance
            this.httpClient = new HttpClient(httpMessageHandler);
             
             // Set the timeout //TODO configurable
            this.httpClient.Timeout = TimeSpan.FromSeconds(10);
        }

        /// <summary>
        /// Gets the content from the URI specified 
        /// by <paramref name="requestUri"/>.
        /// </summary>
        /// <param name="requestUri">The request URI string from which to get the content.</param>
        /// <returns>The <see cref="HttpContent"/> returned in the response.</returns>
        public async Task<HttpContent> GetAsync(string requestUri){
            if (string.IsNullOrWhiteSpace(requestUri)) {
                throw new ArgumentNullException("requestUri");
            }

            // Await the response from the request
            using (HttpResponseMessage responseMessage = await this.httpClient.GetAsync(requestUri))
            {
                // Check if we have got an OK back, if not
                // throw passing up the reason.
                if (!responseMessage.IsSuccessStatusCode) {
                    throw new ConnectionException(responseMessage.StatusCode, responseMessage.ReasonPhrase);
                }

                return responseMessage.Content;
            }
        }
    }
}