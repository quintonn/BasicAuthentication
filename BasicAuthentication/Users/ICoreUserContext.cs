using BasicAuthentication.Security;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicAuthentication.Users
{
    public interface ICoreUserContext
    {
        void Dispose();

        void Initialize();

        Task AddRefreshToken(RefreshToken token);

        Task<RefreshToken> FindRefreshToken(string hashedTokenId);

        Task RemoveRefreshToken(string hashedTokenId);

        Task<bool> UserCanLogIn(string userId);

        Task CreateUserAsync(ICoreIdentityUser user);

        Task DeleteUserAsync(ICoreIdentityUser user);

        Task UpdateUserAsync(ICoreIdentityUser user);

        Task<ICoreIdentityUser> FindUserByIdAsync(string id);

        Task<ICoreIdentityUser> FindUserByNameAsync(string name);

        Task<ICoreIdentityUser> FindUserByLogin(UserLoginInfo login);

        IQueryable<ICoreIdentityUser> GetUsers();

        #region Password Methods

        Task<string> GetPasswordHashAsync(ICoreIdentityUser user);

        Task<bool> HasPasswordAsync(ICoreIdentityUser user);

        Task SetPasswordHashAsync(ICoreIdentityUser user, string passwordHash);

        #endregion

        #region Security Stamp Methods

        Task<string> GetSecurityStampAsync(ICoreIdentityUser user);

        Task SetSecurityStampAsync(ICoreIdentityUser user, string stamp);

        #endregion

        #region Email Methods

        Task<ICoreIdentityUser> FindUserByEmailAsync(string email);

        Task<string> GetEmailAsync(ICoreIdentityUser user);

        Task<bool> GetEmailConfirmedAsync(ICoreIdentityUser user);

        Task SetEmailAsync(ICoreIdentityUser user, string email);

        Task SetEmailConfirmedAsync(ICoreIdentityUser user, bool confirmed);

        #endregion

        #region Claims Methods

        Task AddClaimAsync(ICoreIdentityUser user, System.Security.Claims.Claim claim);

        Task<IList<System.Security.Claims.Claim>> GetClaimsAsync(ICoreIdentityUser user);

        Task RemoveClaimAsync(ICoreIdentityUser user, System.Security.Claims.Claim claim);

        #endregion

        #region User Role Methods

        Task AddToRoleAsync(ICoreIdentityUser user, string roleName);

        Task<IList<string>> GetRolesAsync(ICoreIdentityUser user);

        Task<bool> IsInRoleAsync(ICoreIdentityUser user, string roleName);

        Task RemoveFromRoleAsync(ICoreIdentityUser user, string roleName);

        #endregion
    }
}
