// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProjectServicesTests.cs" company="IBI Group">
//   Copyright 2012 IBI Group (UK). Licensed under the Apache License, Version 2.0 (the "License").
// </copyright>
// <summary>
//   Tests the functionality of <see cref="ProjectServices" />.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ibi.TimesheetDiary.Services.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Ibi.TimesheetDiary.Data;
    using Ibi.TimesheetDiary.ServiceContracts.Exceptions;

    using Moq;

    using NUnit.Framework;

    /// <summary>
    /// Tests the functionality of <see cref="ProjectServices"/>.
    /// </summary>
    [TestFixture]
    public class ProjectServicesTests
    {
        /// <summary>
        /// Local instance of a mocked <see cref="IDataContext"/>.
        /// </summary>
        private Mock<IDataContext> mockDataContext;

        /// <summary>
        /// Local instance of <see cref="IDataContext"/> from mock.
        /// </summary>
        private IDataContext dataContext;

        /// <summary>
        /// Performs setup actions for testing.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.mockDataContext = new Mock<IDataContext>();
            this.dataContext = this.mockDataContext.Object;
        }

        #region Project Tests

        /// <summary>
        /// Tests that the <see cref="ProjectServices.Projects"/> property exposes
        /// the Projects property of the data service.
        /// </summary>
        [Test]
        public void ProjectsPropertyExposesTheProjectCollectionFromDataConext()
        {
            // Arrange
            var projects = new List<Project>().AsQueryable();
            this.mockDataContext.SetupGet(x => x.Projects).Returns(projects).Verifiable();

            var service = new ProjectServices(this.dataContext);

            // Act
            var projectsFromService = service.Projects;

            // Assert
            Assert.AreEqual(projects, projectsFromService);
            this.mockDataContext.Verify();
        }

        /// <summary>
        /// Tests that <see cref="ProjectServices.GetProjectByCode"/> returns <c>null</c>
        /// if no matching items are found in the data store.
        /// </summary>
        [Test]
        public void GetProjectByCodeReturnsNullIfNoMatchingItemsAreFound()
        {
            // Arrange
            var projects = new List<Project>().AsQueryable();
            this.mockDataContext.SetupGet(x => x.Projects).Returns(projects).Verifiable();

            var service = new ProjectServices(this.dataContext);

            // Act
            var project = service.GetProjectByCode("a code");

            // Assert
            Assert.IsNull(project);
            this.mockDataContext.Verify();
        }

        /// <summary>
        /// Tests that <see cref="ProjectServices.GetProjectByCode"/> returns the correct,
        /// matching item from the data store.
        /// </summary>
        [Test]
        public void GetProjectByCodeReturnsTheCorrectItem()
        {
            // Arrange
            const string ProjectCodeToFind = "project code to find";
            var projectObjects = new[]
                {
                    new Project { ProjectCode = ProjectCodeToFind }, 
                    new Project { ProjectCode = "Not the one to find 1" },
                    new Project { ProjectCode = "Not the one to find 2" }
                };

            var projects = new List<Project>(projectObjects).AsQueryable();
            this.mockDataContext.SetupGet(x => x.Projects).Returns(projects).Verifiable();

            var service = new ProjectServices(this.dataContext);

            // Act
            var project = service.GetProjectByCode(ProjectCodeToFind);

            // Assert
            Assert.IsNotNull(project);
            Assert.AreEqual(ProjectCodeToFind, project.ProjectCode);
            this.mockDataContext.Verify();
        }

        /// <summary>
        /// Tests that <see cref="ProjectServices.SaveProject"/> throws a
        /// <see cref="ArgumentNullException"/> if the <see cref="Project"/> to
        /// save is <c>null</c>.
        /// </summary>
        [Test]
        public void SaveProjectThrowsAnExceptionIfTheProjectIsNull()
        {
            // Arrange
            var service = new ProjectServices(this.dataContext);

            this.mockDataContext.Setup(x => x.InsertProject(It.IsAny<Project>())).Callback(
                () => Assert.Fail("Insert should not be called."));

            this.mockDataContext.Setup(x => x.UpdateProject(It.IsAny<Project>())).Callback(
                () => Assert.Fail("Update should not be called."));

            this.mockDataContext.Setup(x => x.SaveChanges()).Callback(
                () => Assert.Fail("Save should not be called."));

            // Assert
            Assert.Throws<ArgumentNullException>(() => service.SaveProject(null));
        }

        /// <summary>
        /// Tests that <see cref="ProjectServices.SaveProject"/> calls an
        /// insert on the data context is the <see cref="Project"/> to save is new.
        /// </summary>
        [Test]
        public void SaveProjectInsertsProjectIfItIsNew()
        {
            // Arrange
            var projectToSave = new Project();
            var service = new ProjectServices(this.dataContext);

            this.mockDataContext.Setup(x => x.InsertProject(projectToSave))
                .Verifiable();

            this.mockDataContext.Setup(x => x.UpdateProject(It.IsAny<Project>())).Callback(
                () => Assert.Fail("Update should not be called."));

            this.mockDataContext.Setup(x => x.SaveChanges()).Verifiable();

            // Act
            service.SaveProject(projectToSave);

            // Assert
            this.mockDataContext.Verify();
        }

        /// <summary>
        /// Tests that <see cref="ProjectServices.SaveProject"/> calls an
        /// update on the data context is the <see cref="Project"/> to save is not new.
        /// </summary>
        [Test]
        public void SaveProjectIUpdatesProjectIfItIsNotNew()
        {
            // Arrange
            var projectToSave = new Project { ProjectId = Guid.NewGuid() };
            var service = new ProjectServices(this.dataContext);

            this.mockDataContext.Setup(x => x.InsertProject(It.IsAny<Project>())).Callback(
                () => Assert.Fail("Insert should not be called."));

            this.mockDataContext.Setup(x => x.UpdateProject(projectToSave))
                .Verifiable();

            this.mockDataContext.Setup(x => x.SaveChanges()).Verifiable();

            // Act
            service.SaveProject(projectToSave);

            // Assert
            this.mockDataContext.Verify();
        }

        #endregion

        #region SubProject Tests

        /// <summary>
        /// Tests that the <see cref="ProjectServices.SubProjects"/> property exposes
        /// the SubProjects property of the data service.
        /// </summary>
        [Test]
        public void SubProjectsPropertyExposesTheSubProjectCollectionFromDataConext()
        {
            // Arrange
            var subProjects = new List<SubProject>().AsQueryable();
            this.mockDataContext.SetupGet(x => x.SubProjects).Returns(subProjects).Verifiable();

            var service = new ProjectServices(this.dataContext);

            // Act
            var subProjectsFromService = service.SubProjects;

            // Assert
            Assert.AreEqual(subProjects, subProjectsFromService);
            this.mockDataContext.Verify();
        }

        #endregion

        #region Workstage Tests

        /// <summary>
        /// Tests that the <see cref="ProjectServices.Workstages"/> property exposes
        /// the Workstages property of the data service.
        /// </summary>
        [Test]
        public void WorkstagesPropertyExposesTheWorkstageCollectionFromDataConext()
        {
            // Arrange
            var workstages = new List<Workstage>().AsQueryable();
            this.mockDataContext.SetupGet(x => x.Workstages).Returns(workstages).Verifiable();

            var service = new ProjectServices(this.dataContext);

            // Act
            var workstagesFromService = service.Workstages;

            // Assert
            Assert.AreEqual(workstages, workstagesFromService);
            this.mockDataContext.Verify();
        }

        #endregion
    }
}
