using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using asdsad;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using ITMApp_WebAPI.Models;
using WebGrease.Css.Extensions;

namespace ITMApp_WebAPI.Providers
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly string _publicClientId;
        private string _displayName;
        private string _userName;
        private string _userEmail;

        public ApplicationOAuthProvider()
        {

        }

        public ApplicationOAuthProvider(string publicClientId)
        {
            if (publicClientId == null)
            {
                throw new ArgumentNullException("publicClientId");
            }

            _publicClientId = publicClientId;
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();

            ApplicationUser user = await userManager.FindAsync(context.UserName, context.Password);

            //if (user == null)
            //{
            //    context.SetError("invalid_grant", "The user name or password is incorrect.");
            //    return;
            //}


            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            var ldap = new LdapAuthentication();

            var response = ldap.connectToAD(context.UserName, context.Password, "Domain Users");


            if (response.Where(item => item != null).Any(item => item.Contains("Correct")))
            {
                _displayName = response[3];
                _userEmail = response[4];
                _userName = response[5];
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);

                identity.AddClaim(new Claim("sub", context.UserName));
                identity.AddClaim(new Claim("role", "user"));

                context.Validated(identity);

            }
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            var x = CreateProperties(_userName, _displayName, _userEmail);

            x.Dictionary.ForEach(c => context.Properties.Dictionary.Add(c));


            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // Resource owner password credentials does not provide a client ID.
            if (context.ClientId == null)
            {
                context.Validated();
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {

            if (context.ClientId == _publicClientId)
            {
                Uri expectedRootUri = new Uri(context.Request.Uri, "/");

                if (expectedRootUri.AbsoluteUri == context.RedirectUri)
                {
                    context.Validated();
                }
            }

            return Task.FromResult<object>(null);
        }

        public static AuthenticationProperties CreateProperties(string userName, string displayName, string userEmail)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                {
                    "userName", userName
                },
                {
                    "email",userEmail
                },
                {
                    "displayName", displayName
                }
            };
            return new AuthenticationProperties(data);
        }
    }
}