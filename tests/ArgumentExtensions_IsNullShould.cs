using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IdentityOverlayNetwork.Tests
{
    /// <summary>
    /// Tests the <see cref="ArgumentExtensions.IsNull{T}(T, string)" /> method
    /// of the <see cref="ArgumentExtensions" /> class.
    /// </summary>
    [TestClass]
    public class ArgumentExtensions_IsNullShould
    {
        /// <summary>
        /// Tests that <see cref="ArgumentNullException" /> is thrown
        /// on null input.
        /// </summary>
        [TestMethod]
        public void IsPopulated_NullInput_ThrowsArgumentNullException()
        {
            object input = null;
            Assert.ThrowsException<ArgumentNullException>(() => input.IsNull("input"));
        }
    }
}