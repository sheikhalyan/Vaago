using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Vaago.Models;
using Unity; 
using Unity.Mvc5; 

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
            //menu controller
            var container = new UnityContainer(); 
            container.RegisterType<IMenuRepository, MenuRepository>();
            container.RegisterType<ICartRepository, CartRepository>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            //cartcontroller
            container.RegisterType<ICartRetrievalStrategy, LoggedInCartRetrievalStrategy>(); // Or NonLoggedInCartRetrievalStrategy
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            //checkout controller
            container.RegisterType<IOrderCommand, PlaceOrderCommand>();
            container.RegisterType<ICartRetrievalStrategy, LoggedInCartRetrievalStrategy>(); // Add other registrations for dependencies
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));


        }
    }
}
