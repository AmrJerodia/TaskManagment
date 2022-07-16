using System;
using System.Collections.Generic;
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
   public class CommentRepo 
    {
        private CommentCRUD _commentCrud;
        public CommentRepo()
        {
            if (_commentCrud == null) _commentCrud = new CommentCRUD();
        }


        public List<Shared.CommentType> GetCommentTypes()
        {
            return _commentCrud.GetCommentTypes();
        }

        public List<Shared.Comments> GetCommentsByComment(Comments comment)
        {
            return _commentCrud.GetCommentsByComment(comment);
        }

        public List<Comments> GetAllCommentsByTask(int id)
        {
            return _commentCrud.GetAllCommentsByTask(id);
        }

        public Comments GetById(int id)
        {
            return _commentCrud.GetById(id);
        }

        public int Add(Comments commentClass)
        {
           return _commentCrud.Add(commentClass);
        }

        public void Delete(Comments commentClass)
        {
            _commentCrud.Delete(commentClass);
        }

        public int Update(Comments commentClass)
        {
           return _commentCrud.Update(commentClass);
        }
    }
}
