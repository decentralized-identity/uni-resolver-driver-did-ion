using System;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IdentityOverlayNetwork.Tests
{
    /// <summary>
    /// Tests the <see cref="ArgumentExtensions.IsPopulated(string, string)" /> method
    /// of the <see cref="ArgumentExtensions" /> class.
    /// </summary>
    [TestClass]
    public class ArgumentExtensions_IsPopulatedShould
    {
        /// <summary>
        /// Tests that <see cref="ArgumentNullException" /> is thrown
        /// on null input.
        /// </summary>
        [TestMethod]
        public void IsPopulated_NullInput_ThrowsArgumentNullException()
        {
            string input = null;
            Assert.ThrowsException<ArgumentNullException>(() => input.IsPopulated("input"));
        }

        /// <summary>
        /// Tests that <see cref="ArgumentException" /> is thrown
        /// on null input.
        /// </summary>
        [TestMethod]
        public void IsPopulated_WhitespaceInput_ThrowsArgumentException()
        {
            string input = " ";
            Assert.ThrowsException<ArgumentException>(() => input.IsPopulated("input"));
        }

        /// <summary>
        /// Tests that <see cref="ArgumentException" /> is thrown
        /// on null input.
        /// </summary>
        [TestMethod]
        public void IsPopulated_EmptyInput_ThrowsArgumentException()
        {
            string input = string.Empty;
            Assert.ThrowsException<ArgumentException>(() => input.IsPopulated("input"));
        }

        /// <summary>
        /// Tests that the argument is returned when valid.
        /// </summary>
        [TestMethod]
        public void IsPopulated_ValidInput_ReturnsArugumernt()
        {
            string input = "valid_input";
            Assert.AreEqual(input, input.IsPopulated("input"));
        }
    }
}