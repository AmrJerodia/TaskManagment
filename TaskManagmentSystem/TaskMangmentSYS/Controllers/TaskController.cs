using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Shared;
using TaskMangmentSYS.Models;

namespace TaskMangmentSYS.Controllers
{
    public class TaskController : BaseController
    {
        // GET: Task
       /// <summary>
       /// called the TaskRepo to get the task by ID and show it in the page .
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
        [Authorize]
        public ActionResult ViewTask(int id)
        {

            TaskCommentModel model = new TaskCommentModel();
            model.Tasks = GetTaskRepo().GetById(id);
            ViewData["AllComments"] = GetCommentRepo().GetAllCommentsByTask(id);
            ViewData["CommentType"] = GetCommentRepo().GetCommentTypes();
            model.Comments = new Comments();
            return View(model);
        }

        /// <summary>
        ///view page add Task  .
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult AddTask()
        {
            ViewData["types"] = GetTaskRepo().GeTasksTypes();
            ViewData["status"] = GetTaskRepo().GetStatuses();

            ViewBag.users = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().Users.Select(a => a.UserName).ToList();
            return View();
        }

        /// <summary>
        /// called the TaskRepo to add the task after form has been submited  .
        /// </summary>
        /// <param name="model"></param>
        /// <returns>redirect to page view task </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult AddTask(TaskCommentModel model)
        {
            if (ModelState.IsValid)
            {
                int result = GetTaskRepo().Add(model.Tasks);
                return RedirectToAction("ViewTask", new { id = result });

            }

            return View(model);
        }
    }
}