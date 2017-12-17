using System.Web.Http;
using WebActivatorEx;
using PoolScoreboard.Services;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace PoolScoreboard.Services
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration 
                .EnableSwagger(c =>
                    {
                        c.SingleApiVersion("v1", "PoolScoreboard.Services");
                    })
                .EnableSwaggerUi();
        }
    }
}