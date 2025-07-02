using Monografia.Models;
using System;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace Monografia.Controllers
{
    public class Admin_presentacionController : Controller
    {
        private proyectotiendaEntities db = new proyectotiendaEntities();

        // GET: departamentoes
        public ActionResult Lista_presentaciones()
        {
            return View(db.tipo_presentacion.Where(x => x.Id_presentacion != 1).ToList());
        }

        // GET: departamentoes/Create
        public ActionResult Create()
        {
            return PartialView();
        }

        // POST: departamentoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tipo_presentacion presentacion)
        {
            try
            {
                if (validadinputs(presentacion))
                {
                    if (db.tipo_presentacion.Where(x => x.Descripcion.ToUpper() == presentacion.Descripcion.ToUpper() && x.Estado == 1).FirstOrDefault() == null)
                    {
                        presentacion.Usuario_alta = (string)Session["usuario_logueado"];
                        presentacion.Fecha_alta = DateTime.Now;
                        presentacion.Estado = 1;
                        db.tipo_presentacion.Add(presentacion);
                        db.SaveChanges();
                        return Json(new { success = true, mensaje = "Se ha creado el tipo de presentación satisfactoriamente." });
                    }
                    else
                    {

                        ViewBag.Mensaje = "<i class='bi bi-exclamation-octagon me-1'></i>Ya existe un tipo de presentación con la misma descripción<br>";
                        return PartialView(presentacion);
                    }
                }
                else
                {
                    return PartialView(presentacion);
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        public Boolean validadinputs(tipo_presentacion datospresentacion)
        {
            Boolean valid = true;
            if (datospresentacion.Descripcion == null)
            {
                ViewBag.Mensaje = "<i class='bi bi-exclamation-octagon me-1'></i>Debe ingresar el descripción del tipo presentación<br>";
                valid = false;
            }
            if (datospresentacion.Descripcion != null)
                if (!sololetras(datospresentacion.Descripcion))
                {
                    ViewBag.Mensaje = "<i class='bi bi-exclamation-octagon me-1'></i>Caracter ingresado en descripción del tipo presentación, no esta permitido<br>";
                    valid = false;
                }
            return valid;
        }

        public Boolean sololetras(string datoingresado)
        {
            if (datoingresado.All(c => char.IsLetterOrDigit(c) || c == ' '))
            {
                return true;
            }
            else
            {

                return false;
            }

        }

        // GET: departamentoes/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                tipo_presentacion presentacion = null;
                if (id != 0 && id != null)
                {


                    presentacion = db.tipo_presentacion.Find(id);
                    if (presentacion == null)
                    {
                        ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>No se encontró tipo de presentación";
                        return PartialView(presentacion);
                    }
                    else
                    {
                        return PartialView(presentacion);
                    }

                }
                else
                {
                    ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Id tipo presentación erróneo";
                    return PartialView(presentacion);

                }


            }
            catch (Exception)
            {

                throw;
            }

        }

        // POST: departamentoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tipo_presentacion presentacion)
        {
            try
            {

                if (validadinputs(presentacion))
                {

                    var datospresentacion = (from d in db.tipo_presentacion where d.Id_presentacion == presentacion.Id_presentacion select d).FirstOrDefault();
                    if (datospresentacion == null)
                    {
                        ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>No se encontró tipo presentación";
                        return PartialView(presentacion);
                    }
                    else
                    {
                        if (db.tipo_presentacion.Where(x => x.Descripcion.ToUpper() == presentacion.Descripcion.ToUpper() && x.Estado == 1 && x.Id_presentacion != presentacion.Id_presentacion).FirstOrDefault() == null)
                        {
                            datospresentacion.Descripcion = presentacion.Descripcion;
                            db.SaveChanges();
                            return Json(new { success = true, mensaje = "Se ha actualizado la información del tipo de presentación satisfactoriamente." });
                        }
                        else
                        {
                            ViewBag.Mensaje = "<i class='bi bi-exclamation-octagon me-1'></i>Ya existe un tipo de presentación con la misma descripción<br>";
                            return PartialView(presentacion);
                        }
                    }
                }
                else
                {

                    return PartialView(presentacion);
                }

            }
            catch (Exception)
            {

                throw;
            }

        }

        // GET: departamentoes/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {
                tipo_presentacion presentacion = null;
                if (id != 0 && id != null)
                {
                    presentacion = db.tipo_presentacion.Find(id);

                    if (presentacion != null)
                    {
                        return PartialView(presentacion);
                    }
                    else
                    {
                        ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>No se encontró tipo presentación";
                        return PartialView(presentacion);
                    }
                }
                else
                {
                    ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Id de tipo presentación erróneo";
                    return PartialView(presentacion);
                }


            }
            catch (Exception)
            {

                throw;
            }

        }

        // POST: departamentoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            try
            {

                tipo_presentacion presentacion = null;
                if (id != 0 && id != null)
                {
                    presentacion = db.tipo_presentacion.Where(x => x.Id_presentacion == id).FirstOrDefault();

                    if (presentacion != null)
                    {

                        presentacion.Fecha_baja = DateTime.Now;
                        presentacion.Usuario_baja = (string)Session["usuario_logueado"];
                        presentacion.Estado = 2;
                        db.SaveChanges();
                        return Json(new { success = true, mensaje = "Se ha inactivado el tipo de presentación satisfactoriamente." });
                    }
                    else
                    {
                        ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>No se encontró tipo presentación";
                        return PartialView(presentacion);
                    }
                }
                else
                {
                    ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Id de tipo presentación erróneo";
                    return PartialView(presentacion);
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

