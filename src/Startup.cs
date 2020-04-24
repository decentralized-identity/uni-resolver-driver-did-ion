using System;
using IdentityOverlayNetwork.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Polly;
using Polly.Extensions.Http;
using Polly.Registry;

namespace IdentityOverlayNetwork
{
    /// <summary>
    /// Class for managing the start up of the application.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Static class providing resilience
        /// poliyc strings.
        /// </summary>
        public static class ResiliencePolicy
        {
            /// <summary>
            /// Constant for the default
            /// http retry policy.
            /// </summary>
            public const string DefaultRetry = "DefaultRetry";

            /// <summary>
            /// Constant for the default
            /// circuit breaker policy.
            /// </summary>
            public const string DefaultCircuitbreaker = "DefaultCircuitbreaker";
        }  

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup" /> class.
        /// </summary>
        /// <param name="configuration">The <see cref="IConfiguration" /> for conifguring the application.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Returns the <see cref="IConfiguration" /> for the application.
        /// </summary>
        /// <value>The <see cref="IConfiguration" /> for the application.</value>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> which to configure.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddControllers()
                .AddNewtonsoftJson();

            // Add response caching
            services.AddResponseCaching();

            // Create apolicy registery and register
            // the default policies.
            PolicyRegistry policyRegistry = new PolicyRegistry()
            {
                 { ResiliencePolicy.DefaultRetry, HttpPolicyExtensions
                    .HandleTransientHttpError()
                    .OrResult(response => (int)response.StatusCode == 429)
                    .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))) },
                 { ResiliencePolicy.DefaultCircuitbreaker, HttpPolicyExtensions
                    .HandleTransientHttpError()
                    .OrResult(response => (int)response.StatusCode == 429)
                    .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))) }
            };
            
            // Add the registery to the services
            services.AddPolicyRegistry(policyRegistry);

            // Add response compression
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
            });

            // Add the http clients
            this.ConfigureHttpClients(services);
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">The <see cref="IApplicationBuilder" /> to configure.</param>
        /// <param name="env">The <see cref="IWebHostEnvironment" /> to configure.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection()
               .UseRouting()
               .UseResponseCaching()
               .UseResponseCompression()
               .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
        }

        /// <summary>
        /// Processes the driver configuration section of 
        /// the app settings, adding each node as an
        /// HttpClient. 
        /// <param name="services">The <see cref="IServiceCollection" /> which to configure.</param>
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="services"/> is null.</exception>
        /// <exception cref="StartupException">Throw when the appsettings.json does not include a driver configuration section.</exception>
        /// <exception cref="StartupException">Throw when the appsettings.json driver configuration section does not include any nodes.</exception>
        public void ConfigureHttpClients(IServiceCollection services)
        {
            services.IsNull("services");

            // Get the driver configuration
            DriverConfiguration driverConfiguration = new DriverConfiguration();
            Configuration.GetSection("DriverConfiguration").Bind(driverConfiguration);

            // Check driver configuration is specified
            if (driverConfiguration == null)
            {
                throw new StartupException("'config.json' does not specify a DriverConfiguration section.");
            }

            // Check nodes are specified
            if (driverConfiguration.Nodes == null || driverConfiguration.Nodes.Length == 0)
            {
                throw new StartupException("'config.json' DriverConfiguration section does not specify any nodes.");
            }

            // For each node, add an HttpClient to the services
            foreach (Node node in driverConfiguration.Nodes)
            {
                // Add the Microsoft discovery service http client
                IHttpClientBuilder httpClientBuilder = services
                    .AddHttpClient(node.Name.IsPopulated("node.name"), client =>
                    {
                        client.BaseAddress = node.Uri;
                        client.DefaultRequestHeaders.Add("Accept", "application/json");
                        client.Timeout = TimeSpan.FromMilliseconds(node.TimeoutInMilliseconds);
                    });

                // Addd the retry policies to the client
                Startup.AddResiliencePolicy(httpClientBuilder, driverConfiguration.Resilience);
            }
        }

        /// <summary>
        /// Checks the <paramref name="resilience"/> to determine which resilience
        /// policies are enabled and adds to the <paramref name="httpClientBuilder" />
        /// as appropriate.
        /// </summary>
        /// <param name="httpClientBuilder">The <see cref="IHttpClientBuilder"/> to add the policy to.</param>
        /// <param name="resilience">The <see cref="Resilience"/> instance with the settings for the service.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="httpClientBuilder"/> or <paramref name="resilience"/> are null.</exception>
        public static void AddResiliencePolicy(IHttpClientBuilder httpClientBuilder, Resilience resilience)
        {
            httpClientBuilder.IsNull("httpClientBuilder");
            resilience.IsNull("resilience");

            // Check if retry is enabled for the service
            if (resilience.EnableRetry)  
            {
                httpClientBuilder.AddPolicyHandlerFromRegistry(ResiliencePolicy.DefaultRetry);
            }

                // Check if circuit breaking is enabled for the service
            if (resilience.EnableCircuitBreaking)  
            {
                httpClientBuilder.AddPolicyHandlerFromRegistry(ResiliencePolicy.DefaultCircuitbreaker);
            }
        }
    }
}
