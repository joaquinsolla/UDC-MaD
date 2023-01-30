using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.Services.CatalogService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Catalog
{
    public partial class AddComment : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAddComment_Click(object sender, EventArgs e)
        {
            try
            {
                // Get the Service
                IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
                ICatalogService catalogService = iocManager.Resolve<ICatalogService>();

                // Get the user
                UserSession user = (UserSession)SessionManager.GetUserSession(Context);
                long proId = Convert.ToInt32(Request.Params.Get("proId"));

                List<string> tagList = new List<string>();

                if (txtAddTag.Text.Trim() != "") 
                {
                    string[] tags = txtAddTag.Text.Trim().Split(',');

                    foreach (string tag in tags)
                    {
                        tagList.Add(tag.Trim());
                    }

                }

                catalogService.AddCommentToProduct(proId, user.UserProfileId, txtCommentText.Text, tagList);

                Response.Redirect("~/Pages/Catalog/ShowProductByProID.aspx" + "?productId=" + proId);

            }
            catch (InstanceNotFoundException)
            {
                lblInternalError.Visible = true;
            }
            catch (Exception)
            {
                lblInternalError.Visible = true;
            }
        }

    }
}