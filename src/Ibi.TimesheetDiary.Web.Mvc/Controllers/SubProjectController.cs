// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SubProjectController.cs" company="IBI Group">
//   Copyright 2012 IBI Group (UK). Licensed under the Apache License, Version 2.0 (the "License").
// </copyright>
// <summary>
//   Controller for <see cref="SubProject" /> related actions.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ibi.TimesheetDiary.Web.Mvc.Controllers
{
    using System;
    using System.Web.Mvc;

    using Ibi.TimesheetDiary.ServiceContracts;

    /// <summary>
    /// Controller for <see cref="SubProject"/> related actions.
    /// </summary>
    public class SubProjectController : ControllerBase
    {
        /// <summary>
        /// Local instance of <see cref="IProjectServices"/>,
        /// </summary>
        private readonly IProjectServices projectServices;

        /// <summary>
        /// Initializes a new instance of the <see cref="SubProjectController"/> class.
        /// </summary>
        /// <param name="serviceProviderFactory">The service provider factory.</param>
        public SubProjectController(IServiceProviderFactory serviceProviderFactory)
            : base(serviceProviderFactory)
        {
            this.projectServices = serviceProviderFactory.GetProjectServices();
        }

        /// <summary>
        /// Returns a view with a list of all sub-projects within the given project.
        /// </summary>
        /// <param name="projectCode">The project code.</param>
        /// <returns>A view with a list of all sub-projects within the given project.</returns>
        public ActionResult Index(string projectCode)
        {
            var project = this.projectServices.GetProjectByCode(projectCode);
            if (project == null)
            {
                return RedirectToAction("Index", "Project");
            }

            ViewBag.ProjectCode = project.ProjectCode;
            return View(project.SubProjects);
        }

        /// <summary>
        /// Returns a view with a form for creating a new <see cref="SubProject"/>.
        /// </summary>
        /// <param name="projectCode">The parent project code.</param>
        /// <returns>A view with a form for creating a new <see cref="SubProject"/>.</returns>
        public ActionResult Create(string projectCode)
        {
            var project = this.projectServices.GetProjectByCode(projectCode);
            if (project == null)
            {
                return RedirectToAction("Index", "Project");
            }

            ViewBag.ProjectCode = projectCode;
            var subProject = new SubProject { Project = project };
            return View(subProject);
        }

        /// <summary>
        /// Handles a POST for creating a new <see cref="SubProject"/>.
        /// </summary>
        /// <param name="projectCode">The parent project code.</param>
        /// <param name="collection">The form collection.</param>
        /// <returns>A redirect to <see cref="Index"/> if successful; otherwise the same
        /// view with errors.</returns>
        [HttpPost]
        public ActionResult Create(string projectCode, FormCollection collection)
        {
            var project = this.projectServices.GetProjectByCode(projectCode);
            if (project == null)
            {
                return RedirectToAction("Index", "Project");
            }

            try
            {
                var newSubProject = new SubProject { Project = project };

                if (TryUpdateModel(newSubProject, null, null, new[] { "Project" }))
                {
                    project.SubProjects.Add(newSubProject);
                    this.projectServices.SaveProject(project);

                    return RedirectToAction("Index", new { projectCode });
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e);
            }

            return View();
        }

        /// <summary>
        /// Returns a view with a form for editing a <see cref="SubProject"/>.
        /// </summary>
        /// <param name="projectCode">The project code of the parent project.</param>
        /// <param name="subNumber">The sub number of the project to edit.</param>
        /// <returns>
        /// A view with a form for editing a sub project.
        /// </returns>
        public ActionResult Edit(string projectCode, int subNumber)
        {
            var project = this.projectServices.GetSubProjectByCodeAndSubNumber(projectCode, subNumber);
            if (project == null)
            {
                return RedirectToAction("Index", new { projectCode });
            }

            return View(project);
        }

        /// <summary>
        /// Handles the POSTing of an edited <see cref="SubProject"/>.
        /// </summary>
        /// <param name="projectCode">The project code of the parent project.</param>
        /// <param name="subNumber">The sub number of the sub project to edit.</param>
        /// <param name="collection">The from collection.</param>
        /// <returns>
        /// A redirect to <see cref="Index"/> if successful; otherwise the
        /// same view with model errors.
        /// </returns>
        [HttpPost]
        public ActionResult Edit(string projectCode, int subNumber, FormCollection collection)
        {
            var project = this.projectServices.GetSubProjectByCodeAndSubNumber(projectCode, subNumber);
            try
            {
                if (ModelState.IsValid)
                {
                    if (TryUpdateModel(project, null, null, new[] { "Project" }))
                    {
                        this.projectServices.SaveSubProject(project);
                        return RedirectToAction("Index", new { projectCode });
                    }

                    return View(project);
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e);
            }

            return View(project);
        }
    }
}
