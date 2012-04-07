// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ControllerBase.cs" company="IBI Group">
//   Copyright 2012 IBI Group (UK). Licensed under the Apache License, Version 2.0 (the "License").
// </copyright>
// <summary>
//   Base controller to provide data context.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ibi.TimesheetDiary.Web.Mvc.Controllers
{
    using System.Web.Mvc;

    using Ibi.TimesheetDiary.ServiceContracts;

    /// <summary>
    /// Base controller to provide data context.
    /// </summary>
    public abstract class ControllerBase : Controller
    {
        /// <summary>
        /// Local instance of the data context.
        /// </summary>
        protected readonly IServiceProviderFactory ServiceProviderFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ControllerBase"/> class.
        /// </summary>
        /// <param name="serviceProviderFactory">The service provider factory.</param>
        protected ControllerBase(IServiceProviderFactory serviceProviderFactory)
        {
            this.ServiceProviderFactory = serviceProviderFactory;
        }
    }
}
