using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.DAOs.CommentDao
{
    public class CommentDaoEntityFramework : GenericDaoEntityFramework<Comment, long>, ICommentDao
    {

        #region Public Constructors

        /// <summary>
        /// Public Constructor
        /// </summary>
        public CommentDaoEntityFramework()
        {
        }

        #endregion Public Constructors

        #region ICommentDao Members. Specific Operations

        /// <summary>
        /// Finds all Comment by idProduct
        /// </summary>
        /// <param name="idProduct">idProduct</param>
        /// <returns>A Comment list</returns>
        public List<Comment> FindByIdProduct(long idProduct, int startIndex, int size) 
        {
            List<Comment> commentList = null;

            #region Using Linq.

            DbSet<Comment> comments = Context.Set<Comment>();

            var result =
                (from c in comments
                 where c.proId == idProduct
                 select c);

            commentList = result.OrderByDescending(c => c.commentDate).Skip(startIndex).Take(size).ToList();

            #endregion Using Linq.

            return commentList;
        }

        /// <summary>
        /// Finds all Comment by idTag
        /// </summary>
        /// <param name="idTag">idTag</param>
        /// <returns>A Comment list</returns>
        public List<Comment> FindByTagName(string tagName, int startIndex, int size)
        {
            List<Comment> commentList = null;

            #region Using Linq.

            DbSet<Tag> tags = Context.Set<Tag>();

            var result =
                (from t in tags
                 where t.tagName == tagName
                 select t.Comments);

            commentList = result.FirstOrDefault().Skip(startIndex).Take(size).ToList();

            #endregion Using Linq.

            return commentList;
        }

        #endregion ICommentDao Members

    }

}
