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
    public class FacturacionController : Controller
    {
        private proyectotiendaEntities db = new proyectotiendaEntities();
        private Modelo_contenedor.facturacion modelofactura = null;
        private List<Modelo_contenedor.facturacion> listamodelofactura = new List<Modelo_contenedor.facturacion>();
        public pagos pagos = null;
        private factura factura = null;
        private detalle_factura detallefact=null;
        
        // GET: facturas
        public ActionResult Venta()
        {
          
            listamodelofactura = new List<Modelo_contenedor.facturacion>();
            if (Session["ListaFactura"] != null)
            {
                listamodelofactura = (List<Modelo_contenedor.facturacion>) Session["ListaFactura"];
                return View(listamodelofactura);
            }
            else
            {

                return View();



            }


        }

        [HttpPost]
        public ActionResult Venta(int codigoproducto)
        {
            modelofactura = new Modelo_contenedor.facturacion();
            listamodelofactura = new List<Modelo_contenedor.facturacion>();
            if (Session["ListaFactura"] != null)
            {
                listamodelofactura = (List<Modelo_contenedor.facturacion>)Session["ListaFactura"];
            }

            var producto = (from x in db.productos where x.Codigo_producto == codigoproducto select x).FirstOrDefault();
            if (producto!=null)
            {
                var existeenlista = listamodelofactura.Where(x => x.CodProd == producto.Codigo_producto).FirstOrDefault();
                if (existeenlista != null)
                {
                    existeenlista.Cant = existeenlista.Cant + 1;
                    existeenlista.Impor = existeenlista.Cant * producto.Precio_venta;

                }
                else
                {
                    modelofactura.CodProd = producto.Codigo_producto;
                    modelofactura.Desc = producto.Descripcion;
                    modelofactura.prec_vent = producto.Precio_venta;
                    modelofactura.Cant = 1;
                    modelofactura.Impor = 1 * producto.Precio_venta;
                    modelofactura.existencia = 20;
                    listamodelofactura.Add(modelofactura);
                }
                
                Session["ListaFactura"] = listamodelofactura;
                Session["totalpago"]= listamodelofactura.Sum(x => x.Impor).ToString();
                return View(listamodelofactura);
            }
            else
            {
                return View();
            }
         
        }

     

        // GET: facturas/Edit/5
        public ActionResult Facturar()
        {
            return PartialView();
        }

        // POST: facturas/Edit/5
        [HttpPost]
        public ActionResult Facturar(decimal montopago)
        {
            decimal montototalfact = 0;
            listamodelofactura = new List<Modelo_contenedor.facturacion>();
            pagos = new pagos();
            factura = new factura();
           

            if (Session["ListaFactura"] != null)
            {
                listamodelofactura = (List<Modelo_contenedor.facturacion>)Session["ListaFactura"];
            }

            montototalfact = listamodelofactura.Sum(x =>x.Impor);

            //datos Factura
            factura.Fecha_alta = DateTime.Now;
            factura.Usuario_alta = (string)Session["usuario_logueado"];
            factura.Num_factura = 1;
            factura.Monto_total = montototalfact;
            factura.Estado = 1;
            db.factura.Add(factura);
            db.SaveChanges();
            //Detalle de factura
            foreach (var item in listamodelofactura)
            {
                detallefact = new detalle_factura();
                detallefact.Fecha_alta = DateTime.Now;
                detallefact.Usuario_alta = (string)Session["usuario_logueado"];
                detallefact.Id_factura = factura.Idfactura;
                detallefact.Id_producto = db.productos.Where(x => x.Codigo_producto == item.CodProd).FirstOrDefault().Idproducto;
                detallefact.Cantidad = item.Cant;
                detallefact.Monto = item.Impor;
                detallefact.Estado = 1;
                db.detalle_factura.Add(detallefact);
                db.SaveChanges();
               
            }
           

            //pago realizado
            pagos.Fecha_alta = DateTime.Now;
            pagos.Usuario_alta = (string)Session["usuario_logueado"];
            pagos.Id_factura = factura.Idfactura;
            pagos.Monto_pagado = montopago;
            pagos.Estado = 1;
            db.pagos.Add(pagos);
            db.SaveChanges();
            return Json(new { success = true });
        }

        // GET: facturas/Delete/5
        public ActionResult Delete(int? codigoproducto)
        {
            if (codigoproducto == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            listamodelofactura = new List<Modelo_contenedor.facturacion>();
            modelofactura = new Modelo_contenedor.facturacion();
            if (Session["ListaFactura"] != null)
            {
                listamodelofactura = (List<Modelo_contenedor.facturacion>)Session["ListaFactura"];
                modelofactura = listamodelofactura.Where(x => x.CodProd == codigoproducto).FirstOrDefault();
            }

            return PartialView(modelofactura);
        }

        // POST: facturas/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int codigoproducto)
        {
            listamodelofactura = new List<Modelo_contenedor.facturacion>();
            if (Session["ListaFactura"] != null)
            {
                listamodelofactura = (List<Modelo_contenedor.facturacion>)Session["ListaFactura"];
                var existeenlista = listamodelofactura.Where(x => x.CodProd == codigoproducto).FirstOrDefault();
                listamodelofactura.Remove(existeenlista);
                Session["ListaFactura"] = listamodelofactura;
            }

            return Json(new { success = true });
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
