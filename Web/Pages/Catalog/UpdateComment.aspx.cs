using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.Services.CatalogService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.View.ApplicationObjects;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Catalog
{
    public partial class UpdateComment : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Get the Service
                IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
                ICatalogService catalogService = iocManager.Resolve<ICatalogService>();

                long commentId = Convert.ToInt32(Request.Params.Get("commentId"));

                Comment comment = catalogService.FindCommentById(commentId);

                String stringTags = "";
                int i = 1;
                foreach(Tag tag in comment.Tags) {
                    stringTags += tag.tagName;
                    if (i < comment.Tags.Count)
                    {
                        stringTags += ", ";
                        i++;
                    }
                }

                txtCommentText.Text = comment.commentText;
                txtNewTag.Text = stringTags;
            }
            
        }

        protected void btnUpdateComment_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    // Get the Service
                    IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
                    ICatalogService catalogService = iocManager.Resolve<ICatalogService>();

                    // Get the user
                    UserSession user = (UserSession)SessionManager.GetUserSession(Context);
                    long commentId = Convert.ToInt32(Request.Params.Get("commentId"));

                    List<string> tagList = new List<string>();

                    if (txtNewTag.Text.Trim() != "")
                    {
                        string[] tags = txtNewTag.Text.Trim().Split(',');

                        foreach (string tag in tags)
                        {
                            tagList.Add(tag.Trim());
                        }

                    }

                    catalogService.UpdateComment(commentId, user.UserProfileId, txtCommentText.Text, tagList);

                    Response.Redirect("~/Pages/Catalog/TagCloud.aspx");

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
}