using ArtistEventListings.DAL;
using ArtistEventListings.Services;
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
        private const string ClientIdentifier = "TaRJnBcw1ZvYOXENCtj5";
        private const string ClientSecret = "ixGDUqRA5coOHf3FQysjd704BPptwbk6zZreELW2aCYSmIT8XJ9ngvN1MuKV";

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

            builder.Register(c => new VApiEventRepository(c.Resolve<IViagogoClient>()))
                .As<IEventRepository>()
                .InstancePerRequest();

            builder.Register(c => new VApiListingRepository(c.Resolve<IViagogoClient>()))
                .As<IListingRepository>()
                .InstancePerRequest();

            builder.Register(c => new EventService(c.Resolve<IEventRepository>()))
                .As<IEventService>()
                .InstancePerRequest();

            builder.Register(c => new ListingService(c.Resolve<IListingRepository>()))
                .As<IListingService>()
                .InstancePerRequest();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
