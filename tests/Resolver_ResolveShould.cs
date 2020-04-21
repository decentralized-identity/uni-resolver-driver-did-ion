using System;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

namespace IdentityOverlayNetwork.Tests
{
    /// <summary>
    /// Tests the <see cref="Resolver.Resove(string)" /> method
    /// of the <see cref="Resolver" /> class.
    /// </summary>
    [TestClass]
    public class Resolver_ResolveShould
    {
        /// <summary>
        /// Tests that <see cref="ArgumentNullException" /> is thrown
        /// on invalid input.
        /// </summary>
        [TestMethod]
        public void Resolve_InvalidInput_ThrowsArgumentNullException()
        {
            MockHttpMessageHandler mockHttpMessageHandler = new MockHttpMessageHandler(HttpStatusCode.InternalServerError, string.Empty);
            Connection connection = new Connection(mockHttpMessageHandler);
            Resolver resolver = new Resolver(connection);

            Assert.ThrowsExceptionAsync<ArgumentException>(() => resolver.Resolve(string.Empty));
            Assert.ThrowsExceptionAsync<ArgumentException>(() => resolver.Resolve(" "));
            Assert.ThrowsExceptionAsync<ArgumentNullException>(() => resolver.Resolve(null));
        }

        /// <summary>
        /// Tests that a populated <see cref="JObject" /> is returned.
        /// </summary>
        [TestMethod]
        public async void Resolve_ValidRequestUri_ReturnsJObject()
        {
            string responseContent = "{ \"document\": {} }";

            MockHttpMessageHandler mockHttpMessageHandler = new MockHttpMessageHandler(HttpStatusCode.OK, responseContent);
            Connection connection = new Connection(mockHttpMessageHandler);
            Resolver resolver = new Resolver(connection);

            JObject json = await resolver.Resolve("did:ion:test");
            Assert.Equals(json.ToString(), responseContent);
        }
    }
}