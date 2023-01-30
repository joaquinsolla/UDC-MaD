using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.Services.CatalogService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Web.Properties;
using System;
using System.Web;
using Es.Udc.DotNet.PracticaMaD.Model.Services.ShoppingService;
using Es.Udc.DotNet.PracticaMaD.Model.Exceptions;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Catalog
{
    public partial class ShowProductsByKeywordAndCategoryName : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int startIndex, count;
            string catName;
            string keyword;
            ProductBlock productBlock;

            lnkPrevious.Visible = false;
            lnkNext.Visible = false;
            lblNoProducts.Visible = false;

            /* Get the Service */
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            ICatalogService catalogService = iocManager.Resolve<ICatalogService>();

            try
            {
                keyword = Request.Params.Get("keyword");
            }
            catch(ArgumentNullException)
            {
                keyword = "";
            }

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

            /* Get Category */
            try
            {
                catName = Request.Params.Get("catName");
            }
            catch (ArgumentNullException)
            {
                catName = "";
            }

            try
            {
                productBlock = catalogService.FindProducts(keyword, catName, startIndex, count);
            }
            catch (InstanceNotFoundException)
            {
                productBlock = catalogService.FindProducts(keyword, startIndex, count);
            }


            if (productBlock.Products.Count == 0)
            {
                lblNoProducts.Visible = true;
                return;
            }

            this.gvProducts.DataSource = productBlock.Products;
            this.gvProducts.DataBind();            

            /* "Previous" link */
            if ((startIndex - count) >= 0)
            {
                String url;

                if (keyword == "")
                {
                    if (catName == "")
                    {
                        url = "~/Pages/Catalog/ShowProductsByKeywordAndCategoryName.aspx" +
                        "?startIndex=" + (startIndex - count) + "&count=" + count;
                    }
                    else
                    {
                        url = "~/Pages/Catalog/ShowProductsByKeywordAndCategoryName.aspx" + "?catName=" + catName +
                        "&startIndex=" + (startIndex - count) + "&count=" + count;
                    }
                }
                else
                {
                    if (catName == "")
                    {
                        url = "~/Pages/Catalog/ShowProductsByKeywordAndCategoryName.aspx" + "?keyword=" + keyword +
                        "&startIndex=" + (startIndex - count) + "&count=" + count;
                    }
                    else
                    {
                        url = "~/Pages/Catalog/ShowProductsByKeywordAndCategoryName.aspx" + "?keyword=" + keyword + "&catName=" + catName +
                        "&startIndex=" + (startIndex - count) + "&count=" + count;
                    }
                }
                

                this.lnkPrevious.NavigateUrl =
                    Response.ApplyAppPathModifier(url);
                this.lnkPrevious.Visible = true;
            }

            /* "Next" link */
            if (productBlock.ExistMoreProducts)
            {
                String url;

                if (keyword == "")
                {
                    if (catName == "")
                    {
                        url = "~/Pages/Catalog/ShowProductsByKeywordAndCategoryName.aspx" +
                        "?startIndex=" + (startIndex + count) + "&count=" + count;
                    }
                    else
                    {
                        url = "~/Pages/Catalog/ShowProductsByKeywordAndCategoryName.aspx" + "?catName=" + catName +
                        "&startIndex=" + (startIndex + count) + "&count=" + count;
                    }
                }
                else
                {
                    if (catName == "")
                    {
                        url = "~/Pages/Catalog/ShowProductsByKeywordAndCategoryName.aspx" + "?keyword=" + keyword +
                        "&startIndex=" + (startIndex + count) + "&count=" + count;
                    }
                    else
                    {
                        url = "~/Pages/Catalog/ShowProductsByKeywordAndCategoryName.aspx" + "?keyword=" + keyword + "&catName=" + catName +
                        "&startIndex=" + (startIndex + count) + "&count=" + count;
                    }
                }

                this.lnkNext.NavigateUrl =
                    Response.ApplyAppPathModifier(url);
                this.lnkNext.Visible = true;
            }
        }

        protected void gvProducts_btnAddToCartClicked(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {

                IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
                IShoppingService shoppingService = iocManager.Resolve<IShoppingService>();
                ICatalogService catalogService = iocManager.Resolve<ICatalogService>();

                ShoppingCart cart = (ShoppingCart)SessionManager.GetShoppingCart(Context);

                long productId = (long)Convert.ToDouble(((Button)sender).CommandArgument.ToString());

                try
                {
                    String productName = catalogService.FindProductDetails(productId).ProName;

                    shoppingService.AddProductToShoppingCart(productId, cart);

                    lblProductAdded.Visible = true;
                    lblProductAddedName.Text = productName;
                    lblProductAddedName.Visible = true;

                    lblInternalError.Visible = false;
                    lblOutOfStock.Visible = false;

                    //Page.Response.Redirect(Page.Request.Url.ToString(), true);
                }
                catch (ProductOutOfStockException)
                {
                    lblOutOfStock.Visible = true;
                    lblInternalError.Visible = false;
                    lblProductAdded.Visible = false;
                    lblProductAddedName.Visible = false;
                }
                catch (InstanceNotFoundException)
                {
                    lblOutOfStock.Visible = false;
                    lblInternalError.Visible = true;
                    lblProductAdded.Visible = false;
                    lblProductAddedName.Visible = false;
                }
                catch (Exception)
                {
                    lblOutOfStock.Visible = false;
                    lblInternalError.Visible = true;
                    lblProductAdded.Visible = false;
                    lblProductAddedName.Visible = false;
                }

            }
        }

    }
}