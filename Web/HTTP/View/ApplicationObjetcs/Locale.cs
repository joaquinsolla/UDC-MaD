namespace Es.Udc.DotNet.PracticaMaD.Web.HTTP.View.ApplicationObjects
{

    public struct Locale
    {

        private string country;
        private string language;

        public Locale(string language, string country) : this()
        {
            this.language = language;
            this.country = country;
        }

        #region Properties

        public string Language
        {
            get { return language; }
            set { language = value; }
        }


        public string Country
        {
            get { return country; }
            set { country = value; }
        }

        #endregion

    }
}
