using System;
using System.Collections;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web.HTTP.View.ApplicationObjects
{
    /// <summary>
    /// Summary description for Countries
    /// </summary>
    public class Countries
    {

        /* 
         * In a more realistic application, these values could be read from a 
         * database in the "static" constructor.
         */
        private static readonly ArrayList countries_es = new ArrayList();
        private static readonly ArrayList countries_en = new ArrayList();
        private static readonly ArrayList countries_gl = new ArrayList();
        private static readonly ArrayList countrieCodes = new ArrayList();
        private static readonly Hashtable countries = new Hashtable();


        /* Access modifiers are not allowed on static constructors
         * so if we want to prevent that anybody creates instances 
         * of this class we must do the following ...
         */
        private Countries() { }

        static Countries()
        {
            #region set the countries

            countries_es.Add(new ListItem("España", "ES"));
            countries_es.Add(new ListItem("Estados Unidos", "US"));
            countries_es.Add(new ListItem("Reino Unido", "UK"));

            countries_en.Add(new ListItem("Spain", "ES"));
            countries_en.Add(new ListItem("United Kingdom", "UK"));
            countries_en.Add(new ListItem("United States", "US"));

            countries_gl.Add(new ListItem("España", "ES"));
            countries_gl.Add(new ListItem("Estados Unidos", "US"));
            countries_gl.Add(new ListItem("Reino Unido", "UK"));

            countrieCodes.Add("ES");
            countrieCodes.Add("UK");
            countrieCodes.Add("US");

            countries.Add("es", countries_es);
            countries.Add("en", countries_en);
            countries.Add("gl", countries_gl);

            #endregion

        }

        public static ICollection GetCountryCodes()
        {
            return countrieCodes;
        }

        public static ArrayList GetCountries(String languageCode)
        {
            ArrayList lang = (ArrayList)countries[languageCode];

            if (lang != null)
            {
                return lang;
            }
            else
            {
                return (ArrayList)countries["en"];
            }
        }
    }
}