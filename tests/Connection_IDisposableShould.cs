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