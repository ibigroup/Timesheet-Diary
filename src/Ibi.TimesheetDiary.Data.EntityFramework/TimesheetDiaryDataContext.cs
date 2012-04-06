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
        public DbSet<Project> ProjectCollection { get; set; }

        /// <summary>
        /// Gets or sets the sub projects.
        /// </summary>
        /// <value>
        /// The sub projects.
        /// </value>
        public DbSet<SubProject> SubProjectCollection { get; set; }

        #region Implementation of IDataContext

        /// <summary>
        /// Gets the projects.
        /// </summary>
        public IQueryable<Project> Projects
        {
            get { return this.ProjectCollection; }
        }

        /// <summary>
        /// Saves the project.
        /// </summary>
        /// <param name="project">The project.</param>
        public void SaveProject(Project project)
        {
            if (project.ProjectId == Guid.Empty)
            {
                this.ProjectCollection.Add(project);
            }

            this.SaveChanges();
        }

        /// <summary>
        /// Gets the sub projects.
        /// </summary>
        public IQueryable<SubProject> SubProjects
        {
            get { return this.SubProjectCollection; }
        }

        #endregion
    }
}
