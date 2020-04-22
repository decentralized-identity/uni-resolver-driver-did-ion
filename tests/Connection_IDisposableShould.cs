using System;
using System.Net;
using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IdentityOverlayNetwork.Tests
{
    /// <summary>
    /// Tests the <see cref="Connection.IDisposable" /> methods
    /// dispose of resources correctly class.
    /// </summary>
    [TestClass]
    public class Connection_IDisposableShould
    {
        /// <summary>
        /// Tests that <see cref="ArgumentNullException" /> is thrown
        /// on invalid input.
        /// </summary>
        [TestMethod]
        public void Constructor_InvalidHttpClientInput_ThrowArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new Connection(httpClient: null));
        }
    }
}