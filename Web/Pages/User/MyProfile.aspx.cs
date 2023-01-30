using System;
using Es.Udc.DotNet.PracticaMaD.Model.Services.UserService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.User
{
    public partial class MyProfile : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UserProfileDetails userProfileDetails = SessionManager.FindUserProfileDetails(Context);

                cellFirstName.Text = userProfileDetails.FirstName;
                cellLastName.Text = userProfileDetails.LastName;
                cellEmail.Text = userProfileDetails.Email;
                cellPostalAddress.Text = userProfileDetails.PostalAddress;
                cellLanguage.Text = userProfileDetails.Language;
                cellCountry.Text = userProfileDetails.Country;
                cellAdmin.Text = userProfileDetails.Admin.ToString();
            }
        }
    }
}