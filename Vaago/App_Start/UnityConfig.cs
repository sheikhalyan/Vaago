using System.Web.Mvc;
using Unity;
using Unity.Lifetime;
using Unity.AspNet.Mvc; // Correct namespace
using MvcUnityDependencyResolver = Unity.Mvc5.UnityDependencyResolver; // Alias for clarity
using Vaago.Controllers;
using Vaago.Models;

namespace Vaago
{
    public static class UnityConfig
    {
        public static IUnityContainer Container { get; private set; }
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // Register VaagoProjectEntities1 as a per-request dependency
            container.RegisterType<VaagoProjectEntities1>(new PerRequestLifetimeManager());

            // Register AuthenticationController as a singleton, resolving VaagoProjectEntities1
            container.RegisterType<AuthenticationController>(new ContainerControlledLifetimeManager());

            DependencyResolver.SetResolver(new MvcUnityDependencyResolver(container));

            container.RegisterType<AuthenticationController>(new ContainerControlledLifetimeManager());
            container.RegisterType<VaagoProjectEntities1>(new PerRequestLifetimeManager());

            // Register VaagoProjectEntities1 as a per-request dependency
            container.RegisterType<VaagoProjectEntities1>(new PerRequestLifetimeManager());

            // Register AuthenticationController with HierarchicalLifetimeManager
            container.RegisterType<AuthenticationController>(new HierarchicalLifetimeManager());

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            container.RegisterType<VaagoProjectEntities1>(new PerRequestLifetimeManager());

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            container.RegisterType<VaagoProjectEntities1>(new PerRequestLifetimeManager());

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            // Register controllers with per-request lifetime
            container.RegisterType<HomeController>(new PerRequestLifetimeManager());
            container.RegisterType<AuthenticationController>(new PerRequestLifetimeManager());
            // Add other controllers as needed

            // Register other dependencies

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}

