using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Monografia.Models;
using System.Text;
using SelectPdf;
using System.IO;
using System.Threading.Tasks;

namespace Monografia.Controllers
{
    public class FacturacionController : Controller
    {
        private proyectotiendaEntities db = new proyectotiendaEntities();
        static Modelo_contenedor.Factura modelocontenedor = new Modelo_contenedor.Factura();
        Modelo_contenedor.Ticket Ticket = null;
        Modelo_contenedor.producto producto = null;
        List<Modelo_contenedor.producto> listaproductos = null;
        correlativos datoscorrelativo = null;
        private pagos pagos = null;
        private factura factura = null;
        private detalle_factura detallefact = null;

        // GET: facturas
        public ActionResult Venta()
        {
            if (modelocontenedor.listatickets == null || modelocontenedor.listatickets.Count() == 0)
            {

                modelocontenedor.listatickets = new List<Modelo_contenedor.Ticket>();
                Ticket = new Modelo_contenedor.Ticket();
                listaproductos = new List<Modelo_contenedor.producto>();
                Ticket.Numero_ticket = 1;
                Ticket.listaproductos = listaproductos;
                modelocontenedor.listatickets.Add(Ticket);
                Session["ticketactivo"] = Ticket.Numero_ticket;
                Session["calculototalpago"] = listaproductos.Sum(x => x.Impor);
                Session["cantproducto"] = listaproductos.Sum(x => x.Cant);
            }

            return View(modelocontenedor);

        }

        [HttpPost]
        public ActionResult Venta(FormCollection fc)
        {
            var action = fc["action"];

            if (action == "agregarticket")
            {
                modelocontenedor = agregar_ticket(modelocontenedor);
            }

            return View(modelocontenedor);

        }

        public Modelo_contenedor.Factura agregar_ticket(Modelo_contenedor.Factura modelocontenedor)
        {
            if (modelocontenedor.listatickets.Count() < 6)
            {
                Ticket = new Modelo_contenedor.Ticket();
                List<Modelo_contenedor.producto> listaproductos = new List<Modelo_contenedor.producto>();
                if (modelocontenedor.listatickets == null)
                {
                    modelocontenedor.listatickets = new List<Modelo_contenedor.Ticket>();
                }

                Ticket.Numero_ticket = modelocontenedor.listatickets.Select(x => x.Numero_ticket).LastOrDefault() + 1;
                Ticket.listaproductos = listaproductos;
                modelocontenedor.listatickets.Add(Ticket);
                Session["ticketactivo"] = Ticket.Numero_ticket;
                Session["calculototalpago"] = listaproductos.Sum(x => x.Impor);
                Session["cantproducto"] = listaproductos.Sum(x => x.Cant);
            }

            return modelocontenedor;
        }

        // GET: facturas/Edit/5
        public ActionResult Facturar(int numticket)
        {
            ViewBag.numticket = numticket;
            var datosfacturar = modelocontenedor.listatickets.Where(x => x.Numero_ticket == numticket).FirstOrDefault();
            ViewBag.totalpago = datosfacturar.listaproductos.Sum(x => x.Impor).ToString();

            return PartialView();
        }

