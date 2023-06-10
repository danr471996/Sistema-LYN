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

namespace Monografia.Controllers
{
    public class Admin_inventarioController : Controller
    {
        private proyectotiendaEntities db = new proyectotiendaEntities();

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
            Session["listaproductos"] = 0;
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
                    int codigodepart = Convert.ToInt32(Session["cod_depart"]);
                    var listaproductos = int.Parse(Session["cod_depart"].ToString()) == 0 ? db.productos.Include(a => a.departamento).ToList() : db.productos.Include(a => a.departamento).Where(x => x.Iddepartamento == codigodepart).ToList();



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
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {

                            agregarestilo.Cell("A" + numcol).Value = "Código de producto";
                            agregarestilo.Cell("B" + numcol).Value = "Descripción";
                            agregarestilo.Cell("C" + numcol).Value = "Precio de venta";
                            agregarestilo.Cell("D" + numcol).Value = "Cantidad actual";
                            agregarestilo.Cell("E" + numcol).Value = "Cantidad mínima";
                            agregarestilo.Cell("F" + numcol).Value = "Descripción departamento";
                        }
                        foreach (DataRow row in dt.Rows)
                        {
                            numcol2 += 1;
                            agregarestilo.Cell("A" + numcol2).Value = row["Código de producto"];
                            agregarestilo.Cell("B" + numcol2).Value = row["Descripción"].ToString();
                            agregarestilo.Cell("C" + numcol2).Value = row["Precio de venta"];
                            agregarestilo.Cell("D" + numcol2).Value = row["Cantidad actual"];
                            agregarestilo.Cell("E" + numcol2).Value = row["Cantidad mínima"];
                            agregarestilo.Cell("F" + numcol2).Value = row["Descripción de departamento"];

                        }



                    }
                    else
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {

                            agregarestilo.Cell("A" + numcol).Value = "Código de producto";
                            agregarestilo.Cell("B" + numcol).Value = "Descripción";
                            agregarestilo.Cell("C" + numcol).Value = "Costo";
                            agregarestilo.Cell("D" + numcol).Value = "Precio de venta";
                            agregarestilo.Cell("E" + numcol).Value = "Cantidad actual";
                            agregarestilo.Cell("F" + numcol).Value = "Cantidad mínima";
                        }
                        foreach (DataRow row in dt.Rows)
                        {
                            numcol2 += 1;
                            agregarestilo.Cell("A" + numcol2).Value = row["Código de producto"];
                            agregarestilo.Cell("B" + numcol2).Value = row["Descripción"].ToString();
                            agregarestilo.Cell("C" + numcol2).Value = row["Costo"];
                            agregarestilo.Cell("D" + numcol2).Value = row["Precio de venta"];
                            agregarestilo.Cell("E" + numcol2).Value = row["Cantidad actual"];
                            agregarestilo.Cell("F" + numcol2).Value = row["Cantidad mínima"];
                            ;

                        }
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

            Modelo_contenedor modelo_contenedor = new Modelo_contenedor();
            modelo_contenedor.listaproductos = new List<productos>();
            modelo_contenedor.listadepartamento = new List<departamento>();

            List<productos> listaproducto = new List<productos>();

