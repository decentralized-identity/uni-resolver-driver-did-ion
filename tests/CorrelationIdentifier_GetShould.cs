using System;
using IdentityOverlayNetwork.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
        /// Verifies an a GUID is returned.
        /// </summary>
        [TestMethod]
        public void Get_ReturnsCorrelationId()
        {
            string correlationId = CorrelationIdentifier.Get();
            Guid result;
            Assert.IsTrue(Guid.TryParse(correlationId, out result));
        }
    }
}