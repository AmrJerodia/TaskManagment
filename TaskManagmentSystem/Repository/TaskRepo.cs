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
        private TaskCRUD _taskCrud;
      public  TaskRepo()
      {
          if (_taskCrud == null) _taskCrud = new TaskCRUD();
      }
        /// <summary>
        /// uses the TaskCRUD in the DAL to call GetStatuses
        /// </summary>
        /// <returns>List of status </returns>
        public List<Shared.TaskStatus> GetStatuses()
      {
          return _taskCrud.GetStatuses();
      }
        /// <summary>
        ///  uses the TaskCRUD in the DAL to call GetTaskTypes
        /// </summary>
        /// <returns>list of Types</returns>
        public List<Shared.TaskType> GeTasksTypes()
      {
          return _taskCrud.GeTasksTypes();
      }

        /// <summary>
        ///  uses the TaskCRUD in the DAL to call GetTasksByComment
        ///
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>

        public List<Shared.Tasks> GetTasksByComment(Comments comment)
        {
          return _taskCrud.GetTasksByComment(comment);
           
           
        }
        /// <summary>
        ///  uses the TaskCRUD in the DAL to call GetById
        /// </summary>
        /// <param name="id">Task ID</param>
        /// <returns>Task object</returns>
        public Tasks GetById(int id)
        {
            return _taskCrud.GetById(id);

        }
        /// <summary>
        ///  uses the TaskCRUD in the DAL to call GetTaskByDate
        ///  searches for all task that has been in at lest one of the search criteria 
        ///  one of the parameters should not be null at least .
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns>list of tasks</returns>
        public List<Shared.Tasks> GetTaskByDate(DateTime? from, DateTime? to)
        {
            if(from!=null||to!=null)
                return _taskCrud.GetTaskByDate(from, to).OrderBy(a=>a.Status).ToList();
            return new List<Tasks>();
        }
        /// <summary>
        ///  uses the TaskCRUD in the DAL to call Add
        ///  add task to the DB
        /// </summary>
        /// <param name="taskclass"></param>
        /// <returns>id of the inserted task </returns>
        public int Add(Tasks taskclass)
        {
            return _taskCrud.Add(taskclass);
        }
        /// <summary>
        ///  uses the TaskCRUD in the DAL to call Delete
        /// </summary>
        /// <param name="taskclass"></param>
        public void Delete(Tasks taskclass)
        {
            _taskCrud.Delete(taskclass);
        }
     
    }
}
