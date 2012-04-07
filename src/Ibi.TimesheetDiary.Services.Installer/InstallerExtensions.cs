// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InstallerExtensions.cs" company="IBI Group">
//   Copyright 2012 IBI Group (UK). Licensed under the Apache License, Version 2.0 (the "License").
// </copyright>
// <summary>
//   Extensions for Castle installers.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ibi.TimesheetDiary.Services.Installer
{
    using Castle.MicroKernel.Registration;
    using Castle.Windsor;

    /// <summary>
    /// Extensions for Castle installers.
    /// </summary>
    internal static class InstallerExtensions
    {
        /// <summary>
        /// Adds a transient registration to the container.
        /// </summary>
        /// <typeparam name="TInterface">The type of the interface.</typeparam>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="container">The container.</param>
        public static void Add<TInterface, TType>(this IWindsorContainer container) where TType : TInterface where TInterface : class
        {
            container.Register(
                Component.For<TInterface>()
                    .ImplementedBy<TType>()
                    .LifeStyle.PerWebRequest);
        }
    }
}
