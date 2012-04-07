// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServiceProviderFactoryTests.cs" company="IBI Group">
//   Copyright 2012 IBI Group (UK). Licensed under the Apache License, Version 2.0 (the "License").
// </copyright>
// <summary>
//   Tests the functionality of the <see cref="ServiceProviderFactory" /> class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ibi.TimesheetDiary.Services.Tests
{
    using System;

    using Ibi.TimesheetDiary.Data;

    using Moq;

    using NUnit.Framework;

    /// <summary>
    /// Tests the functionality of the <see cref="ServiceProviderFactory"/> class.
    /// </summary>
    [TestFixture]
    public class ServiceProviderFactoryTests
    {
        /// <summary>
        /// Tests that the constructor throws an error if the data context is <c>null</c>.
        /// </summary>
        [Test]
        public void ConstructorThrowsErrorIfDataContextIsNull()
        {
            // Assert
            Assert.Throws<ArgumentNullException>(() => new ServiceProviderFactory(null));
        }

        /// <summary>
        /// Tests that the constructor uses the <see cref="IDataContext"/> passed in as the
        /// internal context for the object.
        /// </summary>
        [Test]
        public void ConstructorSetsInternalDataContextToTheValueOfThePassedInObject()
        {
            // Arrange
            var mockContext = new Mock<IDataContext>();

            // Act
            var factory = new ServiceProviderFactory(mockContext.Object);

            // Assert
            Assert.AreEqual(mockContext.Object, factory.DataContext);
        }

        /// <summary>
        /// Tests that <see cref="ServiceProviderFactory.GetProjectServices"/> returns an instance
        /// of <see cref="ProjectServices"/> using the same data context as the factory.
        /// </summary>
        [Test]
        public void GetProjectServicesReturnsInstanceOfProjectServicesUsingTheInternalDataContext()
        {
            // Arrange
            var mockContext = new Mock<IDataContext>();

            // Act
            var factory = new ServiceProviderFactory(mockContext.Object);
            var service = factory.GetProjectServices();

            // Assert
            Assert.IsInstanceOf<ProjectServices>(service);
            Assert.AreEqual(mockContext.Object, ((ProjectServices)service).DataContext);
        }
    }
}
