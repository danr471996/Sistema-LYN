using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Monografia.Models;
using ClosedXML.Excel;
using System.IO;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.Owin.Security.Provider;
using System.Text.RegularExpressions;

namespace Monografia.Controllers
{
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


        public FileResult exportarexcel()
        {
            int numcol = 5, numcol2 = 5;
            string nombrearchivo = "";
            string vistaaccion = this.Request.UrlReferrer.AbsolutePath;
            DataTable dt = new DataTable();
       
            try
            {
                if (vistaaccion.Contains("productos_bajos_inventario"))
                {

                    var listaproductos = db.productos.Include(a => a.departamento).Where(x => x.Cantidad_actual < x.Cantidad_minima).ToList();

                    nombrearchivo = "Productos bajos en inventario " + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".xlsx";

                    dt.Columns.AddRange(new DataColumn[6] { new DataColumn("Código de producto",typeof(int)),
                                            new DataColumn("Descripción",typeof(string)),
                                            new DataColumn("Precio de venta",typeof(int)),
                                            new DataColumn("Cantidad actual",typeof(int)),
                                            new DataColumn("Cantidad mínima",typeof(int)),
                                            new DataColumn("Descripción de departamento",typeof(string))});


                    foreach (var item in listaproductos)
                    {
                        dt.Rows.Add(item.Codigo_producto, item.Descripcion, item.Precio_venta, item.Cantidad_actual, item.Cantidad_minima, item.departamento.Descripcion);
                    }
              
                }
                else
                {
                    int codigodepart = Convert.ToInt32(TempData["cod_depart"]);
                    TempData["cod_depart"] = codigodepart;
                    var listaproductos = int.Parse(TempData["cod_depart"].ToString()) == 0 ? db.productos.Include(a => a.departamento).ToList() : db.productos.Include(a => a.departamento).Where(x => x.Iddepartamento == codigodepart).ToList();



                    nombrearchivo = "Reporte de inventario " + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".xlsx";

                    dt.Columns.AddRange(new DataColumn[6] { new DataColumn("Código de producto",typeof(int)),
                                            new DataColumn("Descripción",typeof(string)),
                                            new DataColumn("Costo",typeof(int)),
                                            new DataColumn("Precio de venta",typeof(int)),
                                            new DataColumn("Cantidad actual",typeof(int)),
                                            new DataColumn("Cantidad mínima",typeof(int))});

                        foreach (var item in listaproductos)
                        {
                            dt.Rows.Add(item.Codigo_producto, item.Descripcion, item.Precio_costo, item.Precio_venta, item.Cantidad_actual, item.Cantidad_minima);
                        }

                }
            
                using (XLWorkbook wb = new XLWorkbook())
                {
                    var agregarestilo = wb.Worksheets.Add("Listado de productos");
                    if (vistaaccion.Contains("productos_bajos_inventario"))
                    {
                        for (int i = 0; dt.Rows.Count == 0 ? i == 0 : i < dt.Rows.Count; i++)
                        {

                            agregarestilo.Cell("A" + numcol).Value = "Código de producto";
                            agregarestilo.Cell("B" + numcol).Value = "Descripción";
                            agregarestilo.Cell("C" + numcol).Value = "Precio de venta";
                            agregarestilo.Cell("D" + numcol).Value = "Cantidad actual";
                            agregarestilo.Cell("E" + numcol).Value = "Cantidad mínima";
                            agregarestilo.Cell("F" + numcol).Value = "Descripción departamento";
                        }
                        if (dt.Rows.Count != 0)
                            foreach (DataRow row in dt.Rows)
                            {
                                numcol2 += 1;
                                agregarestilo.Cell("A" + numcol2).Value = row["Código de producto"].ToString();
                                agregarestilo.Cell("B" + numcol2).Value = row["Descripción"].ToString();
                                agregarestilo.Cell("C" + numcol2).Value = row["Precio de venta"].ToString();
                                agregarestilo.Cell("D" + numcol2).Value = row["Cantidad actual"].ToString();
                                agregarestilo.Cell("E" + numcol2).Value = row["Cantidad mínima"].ToString();
                                agregarestilo.Cell("F" + numcol2).Value = row["Descripción de departamento"].ToString();

                            }
                        else
                            numcol2 += 1;
                      

                    }
                    else
                    {
                        for (int i = 0; dt.Rows.Count == 0 ? i == 0 : i < dt.Rows.Count; i++)
                        {

                            agregarestilo.Cell("A" + numcol).Value = "Código de producto";
                            agregarestilo.Cell("B" + numcol).Value = "Descripción";
                            agregarestilo.Cell("C" + numcol).Value = "Costo";
                            agregarestilo.Cell("D" + numcol).Value = "Precio de venta";
                            agregarestilo.Cell("E" + numcol).Value = "Cantidad actual";
                            agregarestilo.Cell("F" + numcol).Value = "Cantidad mínima";
                        }
                        if (dt.Rows.Count != 0)
                            foreach (DataRow row in dt.Rows)
                        {
                            numcol2 += 1;
                            agregarestilo.Cell("A" + numcol2).Value = row["Código de producto"].ToString();
                            agregarestilo.Cell("B" + numcol2).Value = row["Descripción"].ToString();
                            agregarestilo.Cell("C" + numcol2).Value = row["Costo"].ToString();
                            agregarestilo.Cell("D" + numcol2).Value = row["Precio de venta"].ToString();
                            agregarestilo.Cell("E" + numcol2).Value = row["Cantidad actual"].ToString();
                            agregarestilo.Cell("F" + numcol2).Value = row["Cantidad mínima"].ToString();
                            ;

                        }
                         else
                            numcol2 += 1;
                      

                    }



                    agregarestilo.Columns().AdjustToContents();

                    agregarestilo.Range("A1:F1")
                                  .Merge()
                                  .SetValue(vistaaccion.Contains("productos_bajos_inventario") ? "REPORTE DE PRODUCTOS BAJOS EN INVENTARIO" : "REPORTE DE INVENTARIO")
                                  .Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center)
                                  .Font.Bold = true;

                    agregarestilo.Range("A2:F2")
                                  .Merge()
                                  .SetValue("REPORTE EFECTUADO POR: " + (string)Session["usuario_logueado"])
                                  .Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center)
                                  .Font.Bold = true;

                    agregarestilo.Range("A3:F3")
                                  .Merge()
                                  .SetValue("FECHA DE REPORTE: " + DateTime.Now.Date.ToString("dd-MM-yyyy"))
                                  .Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center)
                                  .Font.Bold = true;

                    agregarestilo.Range("A4:F4")
                                  .Merge()
                                  .SetValue("")
                                  .Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                    if (dt.Rows.Count == 0)
                        agregarestilo.Range("A6:F" + numcol2)
                               .Merge()
                               .SetValue("No existe información")
                               .Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                    var ranguito = agregarestilo.Range("A5:F5");
                    ranguito.Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                    ranguito.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    ranguito.Style.Fill.BackgroundColor = XLColor.BlueGray;
                    ranguito.Style.Font.FontColor = XLColor.White;
                    ranguito.Style.Font.Bold = true;

                    var ranguito2 = agregarestilo.Range("A6:F" + numcol2);
                    ranguito2.Style.Fill.BackgroundColor = XLColor.AliceBlue;
                    ranguito2.Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                    ranguito2.Style.Border.SetInsideBorder(XLBorderStyleValues.Thin);



                    using (MemoryStream stream = new MemoryStream())
                    {

                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nombrearchivo);
                    }
                }
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
