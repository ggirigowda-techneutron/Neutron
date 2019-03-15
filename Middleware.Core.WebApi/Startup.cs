using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
using Classlibrary.Crosscutting.Security;
using Classlibrary.Domain.Administration;
using Classlibrary.Domain.Administration.Queries;
using Classlibrary.Domain.Cqrs;
using Classlibrary.Domain.Utility;
using LinqToDB.Data;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Middleware.Core.WebApi.Validation;
using Serilog;
using SpecExpress;

namespace Middleware.Core.WebApi
{
    /// <summary>
    ///     Represents the <see cref="Startup"/> class.
    /// </summary>
    public class Startup
    {
        /// <summary>
        ///     Creates an instance of <see cref="Startup"/> class.
        /// </summary>
        /// <param name="env"></param>
        public Startup(IHostingEnvironment env)
        {
            // Setup configuration
            var builder = new ConfigurationBuilder();
            Configuration = builder.SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
            // Logger
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.File($"Logs/{ DateTime.UtcNow.ToString("MMddyyyy") }.log"
                    , fileSizeLimitBytes: 1_000_000
                    , shared: true
                    , flushToDiskInterval: TimeSpan.FromSeconds(1))
                .CreateLogger();
        }

        /// <summary>
        ///     Configuration.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        ///     This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
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
                    // Resolve the temporary IApiVersionDescriptionProvider service  
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

                    // Add a custom filter for setting the default values  
                    options.OperationFilter<SwaggerDefaultValues>();

                    // Tells swagger to pick up the output XML document file  
                    options.IncludeXmlComments(Path.Combine(
                        Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                        $"{GetType().Assembly.GetName().Name}.xml"
                    ));

                    // JWT Filter
                    options.AddSecurityDefinition("Bearer", new ApiKeyScheme { In = "header", Description = "Please enter JWT with Bearer into field", Name = "Authorization", Type = "apiKey" });
                    options.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>> {
                        { "Bearer", Enumerable.Empty<string>() },
                    });

                    //options.OperationFilter<SecurityRequirementsOperationFilter>();
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
            Mapper.Initialize(cfg => cfg.AddProfiles("Classlibrary.Domain", "Middleware.Core.WebApi"));

            // Add spec express
            ValidationCatalog.Scan(x => x.AddAssembly(Assembly.GetExecutingAssembly()));

            // DI setup
            // Transient services are created every time they are injected or requested. Register your services as transient wherever possible. Because it’s simple to design transient services. You generally don’t care about multi-threading and memory leaks and you know the service has a short life.
            // Scoped services are created per scope. In a web application, every web request creates a new separated service scope. That means scoped services are generally created per web request. Use scoped service lifetime carefully since it can be tricky if you create child service scopes or use these services from a non-web application.
            // Singleton services are created per DI container. That generally means that they are created only one time per application and then used for whole the application life time. Use singleton lifetime carefully since then you need to deal with multi-threading and potential memory leak problems.
            // DI container keeps track of all resolved services. Services are released and disposed when their lifetime ends:
            // If the service has dependencies, they are also automatically released and disposed.
            // If the service implements the IDisposable interface, Dispose method is automatically called on service release.
            // Do not depend on a transient or scoped service from a singleton service.Because the transient service becomes a singleton instance when a singleton service injects it and that may cause problems if the transient service is not designed to support such a scenario.ASP.NET Core’s default DI container already throws exceptions in such cases.
            services.AddSingleton<IUtilityManager>(new UtilityManager());
            services.AddSingleton<IAdministrationManager>(new AdministrationManager());
            services.AddSingleton<Microsoft.AspNetCore.Identity.IPasswordHasher<User>>(new PasswordStorage<User>());
            // MediatR
            services.AddTransient(typeof(IRequestPreProcessor<>), typeof(CqrsPreProcessor<>));
            services.AddTransient(typeof(IRequestPostProcessor<,>), typeof(CqrsPostProcessor<,>));
            services.AddMediatR(typeof(GetUserQuery).GetTypeInfo().Assembly);

            // Add the dependencies that the validation engine needs.
            ValidationEngine.AdministrationManager =
                services.BuildServiceProvider().GetService<IAdministrationManager>();

            // Logging
            services.AddLogging(configure => {
                configure.AddConfiguration(Configuration.GetSection("Logging"));
                configure.AddSerilog(dispose: true);
            });
        }


        /// <summary>
        ///     This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="provider"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            app.UseHttpsRedirection();

            // Swagger
            app.UseSwagger();
            app.UseSwaggerUI(
                options =>
                {
                    // Build a swagger endpoint for each discovered API version  
                    foreach (var description in provider.ApiVersionDescriptions)
                        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                            description.GroupName.ToUpperInvariant());
                });

            // Exception handling
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