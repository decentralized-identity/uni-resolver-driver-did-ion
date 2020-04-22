namespace IdentityOverlayNetwork
{
    /// <summary>
    /// Error class for returning errors
    /// to clients.
    /// </summary>
    public class Error{

        /// <summary>
        /// List of error type strings.
        /// </summary>
        public static class Types {

            /// <summary>
            /// A request to resolve an identifier.
            /// </summary>
            public const string RequestResolveIdentifier = "request_resolve_identifier";
        }

        /// <summary>
        /// List of error code strings.
        /// </summary>
        public static class Codes {
            
            /// <summary>
            /// Code indicating that unsupported DID methods
            /// was specified in the request.
            /// </summary>
            public const string UnsupportedDidMethod = "unsupported_did_method";
        }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        public string Message 
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the error type.
        /// </summary>
        public string Type 
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the error code.
        /// </summary>
        public string Code 
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a correlation id.
        /// </summary>
        public string CorrelationId 
        {
            get;
            set;
        }
    }
}