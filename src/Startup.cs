using System;
using System.Net.Http;
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
                    .AddSingleton(typeof(Connection), new Connection(new HttpClient()))
                    .AddControllers()
                    .AddNewtonsoftJson();
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
    }
}
