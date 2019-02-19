using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Middleware.Core.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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
                    //Build a swagger endpoint for each discovered API version  
                    foreach (var description in provider.ApiVersionDescriptions)
                        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                            description.GroupName.ToUpperInvariant());
                });


            app.UseMvc();
        }
    }
}