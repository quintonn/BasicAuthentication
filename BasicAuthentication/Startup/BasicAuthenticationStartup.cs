using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;

[assembly: OwinStartup(typeof(BasicAuthentication.Startup.BasicAuthenticationStartup))]
namespace BasicAuthentication.Startup
{
    public class BasicAuthenticationStartup
    {
        public void Configuration(IAppBuilder app)
        {
            var startupType = typeof(IStartup);
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var startupTypes = assemblies.SelectMany(s => s.GetTypes())
                                         .Where(p => startupType.IsAssignableFrom(p) && p.IsInterface == false);

            var startupInstances = new List<IStartup>();

            foreach (var sType in startupTypes)
            {
                var startupInstance = Activator.CreateInstance(sType) as IStartup;
                startupInstances.Add(startupInstance);
            }

            // Register HttpConfiguration
            var config = new System.Web.Http.HttpConfiguration();
            foreach (var sType in startupInstances)
            {
                sType.Register(config, app);
            }

            // Set up all configuration.
            foreach (var sType in startupInstances)
            {
                sType.Configuration(app);
            }

            app.UseWebApi(config);
        }
    }
}