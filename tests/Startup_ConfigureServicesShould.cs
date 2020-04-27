using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace IdentityOverlayNetwork.Tests
{
    /// <summary>
    /// Verifies the <see cref="Startup.ConfigureServices(IServiceCollection)" /> returns
    /// without exception and populates the services as expected.
    /// </summary>
    [TestClass]
    public class Startup_ConfigureServicesShould
    {
        /// <summary>
        /// Verifies that the expected services are added and
        /// no exception thrown.
        /// </summary>
        [TestMethod]
        public void ConfigureServices_AddServices_ReturnsWithoutException()
        {
            try
            {
                ServiceCollection serviceCollection = new ServiceCollection();
                var mockConfiguration = new Mock<IConfiguration>();
                var mockStartup = new Mock<Startup>(mockConfiguration.Object);
                mockStartup
                    .Setup(x => x.ConfigureHttpClients(serviceCollection));

                // Configure the services and then build the provider     
                mockStartup.Object.ConfigureServices(serviceCollection);

                // Check that the services required by the driver have been added
                // to the service collection
                // IHttpContextAccesor
                ServiceDescriptor serviceDescriptor = new ServiceDescriptor(
                    typeof(Microsoft.AspNetCore.Http.IHttpContextAccessor),
                    typeof(Microsoft.AspNetCore.Http.HttpContextAccessor),
                    ServiceLifetime.Singleton
                );

                Assert.IsNotNull(serviceCollection.First(x => 
                    x.ServiceType.Equals(serviceDescriptor.ServiceType) &&
                    x.Lifetime.Equals(serviceDescriptor.Lifetime) && 
                    x.ImplementationType.Equals(serviceDescriptor.ImplementationType)));

                // IResponseCompressionProvider
                serviceDescriptor = new ServiceDescriptor(
                    typeof(Microsoft.AspNetCore.ResponseCompression.IResponseCompressionProvider),
                    typeof(Microsoft.AspNetCore.ResponseCompression.ResponseCompressionProvider),
                    ServiceLifetime.Singleton
                );

                Assert.IsNotNull(serviceCollection.First(x => 
                    x.ServiceType.Equals(serviceDescriptor.ServiceType) &&
                    x.Lifetime.Equals(serviceDescriptor.Lifetime) && 
                    x.ImplementationType.Equals(serviceDescriptor.ImplementationType)));

                // Polly resilience framework
                serviceDescriptor = new ServiceDescriptor(
                    typeof(Polly.Registry.IPolicyRegistry<string>),
                    ServiceLifetime.Singleton
                );

                Assert.IsNotNull(serviceCollection.First(x => 
                    x.ServiceType.Equals(serviceDescriptor.ServiceType) &&
                    x.Lifetime.Equals(serviceDescriptor.Lifetime)));
            }
            catch (Exception exception)
            {
                throw new AssertFailedException($"Unexpected exception '{exception.Message}' executing 'Startup.ConfigureServices(IServiceCollection)'. See inner exception for more details.", exception);
            }
        }
    }
}