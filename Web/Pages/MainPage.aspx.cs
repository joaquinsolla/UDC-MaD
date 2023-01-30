using System;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages
{
    public partial class MainPage : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UserSession session = SessionManager.GetUserSession(Context);

            if (SessionManager.IsUserAuthenticated(Context))
            {
                lblNotAuthenticated.Visible = false;
                lblFirstName.Text = session.FirstName + " " + session.LastName;
            }
            else
            {
                lblAuthenticated.Visible = false;
                lblFirstName.Visible = false;
            }
        }
    }
}