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

        /// <summary>
        /// uses the CommentCRUD in the DAL to call GetCommentTypes
        /// </summary>
        /// <returns>List of CommentType</returns>
        public List<Shared.CommentType> GetCommentTypes()
        {
            return _commentCrud.GetCommentTypes();
        }
        /// <summary>
        ///  uses the CommentCRUD in the DAL to call GetCommentsByComment
        /// </summary>
        /// <param name="comment"></param>
        /// <returns>List of Comments</returns>
        public List<Shared.Comments> GetCommentsByComment(Comments comment)
        {
            return _commentCrud.GetCommentsByComment(comment);
        }
        /// <summary>
        /// uses the CommentCRUD in the DAL to call GetCommentsByComment
        /// get the comments by the id of the task selected.
        /// </summary>
        /// <param name="id">task id </param>
        /// <returns>list of comments</returns>
        public List<Comments> GetAllCommentsByTask(int id)
        {
            return _commentCrud.GetAllCommentsByTask(id);
        }
        /// <summary>
        /// uses the CommentCRUD in the DAL to call GetById
        /// get comment object that has the same id as passed in the param
        /// </summary>
        /// <param name="id">comment id </param>
        /// <returns>comments class object</returns>
        public Comments GetById(int id)
        {
            return _commentCrud.GetById(id);
        }
        /// <summary>
        /// uses the CommentCRUD in the DAL to call Add
        /// adds new comment to DB
        /// </summary>
        /// <param name="commentClass"></param>
        /// <returns> inserted id</returns>
        public int Add(Comments commentClass)
        {
           return _commentCrud.Add(commentClass);
        }
        /// <summary>
        /// uses the CommentCRUD in the DAL to call delete
        /// </summary>
        /// <param name="commentClass"> object of class Comment</param>
        public void Delete(Comments commentClass)
        {
            _commentCrud.Delete(commentClass);
        }
        /// <summary>
        /// uses the CommentCRUD in the DAL to call Add Update
        /// </summary>
        /// <param name="commentClass"></param>
        /// <returns>id of updated object</returns>
        public int Update(Comments commentClass)
        {
           return _commentCrud.Update(commentClass);
        }
    }
}
