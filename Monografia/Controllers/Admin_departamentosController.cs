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
                if (ModelState.IsValid)
                {
                    departamento.Usuario_alta = (string)Session["usuario_logueado"];
                    departamento.Fecha_alta = DateTime.Now;
                    departamento.Estado = 1;
                    db.departamento.Add(departamento);
                    db.SaveChanges();
                    return Json(new { success = true });
                }

                return PartialView(departamento);
            }
            catch (Exception)
            {

                throw;
            }
       
        }

        // GET: departamentoes/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                departamento departamento = db.departamento.Find(id);
                if (departamento == null)
                {
                    return HttpNotFound();
                }
                return PartialView(departamento);
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
                if (ModelState.IsValid)
                {
                    var datosdepartamento = (from d in db.departamento where d.Iddepartmento == departamento.Iddepartmento select d).FirstOrDefault();

                    datosdepartamento.Descripcion = departamento.Descripcion;
                    db.SaveChanges();
                    return Json(new { success = true });
                }
                return PartialView(departamento);
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
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                departamento departamento = db.departamento.Find(id);
                if (departamento == null)
                {
                    return HttpNotFound();
                }
                return PartialView(departamento);

            }
            catch (Exception)
            {

                throw;
            }
          
        }

        // POST: departamentoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var departamento = db.departamento.Where(x => x.Iddepartmento == id).FirstOrDefault();

                departamento.Fecha_baja = DateTime.Now;
                departamento.Usuario_baja = (string)Session["usuario_logueado"];
                departamento.Estado = 2;
                db.SaveChanges();
                return Json(new { success = true });
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
