using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.Services.UserService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.User
{
    public partial class AddBankCard : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Get the Service
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IUserService userService = iocManager.Resolve<IUserService>();

            if (!IsPostBack)
            {
                // Get the first BankCardType
                List<BankCardType> allTypes = userService.FindAllBankCardTypes();
                BankCardType firstType = allTypes.First();

                String firstBankCardType = firstType.typeName;
                UpdateComboBankCardType(firstBankCardType);

            }
        }

        /// <summary>
        /// Loads the BankCardTypes in the comboBox in the *selectedType*.
        /// Also, the selectedType will appear selected in the
        /// ComboBox
        /// </summary>
        private void UpdateComboBankCardType(String selectedType)
        {
            // Get the Service
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IUserService userService = iocManager.Resolve<IUserService>();

            // Get the user
            UserSession user = (UserSession)SessionManager.GetUserSession(Context);

            this.ddlCardType.DataSource = userService.FindAllBankCardTypes();
            this.ddlCardType.DataTextField = "typeName";
            this.ddlCardType.DataValueField = "typeId";
            this.ddlCardType.DataBind();
            this.ddlCardType.SelectedValue = selectedType;
        }

        protected void ddlCardType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.UpdateComboBankCardType(ddlCardType.SelectedValue);
        }

        protected void btnAddCard_Click(object sender, EventArgs e)
        {
            if (cdrCardExpirationDate.SelectedDate == new DateTime(0001, 01, 01))
            {
                lblCardExpirationDateMandatory.Visible = true;
            }
            else if (Page.IsValid)
            {
                try
                {
                    // Get the Service
                    IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
                    IUserService userService = iocManager.Resolve<IUserService>();

                    // Get the user
                    UserSession user = (UserSession)SessionManager.GetUserSession(Context);

                    BankCardDetails cardDetails = new BankCardDetails(
                        (long)Convert.ToDouble(ddlCardType.SelectedValue), Int32.Parse(txtCardCVV.Text.Trim()), cdrCardExpirationDate.SelectedDate,
                        ckbCardDefault.Checked, user.UserProfileId);

                    userService.AddBankCard(user.UserProfileId, (long)Convert.ToDouble(txtCardPAN.Text.Trim()), cardDetails);

                    Response.Redirect("~/Pages/User/MyProfile.aspx");
                }
                catch (DuplicateInstanceException)
                {
                    lblCardAlreadyOwned.Visible = true;
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