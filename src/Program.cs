
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace IdentityOverlayNetwork
{
    /// <summary>
    /// Main class for the application.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Static <see cref="HttpClient" />.
        /// </summary>
        private static HttpClient Client = new HttpClient();

        /// <summary>
        /// Static instance of the <see cref="Connection" /> class
        /// for use by the application.
        /// </summary>
        public static readonly Connection Connection = new Connection(Program.Client);

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
