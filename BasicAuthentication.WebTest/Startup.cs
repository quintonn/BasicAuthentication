using BasicAuthentication.Authentication;
using BasicAuthentication.Security;
using BasicAuthentication.Startup;
using Microsoft.Owin;
using Owin;
using System;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace BasicAuthentication.WebTest
{
    public class Startup : IStartup
    {
        public void Configuration(IAppBuilder app)
        {
            var myApp = app;
            var options = new UserAuthenticationOptions()
            {
                AccessControlAllowOrigin = "*",
                AccessTokenExpireTimeSpan = TimeSpan.FromSeconds(8), //Access tokens should be short lived
                RefreshTokenExpireTimeSpan = TimeSpan.FromMinutes(10), // Refresh token should be long lived but stored securely
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/api/v1/token"), //path is actually now /api/v1/token
                UserContext = new UserContext(),
            };

            myApp.UseBasicUserTokenAuthentication(options);

            var configuration = new HttpConfiguration();
            configuration.MapHttpAttributeRoutes();

            var jsonFormatter = configuration.Formatters.OfType<JsonMediaTypeFormatter>().First();

            myApp.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            myApp.UseWebApi(configuration);
        }

        public void Register(HttpConfiguration config, IAppBuilder app)
        {

        }
    }
}