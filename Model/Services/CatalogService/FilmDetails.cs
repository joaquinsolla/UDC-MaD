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
    public class FilmDetails : ProductDetails
    {

        #region Properties Region

        public string FilmDirector { get; private set; }
        public string FilmGenre { get; private set; }
        public long FilmRating { get; private set; }
        public long FilmDurationMins { get; private set; }
        public DateTime FilmReleaseDate { get; private set; }

        #endregion Properties Region

        /// <summary>
        /// Initializes a new instance of the <see cref="FilmDetails"/>
        /// </summary>
        /// <param name="proId"></param>
        /// <param name="filmDirector"></param>
        /// <param name="filmGenre"></param>
        /// <param name="filmRating"></param>
        /// <param name="filmDurationMins"></param>
        /// <param name="filmReleaseDate"></param>

        public FilmDetails(long proId, string proName, decimal proPrice, DateTime proReleaseDate, long proStock,
            string proCatName, string filmDirector, string filmGenre, long filmRating, long filmDurationMins,
            DateTime filmReleaseDate) : base(proId, proName, proPrice, proReleaseDate, proStock, proCatName)
        {
            this.FilmDirector = filmDirector;
            this.FilmGenre = filmGenre;
            this.FilmRating = filmRating;
            this.FilmDurationMins = filmDurationMins;
            this.FilmReleaseDate = filmReleaseDate;
        }

        public FilmDetails(long proId, string proName, decimal proPrice, long proStock,
            string filmDirector, string filmGenre, long filmRating, long filmDurationMins)
            : base(proId, proName, proPrice, proStock)
        {
            this.FilmDirector = filmDirector;
            this.FilmGenre = filmGenre;
            this.FilmRating = filmRating;
            this.FilmDurationMins = filmDurationMins;
        }

        public override bool Equals(object obj)
        {
            FilmDetails target = (FilmDetails)obj;

            return base.Equals((ProductDetails)obj)
                && (this.FilmDirector == target.FilmDirector)
                && (this.FilmGenre == target.FilmGenre)
                && (this.FilmRating == target.FilmRating)
                && (this.FilmDurationMins == target.FilmDurationMins)
                && (this.FilmReleaseDate == target.FilmReleaseDate);
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
            String strFilmDetails;

            strFilmDetails = base.ToString() +
                "[ filmDirector = " + FilmDirector + " | " +
                "filmGenre = " + FilmGenre + " | " +
                "filmRating = " + FilmRating + " | " +
                "filmDurationMins = " + FilmDurationMins + " | " +
                "filmReleaseDate = " + FilmReleaseDate + " ]";

            return strFilmDetails;
        }

    }
}
