using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;


namespace Es.Udc.DotNet.PracticaMaD.Model.DAOs.BankCardTypeDao
{
    /// <summary>
    /// Specific Operations for BankCardType
    /// </summary>
    public class BankCardTypeDaoEntityFramework :
        GenericDaoEntityFramework<BankCardType, long>, IBankCardTypeDao
    {
        #region Public Constructors

        /// <summary>
        /// Public Constructor
        /// </summary>
        public BankCardTypeDaoEntityFramework()
        {
        }

        #endregion Public Constructors

        #region IBankCardTypeDao Members. Specific Operations
        #endregion IBankCardTypeDao Members
    }

}