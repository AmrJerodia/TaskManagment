using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Repository;

namespace TaskMangmentSYS.Controllers
{
    public class BaseController : Controller
    {

        // GET: Base
        private TaskRepo _taskRepo;
        private CommentRepo _commentRepo;

        public  BaseController()
        {
            if(_taskRepo == null) _taskRepo = new TaskRepo();
            if(_commentRepo == null) _commentRepo = new CommentRepo();

        }

        public TaskRepo GetTaskRepo()
        {
            return _taskRepo;
        }
        public CommentRepo GetCommentRepo()
        {
            return _commentRepo;
        }
    }
}