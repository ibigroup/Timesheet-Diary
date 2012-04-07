// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IServiceProviderFactory.cs" company="IBI Group">
//   Copyright 2012 IBI Group (UK). Licensed under the Apache License, Version 2.0 (the "License").
// </copyright>
// <summary>
//   Interface for a service provider factory.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ibi.TimesheetDiary.ServiceContracts
{
    /// <summary>
    /// Interface for a service provider factory.
    /// </summary>
    public interface IServiceProviderFactory
    {
        /// <summary>
        /// Gets the project services.
        /// </summary>
        /// <returns>An instance of <see cref="IProjectServices"/>.</returns>
        IProjectServices GetProjectServices();
    }
}
