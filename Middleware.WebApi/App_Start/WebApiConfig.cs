using System;
using System.Web.Http;
using System.Web.Http.Routing;
using Microsoft.Web.Http;
using Microsoft.Web.Http.Routing;

namespace Middleware.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // added to the web api configuration in the application setup
            var constraintResolver = new DefaultInlineConstraintResolver
            {
                ConstraintMap =
                {
                    ["apiVersion"] = typeof(ApiVersionRouteConstraint)
                }
            };
            config.MapHttpAttributeRoutes(constraintResolver);
            config.AddApiVersioning(o =>
            {
                o.AssumeDefaultVersionWhenUnspecified = true;
                //o.DefaultApiVersion = new ApiVersion(new DateTime(2019, 02, 14));
                o.DefaultApiVersion = new ApiVersion(1, 0);
                o.ReportApiVersions = true;
            });

            /*
            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{id}",
                new {id = RouteParameter.Optional}
            );
            */
        }
    }
}