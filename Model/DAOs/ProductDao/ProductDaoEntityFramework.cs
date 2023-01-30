using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;


namespace Es.Udc.DotNet.PracticaMaD.Model.DAOs.ProductDao
{
    /// <summary>
    /// Specific Operations for BankCard
    /// </summary>
    public class ProductDaoEntityFramework :
        GenericDaoEntityFramework<Product, long>, IProductDao
    {
        #region Public Constructors

        /// <summary>
        /// Public Constructor
        /// </summary>
        public ProductDaoEntityFramework()
        {
        }

        #endregion Public Constructors

        #region IProductDao Members. Specific Operations

        /// IMPORTANTE CORREGIR LÍNEAS 80 Y 87
        /// <summary>
        /// Finds all Products by proName
        /// </summary>
        /// <param name="keyword">proName</param>
        /// <param name="proCatName">proCatName</param>
        /// <returns>A Products list</returns>
        /// <exception cref="InstanceNotFoundException"/>
        public List<Product> FindByKeywordAndProCatName(string keyword, string proCatName, int startIndex, int size)
        {
            List<Product> productList = null;

            #region Using Linq.

            DbSet<Product> products = Context.Set<Product>();

            if (keyword == "")
            {
                if (proCatName == "")
                {
                    var result =
                        (from p in products
                         select p);
                    productList = result.ToList();
                }
                else
                {
                    var result =
                        (from p in products
                         where p.proCatName == proCatName
                         select p);
                    productList = result.ToList();
                }
            }
            else 
            {
                if (proCatName == "")
                {
                    var result =
                        (from p in products
                         where p.proName.Contains(keyword)
                         select p);
                    productList = result.ToList();
                }
                else
                {
                    var result =
                        (from p in products
                         where p.proName.Contains(keyword) && p.proCatName == proCatName
                         select p);
                    productList = result.ToList();
                }
            }

            #endregion Using Linq.

            return productList.OrderBy(p => p.proId).Skip(startIndex).Take(size).ToList();
        }

        #endregion IProductDao Members
    }

}