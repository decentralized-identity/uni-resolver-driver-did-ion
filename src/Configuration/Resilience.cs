namespace IdentityOverlayNetwork.Configuration
{
    /// <summary>
    /// Represents the resilience entry in the
    /// driver configuration.
    /// </summary>
    public class Resilience
    {
        /// <summary>
        /// Gets or sets a value indicating whether
        /// request retry is enabled. Default is
        /// false.
        /// </summary>
        public bool EnableRetry { get; set;} = false;

        /// <summary>
        /// Gets or sets a valeu indicating whether
        /// circuit breaking is enabled. Default
        /// is false.
        /// </summary>
        public bool EnableCircuitBreaking { get; set;} = false;
    } 
}