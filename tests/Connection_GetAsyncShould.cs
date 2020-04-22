using System;
using System.Net;
using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            MockHttpMessageHandler mockHttpMessageHandler = new MockHttpMessageHandler(HttpStatusCode.InternalServerError, string.Empty);
            Connection connection = new Connection(mockHttpMessageHandler);
            
            Assert.ThrowsExceptionAsync<ArgumentNullException>(() => connection.GetAsync(null));
        }

        /// <summary>
        /// Verifies that <see cref="ArgumentException" /> is thrown
        /// on invalid input.
        /// </summary>
        [TestMethod]
        public void GetSync_InvalidInput_ThrowsArgumentException()
        {
            MockHttpMessageHandler mockHttpMessageHandler = new MockHttpMessageHandler(HttpStatusCode.InternalServerError, string.Empty);
            Connection connection = new Connection(mockHttpMessageHandler);

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
            MockHttpMessageHandler mockHttpMessageHandler = new MockHttpMessageHandler(HttpStatusCode.OK, "test_content");
            Connection connection = new Connection(mockHttpMessageHandler);

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
            MockHttpMessageHandler mockHttpMessageHandler = new MockHttpMessageHandler(HttpStatusCode.BadRequest, "test_error_response");
            Connection connection = new Connection(mockHttpMessageHandler);

            Assert.ThrowsExceptionAsync<ConnectionException>(() => connection.GetAsync("requestUri"));
        }
    }
}