using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;


namespace Es.Udc.DotNet.PracticaMaD.Model.DAOs.OrderLineDao
{
    /// <summary>
    /// Specific Operations for OrderLine
    /// </summary>
    public class OrderLineDaoEntityFramework :
        GenericDaoEntityFramework<OrderLine, long>, IOrderLineDao
    {
        #region Public Constructors

        /// <summary>
        /// Public Constructor
        /// </summary>
        public OrderLineDaoEntityFramework()
        {
        }

        #endregion Public Constructors

        #region IOrderLineDao Members. Specific Operations

        /// <summary>
        /// Finds all OrderLines by lineOrderId
        /// </summary>
        /// <param name="lineOrderId">lineOrderId</param>
        /// <returns>An OrderLine list</returns>
        public List<OrderLine> FindAllByLineOrderId(long lineOrderId)
        {
            List<OrderLine> orderLineList = null;

            #region Using Linq.

            DbSet<OrderLine> orderLines = Context.Set<OrderLine>();

            var result =
                (from l in orderLines
                 where l.lineOrderId == lineOrderId
                 select l);

            orderLineList = result.OrderBy(x => x.lineId).ToList();

            #endregion Using Linq.

            return orderLineList;
        }

        #endregion IOrderLineDao Members
    }

}