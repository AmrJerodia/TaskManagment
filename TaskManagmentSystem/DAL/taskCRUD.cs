using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using Shared;
using TaskStatus = System.Threading.Tasks.TaskStatus;

namespace DAL
{
   public  class TaskCRUD: CRUD
    {
        public TaskCRUD() : base() { }
        /// <summary>
        /// get a list of task status which is limited list 
        /// </summary>
        /// <returns> list of Taskstatus class</returns>
        public List<Shared.TaskStatus> GetStatuses()
        {
            string command = "select * from [TaskManagment].[dbo].[TaskStatus] ";
            var taskStatus = new List<Shared.TaskStatus>();
            var dataTable = GetDb().ExecuteSqlQuery(command);


            foreach (DataRow reader in dataTable.Rows)
            {



                taskStatus.Add(new Shared.TaskStatus()
                {
                    ID = Convert.ToInt32(reader["ID"]),
                    Name = reader["Name"].ToString()


                });
            }



            return taskStatus;

        }
        /// <summary>
        /// get a list of task types 
        /// </summary>
        /// <returns>list of TaskType class</returns>
        public List<Shared.TaskType> GeTasksTypes()
        {
            string command = "select * from [TaskManagment].[dbo].[TaskType] ";
            var taskTypes = new List<TaskType>();
            var dataTable = GetDb().ExecuteSqlQuery(command);


            foreach (DataRow reader in dataTable.Rows)
            {
                taskTypes.Add(new TaskType()
                {
                    ID = Convert.ToInt32(reader["ID"]),
                    Name = reader["Name"].ToString()


                });
            }



            return taskTypes;
        }
        /// <summary>
        /// search for all task that belogn to a specific period 
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns>list of task</returns>
        public List<Shared.Tasks> GetTaskByDate(DateTime? from, DateTime? to)
        {
            if (from == null && to == null) return null;
            var fromDate = from != null ? from.Value.Date.ToString(CultureInfo.InvariantCulture) : string.Empty;
            var toDate = to != null ? to.Value.Date.ToString(CultureInfo.InvariantCulture) : string.Empty;
            var tasks = new List<Shared.Tasks>();
            string command = "select * from [TaskManagment].[dbo].[Task] where ";
            if (from != null)
                command = command + "[CreatedDate] > '" + fromDate + "'";
            if (to != null)
            {
                command = from != null
                    ? command + "and [CreatedDate] < '" + toDate + "'"
                    : command + "[CreatedDate]  < '" + toDate + "'";
            }
            var dataTable = GetDb().ExecuteSqlQuery(command);
            DateTime? nextAction = null;
            DateTime? Required = null;
            foreach (DataRow reader in dataTable.Rows)
            {

                if (reader["NextActionDate"].GetType() != typeof(DBNull))
                    nextAction = Convert.ToDateTime(reader["NextActionDate"]);
                else nextAction = null;
                if (reader["RequiredByDate"].GetType() != typeof(DBNull))
                    Required = Convert.ToDateTime(reader["RequiredByDate"]);
                else Required = null;
                tasks.Add(new Tasks()
                {
                    ID = Convert.ToInt32(reader["ID"]),
                    CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                    Description = reader["Description"].ToString(),
                    AssignedTo = reader["AssignedTo"].ToString(),
                    RequiredByDate = Required,
                    Status = (TasksStatus)Convert.ToInt32(reader["Status"]),
                    Type = (TasksType)Convert.ToInt32(reader["Type"]),
                    NextActionDate = nextAction
                });
            }

            return tasks;

        }
        /// <summary>
        /// search for all task that linked to specific comment 
        /// </summary>
        /// <param name="comment"></param>
        /// <returns> list of task</returns>
        public List<Shared.Tasks> GetTasksByComment(Comments comment)
        {
            var reqDate = comment.ReminderDate != null ? comment.ReminderDate.Value.Date.ToString(CultureInfo.InvariantCulture) : string.Empty;
            var addedDate = comment.DateAdded != default(DateTime) ? comment.DateAdded.Date.ToString(CultureInfo.InvariantCulture) : string.Empty;

            string command = @"select * from [TaskManagment].[dbo].[CommentTaskView] where[CommentType] = " + (int)comment.Type ;
                command =!string.IsNullOrEmpty(addedDate)? command+ " or [DateAdded] = '" + addedDate + "'" :command;
            command = comment.Comment != null ? command + " or Comment like '%" + comment.Comment + "%'" : command;
            command = !string.IsNullOrEmpty(reqDate) ? command + " or [ReminderDate] = '" + reqDate + "'":command;
            
            var dataTable = GetDb().ExecuteSqlQuery(command);
            var tasks = new List<Shared.Tasks>();
            DateTime? nextAction = null;
            DateTime? required = null;
            foreach (DataRow reader in dataTable.Rows)
            {

                if (reader["NextActionDate"].GetType() != typeof(DBNull))
                    nextAction = Convert.ToDateTime(reader["NextActionDate"]);
                else nextAction = null;
                if (reader["RequiredByDate"].GetType() != typeof(DBNull))
                    required = Convert.ToDateTime(reader["RequiredByDate"]);
                else required = null;
                tasks.Add(new Tasks()
                {
                    ID = Convert.ToInt32(reader["tid"]),
                    CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                    Description = reader["Description"].ToString(),
                    AssignedTo = reader["AssignedTo"].ToString(),
                    RequiredByDate = required,
                    Status = (TasksStatus) Convert.ToInt32(reader["Status"]),
                    Type = (TasksType) Convert.ToInt32(reader["Type"]),
                    NextActionDate = nextAction
                });
            }


            return  tasks;
        }
        /// <summary>
        /// get task by Id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Task class</returns>
        public Tasks GetById(int id)
        {
            string command = "select * from [TaskManagment].[dbo].[Task] where [ID]=" + id;
            var dataTable = GetDb().ExecuteSqlQuery(command);
            var task = new Tasks();
            foreach (DataRow reader in dataTable.Rows)
            {
                task.ID = Convert.ToInt32(reader["ID"]);
                task.CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);
                task.Description = reader["Description"].ToString();
                task.AssignedTo = reader["AssignedTo"].ToString();
                if (reader["RequiredByDate"].GetType() != typeof(DBNull))
                    task.RequiredByDate = Convert.ToDateTime(reader["RequiredByDate"]);
                task.Status = (TasksStatus)Convert.ToInt32(reader["Status"]);
                task.Type = (TasksType)Convert.ToInt32(reader["Type"]);
                if (reader["NextActionDate"].GetType() != typeof(DBNull))
                    task.NextActionDate = Convert.ToDateTime(reader["NextActionDate"]);
            }