            try
            {
                Session["cod_depart"] = productos.Iddepartamento;

                if (productos.Iddepartamento != 0)
                {
                    listaproducto = (from x in db.productos where x.Estado == 1 && x.Iddepartamento == productos.Iddepartamento select x).ToList();
                    int? cantidadtotal=listaproducto.Select(x => x.Cantidad_actual).Sum();
                    decimal? costo = listaproducto.Select(x => x.Precio_costo).Sum();
                    Session["cantidad_inventario"] = cantidadtotal.ToString();
                    Session["costo_inventario"] = (cantidadtotal * costo).ToString();
                }
                else
                {
                    listaproducto = (from x in db.productos where x.Estado == 1 select x).ToList();
                    int? cantidadtotal = listaproducto.Select(x => x.Cantidad_actual).Sum();
                    decimal? costo = listaproducto.Select(x => x.Precio_costo).Sum();
                    Session["cantidad_inventario"] = cantidadtotal.ToString();
                    Session["costo_inventario"] = (cantidadtotal * costo).ToString();
                }
        

               
                foreach (var item in listaproducto)
                {

                    modelo_contenedor.listaproductos.Add(item);
                }
                var listadepartamentos = from u in db.departamento where u.Estado == 1 select u;
          

                foreach (var item in listadepartamentos)
                {

                    modelo_contenedor.listadepartamento.Add(item);
                }


                return View(modelo_contenedor);
            }
            catch (Exception ex)
            {

                throw;
            }
          
        }
     

        public ActionResult editar_inventario(int? id, int id2)
        {
            Modelo_contenedor Modelo_contenedor = new Modelo_contenedor();
            try
            {
                if (id == null && id2 == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var productos = db.productos.Include(a => a.departamento).Where(x => x.Codigo_producto == id2).FirstOrDefault();

                Modelo_contenedor = new Modelo_contenedor();

                Modelo_contenedor.productos = productos;
                //producto = db.productos.Find(id);

                if (Modelo_contenedor == null)
                {
                    return HttpNotFound();
                }
                return PartialView(Modelo_contenedor);
            }
            catch (Exception ex)
            {

                throw;
            }
          

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult editar_inventario(productos productos, int? agregar_cantidad)
        {
            int prodcantanterior = 0;
            try
            {
                if (ModelState.IsValid)
                {

                    var producto = db.productos.Where(x => x.Codigo_producto == productos.Codigo_producto).FirstOrDefault();
                    if (producto!=null)
                    {
                        if (producto.Usa_inventario!=2)
                        {
                            prodcantanterior = producto.Cantidad_actual == null ? 0 : Convert.ToInt32(producto.Cantidad_actual);
                            producto.Cantidad_actual = producto.Cantidad_actual==null?0 + agregar_cantidad: producto.Cantidad_actual+ agregar_cantidad;
                            historial_inventario historialinventario = new historial_inventario();
                            historialinventario.Fecha_alta = DateTime.Now;
                            historialinventario.Usuario_alta = (string)Session["usuario_logueado"];
                            historialinventario.Idproducto = producto.Idproducto;
                            historialinventario.Tipo_movimiento = 1;
                            historialinventario.Iddepartamento = producto.Iddepartamento;
                            historialinventario.Cantidad_actual = Convert.ToInt32(producto.Cantidad_actual);
                            historialinventario.Cantidad_anterior = prodcantanterior;
                            historialinventario.Estado = 1;
                            db.historial_inventario.Add(historialinventario);
                            db.SaveChanges();
                            return Json(new { success = true });
                        }
                        else
                        {
                            return Json(new { success = false });
                        }
                    }
                    else
                    {
                        return Json(new { success = false });
                    }
                     
                }
                return PartialView(productos);
            }
            catch (Exception ex)
            {

                throw;
            }
        
        }


        public ActionResult agregar_inventario(int? id_producto)
        {
            try
            {
                Session["id_prod"] = 0;
                Session["id_prod"] = id_producto;
                productos obtener_producto = db.productos.Where(x => x.Codigo_producto == id_producto).FirstOrDefault();
                return View(obtener_producto);
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        // POST: productos/Create
        // 
        // GET: productos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult agregar_inventario2(productos productos, int? agregar_cantidad)
        {
            int? id_prod = 0;
            int prodcantanterior = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    if (Session["id_prod"] != null)
                    {
                        id_prod = (int)Session["id_prod"];
                    }
                    productos datosproducto = db.productos.Where(x => x.Codigo_producto == id_prod).FirstOrDefault();
                    if (datosproducto == null || agregar_cantidad == null)
                    {
                        return RedirectToAction("agregar_inventario");
                    }
                    else
                    {
                        if (agregar_cantidad != null)
                        {
                            prodcantanterior =  datosproducto.Cantidad_actual==null?0:Convert.ToInt32(datosproducto.Cantidad_actual);
                            datosproducto.Cantidad_actual = datosproducto.Cantidad_actual == null ? 0+ agregar_cantidad:datosproducto.Cantidad_actual + agregar_cantidad;
                            historial_inventario historialinventario = new historial_inventario();
                            historialinventario.Fecha_alta = DateTime.Now;
                            historialinventario.Usuario_alta = (string)Session["usuario_logueado"];
                            historialinventario.Idproducto = datosproducto.Idproducto;
                            historialinventario.Tipo_movimiento = 1;
                            historialinventario.Iddepartamento = datosproducto.Iddepartamento;
                            historialinventario.Cantidad_actual = Convert.ToInt32(datosproducto.Cantidad_actual);
                            historialinventario.Cantidad_anterior = prodcantanterior;
                            historialinventario.Estado = 1;
                            db.historial_inventario.Add(historialinventario);
                            db.SaveChanges();
                            return RedirectToAction("agregar_inventario");
                        }

                    }
                }

                return RedirectToAction("agregar_inventario");
            }
            catch (Exception)
            {

                throw;
            }
        
        }

        public ActionResult ajuste_inventario(int? id_producto)
        {
            try
            {
                Session["id_prod"] = 0;
                Session["id_prod"] = id_producto;
                productos obtener_producto = db.productos.Where(x => x.Codigo_producto == id_producto).FirstOrDefault();
                return View(obtener_producto);
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
        public ActionResult ajuste_inventario2(productos productos, int? agregar_cantidad)
        {
            int? id_prod = 0;
            int prodcantanterior = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    if (Session["id_prod"] != null)
                    {
                        id_prod = (int)Session["id_prod"];
                    }
                    productos datosproducto = db.productos.Where(x => x.Codigo_producto == id_prod).FirstOrDefault();
                    if (datosproducto == null || agregar_cantidad == null)
                    {
                        return RedirectToAction("ajuste_inventario");
                    }
                    else
                    {
                        if (agregar_cantidad != null)
                        {
                            prodcantanterior = datosproducto.Cantidad_actual == null ? 0 : Convert.ToInt32(datosproducto.Cantidad_actual);
                            datosproducto.Cantidad_actual = agregar_cantidad;
                            historial_inventario historialinventario = new historial_inventario();
                            historialinventario.Fecha_alta = DateTime.Now;
                            historialinventario.Usuario_alta = (string)Session["usuario_logueado"];
                            historialinventario.Idproducto = datosproducto.Idproducto;
                            historialinventario.Tipo_movimiento = 3;
                            historialinventario.Iddepartamento = datosproducto.Iddepartamento;
                            historialinventario.Cantidad_actual = Convert.ToInt32(datosproducto.Cantidad_actual);
                            historialinventario.Cantidad_anterior = prodcantanterior;
                            historialinventario.Estado = 1;
                            db.historial_inventario.Add(historialinventario);
                            db.SaveChanges();
                            return RedirectToAction("ajuste_inventario");
                        }

                    }
                }

                return RedirectToAction("ajuste_inventario");
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
