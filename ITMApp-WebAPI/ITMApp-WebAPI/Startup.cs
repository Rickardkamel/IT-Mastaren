using System;
using ITMApp_WebAPI.Models;
using ITMApp_WebAPI.Providers;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Owin;

[assembly: OwinStartup(typeof(ITMApp_WebAPI.Startup))]

namespace ITMApp_WebAPI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
            ConfigureAuth(app);
            WebApiConfig.Register(app);

        }
        public void ConfigureAuth(IAppBuilder app)
        {
            //app.CreatePerOwinContext(ApplicationDbContext.Create);
            //app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

            //app.UseCookieAuthentication(new CookieAuthenticationOptions());
            //app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            //PublicClientId = "self";
            //OAuthOptions = new OAuthAuthorizationServerOptions
            //{
            //    TokenEndpointPath = new PathString("/Token"),
            //    Provider = new ApplicationOAuthProvider(PublicClientId),
            //    AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
            //    AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
            //    AllowInsecureHttp = true
            //};

            //app.UseOAuthBearerTokens(OAuthOptions);
            //app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            {

                OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
                {
                    AllowInsecureHttp = true,
                    TokenEndpointPath = new PathString("/token"),
                    AccessTokenExpireTimeSpan = TimeSpan.FromDays(365),
                    Provider = new ApplicationOAuthProvider(),
                };

                app.UseOAuthAuthorizationServer(OAuthServerOptions);
                app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
            }
        }
    }
}
