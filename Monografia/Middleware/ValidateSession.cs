using System.Web;
using System.Web.Mvc;

namespace Monografia.Middleware
{
    public class ValidateSession : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {

            if (HttpContext.Current.Session["TokenActive"]==null)
            {
                RedirigirLogin(context);
            }
        }

        private void RedirigirLogin(ActionExecutingContext context)
        {
            context.Result = new RedirectToRouteResult(
                new System.Web.Routing.RouteValueDictionary
                {
            { "controller", "Usuariologin" },
            { "action", "Login" }
                });
        }

    }
}