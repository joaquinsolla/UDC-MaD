using Es.Udc.DotNet.PracticaMaD.Web.HTTP.View.ApplicationObjects;
using Es.Udc.DotNet.ModelUtil.Log;
using System;
using System.Globalization;
using System.Threading;
using System.Web.UI;

namespace Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session
{
    /// <summary>
    /// This class selects the cultural preferences of the Web application
    /// </summary>
    public class SpecificCulturePage : Page
    {
        /// <summary>
        /// Initializes the cultural preferences
        /// </summary>
        protected override void InitializeCulture()
        {
            /* If the user had selected a especific language (from an
             * application option of from a stored profile) the Web application
             * will use the culture selected by the user. Otherwise, the
             * cultural preferences established in the Web browser will be
             * used.
             * If *Locale* is defined in the session, then we use that locale
             * to override the Culture of the application. Otherwise, we will
             * not do anything and the framework will behave based on
             * configuration on Web.config or page level
             */
            if (SessionManager.IsUserAuthenticated(Context))
            {
                Locale locale = SessionManager.GetLocale(Context);

                String culture = locale.Language + "-" + locale.Country;
                CultureInfo cultureInfo;

                /*
                 * The method CreateSpecificCulture will try to create a
                 * specific culture based on the combination selected by the
                 * user (i.e. <laguageCode2>-<country/regionCode2>). If the
                 * combination is not a valid culture, it will create a
                 * specific culture using 1) the languague and 2) the default
                 * region for that language. For example, if user selects
                 * gl-UK (wich is not a valid culture), an gl-ES specific
                 * culture will be created
                 */
                try
                {
                    cultureInfo = new CultureInfo(culture);

                    LogManager.RecordMessage("Specific culture created: " + cultureInfo.Name, MessageType.Info);
                }
                /*
                 * If any error occurs we will create a default culture
                 * "en-US". This exception should never happen.
                 */
                catch (ArgumentException)
                {
                    cultureInfo = CultureInfo.CreateSpecificCulture("en-US");
                    LogManager.RecordMessage("Default Specific culture created: " + cultureInfo.Name, MessageType.Info);
                }

                Thread.CurrentThread.CurrentCulture = cultureInfo;
                Thread.CurrentThread.CurrentUICulture = cultureInfo;
            }
        }
    }
}