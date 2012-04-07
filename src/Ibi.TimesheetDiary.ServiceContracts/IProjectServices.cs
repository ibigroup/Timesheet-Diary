// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IProjectServices.cs" company="IBI Group">
//   Copyright 2012 IBI Group (UK). Licensed under the Apache License, Version 2.0 (the "License").
// </copyright>
// <summary>
//   Services for <see cref="Project" /> related actions.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ibi.TimesheetDiary.ServiceContracts
{
    using System.Linq;

    /// <summary>
    /// Services for <see cref="Project"/> related actions.
    /// </summary>
    public interface IProjectServices
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
        /// Gets the project by code.
        /// </summary>
        /// <param name="projectCode">The project code.</param>
        /// <returns>A <see cref="Project"/> with the given <paramref name="projectCode"/>.</returns>
        Project GetProjectByCode(string projectCode);

        /// <summary>
        /// Saves the project.
        /// </summary>
        /// <param name="project">The project.</param>
        void SaveProject(Project project);

        /// <summary>
        /// Deletes the project.
        /// </summary>
        /// <param name="project">The project.</param>
        void DeleteProject(Project project);

        /// <summary>
        /// Gets the sub project by code and sub number.
        /// </summary>
        /// <param name="projectCode">The project code.</param>
        /// <param name="subNumber">The sub number.</param>
        /// <returns>A <see cref="SubProject"/> with the given project number
        /// and sub code.</returns>
        SubProject GetSubProjectByCodeAndSubNumber(string projectCode, int subNumber);

        /// <summary>
        /// Saves the sub project.
        /// </summary>
        /// <param name="subProject">The sub project.</param>
        void SaveSubProject(SubProject subProject);

        /// <summary>
        /// Deletes the sub project.
        /// </summary>
        /// <param name="subProject">The sub project.</param>
        void DeleteSubProject(SubProject subProject);

        /// <summary>
        /// Saves the workstage.
        /// </summary>
        /// <param name="workstage">The workstage.</param>
        void SaveWorkstage(Workstage workstage);

        /// <summary>
        /// Deletes the workstage.
        /// </summary>
        /// <param name="workstage">The workstage.</param>
        void DeleteWorkstage(Workstage workstage);
    }
}
