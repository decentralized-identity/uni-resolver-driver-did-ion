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
        /// Gets or sets the name of the node.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the request uri
        /// for the node.
        /// </summary>
        public Uri Uri { get; set; }
    }
}