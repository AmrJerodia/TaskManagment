using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Shared;
using TaskStatus = Shared.TasksStatus;

namespace Repository
{
    public class TaskRepo
    {
        private taskCRUD _taskCrud;
      public  TaskRepo()
      {
          if (_taskCrud == null) _taskCrud = new taskCRUD();
      }

      public List<Shared.TaskStatus> GetStatuses()
      {
          return _taskCrud.GetStatuses();
      }

      public List<Shared.TaskType> GeTasksTypes()
      {
          return _taskCrud.GeTasksTypes();
      }



        public List<Shared.Tasks> GetTasksByComment(Comments comment)
        {
          return _taskCrud.GetTasksByComment(comment);
           
           
        }

        public Tasks GetById(int id)
        {
            return _taskCrud.GetById(id);

        }

        public List<Shared.Tasks> GetTaskByDate(DateTime? from, DateTime? to)
        {
            if(from!=null||to!=null)
                return _taskCrud.GetTaskByDate(from, to).OrderBy(a=>a.Status).ToList();
            return new List<Tasks>();
        }

        public int Add(Tasks taskclass)
        {
            return _taskCrud.Add(taskclass);
        }

        public void Delete(Tasks taskclass)
        {
            _taskCrud.Delete(taskclass);
        }

        public int UpdateNextAction(Tasks taskclass, int taskId,DateTime  commentTime)
        {
           return _taskCrud.UpdateNextAction(taskclass, taskId, commentTime);
        }
    }
}
