using System;
using System.Web;
using Es.Udc.DotNet.PracticaMaD.Model.Services.UserService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Web.Properties;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.User
{
    public partial class MyOrders : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            int startIndex, count;
            OrderBlock orderBlock;

            lnkPrevious.Visible = false;
            lnkNext.Visible = false;
            lblNoOrders.Visible = false;

            UserSession session = SessionManager.GetUserSession(Context);
            long userId = session.UserProfileId;

            //Get the Service
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IUserService userService = iocManager.Resolve<IUserService>();

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

            orderBlock = userService.FindUserOrders(userId, startIndex, count);
 

            if(orderBlock.Orders.Count == 0)
            {
                lblNoOrders.Visible = true;
                return;
            }

            this.gvOrders.DataSource = orderBlock.Orders;
            this.gvOrders.DataBind();

            /* "Previous" link */
            if ((startIndex - count) >= 0)
            {
                String url;

                url = "~/Pages/User/MyOrders.aspx" +
                    "?startIndex=" + (startIndex - count) + "&count=" + count;     
                
                this.lnkPrevious.NavigateUrl = Response.ApplyAppPathModifier(url);
                this.lnkPrevious.Visible = true;
            }

            /* "Next" link */
            if (orderBlock.ExistMoreOrders)
            {
                String url;

                url = "~/Pages/User/MyOrders.aspx" +
                    "?startIndex=" + (startIndex + count) + "&count=" + count;

                this.lnkNext.NavigateUrl = Response.ApplyAppPathModifier(url);
                this.lnkNext.Visible = true;
            }
        }
    }
}