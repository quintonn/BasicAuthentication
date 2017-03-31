using BasicAuthentication.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace BasicAuthentication.ControllerHelpers
{
    public static class Methods
    {
        public static async Task<ICoreIdentityUser> GetLoggedInUserAsync(ICoreUserContext userContext)
        {
            var userIdentity = HttpContext.Current?.User?.Identity;
            if (userIdentity == null)
            {
                return null;
            }
            var user = await userContext.FindUserByNameAsync(userIdentity.Name);
            return user;
        }
        public static async Task<ICoreIdentityUser> GetLoggedInUserAsync(HttpRequestContext requestContext, ICoreUserContext userContext)
        {
            var user = requestContext.Principal.Identity;
            var result = await userContext.FindUserByNameAsync(user.Name);
            return result;
        }

        public static ICoreIdentityUser GetLoggedInUser(HttpRequestContext requestContext, ICoreUserContext userContext)
        {

            var user = requestContext.Principal.Identity;
            //var result = CoreAuthenticationEngine.UserManager.FindByNameAsync(user.Name);
            var result = userContext.FindUserByNameAsync(user.Name);
            result.Wait();
            if (result.Status != TaskStatus.RanToCompletion)
            {
                throw new Exception("Enable to complete async task in non-async mode:\n" + result.Status);
            }
            return result.Result;
        }

        public static async Task<ICoreIdentityUser> GetLoggedInUserAsync(this ApiController controller, ICoreUserContext userContext)
        {
            return await GetLoggedInUserAsync(controller.RequestContext, userContext);
        }

        public static ICoreIdentityUser GetLoggedInUser(this ApiController controller, ICoreUserContext userContext)
        {
            return GetLoggedInUser(controller.RequestContext, userContext);
        }

        public static IDictionary<string, string> ParseFormData(string data)
        {
            var items = data.Split("&".ToCharArray());
            var result = new Dictionary<string, string>();
            foreach (var item in items)
            {
                var parts = item.Split("=".ToCharArray());
                var key = HttpUtility.UrlDecode(parts.First());
                var value = HttpUtility.UrlDecode(parts.Last());

                result.Add(key, value);
            }
            return result;
        }

        public static IDictionary<string, string> ParseFormData(this ApiController controller, string data)
        {
            var items = data.Split("&".ToCharArray());
            var result = new Dictionary<string, string>();
            foreach (var item in items)
            {
                var parts = item.Split("=".ToCharArray());
                var key = HttpUtility.UrlDecode(parts.First());
                var value = HttpUtility.UrlDecode(parts.Last());

                result.Add(key, value);
            }
            return result;
        }
    }
}
