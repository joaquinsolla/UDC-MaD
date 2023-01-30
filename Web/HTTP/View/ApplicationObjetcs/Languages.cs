using System;
using System.Collections;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web.HTTP.View.ApplicationObjects
{
    /// <summary>
    /// Load the languages...
    /// </summary>
    public class Languages
    {
        /* 
         * In a more realistic application, these values could be read from a 
         * database in the "static" contructor.
         */
        private static readonly ArrayList languages_es = new ArrayList();
        private static readonly ArrayList languages_en = new ArrayList();
        private static readonly ArrayList languages_gl = new ArrayList();
        private static readonly Hashtable languages = new Hashtable();

        /* Access modifiers are not allowed on static constructors
         * so if we want to prevent that anybody creates instances 
         * of this class we must do the following ...
         */
        private Languages() { }

        static Languages()
        {
            #region set the languages

            languages_es.Add(new ListItem("Español", "es"));
            languages_es.Add(new ListItem("Gallego", "gl"));
            languages_es.Add(new ListItem("Inglés", "en"));

            languages_en.Add(new ListItem("English", "en"));
            languages_en.Add(new ListItem("Galician", "gl"));
            languages_en.Add(new ListItem("Spanish", "es"));

            languages_gl.Add(new ListItem("Español", "es"));
            languages_gl.Add(new ListItem("Galego", "gl"));
            languages_gl.Add(new ListItem("Inglés", "en"));

            languages.Add("es", languages_es);
            languages.Add("en", languages_en);
            languages.Add("gl", languages_gl);

            #endregion
        }

        public static ICollection GetLaguageCodes()
        {
            return languages.Keys;
        }

        public static ArrayList GetLanguages(String languageCode)
        {
            ArrayList lang = (ArrayList)languages[languageCode];

            if (lang != null)
            {
                return lang;
            }
            else
            {
                return (ArrayList)languages["en"];
            }
        }
    }
}