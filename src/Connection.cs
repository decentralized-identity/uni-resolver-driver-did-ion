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
        /// Constant int holding the default connection timeout
        /// in milliseconds. TODO: Make configurable
        /// </summary>
        public const int DefaultTimeoutInMilliseconds = 10000;
        
        /// <summary>
        /// Private instance of the <see cref="HttpClient" /> 
        /// </summary>
        private readonly HttpClient httpClient;

        /// <summary>
        /// Has the object been disposed of?
        /// </summary>
        private bool disposed  = false;

        /// <summary>
        /// Intance of <see cref="HttpResponseMessage" /> for
        /// the current connection.
        /// </summary>
        private HttpResponseMessage responseMessage;

        /// <summary>
        /// Gets a value for the time connection timeout.
        /// </summary>
        /// <value>A <see cref="int"> in millseconds</value>
        public int TimeoutInMilliseconds 
        {
            get {
                return (int)this.httpClient.Timeout.TotalMilliseconds;
            }
        }

        /// <summary>
        /// Initializes an instance of the <see cref="Connection" /> class.
        /// </summary>
        /// <param name="httpClient">The <see cref="HttpClient" /> to initialize the instance.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="httpClient"> is null.</exception>
        public Connection(HttpClient httpClient): this (httpClient, Connection.DefaultTimeoutInMilliseconds) {
        }

        /// <summary>
        /// Initializes an instance of the <see cref="Connection" /> class.
        /// </summary>
        /// <param name="httpClient">The <see cref="HttpClient" /> to initialize the instance.</param>
        /// <param name="timemoutInMilliseconds">A integer specifying the timeout in milliseconds</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="httpClient"> is null.</exception>
        public Connection(HttpClient httpClient, int timemoutInMilliseconds) {
            // Set the private instance
            this.httpClient = httpClient.IsNull("httpClient");
            
            // Set the timeout
            this.SetTimeout(timemoutInMilliseconds);
        }

        /// <summary>
        /// Initializes an instance of the <see cref="Connection" /> class.
        /// </summary>
        /// <param name="httpMessageHandler">The <see cref="HttpMessageHandler" /> to initialize the instance.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="httpMessageHandler"> is null.</exception>
        public Connection(HttpMessageHandler httpMessageHandler) : this (httpMessageHandler, Connection.DefaultTimeoutInMilliseconds) {
        }

        /// <summary>
        /// Initializes an instance of the <see cref="Connection" /> class.
        /// </summary>
        /// <param name="httpMessageHandler">The <see cref="HttpMessageHandler" /> to initialize the instance.</param>
        /// <param name="timemoutInMilliseconds">A integer specifying the timeout in milliseconds</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="httpMessageHandler"> is null.</exception>
        public Connection(HttpMessageHandler httpMessageHandler, int timemoutInMilliseconds) {
            
            // Check the argument
            httpMessageHandler = httpMessageHandler.IsNull("httpMessageHandler");

            // Set the private instance
            this.httpClient = new HttpClient(httpMessageHandler);
            
            // Set the timeout
            this.SetTimeout(timemoutInMilliseconds);
        }

        /// <summary>
        /// Gets the content from <paramref name="requestUri"/>.
        /// </summary>
        /// <param name="requestUri">The request uri string from which to get the content.</param>
        /// <returns>The <see cref="HttpContent"/> returned in the response.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="requestUri"> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="requestUri"> is empty or is whitespace.</exception>
        /// <exception cref="ConnectionException">Thrown when an exception is received makign the request to <paramref name="requestUri">.</exception>
        public async Task<HttpContent> GetAsync(string requestUri){

            // Check the argument
            requestUri = requestUri.IsPopulated("requestUri");

            // Await the response from the request
            this.responseMessage = await this.httpClient.GetAsync(requestUri);
            
            // Check if we have got an OK back, if not
            // throw passing up the reason.
            if (!responseMessage.IsSuccessStatusCode) {
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
              
            // Despose of the response message
            this.responseMessage.Dispose();
        }

        /// <summary>
        /// Sets the timeout on the <see cref="HttpClient"> used
        /// by the connection.
        /// </summary>
        /// <param name="timemoutInMilliseconds">An integer specifiying the timeout in milliseconds.see If argument 
        /// is the default values for an integer, the <see cref="Connection.DefaultTimeoutInMilliseconds" /> is used.</param>
        private void SetTimeout(int timemoutInMilliseconds = default(int)){
            // Set the timeout, checking whether we need to set the default connection
            // timeout when the provided argument has the default of 0
            timemoutInMilliseconds = timemoutInMilliseconds.IsDefault() ?  Connection.DefaultTimeoutInMilliseconds : timemoutInMilliseconds;               
            this.httpClient.Timeout = TimeSpan.FromMilliseconds(timemoutInMilliseconds);
        }
    }
}