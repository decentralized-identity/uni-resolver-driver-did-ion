using System.IO;
using IdentityOverlayNetwork.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IdentityOverlayNetwork.Tests
{
    /// <summary>
    /// Verifies the <see cref="DriverConfiguration..ctor()" / constructs
    /// the expected instance of the <see cref="DriverConfiguration" /> class.
    /// </summary>
    [TestClass]
    public class DriverConfiguration_ContrusctorShould
    {
        /// <summary>
        /// Verifies instance should be created with default timeout.
        /// </summary>
        [TestMethod]
        public void Constructor_ReturnsDefaultInstance()
        {
            DriverConfiguration driverConfiguration = new DriverConfiguration();

            Assert.IsNotNull(driverConfiguration);
            Assert.IsNotNull(driverConfiguration.Resilience);
            Assert.IsNotNull(driverConfiguration.Consensus);
            Assert.IsNotNull(driverConfiguration.Nodes);

            // Check default values
            Assert.IsFalse(driverConfiguration.Resilience.EnableCircuitBreaking);
            Assert.IsFalse(driverConfiguration.Resilience.EnableRetry);
            Assert.AreEqual(0, driverConfiguration.Nodes.Length);
            Assert.AreEqual(Consensus.ModelType.FirstWins, driverConfiguration.Consensus.Model);
            Assert.AreEqual(1, driverConfiguration.Consensus.InAgreement);
        }
    
        /// <summary>
        /// Verifies instance should be created with default timeout.
        /// </summary>
        [TestMethod]
        public void Constructor_ConfigurationBuilder_BindReturnsInstance()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string testsFolder = currentDirectory.Substring(0, currentDirectory.IndexOf("bin"));

            IConfigurationRoot testConfig = new ConfigurationBuilder()
                .AddJsonFile(testsFolder + "\\config.test.json", optional: false)
                .Build();

            DriverConfiguration driverConfiguration = new DriverConfiguration();
            testConfig.GetSection("DriverConfiguration").Bind(driverConfiguration);

            Assert.IsNotNull(driverConfiguration, "driverConfiguration");
            Assert.IsNotNull(driverConfiguration.Resilience, "driverConfiguration.Resilience");
            Assert.IsNotNull(driverConfiguration.Consensus, "driverConfiguration.Consensus");
            Assert.IsNotNull(driverConfiguration.Nodes, "driverConfiguration.Nodes");

            // Check default values
            Assert.IsTrue(driverConfiguration.Resilience.EnableCircuitBreaking, "driverConfiguration.Resilience.EnableCircuitBreaking");
            Assert.IsTrue(driverConfiguration.Resilience.EnableRetry, "driverConfiguration.Resilience.EnableRetry");
            Assert.AreEqual(Consensus.ModelType.FirstWins, driverConfiguration.Consensus.Model);
            Assert.AreEqual(1, driverConfiguration.Consensus.InAgreement);
            Assert.AreEqual(1, driverConfiguration.Nodes.Length);
            
            Node node = driverConfiguration.Nodes[0];
            Assert.AreEqual("test.node", node.Name);
            Assert.AreEqual(100, node.TimeoutInMilliseconds);
            Assert.AreEqual(new System.Uri("https://test.org"), node.Uri);
            Assert.AreEqual(Node.UseType.Always, node.Use);
            Assert.IsTrue(node.AllowCaching, "node.AllowCaching");
        }
    }
}