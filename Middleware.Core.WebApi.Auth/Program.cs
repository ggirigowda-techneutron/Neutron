using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Middleware.Core.WebApi.Auth
{
    /// <summary>
    ///     Represents the program.
    /// </summary>
    public class Program
    {
        /// <summary>
        ///     The main method.
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        /// <summary>
        ///     Build web host.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
        }
    }
}