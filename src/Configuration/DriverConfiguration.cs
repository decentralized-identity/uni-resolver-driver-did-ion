namespace IdentityOverlayNetwork.Configuration
{
    /// <summary>
    /// Driver configuration.
    /// </summary>
    public class DriverConfiguration
    {
        /// <summary>
        /// Gets or sets the <see cref="Consensus" />
        /// section of the driver configuration.
        /// </summary>
        public Consensus Consensus { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Node" />
        /// array section of the driver configuration.
        /// </summary>
        public Node[] Nodes { get; set; }
    }
}