using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shared;


namespace TaskMangmentSYS.Models
{
    public class TaskCommentModel
    {
        public Tasks Tasks { get; set; }
        public Comments Comments { get; set; }
        public string Users { get; set; }
    }
}