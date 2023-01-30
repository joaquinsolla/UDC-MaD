using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;


namespace Es.Udc.DotNet.PracticaMaD.Model.DAOs.UserOrderDao
{
    /// <summary>
    /// Specific Operations for UserOrder
    /// </summary>
    public class UserOrderDaoEntityFramework :
        GenericDaoEntityFramework<UserOrder, long>, IUserOrderDao
    {
        #region Public Constructors

        /// <summary>
        /// Public Constructor
        /// </summary>
        public UserOrderDaoEntityFramework()
        {
        }

        #endregion Public Constructors

        #region IUserOrderDao Members. Specific Operations

        /// <summary>
        /// Finds all UserOrders by orderUserId and orders them by orderDate
        /// </summary>
        /// <param name="orderUserId">orderUserId</param>
        /// <returns>A UserOrder list</returns>
        public List<UserOrder> FindAllByOrderUserId(long orderUserId, int startIndex, int size)
        {
            List<UserOrder> userOrderList = null;

            #region Using Linq.

            DbSet<UserOrder> userOrders = Context.Set<UserOrder>();

            var result =
                (from o in userOrders
                 where o.orderUserId == orderUserId
                 select o);

            userOrderList = result.ToList<UserOrder>();

            #endregion Using Linq.

            return userOrderList.OrderByDescending(o => o.orderDate).Skip(startIndex).Take(size).ToList();
        }

        #endregion IUserOrderDao Members
    }

}