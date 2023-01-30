using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Es.Udc.DotNet.PracticaMaD.Model.DAOs.TagDao
{
    public class TagDaoEntityFramework : GenericDaoEntityFramework<Tag, string>, ITagDao
    {

        #region Public Constructors

        /// <summary>
        /// Public Constructor
        /// </summary>
        public TagDaoEntityFramework()
        {
        }

        #endregion Public Constructors

        #region ITagDao Members. Specific Operation

        /// <exception cref="InstanceNotFoundException"></exception>
        public Tag FindByTagName(string tagName)
        {
            Tag tag = null;

            #region Using Linq.

            DbSet<Tag> tags = Context.Set<Tag>();

            var result =
                (from t in tags
                 where t.tagName == tagName
                 select t);

            tag = result.FirstOrDefault();

            #endregion Using Linq.

            if (tag == null)
                throw new InstanceNotFoundException(tagName,
                    typeof(Tag).FullName);

            return tag;
        }

        /// <summary>
        /// Finds all Tags by idComment
        /// </summary>
        /// <param name="idComment">idComment</param>
        /// <returns>A Tag list</returns>
        public List<Tag> FindByIdComment(long idComment)
        {
            List<Tag> tagList = null;

            #region Using Linq.

            DbSet<Comment> comments = Context.Set<Comment>();

            var result =
                (from c in comments
                 where c.commentId == idComment
                 select c.Tags);

            tagList = result.FirstOrDefault().ToList<Tag>();

            #endregion Using Linq.

            return tagList;
        }

        #endregion ITagDao Members

    }
}
