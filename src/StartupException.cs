using System;
using System.Runtime.Serialization;

namespace IdentityOverlayNetwork
{
    /// <summary>
    /// The exception that is thrown when an exception
    /// occurs during start up.
    /// </summary>
    [Serializable]
    public class StartupException : Exception, ISerializable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StartupException" /> class.
        /// </summary>
        public StartupException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StartupException" /> class.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner Exception</param>
        public StartupException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StartupException" /> class.
        /// </summary>
        /// <param name="message">Exception message</param>
        public StartupException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StartupException" /> class.
        /// </summary>
        /// <param name="serializationInfo">The <see cref="SerializationInfo" /> from which to create the instance.</param>
        /// <param name="streamingContext">The <see cref="StreamingContext" /> used by serialization.</param>
        public StartupException(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
        }
    }
}
