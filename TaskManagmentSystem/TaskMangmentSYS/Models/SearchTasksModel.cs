using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Shared;

namespace TaskMangmentSYS.Models
{
    public class SearchTasksModel
    {
        public List<Tasks> Tasks { get; set; }
        public Comments Comment { get; set; }
        public List<Comments> Comments { get; set; }
        [DataType(DataType.Date)]
        public DateTime? StartTime { get; set; }
        [DataType(DataType.Date)]
        public DateTime? EndTime { get; set; }
    }
}