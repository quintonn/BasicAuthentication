using BasicAuthentication.Users;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace BasicAuthentication.Security
{
    public class UserAuthenticationOptions
    {
        public ICoreUserContext UserContext { get; set; }

        public PathString TokenEndpointPath { get; set; }

        public TimeSpan AccessTokenExpireTimeSpan { get; set; }
        public TimeSpan RefreshTokenExpireTimeSpan { get; set; }

        public bool AllowInsecureHttp { get; set; }

        public string AccessControlAllowOrigin { get; set; }

        public IIdentityValidator<string> PasswordValidator { get; set; }

        public UserAuthenticationOptions()
        {
            AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30);
            RefreshTokenExpireTimeSpan = TimeSpan.FromMinutes(60);
            TokenEndpointPath = new PathString("/token");

            PasswordValidator = new Microsoft.AspNet.Identity.PasswordValidator();
        }

        public UserAuthenticationOptions(ICoreUserContext userContext, PathString tokenEndpointPath, TimeSpan accessTokenExpireTimeSpan, TimeSpan refreshTokenExpireTimeSpan, bool allowInsecureHttp = true)
        {
            UserContext = userContext;
            TokenEndpointPath = tokenEndpointPath;
            AccessTokenExpireTimeSpan = accessTokenExpireTimeSpan;
            RefreshTokenExpireTimeSpan = refreshTokenExpireTimeSpan;
            AllowInsecureHttp = allowInsecureHttp;

            PasswordValidator = new Microsoft.AspNet.Identity.PasswordValidator();
        }
    }
}
