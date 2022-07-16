using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shared;
using TaskMangmentSYS.Models;

namespace TaskMangmentSYS.Controllers
{
    public class CommentController : BaseController
    {
        // GET: Comment
      
        [HttpPost]
        [Authorize]
        public ActionResult AddComment(TaskCommentModel model)
        {
            if (ModelState.IsValid)
            {
                int result = GetCommentRepo().Add(model.Comments);

                return RedirectToAction("ViewTask","Task" ,new { id = GetCommentRepo().GetById(result).TaskID });
            }
            return View("Error");
        }

        public ActionResult UpdateComment(Comments model)
        {
            if (ModelState.IsValid)
            {
                int result = GetCommentRepo().Update(model);

                return RedirectToAction("ViewTask", "Task", new { id = model.TaskID });
            }
            return View("Error");

        }

        [Authorize]
        public ActionResult DeleteComment(Comments model)
        {
            GetCommentRepo().Delete(model);
            return RedirectToAction("ViewTask", "Task", new { id = model.TaskID });
        }

        [Authorize]
        public ActionResult SearchForComments()
        {
            ViewData["CommentType"] = GetCommentRepo().GetCommentTypes();
            return View();
        }

        public ActionResult LoadUpdate(int id=0)
        {

            ViewData["CommentType"] = GetCommentRepo().GetCommentTypes();
            var model = GetCommentRepo().GetById(id);
            return PartialView("_UpdateComment", model);
        }
        
   
        
        public ActionResult Searcher( DateTime? ReminderDate, DateTime? DateAdded,string Comment="", int Type= 0)
        {
            DateTime dt = DateAdded != null ? DateAdded.Value : default(DateTime);
            var comment = new Comments()
            {
                DateAdded = dt,
                ReminderDate = ReminderDate,
                Comment = Comment,
                Type = (CommentsType)Type
            };
            if (ModelState.IsValid)
            {
                SearchTasksModel searchList = new SearchTasksModel();

                searchList.Comments = GetCommentRepo().GetCommentsByComment(comment);
                return PartialView("_CommentSearch", searchList);
               
            }
            return Json("error");
        }
    }
}