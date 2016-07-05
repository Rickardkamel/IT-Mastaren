using System.Net.Http.Headers;
using System.Web.Http;
using Owin;

namespace ITMApp_WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            //config.SuppressDefaultHostAuthentication();
            //config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();
            
            //var cors = new EnableCorsAttribute("*", "*", "*");
            //config.EnableCors(cors);

            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            app.UseWebApi(config);

            config.Filters.Add(new AuthorizeAttribute());

        }
    }
}
