using System;
using Es.Udc.DotNet.ModelUtil.Log;

namespace Es.Udc.DotNet.PracticaMaD.Model.Exceptions
{
    [Serializable]
    public class NotInShoppingCartException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="ShoppingCartIsEmptyException"/> class.
        /// </summary>
        /// 

        public NotInShoppingCartException(long proId)
            : base("Produduct with id '" + proId +"' is not in the ShoppingCart.")
        {
        }


        #region Test Code Region. Uncomment for testing.


        #endregion
    }
}