        // POST: facturas/Edit/5
        [HttpPost]
        public ActionResult Facturar(int numeroticket, decimal montopago)
        {
            decimal montototalfact = 0;
            pagos = new pagos();
            factura = new factura();
            datoscorrelativo = new correlativos();
            datoscorrelativo = db.correlativos.Where(x => x.idCorrelativo_factura == 1 && x.Estado == 1).FirstOrDefault();
            if (datoscorrelativo == null)
            {
                datoscorrelativo.Correlativo_factura = 1;
                datoscorrelativo.Estado = 1;
                db.correlativos.Add(datoscorrelativo);
                db.SaveChanges();
            }
            else
            {
                datoscorrelativo.Correlativo_factura = datoscorrelativo.Correlativo_factura + 1;
                db.SaveChanges();
            }

            var datosafacturar = modelocontenedor.listatickets.Where(x => x.Numero_ticket == numeroticket).FirstOrDefault();
            montototalfact = datosafacturar.listaproductos.Sum(x => x.Impor);



            //datos Factura
            factura.Fecha_alta = DateTime.Now;
            factura.Usuario_alta = (string)Session["usuario_logueado"];
            factura.Num_factura = datoscorrelativo.Correlativo_factura;
            factura.Monto_total = montototalfact;
            factura.Estado = 1;
            db.factura.Add(factura);

            //Detalle de factura
            foreach (var item in datosafacturar.listaproductos)
            {
                detallefact = new detalle_factura();
                detallefact.Fecha_alta = DateTime.Now;
                detallefact.Usuario_alta = (string)Session["usuario_logueado"];
                detallefact.Id_factura = factura.Idfactura;
                detallefact.Idproducto = db.productos.Where(x => x.Codigo_producto == item.CodProd).FirstOrDefault().Idproducto;
                detallefact.Cantidad = item.Cant;
                detallefact.Monto = item.Impor;
                detallefact.Estado = 1;
                factura.detalle_factura.Add(detallefact);
            }


            //pago realizado
            pagos.Fecha_alta = DateTime.Now;
            pagos.Usuario_alta = (string)Session["usuario_logueado"];
            pagos.Id_factura = factura.Idfactura;
            pagos.Monto_pagado = montopago;
            pagos.Estado = 1;
            factura.pagos.Add(pagos);
            db.SaveChanges();

            //se elimina el ticket al realizar pago de factura
            modelocontenedor.listatickets.Remove(datosafacturar);

            return Json(new { success = true });
        }

        // GET: facturas/Delete/5
        public ActionResult Delete(int codigoproducto, int numeroticket)
        {
            ViewBag.numticket = numeroticket;
            ViewBag.codproducto = codigoproducto;
            Modelo_contenedor.Factura datoseliminar = new Modelo_contenedor.Factura();
            var ticket = modelocontenedor.listatickets.Where(x => x.Numero_ticket == numeroticket).FirstOrDefault();
            var producto = ticket.listaproductos.Where(x => x.CodProd == codigoproducto).FirstOrDefault();
            datoseliminar.listatickets = new List<Modelo_contenedor.Ticket>();
            datoseliminar.listatickets.Add(ticket);
            foreach (var item in datoseliminar.listatickets)
            {
                item.listaproductos = new List<Modelo_contenedor.producto>();
                item.listaproductos.Add(producto);
            }
            return PartialView(datoseliminar);
        }

        // POST: facturas/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int numeroticket, int codigoproducto)
        {

            var ticket = modelocontenedor.listatickets.Where(x => x.Numero_ticket == numeroticket).FirstOrDefault();
            var datosproducto = ticket.listaproductos.Where(x => x.CodProd == codigoproducto).FirstOrDefault();
            if (datosproducto != null)
            {
                if (datosproducto.Cant == 1)
                {
                    ticket.listaproductos.Remove(datosproducto);
                }
                else
                {
                    datosproducto.Cant = datosproducto.Cant - 1;
                    datosproducto.Impor = datosproducto.Cant * datosproducto.prec_vent;
                }
                Session["ticketactivo"] = ticket.Numero_ticket;
                Session["calculototalpago"] = ticket.listaproductos.Sum(x => x.Impor);
                Session["cantproducto"] = ticket.listaproductos.Sum(x => x.Cant);
            }

            return Json(new { success = true });
        }

        public ActionResult Agregarproducto(int numticket)
        {
            ViewBag.numticket = numticket;
            return PartialView();
        }

        [HttpPost]
        public ActionResult Agregarproducto(int numticket, int codproducto)
        {

            var existeproducto = (from x in db.productos where x.Codigo_producto == codproducto select x).FirstOrDefault();
            if (existeproducto != null)
            {
                var ticket = modelocontenedor.listatickets.Where(x => x.Numero_ticket == numticket).FirstOrDefault();
                var listaproductos = ticket.listaproductos.Where(x => x.CodProd == codproducto).FirstOrDefault();
                if (listaproductos != null)
                {
                    listaproductos.Cant = listaproductos.Cant + 1;
                    listaproductos.Impor = listaproductos.Cant * existeproducto.Precio_venta;

                }
                else
                {
                    producto = new Modelo_contenedor.producto();
                    producto.CodProd = existeproducto.Codigo_producto;
                    producto.Desc = existeproducto.Descripcion;
                    producto.prec_vent = existeproducto.Precio_venta;
                    producto.Cant = 1;
                    producto.Impor = 1 * existeproducto.Precio_venta;
                    producto.existencia = 20;
                    ticket.listaproductos.Add(producto);
                }
                Session["ticketactivo"] = ticket.Numero_ticket;
                Session["calculototalpago"] = ticket.listaproductos.Sum(x => x.Impor);
                Session["cantproducto"] = ticket.listaproductos.Sum(x => x.Cant);
            }
            return Json(new { success = true });

        }

