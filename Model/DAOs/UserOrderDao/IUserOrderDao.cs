using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.DAOs.UserOrderDao
{
    public interface IUserOrderDao : IGenericDao<UserOrder, long>
    {

        /// <summary>
        /// Finds all UserOrders by orderUserId and orders them by orderDate
        /// </summary>
        /// <param name="orderUserId">orderUserId</param>
        /// <returns>A UserOrder list</returns>
        List<UserOrder> FindAllByOrderUserId(long orderUserId, int startIndex, int size);
    }
}