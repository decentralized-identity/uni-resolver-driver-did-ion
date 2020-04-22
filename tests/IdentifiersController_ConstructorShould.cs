using System;
using IdentityOverlayNetwork.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
        public void Constructor_InvalidInput_ThrowsArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new IdentifiersController(null));
        }
    }
}