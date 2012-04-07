// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProjectsController.cs" company="IBI Group">
//   Copyright 2012 IBI Group (UK). Licensed under the Apache License, Version 2.0 (the "License").
// </copyright>
// <summary>
//   Controller for <see cref="Project" /> related actions.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ibi.TimesheetDiary.Web.Mvc.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using Ibi.TimesheetDiary.ServiceContracts;

    /// <summary>
    /// Controller for <see cref="Project"/> related actions.
    /// </summary>
    public class ProjectController : ControllerBase
    {
        /// <summary>
        /// Local instance of <see cref="IProjectServices"/>,
        /// </summary>
        private readonly IProjectServices projectServices;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectController"/> class.
        /// </summary>
        /// <param name="serviceProviderFactory">The service provider factory.</param>
        public ProjectController(IServiceProviderFactory serviceProviderFactory)
            : base(serviceProviderFactory)
        {
            this.projectServices = serviceProviderFactory.GetProjectServices();
        }

        /// <summary>
        /// Returns a view with a list of all projects.
        /// </summary>
        /// <returns>A view with a list of all projects.</returns>
        public ActionResult Index()
        {
            var projects = this.projectServices.Projects
                .OrderByDescending(x => x.IsChargeable)
                .ThenByDescending(x => x.ProjectCode)
                .ToList();

            return View(projects);
        }

        /// <summary>
        /// Returns a view with details of the specified project.
        /// </summary>
        /// <param name="projectCode">The project code.</param>
        /// <returns>A view with details of the specified project.</returns>
        public ActionResult Details(string projectCode)
        {
            var project = this.projectServices.GetProjectByCode(projectCode);
            return View(project);
        }

        /// <summary>
        /// Returns a view with a form for creating a new <see cref="Project"/>.
        /// </summary>
        /// <returns>A view with a form for creating a new <see cref="Project"/>.</returns>
        public ActionResult Create()
        {
            return View(new Project());
        }

        /// <summary>
        /// Handles the POSTing of a new <see cref="Project"/>.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Project newProject = new Project();
                    if (TryUpdateModel(newProject))
                    {
                        this.projectServices.SaveProject(newProject);
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e);
            }

            return View();
        }

        /// <summary>
        /// Returns a view with a form for editing a <see cref="Project"/>.
        /// </summary>
        /// <param name="projectCode">The project code of the project to edit.</param>
        /// <returns>A view with a form for editing a project.</returns>
        public ActionResult Edit(string projectCode)
        {
            var project = this.projectServices.GetProjectByCode(projectCode);
            return View(project);
        }

        /// <summary>
        /// Handles the POSTing of an edited <see cref="Project"/>.
        /// </summary>
        /// <param name="projectCode">The project code of the project to edit.</param>
        /// <param name="collection">The from collection.</param>
        /// <returns>A redirect to <see cref="Index"/> if successful; otherwise the
        /// same view with model errors.</returns>
        [HttpPost]
        public ActionResult Edit(string projectCode, FormCollection collection)
        {
            var project = this.projectServices.GetProjectByCode(projectCode);
            try
            {
                if (ModelState.IsValid)
                {
                    if (TryUpdateModel(project))
                    {
                        this.projectServices.SaveProject(project);
                        return RedirectToAction("Index");
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

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Projects/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
