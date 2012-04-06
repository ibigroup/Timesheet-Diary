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
        /// Saves the project.
        /// </summary>
        /// <param name="project">The project.</param>
        void SaveProject(Project project);

        /// <summary>
        /// Gets the sub projects.
        /// </summary>
        IQueryable<SubProject> SubProjects { get; }  
    }
}
