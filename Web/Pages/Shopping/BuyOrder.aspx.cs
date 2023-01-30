using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.Services.ShoppingService;
using Es.Udc.DotNet.PracticaMaD.Model.Services.UserService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Shopping
{
    public partial class BuyOrder : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            // Get the Service
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IShoppingService shoppingService = iocManager.Resolve<IShoppingService>();
            IUserService userService = iocManager.Resolve<IUserService>();

            // Get the user
            UserSession user = (UserSession)SessionManager.GetUserSession(Context);

            // Get the ShoppingCart 
            ShoppingCart cart = (ShoppingCart)SessionManager.GetShoppingCart(Context);

            // Set the postal address
            this.lblUserAddress.Text = user.PostalAddress;

            // Set the total price
            lblTotalPriceNumber.Text = cart.totalPrice.ToString() + " €";

            if (!IsPostBack)
            {
                // Get the default BankCard
                List<BankCard> allCards = userService.FindUserBankCards(user.UserProfileId);
                BankCard defaultCard = null;

                foreach (BankCard card in allCards)
                {
                    if (card.cardDefault == true)
                    {
                        defaultCard = card;
                        break;
                    }
                }

                if (defaultCard != null)
                {
                    String defaultBankCardPAN = defaultCard.cardPAN.ToString();
                    UpdateComboBankCard(defaultBankCardPAN);
                }
                else 
                {
                    spaces1.Visible = false;
                    spaces2.Visible = false;
                    spaces3.Visible = false;
                    lblChooseOne1.Visible = false;
                    ddlBankCards.Visible = false;
                    lblOr1.Visible = false;
                    UpdateComboBankCard("");
                }
            }

        }

        protected void btnConfirmOrder_Click(object sender, EventArgs e)
        {
            if (Page.IsValid) 
            {
                if (ddlBankCards.SelectedValue == "")
                {
                    lblBankCardMandatory.Visible = true;
                }
                else if (ckbCustomAddress.Checked && txtCustomAddress.Text == "")
                {
                    lblCustomAddressMandatory.Visible = true;
                }
                else if (txtOrderDescription.Text == "")
                {
                    lblOrderDescriptionMandatory.Visible = true;
                }
                else 
                {
                    try
                    {
                        // Get the Service
                        IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
                        IShoppingService shoppingService = iocManager.Resolve<IShoppingService>();
                        IUserService userService = iocManager.Resolve<IUserService>();

                        // Get the user
                        UserSession user = (UserSession)SessionManager.GetUserSession(Context);

                        // Get the ShoppingCart 
                        ShoppingCart cart = (ShoppingCart)SessionManager.GetShoppingCart(Context);

                        String address = user.PostalAddress;
                        if (ckbCustomAddress.Checked) address = txtCustomAddress.Text.Trim();

                        long orderId =  shoppingService.BuyOrder(user.UserProfileId, (long)Convert.ToDouble(ddlBankCards.SelectedValue), 
                                            address, txtOrderDescription.Text.Trim(), cart);

                        SessionManager.EmptyShoppingCart(Context);

                        Response.Redirect("~/Pages/Shopping/OrderConfirmation.aspx?orderId=" + orderId);

                    }
                    catch (ShoppingCartIsEmptyException)
                    {
                        lblInternalError.Visible = true;
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

        protected void ddlBankCards_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.UpdateComboBankCard(ddlBankCards.SelectedValue);
        }

        /// <summary>
        /// Loads the BankCards in the comboBox in the *selectedBankCard*.
        /// Also, the selectedBankCard will appear selected in the
        /// ComboBox
        /// </summary>
        private void UpdateComboBankCard(String selectedBankCard)
        {
            // Get the Service
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IUserService userService = iocManager.Resolve<IUserService>();

            // Get the user
            UserSession user = (UserSession)SessionManager.GetUserSession(Context);

            this.ddlBankCards.DataSource = userService.FindUserBankCards(user.UserProfileId);
            this.ddlBankCards.DataTextField = "cardPAN";
            this.ddlBankCards.DataValueField = "cardPAN";
            this.ddlBankCards.DataBind();
            this.ddlBankCards.SelectedValue = selectedBankCard;
        }

    }
}