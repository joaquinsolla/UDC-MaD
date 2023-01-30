using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.Services.ShoppingService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Shopping
{
    public partial class ShoppingCartDetails : SpecificCulturePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            // Get the Service
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IShoppingService shoppingService = iocManager.Resolve<IShoppingService>();

            // Get the ShoppingCart 
            ShoppingCart cart = (ShoppingCart)SessionManager.GetShoppingCart(Context);

            if (shoppingService.ShoppingCartIsEmpty(cart))
            {
                gvwShoppingCartDetails.Visible = false;
                lblTotalPriceTitle.Visible = false;
                lblTotalPriceNumber.Visible = false;
                btnBuyOrder.Visible = false;
            }
            else
            {
                lblEmptyShoppingCart.Visible = false;
                lblTotalPriceNumber.Text = cart.totalPrice.ToString() + " €";
                gvwShoppingCartDetails.DataSource = cart.cartLines;
                gvwShoppingCartDetails.DataBind();

                // Manage GridView Quantity Buttons and Gift CheckBox
                foreach (GridViewRow row in gvwShoppingCartDetails.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        Button addOneButton = row.FindControl("btnAddOne") as Button;
                        Button removeOneButton = row.FindControl("btnRemoveOne") as Button;
                        Button giftButton = row.FindControl("btnGift") as Button;
                        Label lblNo = row.FindControl("lblNo") as Label;
                        Label lblYes = row.FindControl("lblYes") as Label;
                        if (cart.cartLines[row.RowIndex].product.proStock < 1)
                        {
                            addOneButton.Enabled = false;
                        }
                        if (cart.cartLines[row.RowIndex].quantity < 2)
                        {
                            removeOneButton.Enabled = false;
                        }
                        if (cart.cartLines[row.RowIndex].gift == true)
                        {
                            giftButton.Text = "✓";
                        }
                        else
                        {
                            giftButton.Text = "✗";
                        }
                    }
                }

            }
        }

        protected void gvwShoppingCartDetails_btnRemoveOneClicked(object sender, EventArgs e)
        {

            if (Page.IsValid)
            {

                // Get the Service
                IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
                IShoppingService shoppingService = iocManager.Resolve<IShoppingService>();

                // Get the ShoppingCart 
                ShoppingCart cart = (ShoppingCart)SessionManager.GetShoppingCart(Context);

                Button btn = (Button)sender;
                GridViewRow row = (GridViewRow)btn.NamingContainer;

                try
                {
                    shoppingService.RemoveOneFromShoppingCart(cart.cartLines[row.RowIndex].product.proId, cart);

                    Page.Response.Redirect(Page.Request.Url.ToString(), true);
                }
                catch (NotInShoppingCartException)
                {
                    lblInternalError.Visible = true;
                }
                catch (InstanceNotFoundException)
                {
                    lblInternalError.Visible = true;
                }

            }

        }

        protected void gvwShoppingCartDetails_btnAddOneClicked(object sender, EventArgs e)
        {

            if (Page.IsValid)
            {

                // Get the Service
                IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
                IShoppingService shoppingService = iocManager.Resolve<IShoppingService>();

                // Get the ShoppingCart 
                ShoppingCart cart = (ShoppingCart)SessionManager.GetShoppingCart(Context);

                Button btn = (Button)sender;
                GridViewRow row = (GridViewRow)btn.NamingContainer;

                try
                {
                    shoppingService.AddOneToShoppingCart(cart.cartLines[row.RowIndex].product.proId, cart);

                    Page.Response.Redirect(Page.Request.Url.ToString(), true);
                }
                catch (ProductOutOfStockException) 
                {
                    lblInternalError.Visible = true;
                }
                catch (NotInShoppingCartException)
                {
                    lblInternalError.Visible = true;
                }
                catch (InstanceNotFoundException)
                {
                    lblInternalError.Visible = true;
                }

            }

        }

        protected void gvwShoppingCartDetails_btnRemoveClicked(object sender, EventArgs e)
        {

            if (Page.IsValid) {
                
                // Get the Service
                IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
                IShoppingService shoppingService = iocManager.Resolve<IShoppingService>();

                // Get the ShoppingCart 
                ShoppingCart cart = (ShoppingCart)SessionManager.GetShoppingCart(Context);

                Button btn = (Button)sender;
                GridViewRow row = (GridViewRow)btn.NamingContainer;

                try
                {
                    shoppingService.DeleteProductFromShoppingCart(cart.cartLines[row.RowIndex].product.proId, cart);

                    Page.Response.Redirect(Page.Request.Url.ToString(), true);
                }
                catch (NotInShoppingCartException)
                {
                    lblInternalError.Visible = true;
                }
                catch (InstanceNotFoundException)
                {
                    lblInternalError.Visible = true;
                }

            }

        }

        protected void gvwShoppingCartDetails_btnGiftClicked(object sender, EventArgs e)
        {

            if (Page.IsValid)
            {

                // Get the Service
                IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
                IShoppingService shoppingService = iocManager.Resolve<IShoppingService>();

                // Get the ShoppingCart 
                ShoppingCart cart = (ShoppingCart)SessionManager.GetShoppingCart(Context);

                Button button = (Button)sender;
                GridViewRow row = (GridViewRow)button.NamingContainer;

                try
                {
                    shoppingService.ToggleGiftAtShoppingCart(cart.cartLines[row.RowIndex].product.proId, cart);

                    Page.Response.Redirect(Page.Request.Url.ToString(), true);
                }
                catch (NotInShoppingCartException)
                {
                    lblInternalError.Visible = true;
                }
                catch (InstanceNotFoundException)
                {
                    lblInternalError.Visible = true;
                }

            }

        }

        protected void btnBuyOrder_Click(object sender, EventArgs e)
        {

            Response.Redirect("~/Pages/Shopping/BuyOrder.aspx");

        }
    }

}