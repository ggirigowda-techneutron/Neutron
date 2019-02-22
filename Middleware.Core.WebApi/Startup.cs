using System;
using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;
using AutoMapper;
using LinqToDB.Data;
using Microsoft.Extensions.Options;

namespace Middleware.Core.WebApi
{
    /// <summary>
    ///     Represents the <see cref="Startup"/> class.
    /// </summary>
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Setup Ocelot
            var builder = new ConfigurationBuilder();
            Configuration = builder.SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables().Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore()
                .AddJsonFormatters()
                .AddVersionedApiExplorer(
                    options =>
                    {
                        //The format of the version added to the route URL  
                        options.GroupNameFormat = "'v'VVV";

                        //Tells swagger to replace the version in the controller route  
                        options.SubstituteApiVersionInUrl = true;
                    });
            services.AddApiVersioning(o =>
            {
                o.DefaultApiVersion = new ApiVersion(1, 0);
                o.ReportApiVersions = true;
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.ApiVersionReader = new UrlSegmentApiVersionReader();
            });
            services.AddSwaggerGen(
                options =>
                {
                    // Resolve the temprary IApiVersionDescriptionProvider service  
                    var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

                    // Add a swagger document for each discovered API version  
                    foreach (var description in provider.ApiVersionDescriptions)
                        options.SwaggerDoc(description.GroupName, new Info
                        {
                            Title = $"{GetType().Assembly.GetCustomAttribute<AssemblyProductAttribute>().Product} {description.ApiVersion}",
                            Version = description.ApiVersion.ToString(),
                            Description = description.IsDeprecated
                                ? $"{GetType().Assembly.GetCustomAttribute<AssemblyDescriptionAttribute>().Description} - DEPRECATED"
                                : GetType().Assembly.GetCustomAttribute<AssemblyDescriptionAttribute>().Description
                        });

                    // Add a custom filter for settint the default values  
                    options.OperationFilter<SwaggerDefaultValues>();

                    // Tells swagger to pick up the output XML document file  
                    options.IncludeXmlComments(Path.Combine(
                        Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                        $"{GetType().Assembly.GetName().Name}.xml"
                    ));
                });
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.Configure<ConnectionStringSettings>(Configuration.GetSection("ConnectionStringSettings"));

            // Set the default connection string
            DataConnection.DefaultSettings = new Linq2DbSettings(services.BuildServiceProvider().GetService<IOptions<ConnectionStringSettings>>());

            // Can also use multiple assembly names:
            Mapper.Initialize(cfg => cfg.AddProfiles("Classlibrary.Domain"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(
                options =>
                {
                    // Build a swagger endpoint for each discovered API version  
                    foreach (var description in provider.ApiVersionDescriptions)
                        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                            description.GroupName.ToUpperInvariant());
                });

            app.UseExceptionHandler(appBuilder =>
            {
                appBuilder.Use(async (context, next) =>
                {
                    var error = context.Features[typeof(IExceptionHandlerFeature)] as IExceptionHandlerFeature;

                    // When authorization has failed, should return a json message to client
                    if (error?.Error is SecurityTokenException || error?.Error.Message.Contains("authenticationScheme") == true)
                    {
                        context.Response.StatusCode = 401;
                        context.Response.ContentType = "application/json";

                        await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                        {
                            State = "Unauthorized",
                            Msg = "token expired/invalid"
                        }));
                    }
                    // When other error, return a error message json to client
                    else if (error?.Error != null)
                    {
                        context.Response.StatusCode = 500;
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                        {
                            State = "Internal Server Error",
                            Msg = error.Error.Message
                        }));
                    }
                    // When no error, do next.
                    else await next();
                });
            });

            app.UseAuthentication();
            app.UseMvc();
        }
    }
}