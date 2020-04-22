using System;
using System.Net;
using IdentityOverlayNetwork.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

        /// <summary>
        /// Verifies that a NotFound is returned when
        /// an identifier cannot be resolved.
        /// </summary>
        [TestMethod]
        public void Get_ConnectionReturns404_Returns404NotFound()
        {
            MockHttpMessageHandler mockHttpMessageHandler = new MockHttpMessageHandler(HttpStatusCode.NotFound, "test_content");
            Connection connection = new Connection(mockHttpMessageHandler);
            IdentifiersController identifiersController = new IdentifiersController(connection);

            NotFoundResult notFoundResult = identifiersController.Get("did:ion:test").Result as NotFoundResult;

            // Check that action result is returned
            // and as expected
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
        }

        /// <summary>
        /// Verifies that a correct status code is 
        /// returned for the status returned from the
        /// remote service.
        /// </summary>
        [TestMethod]
        public void Get_ConnectionErrors_Returns400BadRequest()
        {
            MockHttpMessageHandler mockHttpMessageHandler = new MockHttpMessageHandler(HttpStatusCode.BadRequest, "test_reasonPhrase");
            Connection connection = new Connection(mockHttpMessageHandler);
            IdentifiersController identifiersController = new IdentifiersController(connection);

            ObjectResult objectResult = identifiersController.Get("did:ion:test").Result as ObjectResult;

            // Check that action result is returned
            // and as expected
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(400, objectResult.StatusCode);
            Assert.IsNotNull(objectResult.Value);
            
            // Check that an an error object is returned
            // and correct
            Error error = objectResult.Value as Error;
            Assert.IsNotNull(error);
            Assert.AreEqual("test_reasonPhrase", error.Message);
            Assert.AreEqual(Error.Types.RequestResolveIdentifier, error.Type);
            Assert.AreEqual(Error.Codes.RemoteServiceError, error.Code);
            Assert.IsNotNull(error.CorrelationId);
            Guid guidResult;
            Assert.IsTrue(Guid.TryParse(error.CorrelationId, out guidResult));
        }

        /// <summary>
        /// Verifies that a correct status code is 
        /// returned for the status returned from the
        /// remote service.
        /// </summary>
        [TestMethod]
        public void Get_ConnectionErrors_Returns502BadGateway()
        {
            MockHttpMessageHandler mockHttpMessageHandler = new MockHttpMessageHandler(HttpStatusCode.BadGateway, "test_reasonPhrase");
            Connection connection = new Connection(mockHttpMessageHandler);
            IdentifiersController identifiersController = new IdentifiersController(connection);

            ObjectResult objectResult = identifiersController.Get("did:ion:test").Result as ObjectResult;

            // Check that action result is returned
            // and as expected
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(502, objectResult.StatusCode);
            Assert.IsNotNull(objectResult.Value);
            
            // Check that an an error object is returned
            // and correct
            Error error = objectResult.Value as Error;
            Assert.IsNotNull(error);
            Assert.AreEqual("test_reasonPhrase", error.Message);
            Assert.AreEqual(Error.Types.RequestResolveIdentifier, error.Type);
            Assert.AreEqual(Error.Codes.RemoteServiceError, error.Code);
            Assert.IsNotNull(error.CorrelationId);
            Guid guidResult;
            Assert.IsTrue(Guid.TryParse(error.CorrelationId, out guidResult));
        }

        /// <summary>
        /// Verifies that json is returned for a supported
        /// identifier.
        /// </summary>
        [TestMethod]
        public void Get_SupportedIdentifier_ReturnsJson()
        {
            string responseContent = "{\"document\":{}}";
            MockHttpMessageHandler mockHttpMessageHandler = new MockHttpMessageHandler(HttpStatusCode.OK, responseContent);
            Connection connection = new Connection(mockHttpMessageHandler);
            IdentifiersController identifiersController = new IdentifiersController(connection);

            JsonResult jsonResult = identifiersController.Get("did:ion:test").Result as JsonResult;

            // Check that action result is returned
            // and as expected
            Assert.IsNotNull(jsonResult);
            Assert.IsNotNull(jsonResult.Value);
            
            // Check that an an json object is
            // as expected
            JObject json = jsonResult.Value as JObject;
            Assert.IsNotNull(json);
            Assert.AreEqual(responseContent, json.ToString(Formatting.None));
        }
    }
}