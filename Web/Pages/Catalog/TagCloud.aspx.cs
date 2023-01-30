using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.ModelUtil.IoC;
using System;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.PracticaMaD.Model.Services.CatalogService;
using Es.Udc.DotNet.PracticaMaD.Web.Properties;
using System.Collections.Generic;
using Es.Udc.DotNet.PracticaMaD.Model;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Catalog
{
    public partial class TagCloud : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            ICatalogService catalogService = iocManager.Resolve<ICatalogService>();

            List<Tag> tags = catalogService.FindAllTags();

            foreach (Tag tag in tags)
                ddlTags.Items.Add(new ListItem(tag.tagName, tag.tagName));
        }

        protected void BtnFindClick(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                String tagName = this.ddlTags.SelectedValue;
                String url = String.Format("~/Pages/Catalog/ShowTagComments.aspx?tagName={0}", tagName);

                Response.Redirect(Response.ApplyAppPathModifier(url));
            }
        }
    }
}