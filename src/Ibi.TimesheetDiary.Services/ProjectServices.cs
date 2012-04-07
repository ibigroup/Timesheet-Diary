// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProjectServices.cs" company="IBI Group">
//   Copyright 2012 IBI Group (UK). Licensed under the Apache License, Version 2.0 (the "License").
// </copyright>
// <summary>
//   Services related to <see cref="Project" />s.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ibi.TimesheetDiary.Services
{
    using System;
    using System.Linq;

    using Ibi.TimesheetDiary.Data;
    using Ibi.TimesheetDiary.ServiceContracts;

    /// <summary>
    /// Services related to <see cref="Project"/>s.
    /// </summary>
    public class ProjectServices : ServiceBase, IProjectServices
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectServices"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public ProjectServices(IDataContext dataContext)
            : base(dataContext)
        {
        }

        #region Implementation of IProjectServices

        /// <summary>
        /// Gets the projects.
        /// </summary>
        public IQueryable<Project> Projects
        {
            get
            {
                return this.DataContext.Projects;
            }
        }

        /// <summary>
        /// Gets the sub projects.
        /// </summary>
        public IQueryable<SubProject> SubProjects
        {
            get
            {
                return this.DataContext.SubProjects;
            }
        }

        /// <summary>
        /// Gets the workstages.
        /// </summary>
        public IQueryable<Workstage> Workstages
        {
            get
            {
                return this.DataContext.Workstages;
            }
        }

        /// <summary>
        /// Gets the project by code.
        /// </summary>
        /// <param name="projectCode">The project code.</param>
        /// <returns>A <see cref="Project"/> with the given <paramref name="projectCode"/>.</returns>
        public Project GetProjectByCode(string projectCode)
        {
            return
                this.DataContext.Projects.SingleOrDefault(
                    x => x.ProjectCode.Equals(projectCode, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Saves the project.
        /// </summary>
        /// <param name="project">The project.</param>
        public void SaveProject(Project project)
        {
            if (project == null)
            {
            throw new ArgumentNullException("project", "Cannot save a null project.");    
            }

            if (project.ProjectId == Guid.Empty)
            {
                this.DataContext.InsertProject(project);
            }
            else
            {
                this.DataContext.UpdateProject(project);
            }

            this.DataContext.SaveChanges();
        }

        /// <summary>
        /// Deletes the project.
        /// </summary>
        /// <param name="project">The project.</param>
        public void DeleteProject(Project project)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Gets the sub project by code and sub number.
        /// </summary>
        /// <param name="projectCode">The project code.</param>
        /// <param name="subNumber">The sub number.</param>
        /// <returns>A <see cref="SubProject"/> with the given project number
        /// and sub code.</returns>
        public SubProject GetSubProjectByCodeAndSubNumber(string projectCode, int subNumber)
        {
            return
                this.DataContext.SubProjects.SingleOrDefault(
                    x =>
                    x.Project.ProjectCode.Equals(projectCode, StringComparison.OrdinalIgnoreCase)
                    && x.SubNumber == subNumber);
        }

        /// <summary>
        /// Saves the sub project.
        /// </summary>
        /// <param name="subProject">The sub project.</param>
        public void SaveSubProject(SubProject subProject)
        {
            if (subProject.SubProjectId == Guid.Empty)
            {
                this.DataContext.InsertSubProject(subProject);
            }
            else
            {
                this.DataContext.UpdateSubProject(subProject);
            }

            this.DataContext.SaveChanges();
        }

        /// <summary>
        /// Deletes the sub project.
        /// </summary>
        /// <param name="subProject">The sub project.</param>
        public void DeleteSubProject(SubProject subProject)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Saves the workstage.
        /// </summary>
        /// <param name="workstage">The workstage.</param>
        public void SaveWorkstage(Workstage workstage)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Deletes the workstage.
        /// </summary>
        /// <param name="workstage">The workstage.</param>
        public void DeleteWorkstage(Workstage workstage)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
