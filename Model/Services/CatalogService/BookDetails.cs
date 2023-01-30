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
    public class BookDetails : ProductDetails
    {

        #region Properties Region
        public string BookISBN { get; private set; }
        public string BookEditorial { get; private set; }
        public string BookEdition { get; private set; }
        public long BookPages { get; private set; }
        public DateTime BookReleaseDate { get; private set; }

        #endregion Properties Region

        /// <summary>
        /// Initializes a new instance of the <see cref="BookDetails"/>
        /// </summary>
        /// <param name="proId"></param>
        /// <param name="bookISBN"></param>
        /// <param name="bookEditorial"></param>
        /// <param name="bookEdition"></param>
        /// <param name="bookPages"></param>
        /// <param name="bookReleaseDate"></param>

        public BookDetails(long proId, string proName, decimal proPrice, DateTime proReleaseDate, long proStock,
            string proCatName, string bookISBN, string bookEditorial, string bookEdition, long bookPages,
            DateTime bookReleaseDate) : base(proId, proName, proPrice, proReleaseDate, proStock, proCatName)
        {
            this.BookISBN = bookISBN;
            this.BookEditorial = bookEditorial;
            this.BookEdition = bookEdition;
            this.BookPages = bookPages;
            this.BookReleaseDate = bookReleaseDate;
        }

        public BookDetails(long proId, string proName, decimal proPrice, long proStock,
            string bookISBN, string bookEditorial, string bookEdition, long bookPages)
            : base(proId, proName, proPrice, proStock)
        {
            this.BookISBN = bookISBN;
            this.BookEditorial = bookEditorial;
            this.BookEdition = bookEdition;
            this.BookPages = bookPages;
        }


        public override bool Equals(object obj)
        {
            BookDetails target = (BookDetails)obj;

            return base.Equals((ProductDetails)obj)
                && (this.BookISBN == target.BookISBN)
                && (this.BookEditorial == target.BookEditorial)
                && (this.BookEdition == target.BookEdition)
                && (this.BookPages == target.BookPages)
                && (this.BookReleaseDate == target.BookReleaseDate);
        }

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
        /// 
        public override String ToString()
        {
            String strMusicDetails;

            strMusicDetails = base.ToString() +
                "[ bookISBN = " + BookISBN + " | " +
                "bookEditorial = " + BookEditorial + " | " +
                "bookEdition = " + BookEdition + " | " +
                "bookPages = " + BookPages + " | " +
                "bookReleaseDate = " + BookReleaseDate + " ]";

            return strMusicDetails;
        }
    }
}
