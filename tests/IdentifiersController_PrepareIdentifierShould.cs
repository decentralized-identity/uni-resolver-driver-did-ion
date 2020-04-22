using System;
using System.Net;
using IdentityOverlayNetwork.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IdentityOverlayNetwork.Tests
{
    /// <summary>
    /// Verifies the <see cref="IdentifiersController.PrepareIdentifier(string)" /> returns
    /// the expected values and errors.
    /// </summary>
    [TestClass]
    public class IdentifiersController_PrepareIdentifierShould
    {
        /// <summary>
        /// Verifies an <see cref="ArgumentNullException" /> is
        /// thrown on invalid input.
        /// </summary>
        [TestMethod]
        public void PrepareIdentifier_InvalidInput_ThrowsArgumentNullException()
        {
            MockHttpMessageHandler mockHttpMessageHandler = new MockHttpMessageHandler(HttpStatusCode.OK, "test_content");
            Connection connection = new Connection(mockHttpMessageHandler);
            IdentifiersController identifiersController = new IdentifiersController(connection);

            Assert.ThrowsException<ArgumentNullException>(() => identifiersController.PrepareIdentifier(null));
        }

        /// <summary>
        /// Verifies an <see cref="ArgumentException" /> is
        /// thrown on invalid input.
        /// </summary>
        [TestMethod]
        public void PrepareIdentifier_InvalidInput_ThrowsArgumentException()
        {
            MockHttpMessageHandler mockHttpMessageHandler = new MockHttpMessageHandler(HttpStatusCode.OK, "test_content");
            Connection connection = new Connection(mockHttpMessageHandler);
            IdentifiersController identifiersController = new IdentifiersController(connection);

            Assert.ThrowsException<ArgumentException>(() => identifiersController.PrepareIdentifier(string.Empty));
            Assert.ThrowsException<ArgumentException>(() => identifiersController.PrepareIdentifier(" ")); //whitespace
        }
    }
}