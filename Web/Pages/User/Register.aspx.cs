using System;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.PracticaMaD.Model.Services.UserService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.View.ApplicationObjects;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Log;
using System.Globalization;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.User
{
    public partial class Register : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblUsernameNotAvaliable.Visible = false;

            if (!IsPostBack)
            {
                String defaultLanguage = GetLanguageFromBrowserPreferences();
                String defaultCountry = GetCountryFromBrowserPreferences();

                UpdateComboLanguage(defaultLanguage);
                UpdateComboCountry(defaultLanguage, defaultCountry);
            }
        }

        private String GetLanguageFromBrowserPreferences()
        {
            String language;
            CultureInfo cultureInfo = CultureInfo.CreateSpecificCulture(Request.UserLanguages[0]);
            language = cultureInfo.TwoLetterISOLanguageName;
            LogManager.RecordMessage("Preferred language of user (based on browser preferences): " + language);
            return language;
        }

        private String GetCountryFromBrowserPreferences()
        {
            String country;
            CultureInfo cultureInfo = CultureInfo.CreateSpecificCulture(Request.UserLanguages[0]);

            if (cultureInfo.IsNeutralCulture)
            {
                country = "";
            }
            else
            {
                String cultureInfoName = cultureInfo.Name;
                country = cultureInfoName.Substring(cultureInfoName.Length - 2);
                LogManager.RecordMessage("Preferred region/country of user (based on browser preferences): " + country);
            }
            return country;
        }

        /// <summary>
        /// Loads the languages in the comboBox in the *selectedLanguage*.
        /// Also, the selectedLanguage will appear selected in the
        /// ComboBox
        /// </summary>
        private void UpdateComboLanguage(String selectedLanguage)
        {
            this.comboLanguage.DataSource = Languages.GetLanguages(selectedLanguage);
            this.comboLanguage.DataTextField = "text";
            this.comboLanguage.DataValueField = "value";
            this.comboLanguage.DataBind();
            this.comboLanguage.SelectedValue = selectedLanguage;
        }

        /// <summary>
        /// Loads the countries in the comboBox in the *selectedLanguage*.
        /// Also, the *selectedCountry* will appear selected in the
        /// ComboBox
        /// </summary>
        private void UpdateComboCountry(String selectedLanguage, String selectedCountry)
        {
            this.comboCountry.DataSource = Countries.GetCountries(selectedLanguage);
            this.comboCountry.DataTextField = "text";
            this.comboCountry.DataValueField = "value";
            this.comboCountry.DataBind();
            this.comboCountry.SelectedValue = selectedCountry;
        }

        /// <summary>
        /// Handles the Click event of the btnRegister control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance
        /// containing the event data.</param>
        protected void BtnRegisterClick(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    UserProfileDetails userProfileDetailsVO =
                        new UserProfileDetails(txtFirstName.Text, txtLastName.Text, txtEmail.Text, txtPostalAddress.Text,
                        comboLanguage.SelectedValue, comboCountry.SelectedValue, false); //variable admin no tengo claro si deberia ser false

                    SessionManager.RegisterUser(Context, txtLogin.Text, txtPassword.Text, userProfileDetailsVO); //falta carrito por añadir

                    Response.Redirect(Response.ApplyAppPathModifier("~/Pages/MainPage.aspx"));
                }
                catch (DuplicateInstanceException)
                {
                    lblUsernameNotAvaliable.Visible = true;
                }
            }
        }

        protected void ComboLanguageSelectedIndexChanged(object sender, EventArgs e)
        {
            this.UpdateComboCountry(comboLanguage.SelectedValue,
                comboCountry.SelectedValue);
        }
    }
}