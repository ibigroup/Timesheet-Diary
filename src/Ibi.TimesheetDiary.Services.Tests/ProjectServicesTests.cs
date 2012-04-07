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

        /// <summary>
        /// Tests that <see cref="ProjectServices.SaveSubProject"/> throws a
        /// <see cref="ArgumentNullException"/> if the <see cref="SubProject"/> to
        /// save is <c>null</c>.
        /// </summary>
        [Test]
        public void SaveSubProjectThrowsAnExceptionIfTheSubProjectIsNull()
        {
            // Arrange
            var service = new ProjectServices(this.dataContext);

            this.mockDataContext.Setup(x => x.InsertSubProject(It.IsAny<SubProject>())).Callback(
                () => Assert.Fail("Insert should not be called."));

            this.mockDataContext.Setup(x => x.UpdateSubProject(It.IsAny<SubProject>())).Callback(
                () => Assert.Fail("Update should not be called."));

            this.mockDataContext.Setup(x => x.SaveChanges()).Callback(
                () => Assert.Fail("Save should not be called."));

            // Assert
            Assert.Throws<ArgumentNullException>(() => service.SaveSubProject(null));
        }

        /// <summary>
        /// Tests that <see cref="ProjectServices.SaveSubProject"/> calls an
        /// insert on the data context is the <see cref="SubProject"/> to save is new.
        /// </summary>
        [Test]
        public void SaveSubProjectInsertsSubProjectIfItIsNew()
        {
            // Arrange
            var subProjectToSave = new SubProject();
            var service = new ProjectServices(this.dataContext);

            this.mockDataContext.Setup(x => x.InsertSubProject(subProjectToSave))
                .Verifiable();

            this.mockDataContext.Setup(x => x.UpdateSubProject(It.IsAny<SubProject>())).Callback(
                () => Assert.Fail("Update should not be called."));

            this.mockDataContext.Setup(x => x.SaveChanges()).Verifiable();

            // Act
            service.SaveSubProject(subProjectToSave);

            // Assert
            this.mockDataContext.Verify();
        }

        /// <summary>
        /// Tests that <see cref="ProjectServices.SaveSubProject"/> calls an
        /// update on the data context is the <see cref="SubProject"/> to save is not new.
        /// </summary>
        [Test]
        public void SaveSubProjectIUpdatesSubProjectIfItIsNotNew()
        {
            // Arrange
            var subProjectToSave = new SubProject { SubProjectId = Guid.NewGuid() };
            var service = new ProjectServices(this.dataContext);

            this.mockDataContext.Setup(x => x.InsertSubProject(It.IsAny<SubProject>())).Callback(
                () => Assert.Fail("Insert should not be called."));

            this.mockDataContext.Setup(x => x.UpdateSubProject(subProjectToSave))
                .Verifiable();

            this.mockDataContext.Setup(x => x.SaveChanges()).Verifiable();

            // Act
            service.SaveSubProject(subProjectToSave);

            // Assert
            this.mockDataContext.Verify();
        }

        /// <summary>
        /// Tests that <see cref="ProjectServices.GetSubProjectByCodeAndSubNumber"/> returns <c>null</c>
        /// if no matching items are found in the data store.
        /// </summary>
        [Test]
        public void GetSubProjectByCodeAndSubNumberReturnsNullIfNoMatchingItemsAreFound()
        {
            // Arrange
            var subProjects = new List<SubProject>().AsQueryable();
            this.mockDataContext.SetupGet(x => x.SubProjects).Returns(subProjects).Verifiable();

            var service = new ProjectServices(this.dataContext);

            // Act
            var project = service.GetSubProjectByCodeAndSubNumber("a code", 1);

            // Assert
            Assert.IsNull(project);
            this.mockDataContext.Verify();
        }

        /// <summary>
        /// Tests that <see cref="ProjectServices.GetSubProjectByCodeAndSubNumber"/> returns the
        /// correct item from the data store.
        /// </summary>
        [Test]
        public void GetSubProjectByCodeAndSubNumberReturnsTheCorrectItem()
        {
            // Arrange
            const string ProjectCodeToFind = "a project code";
            const int SubProjectNumberToFind = 1;
            var items = new[]
                {
                    new SubProject { SubNumber = SubProjectNumberToFind, Project = new Project { ProjectCode = ProjectCodeToFind } },
                    new SubProject { SubNumber = SubProjectNumberToFind, Project = new Project { ProjectCode = "a different code" } },
                    new SubProject { SubNumber = 100, Project = new Project { ProjectCode = ProjectCodeToFind } },
                    new SubProject { SubNumber = 100, Project = new Project { ProjectCode = "a different code" } },
                };

            var subProjects = new List<SubProject>(items).AsQueryable();
            this.mockDataContext.SetupGet(x => x.SubProjects).Returns(subProjects).Verifiable();

            var service = new ProjectServices(this.dataContext);

            // Act
            var subProject = service.GetSubProjectByCodeAndSubNumber(ProjectCodeToFind, SubProjectNumberToFind);

            // Assert
            Assert.IsNotNull(subProject);
            Assert.AreEqual(ProjectCodeToFind, subProject.Project.ProjectCode);
            Assert.AreEqual(SubProjectNumberToFind, subProject.SubNumber);
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

        /// <summary>
        /// Tests that <see cref="ProjectServices.SaveWorkstage"/> throws a
        /// <see cref="ArgumentNullException"/> if the <see cref="Workstage"/> to
        /// save is <c>null</c>.
        /// </summary>
        [Test]
        public void SaveWorkstageThrowsAnExceptionIfTheWorkstageIsNull()
        {
            // Arrange
            var service = new ProjectServices(this.dataContext);

            this.mockDataContext.Setup(x => x.InsertWorkstage(It.IsAny<Workstage>())).Callback(
                () => Assert.Fail("Insert should not be called."));

            this.mockDataContext.Setup(x => x.UpdateWorkstage(It.IsAny<Workstage>())).Callback(
                () => Assert.Fail("Update should not be called."));

            this.mockDataContext.Setup(x => x.SaveChanges()).Callback(
                () => Assert.Fail("Save should not be called."));

            // Assert
            Assert.Throws<ArgumentNullException>(() => service.SaveWorkstage(null));
        }

        /// <summary>
        /// Tests that <see cref="ProjectServices.SaveWorkstage"/> calls an
        /// insert on the data context is the <see cref="Workstage"/> to save is new.
        /// </summary>
        [Test]
        public void SaveWorkstageInsertsWorkstageIfItIsNew()
        {
            // Arrange
            var workstageToSave = new Workstage();
            var service = new ProjectServices(this.dataContext);

            this.mockDataContext.Setup(x => x.InsertWorkstage(workstageToSave))
                .Verifiable();

            this.mockDataContext.Setup(x => x.UpdateWorkstage(It.IsAny<Workstage>())).Callback(
                () => Assert.Fail("Update should not be called."));

            this.mockDataContext.Setup(x => x.SaveChanges()).Verifiable();

            // Act
            service.SaveWorkstage(workstageToSave);

            // Assert
            this.mockDataContext.Verify();
        }

        /// <summary>
        /// Tests that <see cref="ProjectServices.SaveWorkstage"/> calls an
        /// update on the data context is the <see cref="Workstage"/> to save is not new.
        /// </summary>
        [Test]
        public void SaveWorkstageIUpdatesWorkstageIfItIsNotNew()
        {
            // Arrange
            var workstageToSave = new Workstage { WorkstageId = Guid.NewGuid() };
            var service = new ProjectServices(this.dataContext);

            this.mockDataContext.Setup(x => x.InsertWorkstage(It.IsAny<Workstage>())).Callback(
                () => Assert.Fail("Insert should not be called."));

            this.mockDataContext.Setup(x => x.UpdateWorkstage(workstageToSave))
                .Verifiable();

            this.mockDataContext.Setup(x => x.SaveChanges()).Verifiable();

            // Act
            service.SaveWorkstage(workstageToSave);

            // Assert
            this.mockDataContext.Verify();
        }

        #endregion
    }
}