            return task;

        }
        /// <summary>
        /// insert the class to DB 
        /// </summary>
        /// <param name="taskclass"></param>
        /// <returns>the inserted ID </returns>
        public int Add(Tasks taskclass)
        {
            var reqDate = taskclass.RequiredByDate != null ?"'"+ taskclass.RequiredByDate.Value.Date.ToString(CultureInfo.InvariantCulture)+"'" : "NULL";
            string command = @"INSERT INTO [TaskManagment].[dbo].[Task] ([CreatedDate],[RequiredByDate],[Description],[Status],[Type],[AssignedTo],[NextActionDate]) output INSERTED.ID values(GETDATE()," + reqDate + ",'" + taskclass.Description + "'," + (int)taskclass.Status + "," + (int)taskclass.Type + ",'" + taskclass.AssignedTo + "',null )";
           return GetDb().ExecuteSqlTransaction(command);
        }
        /// <summary>
        /// delete the object from the DB 
        /// </summary>
        /// <param name="taskclass"></param>
        public void Delete(Tasks taskclass)
        {
            string command = @"delete from [TaskManagment].[dbo].[Task] where ID=" + taskclass.ID;
            GetDb().ExecuteSqlTransaction(command);
        }
        /// <summary>
        /// update the NextActionDate column in the specific object that belogn to the comment 
        /// note : it will only called after updating or inserting new comment which has reminderDate
        /// </summary>
        /// <param name="taskclass"></param>
        /// <param name="taskId"></param>
        /// <param name="commentTime"></param>
        /// <returns>update record ID </returns>
        public int UpdateNextAction(Tasks taskclass, int taskId, DateTime commentTime)
        {
            var NextActionDate = commentTime != null ? "'"+commentTime.Date.ToString(CultureInfo.InvariantCulture)+"'" : "NULL";
            string command = @"update  [TaskManagment].[dbo].[Task] set [NextActionDate] = '" + NextActionDate + "' where ID=" + taskId;
           return GetDb().ExecuteSqlTransaction(command);
        }
    }
}
