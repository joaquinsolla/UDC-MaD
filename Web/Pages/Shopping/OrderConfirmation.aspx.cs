using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.Services.UserService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Shopping
{
    public partial class OrderConfirmation : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            // Get the Service
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IUserService userService = iocManager.Resolve<IUserService>();

            // Get the user
            UserSession user = (UserSession)SessionManager.GetUserSession(Context);

            // Get the order
            long orderId = (long)Convert.ToDouble(Request.Params.Get("orderId"));
            UserOrderDetails order = userService.FindUserOrderDetails(orderId);

            cellOrderId.Text = order.OrderId.ToString();
            cellOrderDate.Text = order.OrderDate.ToString();
            cellOrderBankCardPAN.Text = order.OrderBankCardPAN.ToString();
            cellOrderPostalAddress.Text = order.OrderPostalAddress;
            cellOrderDescription.Text = order.OrderDescription;
            cellOrderValue.Text = order.OrderValue.ToString() + " €";

        }
    }
}