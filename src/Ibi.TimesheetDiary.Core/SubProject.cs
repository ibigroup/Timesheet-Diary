// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SubProject.cs" company="IBI Group">
//   Copyright 2012 IBI Group (UK). Licensed under the Apache License, Version 2.0 (the "License").
// </copyright>
// <summary>
//   Entity model for a sub project.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ibi.TimesheetDiary
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Entity model for a sub project.
    /// </summary>
    [Table("SubProject")]
    public class SubProject
    {
        /// <summary>
        /// Gets or sets the sub project id.
        /// </summary>
        /// <value>
        /// The sub project id.
        /// </value>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid SubProjectId { get; set; }

        /// <summary>
        /// Gets or sets the sub number.
        /// </summary>
        /// <value>
        /// The sub number.
        /// </value>
        [Required]
        public int SubNumber { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The sub project name.
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
        /// Gets or sets the parent project.
        /// </summary>
        /// <value>
        /// The parent project.
        /// </value>
        [Required]
        public virtual Project Project { get; set; }

        /// <summary>
        /// Gets or sets the workstages.
        /// </summary>
        /// <value>
        /// The workstages.
        /// </value>
        public virtual ICollection<Workstage> Workstages { get; set; } 
    }
}
