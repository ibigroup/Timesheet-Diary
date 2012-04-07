// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServiceException.cs" company="IBI Group">
//   Copyright 2012 IBI Group (UK). Licensed under the Apache License, Version 2.0 (the "License").
// </copyright>
// <summary>
//   Exception type for service level exceptions.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ibi.TimesheetDiary.ServiceContracts.Exceptions
{
    using System;

    /// <summary>
    /// Exception type for service level exceptions.
    /// </summary>
    public class ServiceException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceException"/> class.
        /// </summary>
        public ServiceException()
        {            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceException"/> class.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public ServiceException(string message) : base(message)
        {            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceException"/> class.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="serviceType">Type of the service.</param>
        public ServiceException(string message, Type serviceType) : base(message)
        {
            this.ServiceType = serviceType;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceException"/> class.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="serviceType">Type of the service.</param>
        /// <param name="innerException">The inner exception.</param>
        public ServiceException(string message, Type serviceType, Exception innerException) : base(message, innerException)
        {
            this.ServiceType = serviceType;
        }

        /// <summary>
        /// Gets or sets the type of the service.
        /// </summary>
        /// <value>
        /// The type of the service.
        /// </value>
        public Type ServiceType { get; protected set; }
    }
}
