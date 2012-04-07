// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TimesheetDiaryDataContext.cs" company="IBI Group">
//   Copyright 2012 IBI Group (UK). Licensed under the Apache License, Version 2.0 (the "License").
// </copyright>
// <summary>
//   Timesheet diary data context.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ibi.TimesheetDiary.Data.EntityFramework
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    /// <summary>
    /// Timesheet diary data context.
    /// </summary>
    public class TimesheetDiaryDataContext : DbContext, IDataContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TimesheetDiaryDataContext"/> class.
        /// </summary>
        /// <param name="connectionStringName">Name of the connection string.</param>
        public TimesheetDiaryDataContext(string connectionStringName) : base(connectionStringName)
        {            
        }

        /// <summary>
        /// Gets or sets the projects.
        /// </summary>
        /// <value>
        /// The projects.
        /// </value>
        protected DbSet<Project> ProjectCollection { get; set; }

        /// <summary>
        /// Gets or sets the sub projects.
        /// </summary>
        /// <value>
        /// The sub projects.
        /// </value>
        protected DbSet<SubProject> SubProjectCollection { get; set; }

        /// <summary>
        /// Gets or sets the workstage <see cref="DbSet"/>.
        /// </summary>
        /// <value>
        /// The workstage <see cref="DbSet"/>.
        /// </value>
        protected DbSet<Workstage> WorkstageCollection { get; set; } 

        #region Implementation of IDataContext

        /// <summary>
        /// Gets the projects.
        /// </summary>
        public IQueryable<Project> Projects
        {
            get { return this.ProjectCollection; }
        }

        /// <summary>
        /// Gets the sub projects.
        /// </summary>
        public IQueryable<SubProject> SubProjects
        {
            get { return this.SubProjectCollection; }
        }

        /// <summary>
        /// Gets the workstages.
        /// </summary>
        public IQueryable<Workstage> Workstages
        {
            get
            {
                return this.WorkstageCollection;
            }
        }

        /// <summary>
        /// Saves the project.
        /// </summary>
        /// <param name="project">The project.</param>
        public void InsertProject(Project project)
        {
            this.ProjectCollection.Add(project);
        }

        /// <summary>
        /// Updates the project.
        /// </summary>
        /// <param name="project">The project.</param>
        public void UpdateProject(Project project)
        {
        }

        /// <summary>
        /// Saves the sub project.
        /// </summary>
        /// <param name="subProject">The sub project.</param>
        public void InsertSubProject(SubProject subProject)
        {
            this.SubProjectCollection.Add(subProject);
        }

        /// <summary>
        /// Updates the sub project.
        /// </summary>
        /// <param name="subProject">The sub project.</param>
        public void UpdateSubProject(SubProject subProject)
        {
        }

        /// <summary>
        /// Saves the changes.
        /// </summary>
        public new void SaveChanges()
        {
            base.SaveChanges();
        }

        #endregion
    }
}
