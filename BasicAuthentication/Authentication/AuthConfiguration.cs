using BasicAuthentication.Security;
using BasicAuthentication.Users;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Web;

namespace BasicAuthentication.Authentication
{
    public static class AuthConfiguration
    {
        public static void UseBasicUserTokenAuthentication(this IAppBuilder app, UserAuthenticationOptions userAuthenticationOptions)
        {
            app.Use(async (context, next) =>
            {
                if (context.Request.QueryString.HasValue)
                {
                    if (String.IsNullOrWhiteSpace(context.Request.Headers.Get("Authorization")))
                    {
                        var queryString = HttpUtility.ParseQueryString(context.Request.QueryString.Value);
                        string token = queryString.Get("token");

                        if (!String.IsNullOrWhiteSpace(token))
                        {
                            context.Request.Headers.Add("Authorization", new[] { string.Format("Bearer {0}", token) });
                        }
                    }
                }

                await next.Invoke();
            });

            var userManager = new CoreUserManager(userAuthenticationOptions.UserContext, app.GetDataProtectionProvider());
            var accessTokenLifeSpan = userAuthenticationOptions.AccessTokenExpireTimeSpan;
            var refreshTokenLifeSpan = userAuthenticationOptions.RefreshTokenExpireTimeSpan;
            var accessControlAllowOrigin = userAuthenticationOptions.AccessControlAllowOrigin;

            userManager.PasswordValidator = userAuthenticationOptions.PasswordValidator;
            
            var OAuthServerOptions = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = userAuthenticationOptions.AllowInsecureHttp,
                TokenEndpointPath = userAuthenticationOptions.TokenEndpointPath,
                AccessTokenExpireTimeSpan = accessTokenLifeSpan,
                ApplicationCanDisplayErrors = true,
                Provider = new SimpleAuthorizationServerProvider(userManager, accessControlAllowOrigin),
                RefreshTokenProvider = new SimpleRefreshTokenProvider(userManager, refreshTokenLifeSpan, accessControlAllowOrigin),
            };

            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
            
            userManager.UserContext.Initialize();
        }
    }
}
