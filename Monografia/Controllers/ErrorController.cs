using System.Web.Mvc;



namespace Monografia.Controllers
{
    public class ErrorController : Controller
    {

        public ActionResult Error500()
        {

            if (Session["Idusuario"] != null)
            {
                ViewBag.sesionactiva = true;
            } 

                return View();
        }

    }
}
