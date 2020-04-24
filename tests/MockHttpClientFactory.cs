using System.Net.Http;

namespace IdentityOverlayNetwork.Tests
{
    /// <summary>
    /// Mock of implementation of <see cref="IHttpClientFactory" />
    /// </summary>
    public class MockHttpClientFactory : IHttpClientFactory
    {
        /// <summary>
        /// Instance of <see cref="MockHttpMessageHandler" />
        /// for mocking responses.
        /// </summary>
        private MockHttpMessageHandler mockHttpMessageHandler;

        /// <summary>
        /// Initialized a new instance of the <see cref="MockHttpClientFactory" /> class.
        /// </summary>
        /// <param name="mockHttpMessageHandler">The <see cref="MockHttpMessageHandler" /> to use,</param>
        public MockHttpClientFactory(MockHttpMessageHandler mockHttpMessageHandler)
        {
            this.mockHttpMessageHandler = mockHttpMessageHandler.IsNull("mockHttpMessageHandler");
        }

        /// <summary>
        /// Returns an http client that can be used for
        /// requests.
        /// </summary>
        /// <param name="clientName">The name of the client. Ignored.</param>
        /// <returns>A <see cref="HttpClient" />.</returns>
        public HttpClient CreateClient(string clientName)
        {
            return new HttpClient(this.mockHttpMessageHandler)
            {
                BaseAddress = new System.Uri("https://testuri/")
            };
        }
    }
}