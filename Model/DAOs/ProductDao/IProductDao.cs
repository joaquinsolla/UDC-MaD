using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Es.Udc.DotNet.PracticaMaD.Model.DAOs.ProductDao
{
    public interface IProductDao : IGenericDao<Product, long>
    {

        /// <summary>
        /// Finds all Products by proName
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="proCatName">proCatName</param>
        /// <returns>A Products list</returns>
        /// <exception cref="InstanceNotFoundException"/>
        List<Product> FindByKeywordAndProCatName(string keyword, string proCatName, int startIndex, int size);
    }
}