using Es.Udc.DotNet.PracticaMaD.HTTP.Util.IoC;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.ModelUtil.Log;
using Ninject;
using System;

namespace Es.Udc.DotNet.PracticaMaD.Web
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            Application.Lock();

            IIoCManager IoCManager = new IoCManagerNinject();
            IoCManager.Configure();

            Application["managerIoC"] = IoCManager;
            LogManager.RecordMessage("NInject kernel container started", MessageType.Info);

            Application.UnLock();
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            SessionManager.TouchSession(Context);
            SessionManager.CreateShoppingCart(Context);
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
        }

        protected void Application_Error(object sender, EventArgs e)
        {
        }

        protected void Session_End(object sender, EventArgs e)
        {
        }

        protected void Application_End(object sender, EventArgs e)
        {
            ((IKernel)Application["kernelIoC"]).Dispose();

            LogManager.RecordMessage("NInject kernel container disposed", MessageType.Info);
        }
    }
}