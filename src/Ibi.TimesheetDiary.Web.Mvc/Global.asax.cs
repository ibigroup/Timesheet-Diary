// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Global.asax.cs" company="IBI Group">
//   Copyright 2012 IBI Group (UK). Licensed under the Apache License, Version 2.0 (the "License").
// </copyright>
// <summary>
//   Main web application initialisation scripts.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ibi.TimesheetDiary.Web.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Castle.MicroKernel.Registration;
    using Castle.Windsor;
    using Castle.Windsor.Installer;

    using Ibi.TimesheetDiary.Web.Mvc.Plumbing;

    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    /// <summary>
    /// Main web application initialisation scripts.
    /// </summary>
    public class MvcApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// Local instance of <see cref="IWindsorContainer"/>.
        /// </summary>
        private static IWindsorContainer container;

        /// <summary>
        /// Registers the global filters.
        /// </summary>
        /// <param name="filters">The filters.</param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        /// <summary>
        /// Registers the routes.
        /// </summary>
        /// <param name="routes">The routes.</param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("favicon.ico");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }); // Parameter defaults            
        }

        /// <summary>
        /// Handles application start functions.
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            // Attach Castle
            BootstrapContainer();
        }

        /// <summary>
        /// Bootstraps the Castle container.
        /// </summary>
        private static void BootstrapContainer()
        {
            container = new WindsorContainer()
                .Install(FromAssembly.This())
                .Install(GetInstallersFromWildcard("*.Installer.dll").ToArray());

            var controllerFactory = new WindsorControllerFactory(container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }

        /// <summary>
        /// Gets the <see cref="IWindsorInstaller"/> from the current executing directory which match 
        /// the specified <param name="wildcard"></param>.
        /// </summary>
        /// <param name="wildcard">The wildcard to match.</param>
        /// <returns>A collection of <see cref="IWindsorInstaller"/>s.</returns>
        private static IEnumerable<IWindsorInstaller> GetInstallersFromWildcard(string wildcard)
        {
            var binDirectory = AppDomain.CurrentDomain.SetupInformation.PrivateBinPath;
            if (Directory.Exists(binDirectory))
            {
                var files = Directory.GetFiles(binDirectory, wildcard, SearchOption.TopDirectoryOnly);
                foreach (var file in files)
                {
                    yield return FromAssembly.Named(file);
                }
            }
        }
    }
}