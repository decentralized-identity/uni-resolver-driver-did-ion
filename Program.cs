
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace uni_resolver_driver_ion
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
                    webBuilder.UseStartup<Startup>();
                })
                .Build()
                .Run();
        }
    }
}
