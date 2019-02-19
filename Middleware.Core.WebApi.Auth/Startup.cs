using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Middleware.Core.WebApi.Auth
{
    /// <summary>
    ///     Represents the startup.
    /// </summary>
    public class Startup
    {
        /// <summary>
        ///     Creates an instance of <see cref="Startup" /> class.
        /// </summary>
        /// <param name="env">The <see cref="IHostingEnvironment"/> env.</param>
        public Startup(IHostingEnvironment env)
        {
            // Setup Ocelot
            var builder = new ConfigurationBuilder();
            Configuration = builder.SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables().Build();
        }

        /// <summary>
        ///     Gets Configuration.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        ///     Configuration services. This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> service.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.Configure<TokenDetail>(Configuration.GetSection("TokenDetail"));
        }

        /// <summary>
        ///     This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">The <see cref="IApplicationBuilder" /> app.</param>
        /// <param name="env">The <see cref="IHostingEnvironment" /> env.</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}