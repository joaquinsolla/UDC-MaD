using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;


namespace Es.Udc.DotNet.PracticaMaD.Model.DAOs.FilmDao
{
    /// <summary>
    /// Specific Operations for Film
    /// </summary>
    public class FilmDaoEntityFramework :
        GenericDaoEntityFramework<Film, long>, IFilmDao
    {
        #region Public Constructors

        /// <summary>
        /// Public Constructor
        /// </summary>
        public FilmDaoEntityFramework()
        {
        }

        #endregion Public Constructors

        #region IFilmDao Members. Specific Operations

        #endregion IFilmDao Members
    }

}