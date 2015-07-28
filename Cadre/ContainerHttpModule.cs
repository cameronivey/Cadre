using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using System.Web.Http;
using Cadre.DataAccessLayer;
using Cadre;
using Autofac.Integration.WebApi;
using Autofac;

[assembly: PreApplicationStartMethod(typeof(ContainerHttpModule), "Start")]
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

            var modules = resolver.GetServices(typeof(IHttpModule));
            
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