using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace salesAPI
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
            RouteTable.Routes.MapHttpRoute("ActionApi", "api/{controller}/{action}/{username},{password}", new { username = RouteParameter.Optional, password = RouteParameter.Optional });
            RouteTable.Routes.MapHttpRoute("ActionManifestApi", "api/{controller}/{action}/{Id}/{val}/{flag}", new { Id = RouteParameter.Optional, val = RouteParameter.Optional, flag = RouteParameter.Optional });
            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
        }
    }
}
