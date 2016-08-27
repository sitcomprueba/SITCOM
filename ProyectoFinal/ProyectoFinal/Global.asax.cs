using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using BussinesEntities;

namespace ProyectoFinal
{
    public class MvcApplication : System.Web.HttpApplication
    {

        protected void Session_Start(Object sender, EventArgs e)
        {
            HttpContext.Current.Session["User"] = new UsuarioEntity();
        }
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
