using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IdentityOverlayNetwork.Tests
{
    /// <summary>
    /// Verifies the <see cref="ArgumentExtensions.IsDefault{T}(T)" /> method
    /// of the <see cref="ArgumentExtensions" /> class.
    /// </summary>
    [TestClass]
    public class ArgumentExtensions_IsDefaultShould
    {
        /// <summary>
        /// Verifies that true is returned when then
        /// input is the type default.
        /// </summary>
        [TestMethod]
        public void IsDefault_DefaultInput_ReturnsTrue()
        {
            Assert.IsTrue(new Double().IsDefault());
            Assert.IsTrue(new Int32().IsDefault());
        }

        /// <summary>
        /// Verifies that false is returned when then
        /// input is not the type default.
        /// </summary>
        [TestMethod]
        public void IsDefault_NonDefaultInput_ReturnsFalse()
        {
            Assert.IsFalse(9.9d.IsDefault());
            Assert.IsFalse(99.IsDefault());
        }
    }
}