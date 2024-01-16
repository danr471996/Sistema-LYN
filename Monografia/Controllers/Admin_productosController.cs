using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using DocumentFormat.OpenXml.Office2010.Excel;
using Monografia.Models;

namespace Monografia.Controllers
{
    public class Admin_productosController : Controller
    {
        private proyectotiendaEntities db = new proyectotiendaEntities();
        string patronConDecimales = @"^\d+(\.\d+)?$";
        string patronsindecimales = @"^\d+$";

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
                modelo_contenedor.listadepartamento = new List<departamento>();
                modelo_contenedor.listaproveedor = new List<proveedor>();
                modelo_contenedor.listadepartamento = getdepartamentos();
                modelo_contenedor.listaproveedor = getproveedor();

                return PartialView(modelo_contenedor);
            }
            catch (Exception)
            {

                throw;
            }
         
        }

        public List<departamento> getdepartamentos() {
        
        return (from u in db.departamento where u.Estado == 1 select u).ToList();
        }
        public List<proveedor> getproveedor()
        {

            return (from u in db.proveedor where u.Estado == 1 select u).ToList();
        }
        // POST: usuarios_tienda/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Modelo_contenedor modelo_contenedor, string usainventario)
        {
        

            try
            {


                if (validadinputs(modelo_contenedor, usainventario,null))
                {
                    if (db.productos.Where(x => x.Codigo_producto == modelo_contenedor.productos.Codigo_producto && x.Estado == 1).FirstOrDefault() == null)
                    {
                        if (usainventario == "true")
                        {
                            modelo_contenedor.productos.Usa_inventario = 1;
                        }
                        else
                        {
                            modelo_contenedor.productos.Usa_inventario = 2;
                        }
                        modelo_contenedor.productos.Fecha_alta = DateTime.Now;
                        modelo_contenedor.productos.Usuario_alta = (string)Session["usuario_logueado"];
                        modelo_contenedor.productos.Estado = 1;
                        db.productos.Add(modelo_contenedor.productos);
                        db.SaveChanges();
                        return Json(new { success = true, mensaje = "Se ha creado el producto satisfactoriamente." });
                    }
                    else {
                        modelo_contenedor.listadepartamento = new List<departamento>();
                        modelo_contenedor.listaproveedor = new List<proveedor>();
                        modelo_contenedor.listadepartamento = getdepartamentos();
                        modelo_contenedor.listaproveedor = getproveedor();
                        ViewBag.Mensaje = "<i class='bi bi-exclamation-octagon me-1'></i>Ya existe un producto con el mismo codigo<br>";
                        return PartialView(modelo_contenedor);
                    }
                }
                else
                {

                    modelo_contenedor.listadepartamento = new List<departamento>();
                    modelo_contenedor.listaproveedor = new List<proveedor>();
                    modelo_contenedor.listadepartamento = getdepartamentos();
                    modelo_contenedor.listaproveedor = getproveedor();

                    return PartialView(modelo_contenedor);
                }


            }
            catch (Exception)
            {

                throw;
            }
        }



        public Boolean validadinputs(Modelo_contenedor datosproducto,string usainventario,promocion datospromocion)
        {
            Boolean valid = true;
            if (datosproducto != null)
            {
                if (datosproducto.productos.Codigo_producto == 0)
                {
                    ViewBag.Mensaje = "<i class='bi bi-exclamation-octagon me-1'></i>Debe ingresar el codigo del producto<br>";
                    valid = false;
                }
                if (!Regex.IsMatch(datosproducto.productos.Codigo_producto.ToString(), patronsindecimales))
                {
                    ViewBag.Mensaje = "<i class='bi bi-exclamation-octagon me-1'></i>Debe ingresar solo números en codigo del producto<br>";
                    valid = false;
                }
                if (datosproducto.productos.Descripcion == null)
                {
                    ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Debe ingresar la descripcion del producto<br>";
                    valid = false;
                }
                if (datosproducto.productos.Precio_costo == 0)
                {
                    ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Debe ingresar el precio del producto<br>";
                    valid = false;
                }
                if (!Regex.IsMatch(datosproducto.productos.Precio_costo.ToString(), patronConDecimales))
                {
                    ViewBag.Mensaje = "<i class='bi bi-exclamation-octagon me-1'></i>Debe ingresar solo números en precio del producto<br>";
                    valid = false;
                }
                if (datosproducto.productos.Precio_venta == 0)
                {
                    ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Debe ingresar el precio de venta del producto<br>";
                    valid = false;
                }
                if (!Regex.IsMatch(datosproducto.productos.Precio_venta.ToString(), patronConDecimales))
                {
                    ViewBag.Mensaje = "<i class='bi bi-exclamation-octagon me-1'></i>Debe ingresar solo números en precio de venta del producto<br>";
                    valid = false;
                }
                if (datosproducto.productos.Precio_mayoreo == 0)
                {
                    ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Debe ingresar el precio al por mayor del producto<br>";
                    valid = false;
                }

                if (!Regex.IsMatch(datosproducto.productos.Precio_mayoreo.ToString(), patronConDecimales))
                {
                    ViewBag.Mensaje = "<i class='bi bi-exclamation-octagon me-1'></i>Debe ingresar solo números en precio al por mayor del producto<br>";
                    valid = false;
                }



                if (usainventario == "true")
                {
                    if (datosproducto.productos.Cantidad_actual == 0 || datosproducto.productos.Cantidad_actual == null)
                    {
                        ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Debe ingresar cantidad actual de inventario del producto<br>";
                        valid = false;
                    }
                    if (!Regex.IsMatch(datosproducto.productos.Cantidad_actual.ToString(), patronsindecimales))
                    {
                        ViewBag.Mensaje = "<i class='bi bi-exclamation-octagon me-1'></i>Debe ingresar solo números en cantidad actual de inventario del producto<br>";
                        valid = false;
                    }
                    if (datosproducto.productos.Cantidad_minima == 0 || datosproducto.productos.Cantidad_minima == null)
                    {
                        ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Debe ingresar cantidad minima de inventario del producto<br>";
                        valid = false;
                    }
                    if (!Regex.IsMatch(datosproducto.productos.Cantidad_minima.ToString(), patronsindecimales))
                    {
                        ViewBag.Mensaje = "<i class='bi bi-exclamation-octagon me-1'></i>Debe ingresar solo números en cantidad minima de inventario del producto<br>";
                        valid = false;
                    }
                    if (datosproducto.productos.Cantidad_minima > datosproducto.productos.Cantidad_actual)
                    {
                        ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Cantidad minima de inventario no puede ser mayor a cantidad actual<br>";
                        valid = false;
                    }
                }
            }

            if (datospromocion != null)
            {
                if (datospromocion.Nombre_promocion == null)
                {
                    ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Debe ingresar la descripcion de la promoción<br>";
                    valid = false;
                }
                if (datospromocion.productos.Codigo_producto == 0)
                {
                    ViewBag.Mensaje = "<i class='bi bi-exclamation-octagon me-1'></i>Debe ingresar el codigo del producto<br>";
                    valid = false;
                }
                if (!Regex.IsMatch(datospromocion.productos.Codigo_producto.ToString(), patronsindecimales))
                {
                    ViewBag.Mensaje = "<i class='bi bi-exclamation-octagon me-1'></i>Debe ingresar solo números en codigo del producto<br>";
                    valid = false;
                }

                if (datospromocion.Cant_desde == 0)
                {
                    ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Debe ingresar cantidad desde de la promoción<br>";
                    valid = false;
                }
                if (!Regex.IsMatch(datospromocion.Cant_desde.ToString(), patronsindecimales))
                {
                    ViewBag.Mensaje = "<i class='bi bi-exclamation-octagon me-1'></i>Debe ingresar solo números en cantidad desde de la promoción<br>";
                    valid = false;
                }
                if (datospromocion.Cant_hasta == 0 )
                {
                    ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Debe ingresar cantidad hasta de la promoción<br>";
                    valid = false;
                }
                if (!Regex.IsMatch(datospromocion.Cant_hasta.ToString(), patronsindecimales))
                {
                    ViewBag.Mensaje = "<i class='bi bi-exclamation-octagon me-1'></i>Debe ingresar solo números en cantidad hasta de la promoción<br>";
                    valid = false;
                }
                if (datospromocion.Cant_desde > datospromocion.Cant_hasta)
                {
                    ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Cantidad desde no puede ser mayor a cantidad hasta de la promoción<br>";
                    valid = false;
                }
                if (datospromocion.Precio_unitario == 0)
                {
                    ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Debe ingresar precio unitario del producto en la promoción<br>";
                    valid = false;
                }
                if (!Regex.IsMatch(datospromocion.Precio_unitario.ToString(), patronConDecimales))
                {
                    ViewBag.Mensaje = "<i class='bi bi-exclamation-octagon me-1'></i>Debe ingresar solo números en precio unitario del producto en la promoción<br>";
                    valid = false;
                }
            }

            return valid;
        }


        // GET: usuarios_tienda/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                Modelo_contenedor modelocontenedor = null;
                if (id != 0 && id != null)
                {

                     productos  datosproductos = db.productos.Find(id);

                    if (datosproductos == null)
                    {

                        ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>No se encontro producto";
                        return PartialView(modelocontenedor);

                    }
                    else
                    {

                        modelocontenedor = new Modelo_contenedor
                        {
                            productos = new productos(),
                           listadepartamento = new List<departamento>(),
                           listaproveedor=new List<proveedor>()
                        };
                        modelocontenedor.productos = datosproductos;
                        modelocontenedor.listadepartamento = getdepartamentos();
                        modelocontenedor.listaproveedor = getproveedor();

                        if (modelocontenedor.productos.Usa_inventario == 1)
                            ViewBag.usainventario = true;
                        else
                            ViewBag.usainventario = false;

                        return PartialView(modelocontenedor);
                    }
                }
                else
                {
                    ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Id de producto erroneo";


                    return PartialView(modelocontenedor);
                }
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
        public ActionResult Edit(Modelo_contenedor datosproductoedit, string usainventario)
        {
            try
            {

              ViewBag.usainventario = usainventario == "true" ? true:false ;
              
                if (validadinputs(datosproductoedit, usainventario,null))
                {

                    var producto = db.productos.Where(x => x.Idproducto == datosproductoedit.productos.Idproducto).FirstOrDefault();
                    if (producto == null)
                    {
                        ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>No se encontro producto";

                        datosproductoedit.listadepartamento = new List<departamento>();
                        datosproductoedit.listaproveedor = new List<proveedor>();
                        datosproductoedit.listadepartamento = getdepartamentos();
                        datosproductoedit.listaproveedor = getproveedor();
                        return PartialView(datosproductoedit);
                    }
                    else
                    {

                        if (db.productos.Where(x => x.Codigo_producto == datosproductoedit.productos.Codigo_producto && x.Estado == 1 && x.Idproducto!=datosproductoedit.productos.Idproducto).FirstOrDefault() == null)
                        {
                            if (usainventario == "true")
                            {
                                datosproductoedit.productos.Usa_inventario = 1;
                            }
                            else
                            {
                                datosproductoedit.productos.Usa_inventario = 2;
                            }
                            producto.Codigo_producto = datosproductoedit.productos.Codigo_producto;
                            producto.Descripcion = datosproductoedit.productos.Descripcion;
                            producto.Id_tipoventas = datosproductoedit.productos.Id_tipoventas;
                            producto.Precio_costo = datosproductoedit.productos.Precio_costo;
                            producto.Precio_venta = datosproductoedit.productos.Precio_venta;
                            producto.Precio_mayoreo = datosproductoedit.productos.Precio_mayoreo;
                            producto.Iddepartamento = datosproductoedit.productos.Iddepartamento;
                            producto.Usa_inventario = datosproductoedit.productos.Usa_inventario;
                            producto.Cantidad_actual = datosproductoedit.productos.Cantidad_actual;
                            producto.Cantidad_minima = datosproductoedit.productos.Cantidad_minima;

                            db.SaveChanges();

                            return Json(new { success = true, mensaje = "Se ha actualizado la informacion del producto satisfactoriamente." });
                        }
                        else {
                            datosproductoedit.listadepartamento = new List<departamento>();
                            datosproductoedit.listaproveedor = new List<proveedor>();
                            datosproductoedit.listadepartamento = getdepartamentos();
                            datosproductoedit.listaproveedor = getproveedor();
                            ViewBag.Mensaje = "<i class='bi bi-exclamation-octagon me-1'></i>Ya existe un producto con el mismo codigo<br>";
                            return PartialView(datosproductoedit);
                        }
                    }
                }
                else
                {

                    datosproductoedit.listadepartamento = new List<departamento>();
                    datosproductoedit.listaproveedor = new List<proveedor>();
                    datosproductoedit.listadepartamento = getdepartamentos();
                    datosproductoedit.listaproveedor = getproveedor();
                    return PartialView(datosproductoedit);
                }

            }
            catch (Exception)
            {

                throw;
            }
          
        }


        // GET: usuarios_tienda/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {
                productos datosproducto= null;
                if (id != 0 && id != null)
                {

                     datosproducto = db.productos.Find(id);
                    if (datosproducto != null)
                    {

                        return PartialView(datosproducto);
                    }
                    else {
                        ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>No se encontro producto";
                        return PartialView(datosproducto);

                    }
                }
                else {
                    ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Id de producto erroneo";
                    return PartialView(datosproducto);
                }
            }
            catch (Exception)
            {

                throw;
            }
       
        }

        // POST: usuarios_tienda/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            try
            {
                productos datosproducto = null;
                if (id != 0 && id != null)
                {
                    datosproducto = db.productos.Where(x => x.Idproducto == id).FirstOrDefault();
                    if (datosproducto != null)
                    {
                        datosproducto.Fecha_baja = DateTime.Now;
                        datosproducto.Usuario_baja = (string)Session["usuario_logueado"];
                        datosproducto.Estado = 2;
                        db.SaveChanges();
                        return Json(new { success = true, mensaje = "Se ha inactivado el producto satisfactoriamente." });
                    }
                    else {
                        ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>No se encontro producto";
                        return PartialView(datosproducto);
                    }
                }
                else {

                    ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Id de producto erroneo";
                    return PartialView(datosproducto);
                }
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


        public ActionResult Lista_promocion()
        {
            return View(db.promocion.ToList());
        }


        public ActionResult Createpromocion()
        {
            try
            {
                return PartialView();
            }
            catch (Exception)
            {

                throw;
            }

        }
        // POST: Admin_producto/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Createpromocion(promocion promocion)
        {
            try
            {
            
                if (validadinputs(null, "", promocion))
                {
              
                        var codigoproducto = (from u in db.productos
                                              where u.Codigo_producto == promocion.productos.Codigo_producto
                                              select new
                                              {
                                                  u
                                              }).FirstOrDefault();

                        if (codigoproducto != null)
                        {
                            if (codigoproducto.u.Estado ==1)
                            {
                                var promocionlist = db.promocion.ToList();
                            if (promocionlist.Where(x => x.Nombre_promocion.ToUpper() == promocion.Nombre_promocion.ToUpper() && x.Estado == 1).FirstOrDefault() == null)
                            {
                                if (promocionlist.Where(x => x.Id_producto == codigoproducto.u.Idproducto && x.Estado == 1).FirstOrDefault() == null)
                                {
                                    promocion.Fecha_alta = DateTime.Now;
                                promocion.Estado = 1;
                                promocion.Id_producto = codigoproducto.u.Idproducto;
                                db.promocion.Add(promocion);
                                db.SaveChanges();
                                        return Json(new { success = true, mensaje = "Se ha creado el promocion satisfactoriamente." });
                                    }
                                else
                                {
                             
                                    ViewBag.Mensaje = "<i class='bi bi-exclamation-octagon me-1'></i>Ya existe una promocion para el producto digitado<br>";
                                    return PartialView(promocion);

                                }
                            }
                            else {
                               
                                ViewBag.Mensaje = "<i class='bi bi-exclamation-octagon me-1'></i>Ya existe una promocion con el mismo nombre<br>";
                                return PartialView(promocion);

                            }

                            }
                            else
                            {
                                ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>No se encontro producto activo";
                                return PartialView(promocion);
                            }
                        }
                        else {
                            ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>No se encontro producto";
                            return PartialView(promocion);
                        }



                }
                else {
                    return PartialView(promocion);
                }
             
               
            }
            catch (Exception ex)
            {

                throw;
            }
          

        }

        // GET: usuarios_tienda/Edit/5
        public ActionResult Editpromocion(int? id)
        {
            try
            {
                promocion promocion = null;
                if (id != 0 && id != null)
                {

                    promocion datospromocion = db.promocion.Find(id);

                    if (datospromocion == null)
                    {

                        ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>No se encontro promoción";
                        return PartialView(datospromocion);

                    }
                    else
                    {

                        return PartialView(datospromocion);
                    }
                }
                else
                {
                    ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Id de promoción erroneo";
                    return PartialView(promocion);
                }
            }
            catch (Exception)
            {

                throw;
            }


        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editpromocion(promocion datospromocionedit)
        {
            try
            {


                if (validadinputs(null, "", datospromocionedit))
                {
                    promocion datospromocion = db.promocion.Find(datospromocionedit.Idpromocion);

                    if (datospromocion != null)
                    {
                        var codigoproducto = (from u in db.productos
                                          where u.Codigo_producto == datospromocionedit.productos.Codigo_producto
                                          select new
                                          {
                                              u
                                          }).FirstOrDefault();

                            if (codigoproducto != null)
                            {
                                if (codigoproducto.u.Estado == 1)
                                {
                            

                                    var promocionlist = db.promocion.ToList();
                                    if (promocionlist.Where(x => x.Nombre_promocion.ToUpper() == datospromocionedit.Nombre_promocion.ToUpper() && x.Estado == 1 && x.Idpromocion!=datospromocionedit.Idpromocion).FirstOrDefault() == null)
                                    {
                                        if (promocionlist.Where(x => x.Id_producto == codigoproducto.u.Idproducto && x.Estado == 1 && x.Idpromocion != datospromocionedit.Idpromocion).FirstOrDefault() == null)
                                        {

                                        datospromocion.Nombre_promocion = datospromocionedit.Nombre_promocion;
                                        datospromocion.Id_producto = codigoproducto.u.Idproducto;
                                        datospromocion.Cant_desde = datospromocionedit.Cant_desde;
                                        datospromocion.Cant_hasta = datospromocionedit.Cant_hasta;
                                        datospromocion.Precio_unitario = datospromocionedit.Precio_unitario;

                                            db.SaveChanges();
                                            return Json(new { success = true, mensaje = "Se ha actualizado la promocion satisfactoriamente." });
                                        }
                                        else
                                        {

                                            ViewBag.Mensaje = "<i class='bi bi-exclamation-octagon me-1'></i>Ya existe una promocion para el producto digitado<br>";
                                            return PartialView(datospromocionedit);

                                        }
                                    }
                                    else
                                    {

                                        ViewBag.Mensaje = "<i class='bi bi-exclamation-octagon me-1'></i>Ya existe una promocion con el mismo nombre<br>";
                                        return PartialView(datospromocionedit);

                                    }

                                }
                                else
                                {
                                    ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>No se encontro producto activo";
                                    return PartialView(datospromocionedit);
                                }
                            }
                            else
                            {
                                ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>No se encontro producto";
                                return PartialView(datospromocionedit);
                            }
                    }
                    else
                    {
                        ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>No se encontro promoción";
                        return PartialView(datospromocionedit);
                    }
                }
                else
                {
                    return PartialView(datospromocionedit);
                }

            }
            catch (Exception)
            {

                throw;
            }

        }

        // GET: usuarios_tienda/Delete/5
        public ActionResult Deletepromocion(int? id)
        {
            try
            {

                promocion promocion = null;
                if (id != 0 && id != null)
                {

                    promocion = db.promocion.Find(id);
                    if (promocion != null)
                    {

                        return PartialView(promocion);
                    }
                    else
                    {
                        ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>No se encontro promoción";
                        return PartialView(promocion);

                    }
                }
                else
                {
                    ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Id de promoción erroneo";
                    return PartialView(promocion);
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
        public ActionResult Deletepromocionconfirmed(int? id)
        {
            try
            {

                promocion promocion = null;
                if (id != 0 && id != null)
                {
                    promocion = db.promocion.Where(x => x.Idpromocion == id).FirstOrDefault();
                    if (promocion != null)
                    {
                        promocion.Fecha_baja = DateTime.Now;
                        promocion.Usuario_baja = (string)Session["usuario_logueado"];
                        promocion.Estado = 2;
                        db.SaveChanges();
                        return Json(new { success = true, mensaje = "Se ha inactivado la promoción satisfactoriamente." });
                    }
                    else
                    {
                        ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>No se encontro promoción";
                        return PartialView(promocion);
                    }
                }
                else
                {

                    ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Id de promoción erroneo";
                    return PartialView(promocion);
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
