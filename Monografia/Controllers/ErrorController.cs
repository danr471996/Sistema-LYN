using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using Monografia.Models;
using System.Web.Security;



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
