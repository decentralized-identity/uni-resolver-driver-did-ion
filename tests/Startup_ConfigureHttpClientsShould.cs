using System;
using System.IO;
using IdentityOverlayNetwork.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;


namespace IdentityOverlayNetwork.Tests
{
    /// <summary>
    /// Verifies the <see cref="Startup.ConfigureHttpClients(IServiceCollection)" /> returns
    /// without exception.
    /// </summary>
    [TestClass]
    public class Startup_ConfigureHttpClientsShould
    {
        /// <summary>
        /// Verifies that the expected builders are added and
        /// no exception thrown.
        /// </summary>
        [TestMethod]
        public void ConfigureHttpClients_DriverConfigurationNoNodes_ThrowsStartupException()
        {
            try
            {
                ServiceCollection serviceCollection = new ServiceCollection();
                DriverConfiguration driverConfiguration = new DriverConfiguration();
                var mockConfiguration = new Mock<IConfiguration>();

                string currentDirectory = Directory.GetCurrentDirectory();
                string testsFolder = currentDirectory.Substring(0, currentDirectory.IndexOf("bin"));

                IConfigurationRoot testConfig = new ConfigurationBuilder()
                    .AddJsonFile($"{testsFolder}{Path.DirectorySeparatorChar}config.nonodes.test.json", optional: false)
                    .Build();

                Startup startup = new Startup(testConfig);
                startup.ConfigureHttpClients(serviceCollection);
                Assert.Fail("Expected a StartupException");
            }
            catch (StartupException startupException)
            {
                Assert.AreEqual("Configuration json file 'DriverConfiguration' section does not specify any nodes.", startupException.Message);
            }
        }

        /// <summary>
        /// Verifies that the expected builders are added and
        /// no exception thrown.
        /// </summary>
        [TestMethod]
        public void ConfigureHttpClients_DriverConfigurationMissingNodeName_ThrowsStartupException()
        {
            try
            {
                ServiceCollection serviceCollection = new ServiceCollection();
                DriverConfiguration driverConfiguration = new DriverConfiguration();
                var mockConfiguration = new Mock<IConfiguration>();

                string currentDirectory = Directory.GetCurrentDirectory();
                string testsFolder = currentDirectory.Substring(0, currentDirectory.IndexOf("bin"));

                IConfigurationRoot testConfig = new ConfigurationBuilder()
                    .AddJsonFile($"{testsFolder}{Path.DirectorySeparatorChar}config.nodesmissingname.test.json", optional: false)
                    .Build();

                Startup startup = new Startup(testConfig);
                startup.ConfigureHttpClients(serviceCollection);
                Assert.Fail("Expected a StartupException");
            }
            catch (StartupException startupException)
            {
                Assert.AreEqual("Configuration json file 'DriverConfiguration.Nodes' section specifies a node without a name. Name is required.", startupException.Message);
            }
        }

         /// <summary>
        /// Verifies that the expected builders are added and
        /// no exception thrown.
        /// </summary>
        [TestMethod]
        public void ConfigureHttpClients_DriverConfigurationMissingNodeUri_ThrowsStartupException()
        {
            try
            {
                ServiceCollection serviceCollection = new ServiceCollection();
                DriverConfiguration driverConfiguration = new DriverConfiguration();
                var mockConfiguration = new Mock<IConfiguration>();

                string currentDirectory = Directory.GetCurrentDirectory();
                string testsFolder = currentDirectory.Substring(0, currentDirectory.IndexOf("bin"));

                IConfigurationRoot testConfig = new ConfigurationBuilder()
                    .AddJsonFile($"{testsFolder}{Path.DirectorySeparatorChar}config.nodesmissinguri.test.json", optional: false)
                    .Build();

                Startup startup = new Startup(testConfig);
                startup.ConfigureHttpClients(serviceCollection);
                Assert.Fail("Expected a StartupException");
            }
            catch (StartupException startupException)
            {
                Assert.AreEqual("Configuration json file 'DriverConfiguration.Nodes' section specifies a node without a uri. Uri is required.", startupException.Message);
            }
        }


        /// <summary>
        /// Verifies that the expected builders are added and
        /// no exception thrown.
        /// </summary>
        [TestMethod]
        public void ConfigureHttpClients_ReturnsWithoutException()
        {
            try
            {
                ServiceCollection serviceCollection = new ServiceCollection();
                DriverConfiguration driverConfiguration = new DriverConfiguration();
                var mockConfiguration = new Mock<IConfiguration>();

                string currentDirectory = Directory.GetCurrentDirectory();
                string testsFolder = currentDirectory.Substring(0, currentDirectory.IndexOf("bin"));

                IConfigurationRoot testConfig = new ConfigurationBuilder()
                    .AddJsonFile($"{testsFolder}{Path.DirectorySeparatorChar}config.test.json", optional: false)
                    .Build();

                Startup startup = new Startup(testConfig);
                startup.ConfigureHttpClients(serviceCollection);
            }
            catch (Exception exception)
            {
                throw new AssertFailedException($"Unexpected exception '{exception.Message}' executing 'Startup.ConfigureHttpClients(IServiceCollection)'. See inner exception for more details.", exception);
            }
        }
    }
}