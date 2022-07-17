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
      /// <summary>
      /// uses the repo object to add new comment 
      /// it uses the model TaskCommentModel in the model as parameter which contains Comments and tasks.
      /// </summary>
      /// <param name="model"></param>
      /// <returns>redirect to page ViewTask to view the task or error page</returns>
        [HttpPost]
        [Authorize]
        public ActionResult AddComment(TaskCommentModel model)
        {
            if (ModelState.IsValid)
            {
                int result = GetCommentRepo().Add(model.Comments);

                return RedirectToAction("ViewTask","Task" ,new { id = GetCommentRepo().GetById(result).TaskID });
            }
            return RedirectToAction("ErroResult", "Manegment");
        }
        /// <summary>
        /// 
        /// uses the repo object to Update comment 
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns>redirect to view task page or error if error happens </returns>
        public ActionResult UpdateComment(Comments model)
        {
            if (ModelState.IsValid)
            {
                int result = GetCommentRepo().Update(model);

                return RedirectToAction("ViewTask", "Task", new { id = model.TaskID });
            }
            return RedirectToAction("ErroResult", "Manegment");


        }

        /// <summary>
        /// uses the repo object to delete comment 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>

        [Authorize]
        public ActionResult DeleteComment(Comments model)
        {
            GetCommentRepo().Delete(model);
            return RedirectToAction("ViewTask", "Task", new { id = model.TaskID });
        }
        /// <summary>
        ///  uses the repo object to get list of comments Type to be binded to the page while search 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult SearchForComments()
        {
            ViewData["CommentType"] = GetCommentRepo().GetCommentTypes();
            return View();
        }
        /// <summary>
        /// it will be called from JsJquery in the seaRch page to send the id of the comment and get object comment that need to be updated
        /// this function is going to be called after calling searcher function from UI 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult LoadUpdate(int id=0)
        {

            ViewData["CommentType"] = GetCommentRepo().GetCommentTypes();
            var model = GetCommentRepo().GetById(id);
            return PartialView("_UpdateComment", model);
        }
        
   
        /// <summary>
        /// it will be called from the JS in the UI to pass parameter and let the function do the search and get the result .
        /// </summary>
        /// <param name="ReminderDate"></param>
        /// <param name="DateAdded"></param>
        /// <param name="Comment"></param>
        /// <param name="Type"></param>
        /// <returns>partial view contains the comments list </returns>
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