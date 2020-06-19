using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace WebAPI_2
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ////https://docs.microsoft.com/en-us/aspnet/signalr/overview/guide-to-the-api/handling-connection-lifetime-events
            //// Make long polling connections wait a maximum of 110 seconds for a
            //// response. When that time expires, trigger a timeout command and
            //// make the client reconnect.
            //GlobalHost.Configuration.ConnectionTimeout = TimeSpan.FromSeconds(30);

            //// Wait a maximum of 30 seconds after a transport connection is lost
            //// before raising the Disconnected event to terminate the SignalR connection.
            //GlobalHost.Configuration.DisconnectTimeout = TimeSpan.FromSeconds(20);

            //// For transports other than long polling, send a keepalive packet every
            //// 10 seconds. 
            //// This value must be no more than 1/3 of the DisconnectTimeout value.
            //GlobalHost.Configuration.KeepAlive = TimeSpan.FromSeconds(10);

            //RouteTable.Routes.MapHubs();
        }
    }
}
