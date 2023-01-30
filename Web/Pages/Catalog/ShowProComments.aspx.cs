using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.ModelUtil.IoC;
using System;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.PracticaMaD.Model.Services.CatalogService;
using Es.Udc.DotNet.PracticaMaD.Web.Properties;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages
{
    public partial class ShowProComments : SpecificCulturePage
    {
        private ObjectDataSource pbpDataSource = new ObjectDataSource();

        protected void Page_Load(object sender, EventArgs e)
        {
            int startIndex, count;
            CommentBlock commentBlock;

            lnkPrevious.Visible = false;
            lnkNext.Visible = false;
            lblNoComments.Visible = false;

            /* Get the Service */
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            ICatalogService catalogService = iocManager.Resolve<ICatalogService>();

            UserSession session = SessionManager.GetUserSession(Context);
            long userId;
            try
            {
                userId = session.UserProfileId;
            }
            catch (NullReferenceException)
            {
                userId = -1;
            }

            long proId = Convert.ToInt32(Request.Params.Get("proId"));

            /* Get Start Index */
            try
            {
                startIndex = Int32.Parse(Request.Params.Get("startIndex"));
            }
            catch (ArgumentNullException)
            {
                startIndex = 0;
            }

            /* Get Count */
            try
            {
                count = Int32.Parse(Request.Params.Get("count"));
            }
            catch (ArgumentNullException)
            {
                count = Settings.Default.PracticaMaD_defaultCount;
            }

            commentBlock = catalogService.FindCommentsByProduct(proId, startIndex, count);

            if (commentBlock.Comments.Count == 0)
            {
                lblNoComments.Visible = true;
                return;
            }

            this.gvProComments.DataSource = commentBlock.Comments;
            this.gvProComments.DataBind();

            foreach (GridViewRow row in gvProComments.Rows)
            {
                String userIdRow = this.gvProComments.DataKeys[row.RowIndex]["usrId"].ToString();
                if (userId != -1 && userIdRow == userId.ToString())
                    ((Button)row.FindControl("btnUpdateComment")).Visible = true;
            }

            /* "Previous" link */
            if ((startIndex - count) >= 0)
            {
                String url = "~/Pages/Catalog/ShowProComments.aspx" + "?proId=" + proId +
                    "&startIndex=" + (startIndex - count) + "&count=" + count;

                this.lnkPrevious.NavigateUrl =
                    Response.ApplyAppPathModifier(url);
                this.lnkPrevious.Visible = true;
            }

            /* "Next" link */
            if (commentBlock.ExistMoreComments)
            {
                String url = "~/Pages/Catalog/ShowProComments.aspx" + "?proId=" + proId +
                    "&startIndex=" + (startIndex + count) + "&count=" + count;

                this.lnkNext.NavigateUrl =
                    Response.ApplyAppPathModifier(url);
                this.lnkNext.Visible = true;
            }
        }

        protected void gvProducts_btnUpdateCommentClicked(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                long commentId = (long)Convert.ToDouble(((Button)sender).CommandArgument.ToString());
                String url = "~/Pages/Catalog/UpdateComment.aspx" + "?commentId=" + commentId;
                Response.Redirect(Response.ApplyAppPathModifier(url));
            }
        }
    }
}