using System;

namespace IdentityOverlayNetwork
{
    /// <summary>
    /// The exception that is thrown when an exception
    /// occurs during start up.
    /// </summary>
    public class StartupException : Exception
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
    }
}
