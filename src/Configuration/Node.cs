using System;

namespace IdentityOverlayNetwork.Configuration
{
    /// <summary>
    /// Represents a node entry in the
    /// driver configuration.
    /// </summary>
    public class Node
    {
        /// <summary>
        /// Specifies the how the node should
        /// be used.
        /// </summary>
        public enum UseType
        {
            /// <summary>
            /// Specifies that the node should
            /// be used randomly.
            /// </summary>
            Random = 0,

            /// <summary>
            /// Specifies that the node should
            /// always be used when resolving.
            /// </summary>
            Always
        }

        /// <summary>
        /// Gets or sets the name of the node.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the request uri
        /// for the node.
        /// </summary>
        public Uri Uri { get; set; }

        /// <summary>
        /// Gets or sets the timeout for
        /// the node in milliseconds.
        /// </summary>
        public int TimeoutInMilliseconds { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="UseType"> for
        /// the node. Default is <see cref="UseType.Random">.
        /// </summary>
        /// <remarks>Not currently used.</remarks>
        public UseType Use { get; set; } = UseType.Random;

        /// <summary>
        /// Gets or sets a value indicating whether
        /// responses from the node are allowed to be
        /// cached by the driver for a period of time.
        /// Default is true.
        /// </summary>
        /// <remarks>Not currently used.</remarks>
        public bool AllowCaching { get; set; } = true;
    }
}
