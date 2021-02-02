using System;
using System.Net;
using System.Runtime.Serialization;

namespace IdentityOverlayNetwork
{
    /// <summary>
    /// The exception that is thrown for an invalid request
    /// </summary>
    [Serializable]
    public class ConnectionException : Exception, ISerializable
    {
        /// <summary>
        /// Constant for serialization info property.
        /// </summary>
        const string StatusCodePropertyKey = "StatusCode";
        
        /// <summary>
        /// Constant for serialization info property.
        /// </summary>
        const string ReasonPhrasePropertyKey = "ReasonPhrase";

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

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionException" /> class.
        /// </summary>
        /// <param name="serializationInfo">The <see cref="SerializationInfo" /> from which to create the instance.</param>
        /// <param name="streamingContext">The <see cref="StreamingContext" /> used by serialization.</param>
        public ConnectionException(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
            // Reset the property value using the GetValue method.
            this.StatusCode = (HttpStatusCode) serializationInfo.GetValue(ConnectionException.StatusCodePropertyKey, typeof(HttpStatusCode));
            this.ReasonPhrase = (string) serializationInfo.GetValue(ConnectionException.ReasonPhrasePropertyKey, typeof(string));
        }

        /// <summary>
        /// Adds the data associated with the instance to the serialization info.
        /// </summary>
        /// <param name="serializationInfo">The <see cref="SerializationInfo" /> from which to create the instance.</param>
        /// <param name="streamingContext">The <see cref="StreamingContext" /> used by serialization.</param>
        public override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
            // Use the AddValue method to specify serialized values.
            serializationInfo.AddValue(ConnectionException.StatusCodePropertyKey, this.StatusCode, typeof(HttpStatusCode));
            serializationInfo.AddValue(ConnectionException.ReasonPhrasePropertyKey, this.ReasonPhrase, typeof(string));
        }
    }
}
