using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;

namespace DAL
{
    public class CommentCRUD: CRUD
    {
        private taskCRUD _taskCrud;
       public CommentCRUD() : base() {if(_taskCrud == null) _taskCrud=new taskCRUD(); }
       public List<Shared.CommentType> GetCommentTypes()
       {
           string command = "select * from [TaskManagment].[dbo].[CommentType] ";
           var taskTypes = new List<Shared.CommentType>();
           var dataTable = GetDb().ExecuteSqlQuery(command);


           foreach (DataRow reader in dataTable.Rows)
           {

                taskTypes.Add(new Shared.CommentType()
               {
                   ID = Convert.ToInt32(reader["ID"]),
                   Name = reader["Name"].ToString()


               });
           }



           return taskTypes;
       }
       public List<Shared.Comments> GetCommentsByComment(Comments comment)
       {
           var reqDate = comment.ReminderDate != null ? comment.ReminderDate.Value.Date.ToString(CultureInfo.InvariantCulture) : string.Empty;
           var addedDate = comment.DateAdded != default(DateTime) ? comment.DateAdded.Date.ToString(CultureInfo.InvariantCulture) : string.Empty;

           string command = @"select * from [TaskManagment].[dbo].[CommentTaskView] where[CommentType] = " + (int)comment.Type;
           command = !string.IsNullOrEmpty(addedDate) ? command + " and [DateAdded] = '" + addedDate + "'" : command;
           command = comment.Comment != null ? command + " and Comment like '%" + comment.Comment + "%'" : command;
           command = !string.IsNullOrEmpty(reqDate) ? command + " and [ReminderDate] = '" + reqDate + "'" : command;

           var dataTable = GetDb().ExecuteSqlQuery(command);
           var commentsList = new List<Shared.Comments>();
           DateTime? dt = null;
            foreach (DataRow reader in dataTable.Rows)
            {

                if (reader["ReminderDate"].GetType() != typeof(DBNull))
                    dt = Convert.ToDateTime(reader["ReminderDate"]);
                else
                    dt = null;
                commentsList.Add(new Comments()
               {
                   ID = Convert.ToInt32(reader["ID"]),
                   Comment = reader["Comment"].ToString(),
                   DateAdded = Convert.ToDateTime(reader["DateAdded"]),
                   Type = (CommentsType)Convert.ToInt32(reader["Type"]),
                   ReminderDate = dt,
                   TaskID = Convert.ToInt32(reader["TaskID"])
                });
            }


            return commentsList;
       }

        public List<Comments> GetAllCommentsByTask(int id)
        {
            string command = "select * from [TaskManagment].[dbo].[CommentTaskView] where [tid]=" + id;
            var comments = new List<Comments>();
            var dataTable = GetDb().ExecuteSqlQuery(command);


            DateTime? rddt = null;
            foreach (DataRow reader in dataTable.Rows)
            {

                if (reader["ReminderDate"].GetType() != typeof(DBNull))
                    rddt = Convert.ToDateTime(reader["ReminderDate"]);
                else rddt = null;
                comments.Add(new Comments
                {
                    ID = Convert.ToInt32(reader["ID"]),
                    Comment = reader["Comment"].ToString(),
                    DateAdded = Convert.ToDateTime(reader["DateAdded"]),
                    TaskID = Convert.ToInt32(reader["TaskID"]),
                    Type = (CommentsType)Convert.ToInt32(reader["CommentType"]),
                    ReminderDate = rddt

                });
            };



            return comments;
        }

        public Comments GetById(int id)
        {
            string command = "select * from [TaskManagment].[dbo].[Comment] where [ID]=" + id;
            var dataTable = GetDb().ExecuteSqlQuery(command);
            var comments = new Comments();
            foreach (DataRow reader in dataTable.Rows)
            {
                comments.ID = Convert.ToInt32(reader["ID"]);
                comments.Comment = reader["Comment"].ToString();
                comments.DateAdded = Convert.ToDateTime(reader["DateAdded"]);
                comments.TaskID = Convert.ToInt32(reader["TaskID"]);
                comments.Type = (CommentsType)Convert.ToInt32(reader["CommentType"]);
                if (reader["ReminderDate"].GetType() != typeof(DBNull))
                    comments.ReminderDate = Convert.ToDateTime(reader["ReminderDate"]);
            }

            return comments;
        }

        public int Add(Comments commentClass)
        {
            var remDate = commentClass.ReminderDate != null ? "'"+commentClass.ReminderDate.Value.Date.ToString(CultureInfo.InvariantCulture)+"'" : "NULL";
            string command = @"INSERT INTO [TaskManagment].[dbo].[Comment] ([DateAdded],[Comment],[ReminderDate],[CommentType],[TaskID]) output INSERTED.ID values(GETDATE(),'" + commentClass.Comment + "'," + remDate + "," + (int)commentClass.Type + "," + commentClass.TaskID + ")";
           int result= GetDb().ExecuteSqlTransaction(command);
           if (commentClass.ReminderDate != null)
           {
               _taskCrud.UpdateNextAction(null, commentClass.TaskID, commentClass.ReminderDate.Value);
           }

           return result;
        }

        public void Delete(Comments commentClass)
        {
            string command = @"delete from [TaskManagment].[dbo].[Comment] where ID=" + commentClass.ID;
            GetDb().ExecuteSqlTransaction(command);
        }

        public int Update(Comments commentClass)
        {
            var remDate = commentClass.ReminderDate != null ?"'"+ commentClass.ReminderDate.Value.Date.ToString(CultureInfo.InvariantCulture) +"'": "NULL";
            string command = @"update  [TaskManagment].[dbo].[Comment] set[Comment]='" + commentClass.Comment + "',[ReminderDate]=" + remDate + ",[CommentType]=" + (int)commentClass.Type + " where ID=" + commentClass.ID;
           return GetDb().ExecuteSqlTransaction(command);
        }
    }
}
