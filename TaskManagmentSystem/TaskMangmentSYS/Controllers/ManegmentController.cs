using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;
using Shared;
using TaskMangmentSYS.Models;

namespace TaskMangmentSYS.Controllers
{
    [Authorize]
    public class ManegmentController : BaseController
    {
        // GET: Manegment
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Search(DateTime? StartTime=null, DateTime? EndTime=null)
        {
            if (ModelState.IsValid)
            {
                SearchTasksModel searchList = new SearchTasksModel();

                searchList.Tasks = GetTaskRepo().GetTaskByDate(StartTime,EndTime);
                return PartialView("_SearchResult", searchList);
            }
            return Json("error");
        }

        public ActionResult ErroResult()
        {
            return View();
        }


      


      
    }
}