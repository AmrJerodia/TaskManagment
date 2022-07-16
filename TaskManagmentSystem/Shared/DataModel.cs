using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class TaskStatus
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
    public class TaskType
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }


    public class Tasks  
    {
        public int ID { get; set; }
        [DataType(DataType.Date)]
        
        public DateTime? RequiredByDate { get; set; }
        public string Description { get; set; }
        public TasksStatus? Status { get; set; }
        public TasksType? Type { get; set; }

        public string AssignedTo { get; set; }
        public DateTime CreatedDate { get; set; }
        [DataType(DataType.Date)]
       
        public DateTime? NextActionDate { get; set; }
    }
    public class CommentType
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    public class Comments 
    {
        public int ID { get; set; }

        [Display(Name = "Date Added")]
        [DataType(DataType.Date)]
       
        public DateTime DateAdded { get; set; }
        [Display(Name = "Comment")]
        public string Comment { get; set; }
        [Display(Name = "Comment Type")]
        public CommentsType? Type { get; set; }

        public int TaskID { get; set; }
        [DataType(DataType.Date)]
      
        [Display(Name = "Reminder Date")]
        public DateTime? ReminderDate { get; set; }
    }
}
