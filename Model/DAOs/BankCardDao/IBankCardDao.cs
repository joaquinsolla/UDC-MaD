using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.DAOs.BankCardDao
{
    public interface IBankCardDao : IGenericDao<BankCard, long>
    {

        /// <summary>
        /// Finds all BankCards by cardOwnerId
        /// </summary>
        /// <param name="cardOwnerId">cardOwnerId</param>
        /// <returns>A BankCard list</returns>
        List<BankCard> FindAllByCardOwnerId(long cardOwnerId);

        /// <summary>
        /// Finds the default BankCard by cardOwnerId
        /// </summary>
        /// <param name="cardOwnerId">cardOwnerId</param>
        /// <returns>A BankCard list</returns>
        BankCard FindDefaultCardByCardOwnerId(long cardOwnerId);
    }
}