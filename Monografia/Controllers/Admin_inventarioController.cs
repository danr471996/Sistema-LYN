
using Monografia.Middleware;
using Monografia.Models;
using Monografia.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Monografia.Controllers
{
    [ValidateSession]
    public class Admin_inventarioController : Controller
    {
        private proyectotiendaEntities db = new proyectotiendaEntities();
        string patronsindecimales = @"^\d+$";

        // GET: productos
        public ActionResult productos_bajos_inventario()
        {
            try
            {
                var model = new List<Modelo_contenedor>();


                var p = db.productos.Include(a =>a.departamento).Where(x => x.Cantidad_actual < x.Cantidad_minima).ToList();

                foreach (var item in p)
                {
                    model.Add(new Modelo_contenedor
                    {
                        codigo_producto1 = item.Codigo_producto,
                        descripcionproducto1 = item.Descripcion,
                        preciodeventa1 = item.Precio_venta,
                        cantidaactual1 = item.Cantidad_actual,
                        cantidaminima1 = item.Cantidad_minima,
                        descripciondepartamento = item.departamento.Descripcion
                    });
                }

                return View(model);
            }
            catch (Exception)
            {

                throw;
            }
          
        }

        // GET: productos
        public ActionResult reporte_inventario(productos productos)
        {

            Modelo_contenedor modelo_contenedor = new Modelo_contenedor
            {
                listaproductos = new List<productos>(),
                listadepartamento = new List<departamento>()
            };

            List<productos> listaproducto = new List<productos>();

            try
            {

                TempData["cod_depart"] = productos.Iddepartamento;

                if (productos.Iddepartamento != 0)
                {
                    listaproducto = (from x in db.productos where x.Estado == 1 && x.Iddepartamento == productos.Iddepartamento select x).ToList();
                    int? cantidadtotal=listaproducto.Select(x => x.Cantidad_actual).Sum();
                    decimal? costo = listaproducto.Select(x => x.Precio_costo).Sum();
                    ViewBag.cantidad_inventario = cantidadtotal.ToString();
                    ViewBag.costo_inventario = (cantidadtotal * costo).ToString();
                }
                else
                {
                    listaproducto = (from x in db.productos where x.Estado == 1 select x).ToList();
                    int? cantidadtotal = listaproducto.Select(x => x.Cantidad_actual).Sum();
                    decimal? costo = listaproducto.Select(x => x.Precio_costo).Sum();
                    ViewBag.cantidad_inventario = cantidadtotal.ToString();
                    ViewBag.costo_inventario = (cantidadtotal * costo).ToString();
                }
        

               modelo_contenedor.listaproductos= listaproducto;
                
                var listadepartamentos = (from u in db.departamento where u.Estado == 1 select u).ToList();
          
                modelo_contenedor.listadepartamento = listadepartamentos;
                


                return View(modelo_contenedor);
            }
            catch (Exception ex)
            {

                throw;
            }
          
        }

        public ActionResult reporte_inventario_Preview()
        {
            string reportPath = Server.MapPath("~/Reportes/ReportInventario.rdlc");

            int codigodepart = Convert.ToInt32(TempData["cod_depart"]);
            TempData["cod_depart"] = codigodepart;
            var listaproductos = int.Parse(TempData["cod_depart"].ToString()) == 0 ? db.productos.Include(a => a.departamento).ToList() : db.productos.Include(a => a.departamento).Where(x => x.Iddepartamento == codigodepart).ToList();
            
            ViewBag.ReportViewer= ReportCreate.GetReport(reportPath, listaproductos);
            return View("reporte_Preview");

        }


        public ActionResult reporte_bajos_inventario_Preview()
        {
            string reportPath = Server.MapPath("~/Reportes/ReportBajoInventario.rdlc");

            var listaProductos = db.productos
     .Include(p => p.departamento)
     .Where(p => p.Cantidad_actual < p.Cantidad_minima)
     .Select(p => new
     {
         Codigo_producto = p.Codigo_producto,
         Descripcion = p.Descripcion,
         Precio_venta = p.Precio_venta,
         Cantidad_actual = p.Cantidad_actual,
         Cantidad_minima = p.Cantidad_minima,
         DescripcionDep = p.departamento.Descripcion
     })
     .ToList();

            ViewBag.ReportViewer = ReportCreate.GetReport(reportPath, listaProductos);
            return View("reporte_Preview");

        }


        public ActionResult editar_inventario(int? id)
        {

            try
            {
                Modelo_contenedor modelo_Contenedor = null;
                if (id != 0 && id != null)
                {

                    productos datosproductos = db.productos.Include(a => a.departamento).Where(x => x.Idproducto == id).FirstOrDefault();
                
                    if (datosproductos == null)
                    {

                        ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>No se encontro producto";
                        return PartialView(modelo_Contenedor);

                    }
                    else
                    {
                        modelo_Contenedor = new Modelo_contenedor
                        {
                            productos = datosproductos
                        };
                        return PartialView(modelo_Contenedor);
                    }
                }
                else
                {
                    ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Id de producto erroneo";
                    return PartialView(modelo_Contenedor);

                }
            }
            catch (Exception ex)
            {

                throw;
            }
          

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult editar_inventario(Modelo_contenedor modelocontenedor, int? agregar_cantidad)
        {
            int prodcantanterior = 0;
            try
            {
                productos producto = null;
                if (modelocontenedor.productos.Idproducto != 0 && modelocontenedor != null)
                {


                   producto = db.productos.Where(x => x.Idproducto == modelocontenedor.productos.Idproducto).FirstOrDefault();

                    if (producto == null)
                    {
                        ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>No se encontro producto";

                      return  PartialView(modelocontenedor);
                    }
                    else
                    {
                        if (producto.Usa_inventario != 2)
                        {
                            if (agregar_cantidad != null)
                            {
                                if (Regex.IsMatch(agregar_cantidad.ToString(), patronsindecimales))
                                {
                                    prodcantanterior = producto.Cantidad_actual == null ? 0 : Convert.ToInt32(producto.Cantidad_actual);
                                    producto.Cantidad_actual = producto.Cantidad_actual == null ? 0 + agregar_cantidad : producto.Cantidad_actual + agregar_cantidad;
                                    historial_inventario historialinventario = new historial_inventario
                                    {
                                        Fecha_alta = DateTime.Now,
                                        Usuario_alta = (string)Session["usuario_logueado"],
                                        Idproducto = producto.Idproducto,
                                        Tipo_movimiento = 1,
                                        Iddepartamento = producto.Iddepartamento,
                                        Cantidad_actual = Convert.ToInt32(producto.Cantidad_actual),
                                        Cantidad_anterior = prodcantanterior,
                                        Estado = 1
                                    };
                                    db.historial_inventario.Add(historialinventario);
                                    db.SaveChanges();

                                    return Json(new { success = true, mensaje = "Se ha editado el inventario del producto satisfactoriamente." });
                                }
                                else {

                                    ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Favor ingrese solo números en cantidad para realizar la edición de inventario.";
                                    return PartialView(modelocontenedor);
                                }

                            }
                            else
                            {
                                ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Favor ingrese una cantidad para realizar la edición de inventario.";
                                return PartialView(modelocontenedor);
                            }
                        }
                        else {
                            ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Producto no usa inventario.";
                            return PartialView(modelocontenedor);
                        }

                    }

                }
                else
                {
                    ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>No se ha ingresado producto para editar su inventario,favor ingrese uno.";
                    return PartialView(modelocontenedor);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        
        }
        public ActionResult agregar_inventario()
        {
            if (TempData["ajusteexitoso"] != null)
            {
                ViewBag.mensajeexito = "Se agrego producto al inventario satisfactoriamente";
            }

            return View();

        }

      [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult agregar_inventario(int? cod_producto)
        {
            try
            {
                productos obtener_producto = null;
                if (cod_producto != 0 && cod_producto != null)
                {
                    if (Regex.IsMatch(cod_producto.ToString(), patronsindecimales))
                    {
                        TempData["cod_producto"] = cod_producto;
                        obtener_producto = db.productos.Where(x => x.Codigo_producto == cod_producto).FirstOrDefault();

                        if (obtener_producto != null)
                        {
                            return View(obtener_producto);

                        }
                        else
                        {

                            ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>No se encontro Producto";
                            return View(obtener_producto);
                        }
                    }
                    else {
                        ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Favor ingresar solo números en codigo de producto";
                        return View(obtener_producto);
                    }

                }
                else
                {

                    ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Favor ingresar codigo de producto";
                    return View(obtener_producto);

                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        // POST: productos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult procesar_incremento_inventario(productos productos, int? agregar_cantidad)
        {
            int? cod_producto = 0;
            int prodcantanterior = 0;

            try
            {

                if (TempData["cod_producto"] != null)
                {
                    cod_producto = (int)TempData["cod_producto"];
                    TempData["cod_producto"] = cod_producto;
                    ViewBag.cod_producto = (int)cod_producto;
                    productos datosproducto = db.productos.Where(x => x.Codigo_producto == cod_producto).FirstOrDefault();

                    if (datosproducto == null)
                    {
                        ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>No se encontro producto";

                        return View("agregar_inventario", productos);
                    }
                    else
                    {
                        if (agregar_cantidad != null)
                        {
                            if (Regex.IsMatch(agregar_cantidad.ToString(), patronsindecimales))
                            {
                                prodcantanterior = datosproducto.Cantidad_actual == null ? 0 : Convert.ToInt32(datosproducto.Cantidad_actual);
                                datosproducto.Cantidad_actual = datosproducto.Cantidad_actual == null ? 0 + agregar_cantidad : datosproducto.Cantidad_actual + agregar_cantidad;
                                historial_inventario historialinventario = new historial_inventario
                                {
                                    Fecha_alta = DateTime.Now,
                                    Usuario_alta = (string)Session["usuario_logueado"],
                                    Idproducto = datosproducto.Idproducto,
                                    Tipo_movimiento = 1,
                                    Iddepartamento = datosproducto.Iddepartamento,
                                    Cantidad_actual = Convert.ToInt32(datosproducto.Cantidad_actual),
                                    Cantidad_anterior = prodcantanterior,
                                    Estado = 1
                                };
                                db.historial_inventario.Add(historialinventario);
                                db.SaveChanges();

                                TempData["ajusteexitoso"] = true;

                                return RedirectToAction("agregar_inventario");
                            }
                            else {
                                ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Favor ingrese solo números en cantidad para realizar el incremento de inventario.";
                                return View("agregar_inventario", productos);
                            }

                        }
                        else
                        {

                            ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Favor ingrese una cantidad para realizar el incremento de inventario.";
                            return View("agregar_inventario", productos);
                        }

                    }

                }
                else
                {
                    ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>No se ha ingresado producto para realizar el incremento de inventario,favor ingrese uno.";
                    return View("agregar_inventario", productos);
                }


            }
            catch (Exception)
            {

                throw;
            }

        }


        public ActionResult ajuste_inventario()
        {
            if (TempData["ajusteexitoso"]!=null) {
                ViewBag.mensajeexito = "Se realizo ajuste de inventario satisfactoriamente";
            }

            return View();
      
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ajuste_inventario(int? cod_producto)
        {
            try
            {
                productos obtener_producto = null;
                if (cod_producto != 0 && cod_producto != null)
                {
                    if (Regex.IsMatch(cod_producto.ToString(), patronsindecimales))
                    {
                        TempData["cod_producto"] = cod_producto;
                        obtener_producto = db.productos.Where(x => x.Codigo_producto == cod_producto).FirstOrDefault();

                        if (obtener_producto != null)
                        {
                            return View(obtener_producto);

                        }
                        else
                        {

                            ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>No se encontro Producto";
                            return View(obtener_producto);
                        }
                    }
                    else {
                        ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Favor ingresar solo números en codigo de producto";
                        return View(obtener_producto);
                    }

                }
                else
                {

                    ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Favor ingresar codigo de producto";
                    return View(obtener_producto);

                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        // POST: productos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult procesar_ajuste_inventario(productos productos, int? agregar_cantidad)
        {
            int? cod_producto = 0;
            int prodcantanterior = 0;
         
            try
            {
           
                    if (TempData["cod_producto"] != null)
                    {
                    cod_producto = (int)TempData["cod_producto"];
                    TempData["cod_producto"] = cod_producto;
                    ViewBag.cod_producto = (int)cod_producto;
                    productos datosproducto = db.productos.Where(x => x.Codigo_producto == cod_producto).FirstOrDefault();

                    if (datosproducto == null )
                    {
                        ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>No se encontro producto";
                       
                        return View("ajuste_inventario", productos);
                    }
                    else
                    {
                        if (agregar_cantidad != null)
                        {
                            if (Regex.IsMatch(agregar_cantidad.ToString(), patronsindecimales))
                            {
                                prodcantanterior = Convert.ToInt32(datosproducto.Cantidad_actual);
                                datosproducto.Cantidad_actual = agregar_cantidad;
                                historial_inventario historialinventario = new historial_inventario
                                {
                                    Fecha_alta = DateTime.Now,
                                    Usuario_alta = (string)Session["usuario_logueado"],
                                    Idproducto = datosproducto.Idproducto,
                                    Tipo_movimiento = 3,
                                    Iddepartamento = datosproducto.Iddepartamento,
                                    Cantidad_actual = Convert.ToInt32(datosproducto.Cantidad_actual),
                                    Cantidad_anterior = prodcantanterior,
                                    Estado = 1
                                };
                                db.historial_inventario.Add(historialinventario);
                                db.SaveChanges();

                                TempData["ajusteexitoso"] = true;

                                return RedirectToAction("ajuste_inventario");
                            }
                            else {
                                ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Favor ingrese solo números en cantidad para realizar el ajuste de inventario.";
                                return View("ajuste_inventario", productos);
                            }

                        }
                        else {

                            ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Favor ingrese una cantidad para realizar el ajuste de inventario.";
                            return View("ajuste_inventario", productos);
                        }

                    }

                }
                    else {
                            ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>No se ha ingresado producto para realizar el ajuste de inventario,favor ingrese uno.";
                            return View("ajuste_inventario", productos);
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
