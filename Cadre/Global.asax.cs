using Autofac;
using Autofac.Integration.WebApi;
using Cadre.DataAccessLayer;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace Cadre
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            GlobalConfiguration.Configuration.Formatters.Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);

            this.RegisterServicesInAutofac();
            //var database = GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IPostDatabase)) as IPostDatabase;
            //var authenticationModule = new BasicAuthHttpModule(database);
            //RegisterModule(authenticationModule.GetType());
        }

        private void RegisterServicesInAutofac()
        {
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            
            builder.RegisterType<PostDatabase>().As<IPostDatabase>().InstancePerRequest();
            //builder.RegisterModule(new BasicAuthHttpModule());
            //builder.RegisterModule(new BasicAuthHttpModule(IPostDatabase)).As<IHttpModule>();
            builder.RegisterType<BasicAuthHttpModule>().As<IHttpModule>().SingleInstance(); 

            var container = builder.Build();
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
