// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NonProjectTask.cs" company="IBI Group">
//   Copyright 2012 IBI Group (UK). Licensed under the Apache License, Version 2.0 (the "License").
// </copyright>
// <summary>
//   Entity model for a non-project task.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



namespace Ibi.TimesheetDiary
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Entity model for a non-project task.
    /// </summary>
    [Table("NonChargeableTask")]
    public class NonChargeableTask
    {
        /// <summary>
        /// Gets or sets the non project task id.
        /// </summary>
        /// <value>
        /// The non project task id.
        /// </value>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid NonProjectTaskId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The task name.
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
    }
}
