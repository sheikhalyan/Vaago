using System;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Vaago.Models;
using Unity;
using Unity.Mvc5;
using Unity.Lifetime;
using Vaago.Controllers;

namespace Vaago
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var container = new UnityContainer();

            // Register VaagoProjectEntities1 as a singleton
            container.RegisterType<VaagoProjectEntities1>(new ContainerControlledLifetimeManager());

            // Manually register AuthenticationController as a singleton
            container.RegisterType<AuthenticationController>(new ContainerControlledLifetimeManager());

            // Register other dependencies for menu controller
            container.RegisterType<IMenuRepository, MenuRepository>();
            container.RegisterType<ICartRepository, CartRepository>();

            // Register dependencies for cart controller
            container.RegisterType<ICartRetrievalStrategy, LoggedInCartRetrievalStrategy>();

            // Register dependencies for checkout controller
            container.RegisterType<IOrderCommand, PlaceOrderCommand>();
            container.RegisterType<ICartRetrievalStrategy, LoggedInCartRetrievalStrategy>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            // Call the method to register Unity components
            UnityConfig.RegisterComponents();
        }
    }
}
