using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IdentityOverlayNetwork.Tests
{
    /// <summary>
    /// Verifies the <see cref="ConnectionException(statusCode, reasonPhrase)" /> constructs
    /// the expected instance of the <see cref="ConnectionException" /> class.
    /// </summary>
    [TestClass]
    public class ConnectionException_ConstructorShould
    {
        /// <summary>
        /// Verifies instance should be created with default timeout.
        /// </summary>
        [TestMethod]
        public void Constructor_ReturnsInstance()
        {
            ConnectionException connectionException = new ConnectionException(HttpStatusCode.InternalServerError, "test_string");
            Assert.IsNotNull(connectionException);
            Assert.AreEqual("test_string", connectionException.ReasonPhrase);
            Assert.AreEqual(HttpStatusCode.InternalServerError, connectionException.StatusCode);
        }
    }
}