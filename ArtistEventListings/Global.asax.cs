using Autofac;
using Autofac.Integration.Mvc;
using GogoKit;
using GogoKit.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ArtistEventListings
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private const string ClientIdentifier = "CLIENT ID";
        private const string ClientSecret = "CLIENT SECRET";

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            RegisterIOC();
        }

        private void RegisterIOC()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterFilterProvider();

            builder.Register(c => new ViagogoClient(ClientIdentifier,
                                                    ClientSecret,
                                                    new ProductHeaderValue("ArtistEventListings"),
                                                    new GogoKitConfiguration
                                                    {
                                                        ViagogoApiEnvironment = ApiEnvironment.Production,
                                                        CaptureSynchronizationContext = true
                                                    }))
                   .As<IViagogoClient>()
                   .InstancePerRequest();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
