using Owin;
using System.Web.Http;

namespace BasicAuthentication.Startup
{
    public interface IStartup
    {
        /// <summary>
        /// Called by default startup class inside base authentication module.
        /// Called after Register
        /// </summary>
        /// <param name="app"></param>
        void Configuration(IAppBuilder app);

        /// <summary>
        /// Call before all Configuration calls have been completed.
        /// </summary>
        /// <param name="config"></param>
        /// <param name="app"></param>
        void Register(HttpConfiguration config, IAppBuilder app);
    }
}
