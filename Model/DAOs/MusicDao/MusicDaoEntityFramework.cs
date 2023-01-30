using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;


namespace Es.Udc.DotNet.PracticaMaD.Model.DAOs.MusicDao
{
    /// <summary>
    /// Specific Operations for Music
    /// </summary>
    public class MusicDaoEntityFramework :
        GenericDaoEntityFramework<Music, long>, IMusicDao
    {
        #region Public Constructors

        /// <summary>
        /// Public Constructor
        /// </summary>
        public MusicDaoEntityFramework()
        {
        }

        #endregion Public Constructors

        #region IMusicDao Members. Specific Operations

        #endregion IMusicDao Members
    }

}