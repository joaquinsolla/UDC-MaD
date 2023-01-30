using System;
using System.Web.UI;
using Es.Udc.DotNet.PracticaMaD.Model.Services.UserService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.View.ApplicationObjects;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.User
{
    public partial class UpdateUserProfile : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                UserProfileDetails userProfileDetails = SessionManager.FindUserProfileDetails(Context);

                txtFirstName.Text = userProfileDetails.FirstName;
                txtLastName.Text = userProfileDetails.LastName;
                txtEmail.Text = userProfileDetails.Email;
                txtPostalAddress.Text = userProfileDetails.PostalAddress;
                txtAdmin.Text = userProfileDetails.Admin.ToString();

                UpdateComboLanguage(userProfileDetails.Language);
                UpdateComboCountry(userProfileDetails.Language, userProfileDetails.Country);
            }
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
        /// Handles the Click event of the btnUpdate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance 
        /// containing the event data.</param>
        protected void BtnUpdateClick(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                UserSession session = SessionManager.GetUserSession(Context);
                bool userAdmin = session.Admin;

                UserProfileDetails userProfileDetails = new UserProfileDetails(txtFirstName.Text, txtLastName.Text,
                    txtEmail.Text, txtPostalAddress.Text, comboLanguage.SelectedValue, comboCountry.SelectedValue, userAdmin);

                SessionManager.UpdateUserProfileDetails(Context, userProfileDetails);

                Response.Redirect(Response.ApplyAppPathModifier(("~/Pages/User/MyProfile.aspx")));
            }
        }

        protected void ComboLanguageSelectedIndexChanged(object sender, EventArgs e)
        {
            this.UpdateComboCountry(comboLanguage.SelectedValue,
                comboCountry.SelectedValue);
        }
    }
}