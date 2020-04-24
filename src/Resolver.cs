using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace IdentityOverlayNetwork
{
    /// <summary>
    /// Class providing methods for validating and resolving
    /// identifiers
    /// </summary>
    public class Resolver : IDisposable
    {
        /// <summary>
        /// Regular expression for matching DID methods supported
        /// by the driver.
        /// </summary>
        public static readonly Regex SupportedMethods = new Regex("^did:(ion):(\\S*)$", MatchOptions, TimeSpan.FromMilliseconds(100));

        /// <summary>
        /// Constant declaring options for
        /// the DID supported methods regular expression.
        /// </summary>
        private const RegexOptions MatchOptions = RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline;

        /// <summary>
        /// Instance of the <see cref="Connection" /> for making the
        /// resolution requests.
        /// </summary>
        private readonly Connection connection;

        /// <summary>
        /// Has the object been disposed of?
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// Uses the regular expression to check if 
        /// <paramref name="identifier"> is a supported
        /// method.
        /// </summary>
        /// <param name="identifier">The identifier to check.</param>
        /// <returns>True if <paramref name="identifier"> is supported, otherwise false.</returns>
        public static bool IsSupported(string identifier)
        {
            // Only do work if passed an identifier
            if (string.IsNullOrWhiteSpace(identifier) || string.IsNullOrEmpty(identifier))
            {
                return false;
            }

            return SupportedMethods.IsMatch(identifier);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Resolver" /> class.
        /// </summary>
        /// <param name="connection">The <see cref="Connection" /> to initialize the instance with.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="connection"> is null.</exception>
        public Resolver(Connection connection)
        {
            // Set the private instance
            this.connection = connection.IsNull("connection");
        }

        /// <summary>
        /// Method providing a shim to the Microsoft's beta DID discovery
        /// service. TODO Look at pointing this to the Microsoft ION node
        /// when ready.
        /// </summary>
        /// <param name="identifier">The identifier to resolve.</param>
        /// <returns>A string containing the resolved identifier document.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="argument"> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="argument"> is empty or is whitespace.</exception>
        public async Task<JObject> Resolve(string identifier)
        {
            // Check the argument
            identifier = identifier.IsPopulated("identifier");

            JObject jsonDocument = null;
            using (HttpContent httpContent = await this.connection.GetAsync(identifier))
            {
                // Read the document from the content
                string document = await httpContent.ReadAsStringAsync();

                if (!string.IsNullOrWhiteSpace(document))
                {
                    jsonDocument = JObject.Parse(document);
                }
            }

            return jsonDocument;
        }

        /// <summary>
        /// Dispose of the object
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose of the connection object.
        /// </summary>
        /// <param name="disposing">True if the method is called from user code, false if called by finalizer.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed || !disposing)
            {
                return;
            }

            // Update the flag to indicate dispose
            // has been called
            this.disposed = true;

            // Despose of the connection
            if (this.connection != null)
            {
                this.connection.Dispose();
            }
        }
    }
}
