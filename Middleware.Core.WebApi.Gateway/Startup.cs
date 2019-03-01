using System;
using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Swashbuckle.AspNetCore.Swagger;

namespace Middleware.Core.WebApi.Gateway
{
    /// <summary>
    ///     Start Up
    /// </summary>
    public class Startup
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="env"></param>
        public Startup(IHostingEnvironment env)
        {
            // Setup Ocelot
            var builder = new ConfigurationBuilder();
            Configuration = builder.SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables().Build();
        }

        /// <summary>
        ///     Configuration
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        ///     This method gets called by the runtime. Use this method to add services to the container
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Token 
            var tokenDetail = Configuration.GetSection("TokenDetail");
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenDetail["Secret"]));
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidateIssuer = true,
                ValidIssuer = tokenDetail["Issuer"],
                ValidateAudience = true,
                ValidAudience = tokenDetail["Audience"],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                RequireExpirationTime = true,
            };
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = "DefaultAuthKey";
            })
            .AddJwtBearer("DefaultAuthKey", x =>
            {
                x.RequireHttpsMetadata = true;
                x.TokenValidationParameters = tokenValidationParameters;
            });

            // Add CORS
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials());
            });

            // Set compatibility
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //  Add Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("V1", new Info { Title = "Neutron Core WebApi Gateway", Description = "Neutron Core WebApi Gateway Application", Version = "v1" });
                // In the project properties be sure to enable XML documentation
                var xmlPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), $"{GetType().Assembly.GetName().Name}.xml");
                c.IncludeXmlComments(xmlPath);
            });

            // Add Ocelot
            services.AddOcelot(Configuration);
        }

        /// <summary>
        ///     This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public async void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // If development
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            app.UseHttpsRedirection();
            app.UseCors("CorsPolicy");
            app.UseMvc();

            // Add swagger
            app.UseSwagger().UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/core/v1.0/swagger.json", "Core V1.0 API");
                c.SwaggerEndpoint("/core/v2.0/swagger.json", "Core V2.0 API");
            });

            // Add Ocelot
            app.UseAuthentication();
            await app.UseOcelot();
        }
    }
}