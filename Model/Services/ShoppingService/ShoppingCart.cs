using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.Services.ShoppingService
{
    /// <summary>
    /// VO Class which contains the ShoppingCart details
    /// </summary>
    [Serializable()]
    public class ShoppingCart
    {

        #region Properties Region

        public List<ShoppingCartLine> cartLines { get; set; }

        public decimal totalPrice { get; set; }

        #endregion Properties Region

        /// <summary>
        /// Initializes a new instance of the <see cref="ShoppingCart"/>
        /// </summary>
        /// <param name="cartLines"></param>
        /// <param name="totalPrice"></param>

        public ShoppingCart()
        {
            this.cartLines = new List<ShoppingCartLine>();
            this.totalPrice = 0M;
        }

        public ShoppingCart(List<ShoppingCartLine> cartLines)
        {
            this.cartLines = cartLines;

            foreach (ShoppingCartLine line in cartLines) {
                this.totalPrice += line.linePrice;
            }

        }

        public override bool Equals(object obj)
        {

            ShoppingCart target = (ShoppingCart)obj;

            return (this.cartLines == target.cartLines)
                && (this.totalPrice == target.totalPrice);
        }

        // The GetHashCode method is used in hashing algorithms and data 
        // structures such as a hash table.       
        public override int GetHashCode()
        {
            return this.cartLines.GetHashCode();
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"></see> that represents the 
        /// current <see cref="T:System.Object"></see>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"></see> that represents the current 
        /// <see cref="T:System.Object"></see>.
        /// </returns>
        public override String ToString()
        {
            String strShoppingCartDetails;

            strShoppingCartDetails = "[Cart lines = " + cartLines.Count + ", Total Price: " + totalPrice + "]\n";

            foreach (ShoppingCartLine line in cartLines) {
                strShoppingCartDetails += "\n";
                strShoppingCartDetails += line.ToString();
            }

            return strShoppingCartDetails;
        }

    }
}
