namespace IdentityOverlayNetwork.Configuration
{
    /// <summary>
    /// Represents a consensus entry in the
    /// driver configuration.
    /// </summary>
    public class Consensus
    {
        /// <summary>
        /// Specifies the learning mode for a given feature or setting.
        /// </summary>
        public enum ModelType
        {
            /// <summary>
            /// Specifies that learning mode is off. This is the default.
            /// </summary>
            FirstWins = 0,

            /// <summary>
            /// Specifies that learning mode is on. This is the default.
            /// </summary>
            PartialAgreement,

            /// <summary>
            /// Specifies that learning mode is on. This is the default.
            /// </summary>
            FullAgrement
        }

        /// <summary>
        /// Gets or sets the <see cref="ModelType" /> being
        /// used by the driver.
        /// </summary>
        public ModelType Model { get; set;} = ModelType.FirstWins;

        /// <summary>
        /// Gets or sets an integer indicating the
        /// number of nodes that need to be in agreement
        /// when <see cref="ModelType.FullAgrement" />
        /// </summary>
        public int InAgreement { get; set; } = 1;

    } 
}