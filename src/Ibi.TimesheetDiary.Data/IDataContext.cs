// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDataContext.cs" company="IBI Group">
//   Copyright 2012 IBI Group (UK). Licensed under the Apache License, Version 2.0 (the "License").
// </copyright>
// <summary>
//   Interface for entity persistence interaction.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ibi.TimesheetDiary.Data
{
    using System.Linq;

    /// <summary>
    /// Interface for entity persistence interaction.
    /// </summary>
    public interface IDataContext
    {
        /// <summary>
        /// Gets the projects.
        /// </summary>
        IQueryable<Project> Projects { get; }

        /// <summary>
        /// Gets the sub projects.
        /// </summary>
        IQueryable<SubProject> SubProjects { get; }

        /// <summary>
        /// Gets the workstages.
        /// </summary>
        IQueryable<Workstage> Workstages { get; } 

        /// <summary>
        /// Saves the project.
        /// </summary>
        /// <param name="project">The project.</param>
        void InsertProject(Project project);

        /// <summary>
        /// Updates the project.
        /// </summary>
        /// <param name="project">The project.</param>
        void UpdateProject(Project project);

        /// <summary>
        /// Saves the sub project.
        /// </summary>
        /// <param name="subProject">The sub project.</param>
        void InsertSubProject(SubProject subProject);

        /// <summary>
        /// Updates the sub project.
        /// </summary>
        /// <param name="subProject">The sub project.</param>
        void UpdateSubProject(SubProject subProject);

        /// <summary>
        /// Inserts the workstage.
        /// </summary>
        /// <param name="workstage">The workstage.</param>
        void InsertWorkstage(Workstage workstage);

        /// <summary>
        /// Updates the workstage.
        /// </summary>
        /// <param name="workstage">The workstage.</param>
        void UpdateWorkstage(Workstage workstage);

        /// <summary>
        /// Saves the changes.
        /// </summary>
        void SaveChanges();
    }
}
