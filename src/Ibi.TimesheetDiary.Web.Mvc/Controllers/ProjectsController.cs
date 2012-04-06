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

    using Ibi.TimesheetDiary.Data;

    /// <summary>
    /// Controller for <see cref="Project"/> related actions.
    /// </summary>
    public class ProjectsController : Controller
    {
        /// <summary>
        /// Local instance of <see cref="IDataContext"/> implementation.
        /// </summary>
        private readonly IDataContext dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectsController"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public ProjectsController(IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        /// <summary>
        /// Returns a view with a list of all projects.
        /// </summary>
        /// <returns>A view with a list of all projects.</returns>
        public ActionResult Index()
        {
            var projects = this.dataContext.Projects
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
            var project =
                this.dataContext.Projects.SingleOrDefault(
                    x => x.ProjectCode.Equals(projectCode, StringComparison.OrdinalIgnoreCase));

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
                        this.dataContext.SaveProject(newProject);
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
        
        //
        // GET: /Projects/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Projects/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Projects/Delete/5
 
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
