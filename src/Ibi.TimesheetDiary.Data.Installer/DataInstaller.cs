// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataInstaller.cs" company="IBI Group">
//   Copyright 2012 IBI Group (UK). Licensed under the Apache License, Version 2.0 (the "License").
// </copyright>
// <summary>
//   Data installers for Castle Windsor IoC.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ibi.TimesheetDiary.Data.Installer
{
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;

    using Ibi.TimesheetDiary.Data.EntityFramework;

    /// <summary>
    /// Data installers for Castle Windsor IoC.
    /// </summary>
    public class DataInstaller : IWindsorInstaller
    {
        #region Implementation of IWindsorInstaller

        /// <summary>
        /// Performs the installation in the <see cref="T:Castle.Windsor.IWindsorContainer"/>.
        /// </summary>
        /// <param name="container">The container.</param><param name="store">The configuration store.</param>
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IDataContext>()
                .DependsOn(Dependency.OnValue("connectionStringName", "TimesheetDiaryDataContext"))
                .ImplementedBy<TimesheetDiaryDataContext>()
                .LifeStyle.PerWebRequest);
        }

        #endregion
    }
}
