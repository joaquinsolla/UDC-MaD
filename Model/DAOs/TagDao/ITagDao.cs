using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.DAOs.TagDao
{
    public interface ITagDao : IGenericDao<Tag, string>
    {

        /// <exception cref="InstanceNotFoundException"></exception>
        Tag FindByTagName(string tagName);

        /// <summary>
        /// Finds all Tags by idComment
        /// </summary>
        /// <param name="idComment">idComment</param>
        /// <returns>A Tag list</returns>
        List<Tag> FindByIdComment(long idComment);

    }
}
