using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;

namespace IdentityOverlayNetwork.Tests
{
    /// <summary>
    /// Verifies the <see cref="Connection.GetSync(string)" /> returns
    /// expected results.
    /// </summary>
    [TestClass]
    public class Connection_GetAsyncShould
    {
        /// <summary>
        /// Verifies that <see cref="ArgumentNullException" /> is thrown
        /// on invalid input.
        /// </summary>
        [TestMethod]
        public void GetSync_InvalidInput_ThrowsArgumentNullException()
        {
            Connection connection = new Connection(new Mock<IHttpClientFactory>().Object);

            Assert.ThrowsExceptionAsync<ArgumentNullException>(() => connection.GetAsync(null));
        }

        /// <summary>
        /// Verifies that <see cref="ArgumentException" /> is thrown
        /// on invalid input.
        /// </summary>
        [TestMethod]
        public void GetSync_InvalidInput_ThrowsArgumentException()
        {
            Connection connection = new Connection(new Mock<IHttpClientFactory>().Object);

            Assert.ThrowsExceptionAsync<ArgumentException>(() => connection.GetAsync(string.Empty));
            Assert.ThrowsExceptionAsync<ArgumentException>(() => connection.GetAsync(" "));
        }

        /// <summary>
        /// Verifies that content is returned when the request
        /// is successful.
        /// </summary>
        [TestMethod]
        public void GetSync_ValidInput_ReturnsContent()
        {
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),  ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage()
                    {
                        StatusCode = HttpStatusCode.OK,
                        Content = new StringContent("test_content")
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

            using (HttpContent httpContent = connection.GetAsync("https://requestUri").Result)
            {
                Assert.IsNotNull(httpContent);
                
                // Read the document from the content
                string content = httpContent.ReadAsStringAsync().Result;
                Assert.AreEqual("test_content", content);
            }
        }

        /// <summary>
        /// Verifies that <see cref="ConnectionException" /> is thrown
        /// when the request results in an error from the service
        /// being callled.
        /// </summary>
        [TestMethod]
        public void GetSync_ValidInput_ThrowsConnectionException()
        {
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),  ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage()
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Content = new StringContent("test_error_response")
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

            Assert.ThrowsExceptionAsync<ConnectionException>(() => connection.GetAsync("requestUri"));
        }
    }
}