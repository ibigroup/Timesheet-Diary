// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServiceProviderFactory.cs" company="IBI Group">
//   Copyright 2012 IBI Group (UK). Licensed under the Apache License, Version 2.0 (the "License").
// </copyright>
// <summary>
//   Implementation of <see cref="IServiceProviderFactory" />.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ibi.TimesheetDiary.Services
{
    using System;

    using Ibi.TimesheetDiary.Data;
    using Ibi.TimesheetDiary.ServiceContracts;

    /// <summary>
    /// Implementation of <see cref="IServiceProviderFactory"/>.
    /// </summary>
    public class ServiceProviderFactory : IServiceProviderFactory
    {
        /// <summary>
        /// Local instance of a <see cref="IDataContext"/> implementation.
        /// </summary>
        public readonly IDataContext DataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceProviderFactory"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        /// <exception cref="ArgumentNullException">Throw if the data context is <c>null</c>."/></exception>
        public ServiceProviderFactory(IDataContext dataContext)
        {
            if (dataContext == null)
            {
                throw new ArgumentNullException("dataContext", "Data context must not be null.");
            }

            this.DataContext = dataContext;
        }

        #region Implementation of IServiceProviderFactory

        /// <summary>
        /// Gets the project services.
        /// </summary>
        /// <returns>An instance of <see cref="IProjectServices"/>.</returns>
        public IProjectServices GetProjectServices()
        {
            return new ProjectServices(this.DataContext);
        }

        #endregion
    }
}
