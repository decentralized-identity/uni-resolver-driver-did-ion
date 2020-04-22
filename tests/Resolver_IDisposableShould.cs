using System;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IdentityOverlayNetwork.Tests
{
    /// <summary>
    /// Verifies the <see cref="Resolver.IDisposable" /> methods
    /// dispose of resources correctly class.
    /// </summary>
    [TestClass]
    public class Resolver_IDisposableShould
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
                MockHttpMessageHandler mockHttpMessageHandler = new MockHttpMessageHandler(HttpStatusCode.InternalServerError, string.Empty);
                Connection connection = new Connection(mockHttpMessageHandler);
                Resolver resolver = new Resolver(connection);
                
                Assert.IsNotNull(resolver);
                
                resolver.Dispose();
                resolver.Dispose(); // Multiple calls to dispose shouldn't error as flag should be set
            }
            catch (Exception exception)
            {
                throw new AssertFailedException($"Unexpected exception '{exception.Message}' executing 'Resolver.Dispose()'. See inner exception for more details.", exception);
            }
        }
    }
}