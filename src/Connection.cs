using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace IdentityOverlayNetwork
{
    /// <summary>
    /// Connection class for fetching data from
    /// remote servers.
    /// </summary>
    public class Connection : IDisposable
    {
        /// <summary>
        /// Has the object been disposed of?
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// Intance of <see cref="HttpResponseMessage" /> for
        /// the current connection.
        /// </summary>
        private HttpResponseMessage responseMessage;

        /// <summary>
        /// Instance of the <see cref="IHttpClientFactory" /> for creating
        /// clients <see cref="HttpClient"/>.
        /// </summary>
        private readonly IHttpClientFactory httpClientFactory;

        /// <summary>
        /// Initializes an instance of the <see cref="Connection" /> class.
        /// </summary>
        /// <param name="httpClient">The <see cref="HttpClient" /> to initialize the instance.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="httpClient"> is null.</exception>
        public Connection(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory.IsNull("httpClientFactory");
        }

        /// <summary>
        /// Gets the content from <paramref name="requestUri"/>.
        /// </summary>
        /// <param name="identifier">The identifier string for which to get the content.</param>
        /// <returns>The <see cref="HttpContent"/> returned in the response.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="requestUri"> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="requestUri"> is empty or is whitespace.</exception>
        /// <exception cref="ConnectionException">Thrown when an exception is received makign the request to <paramref name="requestUri">.</exception>
        public async Task<HttpContent> GetAsync(string identifier)
        {
            // Check the argument
            identifier = identifier.IsPopulated("requestUri");

            // Get the http client TODO Update factory logic to return default
            // client and support random client selection etc
            HttpClient httpClient = this.httpClientFactory.CreateClient("test.direct.ion");

            // TODO temporary workaround until I figure out
            // why baseaddress is not included getasync below
            // resulting in an invalid URI exception
            identifier = httpClient.BaseAddress.ToString() + identifier;

            // Await the response from the request
            this.responseMessage = await httpClient.GetAsync(identifier);

            // Check if we have got an OK back, if not
            // throw passing up the reason.
            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new ConnectionException(responseMessage.StatusCode, responseMessage.ReasonPhrase);
            }

            return this.responseMessage.Content;
        }

        /// <summary>
        /// Dispose of the object
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose of the connection object.
        /// </summary>
        /// <param name="disposing">True if the method is called from user code, false if called by finalizer.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed || !disposing)
            {
                return;
            }

            // Update the flag to indicate dispose
            // has been called
            this.disposed = true;

            // Despose of the response message if
            // not already disposed
            if (this.responseMessage != null)
            {
                this.responseMessage.Dispose();
            }
        }
    }
}