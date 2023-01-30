using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.DAOs.OrderLineDao
{
    public interface IOrderLineDao : IGenericDao<OrderLine, long>
    {

        /// <summary>
        /// Finds all OrderLines by lineOrderId
        /// </summary>
        /// <param name="lineOrderId">lineOrderId</param>
        /// <returns>A OrderLine list</returns>
        List<OrderLine> FindAllByLineOrderId(long lineOrderId);
    }
}