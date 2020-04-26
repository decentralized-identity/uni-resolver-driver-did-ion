using System;
using System.Net.Http;
using IdentityOverlayNetwork.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace IdentityOverlayNetwork.Tests
{
    /// <summary>
    /// Verifies the <see cref="IdentifiersController(connection)" /> constructs
    /// the expected instance of the <see cref="ConnectionException" /> class.
    /// </summary>
    [TestClass]
    public class IdentifiersController_ConstructorShould
    {
        /// <summary>
        /// Verifies that <see cref="ArgumentNullException" /> is thrown
        /// on invalid input.
        /// </summary>
        [TestMethod]
        public void Constructor_IHttpClientFactoryNotProvided_ThrowsArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new IdentifiersController(null, null));
        }

        /// <summary>
        /// Verifies that <see cref="ArgumentNullException" /> is thrown
        /// on invalid input.
        /// </summary>
        [TestMethod]
        public void Constructor_IHttpContextAccessorNotProvided_ThrowsArgumentNullException()
        {
            var httpClientFactory = new Mock<IHttpClientFactory>();
            Assert.ThrowsException<ArgumentNullException>(() => new IdentifiersController(httpClientFactory.Object, null));
        }

         /// <summary>
        /// Verifies that an instance of <see cref="IdentifiersController"/>
        /// is returned when valid input provided.
        /// </summary>
        [TestMethod]
        public void Constructor_ValidInput_ReturnsInstance()
        {
            var httpClientFactory = new Mock<IHttpClientFactory>();
            var httpContextAccessor = new Mock<IHttpContextAccessor>();
            Assert.IsNotNull(new IdentifiersController(httpClientFactory.Object, httpContextAccessor.Object));
        }
    }
}