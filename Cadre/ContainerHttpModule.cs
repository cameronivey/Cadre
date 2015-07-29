using System;
using System.Collections.Generic;
using System.Web;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using System.Web.Http;
using Cadre;
using Autofac.Integration.WebApi;
using System.Net.Http;

//[assembly: PreApplicationStartMethod(typeof(ContainerHttpModule), "Start")]
namespace Cadre
{
    public class ContainerHttpModule : IHttpModule
    {
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(ContainerHttpModule));
        }

        Lazy<IEnumerable<IHttpModule>> _modules = new Lazy<IEnumerable<IHttpModule>>(RetrieveModules);

        private static IEnumerable<IHttpModule> RetrieveModules()
        {
            var resolver = (AutofacWebApiDependencyResolver)GlobalConfiguration.Configuration.DependencyResolver;

            var config = new HttpConfiguration();
            config.DependencyResolver = resolver;
            config.EnsureInitialized();

            var request = new HttpRequestMessage();
            request.SetConfiguration(config);

            var modules = request.GetDependencyScope().GetServices(typeof(IHttpModule));
            return null;     
        }

        public void Dispose()
        {
            var modules = _modules.Value;
            foreach (var module in modules)
            {
                var disposableModule = module as IDisposable;
                if (disposableModule != null)
                {
                    disposableModule.Dispose();
                }
            }
        }

        public void Init(HttpApplication context)
        {
            var modules = _modules.Value;
            foreach (var module in modules)
            {
                module.Init(context);
            }
        }
    }
}