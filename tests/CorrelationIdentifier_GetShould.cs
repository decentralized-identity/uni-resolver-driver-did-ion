using System;
using IdentityOverlayNetwork.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace IdentityOverlayNetwork.Tests
{
    /// <summary>
    /// Verifies the <see cref="IdentifiersController.GetCorrelationId()" /> returns
    /// the expected values.
    /// </summary>
    [TestClass]
    public class IdentifiersController_GetCorrelationIdShould
    {
        /// <summary>
        /// Verifies a GUID is returned.
        /// </summary>
        [TestMethod]
        public void Get_ReturnsGuidCorrelationId()
        {
            string correlationId = CorrelationIdentifier.Get();
            Guid result;
            Assert.IsTrue(Guid.TryParse(correlationId, out result));
        }

        /// <summary>
        /// Verifies the <see cref="HttpContext.TraceIdentifier"/> is
        /// returned when exists in context.
        /// </summary>
        [TestMethod]
        public void Get_WithTraceIdentifierInContext_ReturnsTraceIdentifierCorrelationId()
        {
            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            HttpContext context = new DefaultHttpContext();
            context.TraceIdentifier = "test_traceIdentifier";
            mockHttpContextAccessor.Setup(x => x.HttpContext).Returns(context);

            string correlationId = CorrelationIdentifier.Get(mockHttpContextAccessor.Object);
            Assert.AreEqual("test_traceIdentifier", correlationId);
        }
    }
}