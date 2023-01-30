using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.Services.CatalogService
{
    /// <summary>
    /// VO Class which contains the product details
    /// </summary>
    [Serializable()]
    public class ProductDetails
    {

        #region Properties Region

        public long ProId { get; private set; }

        public string ProName { get; private set; }

        public decimal ProPrice { get; private set; }

        public DateTime ProReleaseDate { get; private set; }

        public long ProStock { get; private set; }

        public string ProCatName { get; private set; }

        #endregion Properties Region

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductDetails"/>
        /// </summary>
        /// <param name="proId"></param>
        /// <param name="proName"></param>
        /// <param name="proPrice"></param>
        /// <param name="proReleaseDate"></param>
        /// <param name="proStock"></param>
        /// <param name="proCatName"></param>

        public ProductDetails(long proId, string proName, decimal proPrice,
            DateTime proReleaseDate, long proStock, string proCatName)
        {
            this.ProId = proId;
            this.ProName = proName;
            this.ProPrice = proPrice;
            this.ProReleaseDate = proReleaseDate;
            this.ProStock = proStock;
            this.ProCatName = proCatName;
        }

        public ProductDetails(long proId, string proName, decimal proPrice,
            long proStock)
        {
            this.ProId = proId;
            this.ProName = proName;
            this.ProPrice = proPrice;
            this.ProStock = proStock;
        }

        public override bool Equals(object obj)
        {

            ProductDetails target = (ProductDetails)obj;

            return (this.ProId == target.ProId)
                && (this.ProName == target.ProName)
                && (this.ProPrice == target.ProPrice)
                && (this.ProReleaseDate == target.ProReleaseDate)
                && (this.ProStock == target.ProStock)
                && (this.ProCatName == target.ProCatName);
        }

        // The GetHashCode method is used in hashing algorithms and data 
        // structures such as a hash table. In order to ensure that it works 
        // properly, we suppose that the ProId does not change.        
        public override int GetHashCode()
        {
            return this.ProId.GetHashCode();
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
            String strProductDetails;

            strProductDetails =
                "[ proId = " + ProId + " | " +
                "proName = " + ProName + " | " +
                "proPrice = " + ProPrice + " | " +
                "proReleaseDate = " + ProReleaseDate + " | " +
                "proStock = " + ProStock + " | " +
                "proCatName = " + ProCatName + " ]";

            return strProductDetails;
        }

    }
}
