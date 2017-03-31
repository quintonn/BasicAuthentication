using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BasicAuthentication.Users
{
    internal class UserStore<TUser> : IUserStore<TUser>,
                                      IUserPasswordStore<TUser>,
                                      IUserSecurityStampStore<TUser>,
                                      IUserEmailStore<TUser>,
                                      IUserClaimStore<TUser>,
                                      IUserRoleStore<TUser>

    where TUser : class, ICoreIdentityUser
    {
        private ICoreUserContext UserContext { get; set; }

        public UserStore(ICoreUserContext userContext)
        {
            UserContext = userContext;
        }

        public Task CreateAsync(TUser user)
        {
            return UserContext.CreateUserAsync(user);
        }

        public Task DeleteAsync(TUser user)
        {
            return UserContext.DeleteUserAsync(user);
        }

        public Task<TUser> FindByIdAsync(string userId)
        {
            var result = UserContext.FindUserByIdAsync(userId).Result;
            return Task.FromResult<TUser>(result as TUser);
        }

        public Task<TUser> FindByNameAsync(string userName)
        {
            var result = UserContext.FindUserByNameAsync(userName).Result;
            return Task.FromResult(result as TUser);
        }

        public Task UpdateAsync(TUser user)
        {
            return UserContext.UpdateUserAsync(user);
        }

        public void Dispose()
        {
            UserContext.Dispose();
        }

        public Task<string> GetPasswordHashAsync(TUser user)
        {
            return UserContext.GetPasswordHashAsync(user);
        }

        public Task<bool> HasPasswordAsync(TUser user)
        {
            return UserContext.HasPasswordAsync(user);
        }

        public Task SetPasswordHashAsync(TUser user, string passwordHash)
        {
            return UserContext.SetPasswordHashAsync(user, passwordHash);
        }

        public Task<string> GetSecurityStampAsync(TUser user)
        {
            return UserContext.GetSecurityStampAsync(user);
        }

        public Task SetSecurityStampAsync(TUser user, string stamp)
        {
            return UserContext.SetSecurityStampAsync(user, stamp);
        }

        public async Task<TUser> FindByEmailAsync(string email)
        {
            var result = await UserContext.FindUserByEmailAsync(email);
            var user = result as TUser;
            return user;
        }

        public Task<string> GetEmailAsync(TUser user)
        {
            return UserContext.GetEmailAsync(user);
        }

        public Task<bool> GetEmailConfirmedAsync(TUser user)
        {
            return UserContext.GetEmailConfirmedAsync(user);
        }

        public Task SetEmailAsync(TUser user, string email)
        {
            return UserContext.SetEmailAsync(user, email);
        }

        public Task SetEmailConfirmedAsync(TUser user, bool confirmed)
        {
            return UserContext.SetEmailConfirmedAsync(user, confirmed);
        }

        public Task AddClaimAsync(TUser user, System.Security.Claims.Claim claim)
        {
            return UserContext.AddClaimAsync(user, claim);
        }

        public Task<IList<System.Security.Claims.Claim>> GetClaimsAsync(TUser user)
        {
            return UserContext.GetClaimsAsync(user);
        }

        public Task RemoveClaimAsync(TUser user, System.Security.Claims.Claim claim)
        {
            return UserContext.RemoveClaimAsync(user, claim);
        }

        public Task AddToRoleAsync(TUser user, string roleName)
        {
            return UserContext.AddToRoleAsync(user, roleName);
        }

        public Task<IList<string>> GetRolesAsync(TUser user)
        {
            return UserContext.GetRolesAsync(user);
        }

        public Task<bool> IsInRoleAsync(TUser user, string roleName)
        {
            return IsInRoleAsync(user, roleName);
        }

        public Task RemoveFromRoleAsync(TUser user, string roleName)
        {
            return RemoveFromRoleAsync(user, roleName);
        }
    }
}
