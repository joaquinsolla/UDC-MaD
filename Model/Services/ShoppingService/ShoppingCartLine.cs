using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.Services.ShoppingService
{
    public class ShoppingCartLine
    {

        #region Properties Region

        public Product product { get; set; }

        public int quantity { get; set; }

        public bool gift { get; set; }

        public decimal linePrice { get; set; }

        #endregion Properties Region

        /// <summary>
        /// Initializes a new instance of the <see cref="ShoppingCartLine"/>
        /// </summary>
        /// <param name="product"></param>
        /// <param name="quantity"></param>
        /// <param name="gift"></param>
        /// <param name="linePrice"></param>
        public ShoppingCartLine()
        {
            this.product = null;
            this.quantity = 0;
            this.gift = false;
            this.linePrice = 0M;
        }

        public ShoppingCartLine(Product product, int quantity, bool gift)
        {
            this.product = product;
            this.quantity = quantity;
            this.gift = gift;
            this.linePrice = (product.proPrice*quantity);
        }

        public override bool Equals(object obj)
        {

            ShoppingCartLine target = (ShoppingCartLine)obj;

            return (this.product == target.product)
                && (this.quantity == target.quantity)
                && (this.gift == target.gift)
                && (this.linePrice == target.linePrice);
        }

        // The GetHashCode method is used in hashing algorithms and data 
        // structures such as a hash table.       
        public override int GetHashCode()
        {
            return this.product.GetHashCode();
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
            String strShoppingCartLine;

            strShoppingCartLine =
                "· Product = " + product.proName + ", Quantity = " + quantity + 
                ", Gift = " + gift.ToString() + ", Line Price = " + linePrice +" ·";

            return strShoppingCartLine;
        }

    }
}
