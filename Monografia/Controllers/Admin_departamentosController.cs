using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Monografia.Models;

namespace Monografia.Controllers
{
    public class Admin_departamentosController : Controller
    {
        private proyectotiendaEntities db = new proyectotiendaEntities();

        // GET: departamentoes
        public ActionResult Lista_departamentos()
        {
            return View(db.departamento.Where(x=>x.Iddepartmento!=1).ToList());
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
        public ActionResult Create( departamento departamento)
        {
            try
            {
                if (validadinputs(departamento))
                {
                    if (db.departamento.Where(x => x.Descripcion.ToUpper() == departamento.Descripcion.ToUpper() && x.Estado == 1).FirstOrDefault() == null)
                    {
                        departamento.Usuario_alta = (string)Session["usuario_logueado"];
                        departamento.Fecha_alta = DateTime.Now;
                        departamento.Estado = 1;
                        db.departamento.Add(departamento);
                        db.SaveChanges();
                        return Json(new { success = true, mensaje = "Se ha creado departamento satisfactoriamente." });
                    }
                    else {

                        ViewBag.Mensaje = "<i class='bi bi-exclamation-octagon me-1'></i>Ya existe un departamento con la misma descripcion<br>";
                        return PartialView(departamento);
                    }
                }
                else {
                    return PartialView(departamento);
                } 
            }
            catch (Exception)
            {

                throw;
            }
       
        }

        public Boolean validadinputs(departamento datosdepartamento)
        {
            Boolean valid = true;
            if (datosdepartamento.Descripcion == null)
            {
                ViewBag.Mensaje = "<i class='bi bi-exclamation-octagon me-1'></i>Debe ingresar el descripción del departamento<br>";
                valid = false;
            }
            if (datosdepartamento.Descripcion != null)
                if (!sololetras(datosdepartamento.Descripcion))
            {
                ViewBag.Mensaje = "<i class='bi bi-exclamation-octagon me-1'></i>Debe ingresar solo letras en descripción del departamento<br>";
                valid = false;
            }
            return valid;
        }

        public Boolean sololetras(string datoingresado)
        {
            if (datoingresado.All(char.IsLetter))
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
                departamento departamento = null;
                if (id != 0 && id != null)
                {


                  departamento = db.departamento.Find(id);
                    if (departamento == null)
                    {
                        ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>No se encontro departamento";
                        return PartialView(departamento);
                    }
                    else {
                        return PartialView(departamento);
                    }
          
                }
                else
                {
                    ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Id de departamento erroneo";
                    return PartialView(departamento);

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
        public ActionResult Edit( departamento departamento)
        {
            try
            {

                if (validadinputs(departamento))
                {

                    var datosdepartamento = (from d in db.departamento where d.Iddepartmento == departamento.Iddepartmento select d).FirstOrDefault();
                    if (datosdepartamento == null)
                    {
                        ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>No se encontro departamento";
                        return PartialView(departamento);
                    }
                    else
                    {
                        if (db.departamento.Where(x => x.Descripcion.ToUpper() == departamento.Descripcion.ToUpper() && x.Estado == 1 && x.Iddepartmento!=departamento.Iddepartmento).FirstOrDefault() == null)
                        {
                            datosdepartamento.Descripcion = departamento.Descripcion;
                            db.SaveChanges();
                            return Json(new { success = true, mensaje = "Se ha actualizado la informacion del departamento satisfactoriamente." });
                        }
                        else {
                            ViewBag.Mensaje = "<i class='bi bi-exclamation-octagon me-1'></i>Ya existe un departamento con la misma descripcion<br>";
                            return PartialView(departamento);
                        }
                    }
                }
                else
                {

                    return PartialView(departamento);
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
                departamento departamento = null;
                if (id != 0 && id != null)
                {
                  departamento = db.departamento.Find(id);

                    if (departamento != null)
                    {
                        return PartialView(departamento);
                    }
                    else
                    {
                        ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>No se encontro departamento";
                        return PartialView(departamento);
                    }
                }
                else
                {
                    ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Id de departamento erroneo";
                    return PartialView(departamento);
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

                departamento departamento = null;
                if (id != 0 && id != null)
                {
                    departamento = db.departamento.Where(x => x.Iddepartmento == id).FirstOrDefault();

                    if (departamento != null)
                    {

                        departamento.Fecha_baja = DateTime.Now;
                        departamento.Usuario_baja = (string)Session["usuario_logueado"];
                        departamento.Estado = 2;
                        db.SaveChanges();
                        return Json(new { success = true, mensaje = "Se ha inactivado el departamento satisfactoriamente." });
                    }
                    else
                    {
                        ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>No se encontro departamento";
                        return PartialView(departamento);
                    }
                }
                else
                {
                    ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Id de departamento erroneo";
                    return PartialView(departamento);
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
