using System;
using System.Net;
using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IdentityOverlayNetwork.Tests
{
    /// <summary>
    /// Verifies the <see cref="Connection.IDisposable" /> methods
    /// dispose of resources correctly class.
    /// </summary>
    [TestClass]
    public class Connection_IDisposableShould
    {
        /// <summary>
        /// Verifies that Dispose executes without
        /// exception
        /// </summary>
        [TestMethod]
        public void Dispose_ExecutesWithoutException()
        {
            try
            {
                MockHttpMessageHandler mockHttpMessageHandler = new MockHttpMessageHandler(HttpStatusCode.OK, "test_cotent");
                MockHttpClientFactory mockHttpClientFactory = new MockHttpClientFactory(mockHttpMessageHandler);
                Connection connection = new Connection(mockHttpClientFactory);

                HttpContent content = connection.GetAsync("https://testuri").Result; // Call GetAsync so that local response message is populated
                Assert.IsNotNull(connection);
                connection.Dispose();
                connection.Dispose(); // Multiple calls to dispose shouldn't error as flag should be set
            }
            catch (Exception exception)
            {
                throw new AssertFailedException($"Unexpected exception '{exception.Message}' executing 'Connection.Dispose()'. See inner exception for more details.", exception);
            }
        }
    }
}