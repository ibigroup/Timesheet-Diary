// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Project.cs" company="IBI Group">
//   Copyright 2012 IBI Group (UK). Licensed under the Apache License, Version 2.0 (the "License").
// </copyright>
// <summary>
//   Entity model for a project.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ibi.TimesheetDiary
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Entity model for a project.
    /// </summary>
    [Table("Projects")]
    public class Project
    {
        /// <summary>
        /// Gets or sets the sub project id.
        /// </summary>
        /// <value>
        /// The sub project id.
        /// </value>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ProjectId { get; set; }

        /// <summary>
        /// Gets or sets the project code.
        /// </summary>
        /// <value>
        /// The project code.
        /// </value>
        [Required]
        public string ProjectCode { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The project name.
        /// </value>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this project is chargeable.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is chargeable; otherwise, <c>false</c>.
        /// </value>
        public bool IsChargeable { get; set; }

        /// <summary>
        /// Gets or sets the sub projects.
        /// </summary>
        /// <value>
        /// The sub projects.
        /// </value>
        public virtual ICollection<SubProject> SubProjects { get; set; } 
    }
}
