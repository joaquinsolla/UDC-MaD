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
    public class MusicDetails : ProductDetails
    {

        #region Properties Region

        public string MusicArtist { get; private set; }
        public string MusicAlbum { get; private set; }
        public long MusicSongs { get; private set; }
        public long MusicDurationMins { get; private set; }
        public DateTime MusicReleaseDate { get; private set; }

        #endregion Properties Region

        /// <summary>
        /// Initializes a new instance of the <see cref="MusicDetails"/>
        /// </summary>
        /// <param name="proId"></param>
        /// <param name="musicArtist"></param>
        /// <param name="musicAlbum"></param>
        /// <param name="musicSongs"></param>
        /// <param name="musicDurationMins"></param>
        /// <param name="musicReleaseDate"></param>

        public MusicDetails(long proId, string proName, decimal proPrice, DateTime proReleaseDate, long proStock,
            string proCatName, string musicArtist, string musicAlbum, long musicSongs, long musicDurationMins,
            DateTime musicReleaseDate) : base(proId, proName, proPrice, proReleaseDate, proStock, proCatName)
        {
            this.MusicArtist = musicArtist;
            this.MusicAlbum = musicAlbum;
            this.MusicSongs = musicSongs;
            this.MusicDurationMins = musicDurationMins;
            this.MusicReleaseDate = musicReleaseDate;
        }

        public MusicDetails(long proId, string proName, decimal proPrice, long proStock,
            string musicArtist, string musicAlbum, long musicSongs, long musicDurationMins)
            : base(proId, proName, proPrice, proStock)
        {
            this.MusicArtist = musicArtist;
            this.MusicAlbum = musicAlbum;
            this.MusicSongs = musicSongs;
            this.MusicDurationMins = musicDurationMins;
        }

        public override bool Equals(object obj)
        {
            MusicDetails target = (MusicDetails)obj;

            return base.Equals((ProductDetails)obj)
                && (this.MusicArtist == target.MusicArtist)
                && (this.MusicAlbum == target.MusicAlbum)
                && (this.MusicSongs == target.MusicSongs)
                && (this.MusicDurationMins == target.MusicDurationMins)
                && (this.MusicReleaseDate == target.MusicReleaseDate);
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
                "[ musicArtist = " + MusicArtist + " | " +
                "musicAlbum = " + MusicAlbum + " | " +
                "musicSongs = " + MusicSongs + " | " +
                "musicDurationMins = " + MusicDurationMins + " | " +
                "musicReleaseDate = " + MusicReleaseDate + " ]";

            return strMusicDetails;
        }
    }
}