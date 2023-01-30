using System;
using Es.Udc.DotNet.ModelUtil.Log;

namespace Es.Udc.DotNet.PracticaMaD.Model.Exceptions
{
    [Serializable]
    public class ShoppingCartIsEmptyException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="ShoppingCartIsEmptyException"/> class.
        /// </summary>
        /// 

        public ShoppingCartIsEmptyException()
            : base("Shopping cart is empty")
        {
        }


        #region Test Code Region. Uncomment for testing.


        #endregion
    }
}