        public ActionResult Lista_Facturas()
        {
            return View(db.factura.ToList());
        }



        public ActionResult impresionfactura(int idfactura)
        {
            ViewBag.idfactura = idfactura;
            TempData["idfactura"] = idfactura;
            return PartialView();
        }

        [HttpPost]
        public JsonResult downloadpdf(int idfactura )
        {
            factura = new factura();
            factura = db.factura.Where(x => x.Idfactura == idfactura).FirstOrDefault();

            var Nombrearchivo = "Factura" + factura.Num_factura + DateTime.Now.Year + DateTime.Now.Day + DateTime.Now.Second + ".pdf";
     
            return Json(new { filename = Nombrearchivo, message = Convert.ToBase64String(crearfactura(factura)) });
        }

        public  ActionResult getpdf(int idfactura)
        {
            factura = new factura();
            factura = db.factura.Where(x => x.Idfactura == idfactura).FirstOrDefault();

            return new FileContentResult(crearfactura(factura), "application/pdf");
        }

        public byte[] crearfactura(factura datosfactura)
        {

            string htmldocfactura = "";
            var formatofactura = db.formato_factura.Where(x => x.idformato_factura == 1 && x.Estado == 1).FirstOrDefault();
            htmldocfactura = formatofactura.Cabeza_factura;

            StringBuilder tabledatosfactura = new StringBuilder();

            tabledatosfactura.Append("<table>");
            tabledatosfactura.Append("<thead>");
            tabledatosfactura.Append("<tr>");
            tabledatosfactura.Append("<th>Producto</th>");
            tabledatosfactura.Append("<th>Descripcion</th>");
            tabledatosfactura.Append("</tr>");
            tabledatosfactura.Append("</thead>");
            tabledatosfactura.Append("<tbody>");
            tabledatosfactura.Append("<tr>");

            foreach (var item in datosfactura.detalle_factura)
            {
                tabledatosfactura.Append("<td>" + item.productos.Codigo_producto + "</td>");
                tabledatosfactura.Append("<td>" + item.productos.Descripcion + "</td>");
            }
            tabledatosfactura.Append("</tr>");
            tabledatosfactura.Append("</tbody>");

            tabledatosfactura.Append("</table>");
            htmldocfactura += tabledatosfactura;
            htmldocfactura += formatofactura.Cuerpo_factura;


            HtmlToPdf convertidor = new HtmlToPdf();
            convertidor.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
            convertidor.Options.MarginLeft = 40;
            convertidor.Options.MarginRight = 40;
            convertidor.Options.MarginTop = 20;
            PdfDocument doc = convertidor.ConvertHtmlString(htmldocfactura);

            //bytes de doc con select pdf
            byte[] pdfBytes = doc.Save();

            return pdfBytes;


        }

        [HttpGet]
        public ActionResult getsesiones()
        {
            return Json(new { ticketactivo = (int)Session["ticketactivo"], Totalpago = (decimal)Session["calculototalpago"], cantproducto = (int)Session["cantproducto"] }, JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public ActionResult calculototalpago(int numticket)
        {
            var datosfacturar = modelocontenedor.listatickets.Where(x => x.Numero_ticket == numticket).FirstOrDefault();
            Session["calculototalpago"] = datosfacturar.listaproductos.Sum(x => x.Impor);
            Session["cantproducto"] = datosfacturar.listaproductos.Sum(x => x.Cant);
            Session["ticketactivo"] = numticket;
            Session["cantproducto"] = datosfacturar.listaproductos.Sum(x => x.Cant);
            return Json(new { Totalpago = (decimal)Session["calculototalpago"], cantproducto = (int)Session["cantproducto"] }, JsonRequestBehavior.AllowGet);
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
