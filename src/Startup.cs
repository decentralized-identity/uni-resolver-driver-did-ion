using System;
using IdentityOverlayNetwork.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IdentityOverlayNetwork
{
    /// <summary>
    /// Class for managing the start up of the application.
    /// </summary>
    public class Startup
    {
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
               .UseAuthorization()
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
                throw new StartupException("'appsettings.json' does not specify a DriverConfiguration section.");
            }

            // Check nodes are specified
            if (driverConfiguration.Nodes == null || driverConfiguration.Nodes.Length == 0)
            {
                throw new StartupException("'appsettings.json' DriverConfiguration section does not specify any nodes.");
            }

            // For each node, add an HttpClient to the services
            foreach(Node node in driverConfiguration.Nodes)
            {
                // Add the Microsoft discovery service http client
                services
                    .AddHttpClient(node.Name.IsPopulated("node.name"), client => 
                    {
                        client.BaseAddress = node.Uri;
                        client.DefaultRequestHeaders.Add("Accept", "application/json");
                    });
            }
        }
    }
}
