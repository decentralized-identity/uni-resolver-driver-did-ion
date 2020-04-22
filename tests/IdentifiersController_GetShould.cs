using System;
using System.Net;
using IdentityOverlayNetwork.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IdentityOverlayNetwork.Tests
{
    /// <summary>
    /// Verifies the <see cref="IdentifiersController.Get(string)" /> returns
    /// the expected values and errors.
    /// </summary>
    [TestClass]
    public class IdentifiersController_GetShould
    {
        /// <summary>
        /// Verifies that a BadRequest is returned
        /// when a non-supported identifier type
        /// is specified.
        /// </summary>
        [TestMethod]
        public void Get_NonSupportedIdentifier_Returns400BadRequest()
        {
            const string BadRequestMessage = "The specified DID method is not supported. Only ION (https://github.com/decentralized-identity/ion) based identifiers can be resolved.";

            MockHttpMessageHandler mockHttpMessageHandler = new MockHttpMessageHandler(HttpStatusCode.OK, "test_content");
            Connection connection = new Connection(mockHttpMessageHandler);
            IdentifiersController identifiersController = new IdentifiersController(connection);

            BadRequestObjectResult badRequestResult = identifiersController.Get("did:notsupported").Result as BadRequestObjectResult;

            // Check that action result is returned
            // and as expected
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
            Assert.IsNotNull(badRequestResult.Value);
            
            // Check that an an error object is returned
            // and correct
            Error error = badRequestResult.Value as Error;
            Assert.IsNotNull(error);
            Assert.AreEqual(BadRequestMessage, error.Message);
            Assert.AreEqual(Error.Types.RequestResolveIdentifier, error.Type);
            Assert.AreEqual(Error.Codes.UnsupportedDidMethod, error.Code);
            Assert.IsNotNull(error.CorrelationId);
            Guid guidResult;
            Assert.IsTrue(Guid.TryParse(error.CorrelationId, out guidResult));
        }
    }
}