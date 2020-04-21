using System;
using System.Net;

namespace IdentityOverlayNetwork
{
    /// <summary>
    /// The exception that is thrown for an invalid request
    /// </summary>
    public class ConnectionException : Exception
    {
        /// <summary>
        /// Gets the <see cref="HttpStatusCode" /> for the
        /// exception.
        /// </summary>
        public HttpStatusCode StatusCode { get; private set; }

        /// <summary>
        /// Gets the reason phrase for the
        /// exception.
        /// </summary>
        public string ReasonPhrase { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionException" /> class.
        /// </summary>
        /// <param name="statusCode">The <see cref="HttpStatusCode" /> from the http response.</param>
        /// <param name="reasonPhrase">The reason phrase corresponding to <paramref name="statusCode"/>.</param>
        public ConnectionException(HttpStatusCode statusCode, string reasonPhrase)
            : base()
        {
            this.StatusCode = statusCode;
            this.ReasonPhrase = reasonPhrase;
        }
    }
}
