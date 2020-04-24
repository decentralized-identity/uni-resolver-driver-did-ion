namespace IdentityOverlayNetwork.Configuration
{
    /// <summary>
    /// Driver configuration.
    /// </summary>
    public class DriverConfiguration
    {
        /// <summary>
        /// Gets or sets the <see cref="Resilience" />
        /// section of the driver configuration.
        /// </summary>
        public Resilience Resilience { get; set; } = new Resilience();

        /// <summary>
        /// Gets or sets the <see cref="Consensus" />
        /// section of the driver configuration.
        /// </summary>
        public Consensus Consensus { get; set; } = new Consensus();

        /// <summary>
        /// Gets or sets the <see cref="Node" />
        /// array section of the driver configuration.
        /// </summary>
        public Node[] Nodes { get; set; } = new Node[0];
    }
}