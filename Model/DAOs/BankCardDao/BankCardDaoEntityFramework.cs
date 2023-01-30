using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;


namespace Es.Udc.DotNet.PracticaMaD.Model.DAOs.BankCardDao
{
    /// <summary>
    /// Specific Operations for BankCard
    /// </summary>
    public class BankCardDaoEntityFramework :
        GenericDaoEntityFramework<BankCard, long>, IBankCardDao
    {
        #region Public Constructors

        /// <summary>
        /// Public Constructor
        /// </summary>
        public BankCardDaoEntityFramework()
        {
        }

        #endregion Public Constructors

        #region IBankCardDao Members. Specific Operations

        /// <summary>
        /// Finds all BankCards by cardOwnerId
        /// </summary>
        /// <param name="cardOwnerId">cardOwnerId</param>
        /// <returns>A BankCard list</returns>
        public List<BankCard> FindAllByCardOwnerId(long cardOwnerId)
        {
            List<BankCard> bankCardList = null;

            #region Using Linq.

            DbSet<BankCard> bankCards = Context.Set<BankCard>();

            var result =
                (from b in bankCards
                 where b.cardOwnerId == cardOwnerId
                 select b);

            bankCardList = result.ToList();

            #endregion Using Linq.

            return bankCardList;
        }

        /// <summary>
        /// Finds the default BankCard by cardOwnerId
        /// </summary>
        /// <param name="cardOwnerId">cardOwnerId</param>
        /// <returns>A BankCard list</returns>
        public BankCard FindDefaultCardByCardOwnerId(long cardOwnerId)
        {
            BankCard bankCard = null;

            #region Using Linq.

            DbSet<BankCard> bankCards = Context.Set<BankCard>();

            var result =
                (from b in bankCards
                 where b.cardOwnerId == cardOwnerId && b.cardDefault == true
                 select b);

            bankCard = result.FirstOrDefault();

            #endregion Using Linq.

            if (bankCard == null)
                throw new InstanceNotFoundException(cardOwnerId,
                    typeof(BankCard).FullName);

            return bankCard;
        }

        #endregion IBankCardDao Members
    }

}