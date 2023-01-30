using System;
using Es.Udc.DotNet.ModelUtil.Log;

namespace Es.Udc.DotNet.PracticaMaD.Model.Exceptions
{
    [Serializable]
    public class ProductOutOfStockException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="ProductOutOfStockException"/> class.
        /// </summary>
        /// <param name="proId"><c>proId</c></param>

        public ProductOutOfStockException(long proId)
            : base("Product " + proId + " is out of stock")
        {
            this.proId = proId;
        }

        public long proId { get; private set; }


        #region Test Code Region. Uncomment for testing.


        #endregion
    }
}