using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Monografia
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

       protected void Application_Error(object sender, EventArgs e)
        {
            // Obtiene la última excepción ocurrida
            Exception exception = Server.GetLastError();

            // Limpiar el error del servidor para evitar que ASP.NET muestre la página de error predeterminada
            Server.ClearError();

            // Redirige a la página de error personalizada
            Response.Redirect("~/Error/Error500");
        }
    }
}
