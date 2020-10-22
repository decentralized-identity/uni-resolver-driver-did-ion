using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace IdentityOverlayNetwork
{
    /// <summary>
    /// Main class for the application.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main entry point for the application.
        /// </summary>
        /// <param name="args">String array with arguments to use on start.</param>
        public static void Main(string[] args)
        {
            // Configure, build and run the application
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                    {
                        webBuilder
                            .ConfigureAppConfiguration((hostingContext, config) =>
                                {
                                    // Clear any previous configuration that might have
                                    // been added
                                    config.Sources.Clear();

                                    // Get the hosting environment
                                    IWebHostEnvironment hostingEnvironment = hostingContext.HostingEnvironment;
                                    
                                    // Add the default .netcore appsettings
                                    config
                                        .AddJsonFile($"appsettings.json", optional: true, reloadOnChange: true)
                                        .AddJsonFile($"appsettings.{hostingEnvironment.EnvironmentName}.json",
                                                        optional: true, reloadOnChange: true);

                                    // Add driver specific config
                                    config
                                        .AddJsonFile($"config.json", optional: false, reloadOnChange: true)
                                        .AddJsonFile($"config.{hostingEnvironment.EnvironmentName}.json",
                                                        optional: true, reloadOnChange: true);

                                    config.AddEnvironmentVariables();
                                })
                            .UseStartup<Startup>();
                    })
                .Build()
                .Run();
        }
    }
}
