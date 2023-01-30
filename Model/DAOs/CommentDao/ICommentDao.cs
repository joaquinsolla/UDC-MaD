using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Es.Udc.DotNet.PracticaMaD.Model.DAOs.CommentDao
{
    public interface ICommentDao : IGenericDao<Comment, long>
    {

        /// <summary>
        /// Finds all Comment by idProduct
        /// </summary>
        /// <param name="idProduct">idProduct</param>
        /// <returns>A Comment list</returns>
        List<Comment> FindByIdProduct(long idProduct, int startIndex, int size);

        /// <summary>
        /// Finds all Comments by tagName
        /// </summary>
        /// <param name="tagName">tagName</param>
        /// <returns>A Comment list</returns>
        List<Comment> FindByTagName(string tagName, int startIndex, int size);
    }
}
