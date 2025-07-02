using Monografia.Middleware;
using Monografia.Models;
using System;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace Monografia.Controllers
{
    [ValidateSession]
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
            
                proveedor proveedor = null;
                if (id != 0 && id != null)
                {

                    proveedor = db.proveedor.Find(id);
                    if (proveedor == null)
                    {

                        ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>No se encontró proveedor";
                        return PartialView(proveedor);

                    }
                    else
                    {
                        return PartialView(proveedor);
                    }
                }
                else
                {
                    ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Id de proveedor erróneo";
                    return PartialView(proveedor);

                }
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

                if (validadinputs(proveedor))
                {
                    if (db.proveedor.Where(x => x.Descripcion.Replace(" ","").ToUpper() == proveedor.Descripcion.Replace(" ", "").ToUpper()).FirstOrDefault() == null)
                    {
                        proveedor.Fecha_alta = DateTime.Now;
                        proveedor.Estado = 1;
                        proveedor.Usuario_alta = (string)Session["usuario_logueado"];
                        db.proveedor.Add(proveedor);
                        db.SaveChanges();
                        return Json(new { success = true, mensaje = "Se ha creado el proveedor satisfactoriamente." });
                    }
                    else {
                        ViewBag.Mensaje = "<i class='bi bi-exclamation-octagon me-1'></i>Ya existe un proveedor con el mismo nombre<br>";
                        return PartialView(proveedor);
                    }
                }
                else
                {
                    return PartialView(proveedor);
                }
            }
            catch (Exception)
            {

                throw;
            }
          
        }

        public Boolean validadinputs(proveedor datosproveedor)
        {
            Boolean valid = true;
            if (datosproveedor.Descripcion == null)
            {
                ViewBag.Mensaje = "<i class='bi bi-exclamation-octagon me-1'></i>Debe ingresar el descripción del proveedor<br>";
                valid = false;
            }
            if (datosproveedor.Telefono == null)
            {
                ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Debe ingresar el número telefónico del proveedor<br>";
                valid = false;
            }
            else
            {
                if (datosproveedor.Telefono.ToString().Length < 8)
                {
                    ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Debe ingresar un número telefónico Válido<br>";
                    valid = false;
                }
            }
            if (datosproveedor.Email == null)
            {
                ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Debe ingresar la dirección del proveedor";
                valid = false;

            }
            if (datosproveedor.Direccion == null)
            {
                ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Debe ingresar la dirección del proveedor";
                valid = false;

            }
            return valid;
        }


        // GET: proveedors/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {

                proveedor proveedor = null;
                if (id != 0 && id != null)
                {

                   proveedor = db.proveedor.Find(id);
                    if (proveedor == null)
                    {

                        ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>No se encontró proveedor";
                        return PartialView(proveedor);

                    }
                    else
                    {
                        return PartialView(proveedor);
                    }
                }
                else
                {
                    ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Id de proveedor erróneo";
                    return PartialView(proveedor);

                }
         
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
                proveedor datosproveedor = null;
                if (validadinputs(proveedor))
                {
                    datosproveedor = (db.proveedor.Where(x => x.IdProveedor == proveedor.IdProveedor).FirstOrDefault());
                    if (datosproveedor == null)
                    {
                        ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>No se encontró proveedor";
       
                        return PartialView(proveedor);
                    }
                    else
                    {
                        if (db.proveedor.Where(x => x.Descripcion.Replace(" ", "").ToUpper() == proveedor.Descripcion.Replace(" ", "").ToUpper()&& x.IdProveedor!=proveedor.IdProveedor).FirstOrDefault() == null)
                        {
                            datosproveedor.Descripcion = proveedor.Descripcion;
                            datosproveedor.Direccion = proveedor.Direccion;
                            datosproveedor.Email = proveedor.Email;
                            datosproveedor.Telefono = proveedor.Telefono;
                            db.SaveChanges();
                            return Json(new { success = true, mensaje = "Se ha actualizado la información del proveedor satisfactoriamente." });
                        }
                        else {
                            ViewBag.Mensaje = "<i class='bi bi-exclamation-octagon me-1'></i>Ya existe un proveedor con el mismo nombre<br>";
                            return PartialView(proveedor);
                        }
                    }
                }
                else
                {

                    return PartialView(proveedor);
                }
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
                proveedor proveedor = null;
         
                if (id != 0 && id != null)
                {
                    proveedor = db.proveedor.Find(id);

                    if (proveedor != null)
                    {
                        return PartialView(proveedor);
                    }
                    else
                    {
                        ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>No se encontró proveedor";
                        return PartialView(proveedor);
                    }
                }
                else
                {
                    ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Id de proveedor erróneo";
                    return PartialView(proveedor);
                }

            }
            catch (Exception)
            {

                throw;
            }
           
        }

        // POST: proveedors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            try
            {

                proveedor datosproveedor = null;
                if (id != 0 && id != null)
                {
                   datosproveedor = (from d in db.proveedor where d.IdProveedor == id select d).FirstOrDefault();

                    if (datosproveedor != null)
                    {

                        var productosactivos = db.productos.FirstOrDefault(x => x.Idproveedor==datosproveedor.IdProveedor && x.Estado==1);

                        if (productosactivos == null)
                        {
                            datosproveedor.Fecha_baja = DateTime.Now;
                            datosproveedor.Usuario_baja = (string)Session["usuario_logueado"];
                            datosproveedor.Estado = 2;
                            db.SaveChanges();
                            return Json(new { success = true, mensaje = "Se ha inactivado el proveedor satisfactoriamente." });
                        }
                        else {

                            ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>No se puede inactivar proveedor con un producto activo";
                            return PartialView(datosproveedor);
                        }
                           
                    }
                    else
                    {
                        ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>No se encontró proveedor";
                        return PartialView(datosproveedor);
                    }
                }
                else
                {
                    ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Id de proveedor erróneo";
                    return PartialView(datosproveedor);
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
