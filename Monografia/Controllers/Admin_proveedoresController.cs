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
    public class Admin_proveedoresController : Controller
    {
        private proyectotiendaEntities db = new proyectotiendaEntities();

        // GET: proveedors
        public ActionResult Lista_proveedores()
        {
            return View(db.proveedor.ToList());
        }

        // GET: proveedors/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                proveedor proveedor = db.proveedor.Find(id);
                if (proveedor == null)
                {
                    return HttpNotFound();
                }
                return PartialView(proveedor);
            }
            catch (Exception)
            {

                throw;
            }
         
        }

        // GET: proveedors/Create
        public ActionResult Create()
        {
            return PartialView();
        }

        // POST: proveedors/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( proveedor proveedor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    proveedor.Fecha_alta = DateTime.Now;
                    proveedor.Estado = 1;
                    proveedor.Usuario_alta = (string)Session["usuario_logueado"];
                    db.proveedor.Add(proveedor);
                    db.SaveChanges();
                    return Json(new { success = true, mensaje = "Se ha creado el proveedor satisfactoriamente." });
                }

                return PartialView(proveedor);
            }
            catch (Exception)
            {

                throw;
            }
          
        }

        // GET: proveedors/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                proveedor proveedor = db.proveedor.Find(id);
                if (proveedor == null)
                {
                    return HttpNotFound();
                }
                return PartialView(proveedor);
            }
            catch (Exception)
            {

                throw;
            }
         
        }

        // POST: proveedors/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(proveedor proveedor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var datosproveedores = (db.proveedor.Where(x => x.IdProveedor == proveedor.IdProveedor).FirstOrDefault());
                    datosproveedores.Descripcion = proveedor.Descripcion;
                    datosproveedores.Direccion = proveedor.Direccion;
                    datosproveedores.Email = proveedor.Email;
                    datosproveedores.Telefono = proveedor.Telefono;
                    db.SaveChanges();
                    return Json(new { success = true, mensaje = "Se ha actualizado la informacion del proveedor satisfactoriamente." });
                }
                return PartialView(proveedor);
            }
            catch (Exception)
            {

                throw;
            }
           
           
        }

        // GET: proveedors/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                proveedor proveedor = db.proveedor.Find(id);
                if (proveedor == null)
                {
                    return HttpNotFound();
                }
                return PartialView(proveedor);
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        // POST: proveedors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var datosproveedor = (from d in db.proveedor where d.IdProveedor == id select d).FirstOrDefault();
                datosproveedor.Fecha_baja = DateTime.Now;
                datosproveedor.Usuario_baja = (string)Session["usuario_logueado"];
                datosproveedor.Estado = 2;
                db.SaveChanges();
                return Json(new { success = true, mensaje = "Se ha inactivado el proveedor satisfactoriamente." });
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
