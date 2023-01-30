using System;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.PracticaMaD.Model.Exceptions;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.User
{
    public partial class ChangePassword : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblOldPasswordError.Visible = false;
        }

        /// <summary>
        /// Handles the Click event of the btnChangePassword control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance 
        /// containing the event data.</param>
        protected void BtnChangePasswordClick(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    SessionManager.ChangePassword(Context, txtOldPassword.Text, txtNewPassword.Text);
                    Response.Redirect(Response.ApplyAppPathModifier("~/Pages/User/MyProfile.aspx"));
                }
                catch (IncorrectPasswordException)
                {
                    lblOldPasswordError.Visible = true;
                }
            }
        }
    }
}