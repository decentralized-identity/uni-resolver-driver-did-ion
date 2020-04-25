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
            Assert.ThrowsException<ArgumentNullException>(() => IdentifiersController.PrepareIdentifier(null));
        }

        /// <summary>
        /// Verifies an <see cref="ArgumentException" /> is
        /// thrown on invalid input.
        /// </summary>
        [TestMethod]
        public void PrepareIdentifier_InvalidInput_ThrowsArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() => IdentifiersController.PrepareIdentifier(string.Empty));
            Assert.ThrowsException<ArgumentException>(() => IdentifiersController.PrepareIdentifier(" ")); //whitespace
        }

        /// <summary>
        /// Verifies that the identifier is returned
        /// unmodified if no initial state.
        /// </summary>
        [TestMethod]
        public void PrepareIdentifier_NoInitialState_ReturnsIdentifier()
        {
            Assert.AreEqual("testIdentifier", IdentifiersController.PrepareIdentifier("testIdentifier"));
        }

        /// <summary>
        /// Verifies that the identifier is returned
        /// unmodified if initial state is whitespace.
        /// </summary>
        [TestMethod]
        public void PrepareIdentifier_InitialStateWhiteSpace_ReturnsIdentifier()
        {
            Assert.AreEqual("testIdentifier", IdentifiersController.PrepareIdentifier("testIdentifier", "  "));
        }

        /// <summary>
        /// Verifies that the identifier is returned
        /// with correctly formatted initial state.
        /// </summary>
        [TestMethod]
        public void PrepareIdentifier_InitialStateIncluded_ReturnsIdentifierAndInitialState()
        {
            Assert.AreEqual("testIdentifier?-ion-initial-state=testState", IdentifiersController.PrepareIdentifier("testIdentifier", "testState"));
        }
    }
}