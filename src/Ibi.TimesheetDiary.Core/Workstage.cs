// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Workstage.cs" company="IBI Group">
//   Copyright 2012 IBI Group (UK). Licensed under the Apache License, Version 2.0 (the "License").
// </copyright>
// <summary>
//   Entity model for a sub project workstage.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ibi.TimesheetDiary
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Entity model for a sub project workstage.
    /// </summary>
    [Table("Workstage")]
    public class Workstage
    {
        /// <summary>
        /// Gets or sets the workstage id.
        /// </summary>
        /// <value>
        /// The workstage id.
        /// </value>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid WorkstageId { get; set; }

        /// <summary>
        /// Gets or sets the workstage number.
        /// </summary>
        /// <value>
        /// The workstage number.
        /// </value>
        [Required]
        public string WorkstageNumber { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The workstage name.
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
        /// Gets or sets the sub project.
        /// </summary>
        /// <value>
        /// The sub project.
        /// </value>
        [Required]
        public virtual SubProject SubProject { get; set; }
    }
}
