using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.Services.CatalogService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;

namespace Es.Udc.DotNet.PracticaMaD.Web
{
    public partial class PracticaMaD : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (SessionManager.IsUserAuthenticated(Context))
            {
                lnkAuthentication.Visible = false;
            }
            else 
            {
                lnkMyProfile.Visible = false;
                lnkMyOrders.Visible = false;
                lnkLogout.Visible = false;
            }
        }
    }
}