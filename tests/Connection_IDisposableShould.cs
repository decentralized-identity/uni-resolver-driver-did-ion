using System;
using System.Net;
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
        /// Verifies that <see cref="ArgumentNullException" /> is thrown
        /// on invalid input.
        /// </summary>
        [TestMethod]
        public void Dispose_ExecutesWithoutException()
        {
            try
            {
                Connection connection = new Connection(new MockHttpMessageHandler(HttpStatusCode.OK, string.Empty));
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