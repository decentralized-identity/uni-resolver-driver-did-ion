using System;
using System.Net;
using System.Net.Http;
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
        public void Constructor_InvalidHttpClientInput_ThrowArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new Connection(httpClient: null));
        }

        /// <summary>
        /// Instance should be created with default timeout.
        /// </summary>
        [TestMethod]
        public void Constructor_HttpClientOnly_ReturnsInstanceWithDefaultTimeout()
        {
            Connection connection = new Connection(new HttpClient());

            Assert.IsNotNull(connection);
            Assert.AreEqual(Connection.DefaultTimeoutInMilliseconds, connection.TimeoutInMilliseconds);
        }

        /// <summary>
        /// Instance should be created with the specified timeout.
        /// </summary>
        [TestMethod]
        public void Constructor_HttpClientTimeout_ReturnsInstanceWithSpecifiedTimeout()
        {
            Connection connection = new Connection(new HttpClient(), 5000);

            Assert.IsNotNull(connection);
            Assert.AreEqual(5000, connection.TimeoutInMilliseconds);
        }

        /// <summary>
        /// Instance should be created with default timeout.
        /// </summary>
        [TestMethod]
        public void Constructor_HttpMessageHandlerOnly_ReturnsInstanceWithDefaultTimeout()
        {
            Connection connection = new Connection(new HttpClient());

            Assert.IsNotNull(connection);
            Assert.AreEqual(Connection.DefaultTimeoutInMilliseconds, connection.TimeoutInMilliseconds);
        }

        /// <summary>
        /// Instance should be created with the specified timeout.
        /// </summary>
        [TestMethod]
        public void Constructor_HttpMessageHandlerTimeout_ReturnsInstanceWithSpecifiedTimeout()
        {
            Connection connection = new Connection(new MockHttpMessageHandler(HttpStatusCode.OK, string.Empty), 5000);

            Assert.IsNotNull(connection);
            Assert.AreEqual(5000, connection.TimeoutInMilliseconds);
        }
    }
}