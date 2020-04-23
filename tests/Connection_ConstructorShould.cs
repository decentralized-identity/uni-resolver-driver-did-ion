using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IdentityOverlayNetwork.Tests
{
    /// <summary>
    /// Verifies the <see cref="Connection..ctor()" /> constructs
    /// the expected instance of the <see cref="Connection" /> class.
    /// </summary>
    [TestClass]
    public class Connection_ConstructorShould
    {
        /// <summary>
        /// Verifies that <see cref="ArgumentNullException" /> is thrown
        /// on invalid input.
        /// </summary>
        [TestMethod]
        public void Constructor_InvalidHttpClientFactoryInput_ThrowArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new Connection(httpClientFactory: null));
        }
    }
}