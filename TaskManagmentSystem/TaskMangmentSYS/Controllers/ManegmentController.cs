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
        /// <summary>
        /// The Dashboard page that contains the task search criteria allow the user to do the selection of period from-to 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// it will be called from the dashboard JS page . to pass the parameter StartTime , EndTime .
        /// it will do the search of getting all the result that match the search criteria .
        /// at least startTime or EndTime has to be not null 
        /// 
        /// </summary>
        /// <param name="StartTime"></param>
        /// <param name="EndTime"></param>
        /// <returns> partial view contains the list of tasks </returns>
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