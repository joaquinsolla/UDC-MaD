using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.Services.CatalogService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.IoC;
using System;
using System.Web;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages
{
    public partial class FindProducts : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblIdentifierError.Visible = false;

            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            ICatalogService catalogService = iocManager.Resolve<ICatalogService>();

            List<Category> categories = catalogService.FindAllCategories();

            ddlCategory.Items.Add(new ListItem("-", "-"));
            foreach (Category category in categories)
                ddlCategory.Items.Add(new ListItem(category.catName, category.catName));
        }

        protected void BtnFindClick(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                String keyword = this.txtIdentifier.Text;
                String catName = this.ddlCategory.SelectedValue;
                String url;

                // FALTA AÑADIR PAGINACIÓN (quizá no sea necesario)
                if (keyword == "" || keyword == null)
                {
                    if (catName == "-")
                        url = "~/Pages/Catalog/ShowProductsByKeywordAndCategoryName.aspx";
                    else
                        url = String.Format("~/Pages/Catalog/ShowProductsByKeywordAndCategoryName.aspx?catName={1}", keyword, catName);
                }
                else
                {
                    if (catName == "-")
                        url = String.Format("~/Pages/Catalog/ShowProductsByKeywordAndCategoryName.aspx?keyword={0}", keyword);
                    else
                        url = String.Format("~/Pages/Catalog/ShowProductsByKeywordAndCategoryName.aspx?keyword={0}&catName={1}", keyword, catName);
                }

                Response.Redirect(Response.ApplyAppPathModifier(url));
            }
        }
    }
}