// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServicesInstaller.cs" company="IBI Group">
//   Copyright 2012 IBI Group (UK). Licensed under the Apache License, Version 2.0 (the "License").
// </copyright>
// <summary>
//   Installer for services using Castle.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ibi.TimesheetDiary.Services.Installer
{
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;

    using Ibi.TimesheetDiary.ServiceContracts;

    /// <summary>
    /// Installer for services using Castle.
    /// </summary>
    public class ServicesInstaller : IWindsorInstaller
    {
        /// <summary>
        /// Performs the installation in the <see cref="T:Castle.Windsor.IWindsorContainer"/>.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="store">The configuration store.</param>
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Add<IServiceProviderFactory, ServiceProviderFactory>();
            container.Add<IProjectServices, ProjectServices>();
        }
    }
}
