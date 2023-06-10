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
    public class Admin_productosController : Controller
    {
        private proyectotiendaEntities db = new proyectotiendaEntities();

    
        // GET: usuarios_tienda
        public ActionResult Lista_productos()
        {
            return View(db.productos.ToList());
        }

        // GET: usuarios_tienda/Create
        public ActionResult Create()
        {
            try
            {
                Modelo_contenedor modelo_contenedor = new Modelo_contenedor();
                var departamentos = from u in db.departamento where u.Estado == 1 select u;
                modelo_contenedor.listadepartamento = new List<departamento>();
                foreach (var item in departamentos)
                {

                    modelo_contenedor.listadepartamento.Add(item);
                }
               
                return PartialView(modelo_contenedor);
            }
            catch (Exception)
            {

                throw;
            }
         
        }

        // POST: usuarios_tienda/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(productos productos, string usainventario)
        {
            try
            {
                if (ModelState.IsValid)
                {


                    if (usainventario == "true")
                    {
                        productos.Usa_inventario = 1;
                    }
                    else
                    {
                        productos.Usa_inventario = 2;
                    }
                    productos.Fecha_alta = DateTime.Now;
                    productos.Usuario_alta = (string)Session["usuario_logueado"];
                    productos.Estado = 1;
                    db.productos.Add(productos);
                    db.SaveChanges();
                    return Json(new { success = true, mensaje = "Se ha creado el producto satisfactoriamente." });
                }

                return PartialView(productos);

            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET: usuarios_tienda/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                Modelo_contenedor modelocontenedor = new Modelo_contenedor();
                var departamentos = from u in db.departamento where u.Estado == 1 select u;
                modelocontenedor.listadepartamento = new List<departamento>();
                foreach (var item in departamentos)
                {

                    modelocontenedor.listadepartamento.Add(item);
                }
                modelocontenedor.productos = db.productos.Find(id);

                if (modelocontenedor == null)
                {
                    return HttpNotFound();
                }
                return PartialView(modelocontenedor);
            }
            catch (Exception)
            {

                throw;
            }
          

        }

        // POST: usuarios_tienda/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Modelo_contenedor modelocontenedor, string usainventario)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var producto = db.productos.Where(x => x.Codigo_producto == modelocontenedor.productos.Codigo_producto).FirstOrDefault();

                    if (usainventario == "true")
                    {
                        modelocontenedor.productos.Usa_inventario = 1;
                    }
                    else
                    {
                        modelocontenedor.productos.Usa_inventario = 2;
                    }
                    producto.Codigo_producto = modelocontenedor.productos.Codigo_producto;
                    producto.Descripcion = modelocontenedor.productos.Descripcion;
                    producto.Id_tipoventas = modelocontenedor.productos.Id_tipoventas;
                    producto.Precio_costo = modelocontenedor.productos.Precio_costo;
                    producto.Precio_venta = modelocontenedor.productos.Precio_venta;
                    producto.Precio_mayoreo = modelocontenedor.productos.Precio_mayoreo;
                    producto.Iddepartamento = modelocontenedor.productos.Iddepartamento;
                    producto.Usa_inventario = modelocontenedor.productos.Usa_inventario;
                    producto.Cantidad_actual = modelocontenedor.productos.Cantidad_actual;
                    producto.Cantidad_minima = modelocontenedor.productos.Cantidad_minima;

                    db.SaveChanges();

                    return Json(new { success = true, mensaje = "Se ha actualizado la informacion del producto satisfactoriamente." });
                }
                return PartialView(modelocontenedor.productos);
            }
            catch (Exception)
            {

                throw;
            }
          
        }


        // GET: usuarios_tienda/Delete/5
        public ActionResult Delete(int? id, int id2)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                productos productos = db.productos.Find(id);
                if (productos == null)
                {
                    return HttpNotFound();
                }
                return PartialView(productos);
            }
            catch (Exception)
            {

                throw;
            }
       
        }

        // POST: usuarios_tienda/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var productos = db.productos.Where(x => x.Idproducto == id).FirstOrDefault();

                productos.Fecha_baja = DateTime.Now;
                productos.Usuario_baja = (string)Session["usuario_logueado"];
                productos.Estado = 2;
                db.SaveChanges();
                return Json(new { success = true, mensaje = "Se ha inactivado el producto satisfactoriamente." });
            }
            catch (Exception)
            {

                throw;
            }
        
        }
      

        public ActionResult Lista_departamentos()
        {
            return RedirectToAction("Lista_departamentos", "Admin_departamentos");
        }
        public ActionResult Createpromocion()
        {
            try
            {
                ViewBag.listapromocion = db.promocion.ToList();
                return View();
            }
            catch (Exception)
            {

                throw;
            }
           
        }
        // POST: usuarios_tienda/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Createpromocion(promocion promocion,int? Codigo_producto)
        {
            try
            {
                if (Codigo_producto!=null)
                {

                    if (ModelState.IsValid)
                    {
              

                        var codigoproducto = (from u in db.productos
                                              where u.Codigo_producto == Codigo_producto
                                           && u.Estado == 1
                                              select new
                                              {
                                                  u
                                              }).FirstOrDefault();

                            if (codigoproducto != null)
                            {

                                promocion.Fecha_alta = DateTime.Now;
                                promocion.Estado = 1;
                                promocion.Id_producto = codigoproducto.u.Idproducto;
                                db.promocion.Add(promocion);
                                db.SaveChanges();
                       
                            }
                   

                    }
                }

                ViewBag.listapromocion = db.promocion.ToList();
                return View();
            }
            catch (Exception ex)
            {

                throw;
            }
          

        }

        // GET: usuarios_tienda/Delete/5
        public ActionResult Deletepromocion(int? id)
        {
            try
            {
                if (id != null)
                {
           
                    promocion promocion = db.promocion.Find(id);
                    if (promocion == null)
                    {
                        return HttpNotFound();
                    }
                    else
                    {
                        return PartialView(promocion);
                    }
                }
                else
	            {
                  return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
           
            }
            catch (Exception ex)
            {

                throw;
            }
         
        }

        // POST: usuarios_tienda/Delete/5
        [HttpPost, ActionName("Deletepromocion")]
        [ValidateAntiForgeryToken]
        public ActionResult Deletepromocion(int id)
        {
            try
            {
                var promocion = db.promocion.Where(x => x.Idpromocion == id).FirstOrDefault();

                promocion.Fecha_baja = DateTime.Now;
                promocion.Usuario_baja = (string)Session["usuario_logueado"];
                promocion.Estado = 2;
                db.SaveChanges();
                return Json(new { success = true, mensaje = "Se ha inactivado la promoción satisfactoriamente." });
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
