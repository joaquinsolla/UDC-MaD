using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;


namespace Es.Udc.DotNet.PracticaMaD.Model.DAOs.CategoryDao
{
    /// <summary>
    /// Specific Operations for Category
    /// </summary>
    public class CategoryDaoEntityFramework :
        GenericDaoEntityFramework<Category, string>, ICategoryDao
    {
        #region Public Constructors

        /// <summary>
        /// Public Constructor
        /// </summary>
        public CategoryDaoEntityFramework()
        {
        }

        #endregion Public Constructors

        #region ICategoryDao Members. Specific Operations

        #endregion ICategoryDao Members
    }

}