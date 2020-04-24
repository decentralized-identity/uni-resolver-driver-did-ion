using System;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace IdentityOverlayNetwork.Tests
{
    /// <summary>
    /// Verifies the <see cref="Resolver.Resove(string)" /> method
    /// of the <see cref="Resolver" /> class.
    /// </summary>
    [TestClass]
    public class Resolver_ResolveShould
    {
        /// <summary>
        /// Verifies that <see cref="ArgumentNullException" /> is thrown
        /// on invalid input.
        /// </summary>
        [TestMethod]
        public void Resolve_InvalidInput_ThrowsArgumentNullException()
        {
            MockHttpMessageHandler mockHttpMessageHandler = new MockHttpMessageHandler(HttpStatusCode.InternalServerError, string.Empty);
            MockHttpClientFactory mockHttpClientFactory = new MockHttpClientFactory(mockHttpMessageHandler);
            Connection connection = new Connection(mockHttpClientFactory);
            Resolver resolver = new Resolver(connection);

            Assert.ThrowsExceptionAsync<ArgumentNullException>(() => resolver.Resolve(null));
        }

        /// <summary>
        /// Verifies that <see cref="ArgumentException" /> is thrown
        /// on invalid input.
        /// </summary>
        [TestMethod]
        public void Resolve_InvalidInput_ThrowsArgumentException()
        {
            MockHttpMessageHandler mockHttpMessageHandler = new MockHttpMessageHandler(HttpStatusCode.InternalServerError, string.Empty);
            MockHttpClientFactory mockHttpClientFactory = new MockHttpClientFactory(mockHttpMessageHandler);
            Connection connection = new Connection(mockHttpClientFactory);
            Resolver resolver = new Resolver(connection);

            Assert.ThrowsExceptionAsync<ArgumentException>(() => resolver.Resolve(string.Empty));
            Assert.ThrowsExceptionAsync<ArgumentException>(() => resolver.Resolve(" "));
        }

        /// <summary>
        /// Verifies that a populated <see cref="JObject" /> is returned.
        /// </summary>
        [TestMethod]
        public void Resolve_ValidRequestUri_ReturnsJObject()
        {
            string responseContent = "{\"document\":{}}";

            MockHttpMessageHandler mockHttpMessageHandler = new MockHttpMessageHandler(HttpStatusCode.OK, responseContent);
            MockHttpClientFactory mockHttpClientFactory = new MockHttpClientFactory(mockHttpMessageHandler);
            Connection connection = new Connection(mockHttpClientFactory);
            Resolver resolver = new Resolver(connection);

            JObject json = resolver.Resolve("did:ion:test").Result;
            Assert.AreEqual(json.ToString(Formatting.None), responseContent);
        }
    }
}