using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Moq;
using Moq.Protected;

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
            //MockHttpMessageHandler mockHttpMessageHandler = new MockHttpMessageHandler(HttpStatusCode.InternalServerError, string.Empty);
            //MockHttpClientFactory mockHttpClientFactory = new MockHttpClientFactory(mockHttpMessageHandler);
            //Connection connection = new Connection(mockHttpClientFactory);
            //Resolver resolver = new Resolver(connection);

            var mockHttpClientFactory = new Mock<IHttpClientFactory>();
            Connection connection = new Connection(mockHttpClientFactory.Object);
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
            var mockHttpClientFactory = new Mock<IHttpClientFactory>();
            Connection connection = new Connection(mockHttpClientFactory.Object);
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
/*
            MockHttpMessageHandler mockHttpMessageHandler = new MockHttpMessageHandler(HttpStatusCode.OK, responseContent);
            MockHttpClientFactory mockHttpClientFactory = new MockHttpClientFactory(mockHttpMessageHandler);
            Connection connection = new Connection(mockHttpClientFactory);
            Resolver resolver = new Resolver(connection);
*/
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),  ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage()
                    {
                        StatusCode = HttpStatusCode.OK,
                        Content = new StringContent(responseContent)
                    });

            HttpClient httpClient = new HttpClient(mockHttpMessageHandler.Object)
            {
                BaseAddress = new Uri("https://test.org")
            };
            
            var mockHttpClientFactory = new Mock<IHttpClientFactory>();
            mockHttpClientFactory
                .Setup(x => x.CreateClient(It.IsAny<string>()))
                .Returns(httpClient);

            Connection connection = new Connection(mockHttpClientFactory.Object);
            Resolver resolver = new Resolver(connection);

            JObject json = resolver.Resolve("did:ion:test").Result;
            Assert.AreEqual(json.ToString(Formatting.None), responseContent);
        }
    }
}