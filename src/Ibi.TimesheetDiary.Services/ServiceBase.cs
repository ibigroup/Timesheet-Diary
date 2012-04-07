// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServiceBase.cs" company="IBI Group">
//   Copyright 2012 IBI Group (UK). Licensed under the Apache License, Version 2.0 (the "License").
// </copyright>
// <summary>
//   Base class for all data dependent services.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ibi.TimesheetDiary.Services
{
    using Ibi.TimesheetDiary.Data;

    /// <summary>
    /// Base class for all data dependent services.
    /// </summary>
    public abstract class ServiceBase
    {
        /// <summary>
        /// Local instance of a <see cref="IDataContext"/> implementation.
        /// </summary>
        protected readonly IDataContext DataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceBase"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        protected ServiceBase(IDataContext dataContext)
        {
            this.DataContext = dataContext;
        }
    }
}
