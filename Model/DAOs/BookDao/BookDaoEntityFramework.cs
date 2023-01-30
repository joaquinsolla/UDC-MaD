using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;


namespace Es.Udc.DotNet.PracticaMaD.Model.DAOs.BookDao
{
    /// <summary>
    /// Specific Operations for Book
    /// </summary>
    public class BookDaoEntityFramework :
        GenericDaoEntityFramework<Book, long>, IBookDao
    {
        #region Public Constructors

        /// <summary>
        /// Public Constructor
        /// </summary>
        public BookDaoEntityFramework()
        {
        }

        #endregion Public Constructors

        #region IBookDao Members. Specific Operations

        #endregion IBookDao Members
    }

}