using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.Configuration;
using Moq;

namespace IdentityOverlayNetwork.Tests
{
    /// <summary>
    /// Verifies the <see cref="Startup.Startup(IConfiguration)" /> returns
    /// without exception.
    /// </summary>
    [TestClass]
    public class Startup_StartupShould
    {
        /// <summary>
        /// Verifies that <see cref="ArgumentNullException" /> is thrown
        /// on invalid input.
        /// </summary>
        [TestMethod]
        public void Startup_ReturnsWithoutException()
        {
            try
            {
                var mockConfiguration = new Mock<IConfiguration>();
                Startup startup = new Startup(mockConfiguration.Object);
            }
            catch (Exception exception)
            {
                throw new AssertFailedException($"Unexpected exception '{exception.Message}' executing 'Startup.Startup(IConfiguration)'. See inner exception for more details.", exception);
            }
        }
    }
}