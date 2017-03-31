using BasicAuthentication.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;
using System;
using System.Threading.Tasks;

namespace BasicAuthentication.Users
{
    /// <summary>
    /// A simple implementation of the Microsoft.AspNet.Identity.UserManager&lt;TUser&gt; class.
    /// </summary>
    public class CoreUserManager : UserManager<ICoreIdentityUser>
    {
        public ICoreUserContext UserContext { get; set; }

        /// <summary>
        /// This creates an instance of CoreUserManager
        /// </summary>
        /// <param name="userContext">An implementation of the BasicAuthentication.Users.UserContext abstract class. </param>
        /// <param name="provider">An instance of a Microsoft.AspNet.IdentityIUserTokenProvider &lt; TUser, string &gt; class which is used to provide password reset tokens.</param>
        /// <example>
        /// An Microsoft.AspNet.IdentityIUserTokenProvider &lt; TUser string &gt; can be created using a Microsoft.Owin.Security.DataProtection.IDataProtectionProvider as follows:
        /// <code>
        /// Microsoft.Owin.Security.DataProtection.IDataProtectionProvider provider; This needs to be created, here for reference.
        /// var tokenProvider = new Microsoft.AspNet.Identity.Owin.DataProtectorTokenProvider&lt;BasicAuthentication.Users.IdentityUser&gt;(provider.Create("EmailConfirmation"));
        /// </code>
        /// </example>
        public CoreUserManager(ICoreUserContext userContext, IUserTokenProvider<ICoreIdentityUser, string> provider)
            : base(new UserStore<ICoreIdentityUser>(userContext))
        {
            this.UserContext = userContext;
            this.UserTokenProvider = provider;
        }

        /// <summary>
        /// This creates an instance of CoreUserManager
        /// </summary>
        /// <param name="userContext">An implementation of the BasicAuthentication.Users.UserContext abstract class. </param>
        /// <param name="provider">A Microsoft.Owin.Security.DataProtection.IDataProtectionProvider instance used to create a Microsoft.AspNet.IdentityIUserTokenProvider &lt; TUser, string &gt;.</param>
        /// <example>
        /// A Microsoft.Owin.Security.DataProtection.IDataProtectionProvider can be created inside an OWIN startup class as follows:
        /// <code>app.GetDataProtectionProvider();</code>
        /// </example>
        public CoreUserManager(ICoreUserContext userContext, IDataProtectionProvider provider)
            : base(new UserStore<ICoreIdentityUser>(userContext))
        {
            this.UserContext = userContext;
            var tokenProvider = new DataProtectorTokenProvider<ICoreIdentityUser>(provider.Create("EmailConfirmation"))
            {
                TokenLifespan = TimeSpan.FromHours(6)
            };
            this.UserTokenProvider = tokenProvider;
        }

        public Task AddRefreshToken(RefreshToken token)
        {
            return UserContext.AddRefreshToken(token);
        }

        public Task<RefreshToken> FindRefreshToken(string hashedTokenId)
        {
            return UserContext.FindRefreshToken(hashedTokenId);
        }

        public Task RemoveRefreshToken(string hashedTokenId)
        {
            return UserContext.RemoveRefreshToken(hashedTokenId);
        }

        public Task<bool> UserCanLogIn(string userId)
        {
            return UserContext.UserCanLogIn(userId);
        }
    }
}
