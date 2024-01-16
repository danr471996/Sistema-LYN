using Antlr.Runtime;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Ajax.Utilities;
using Monografia.Models;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using static Monografia.Models.Modelo_contenedor;

namespace Monografia.Controllers
{
    public class FacturacionController : Controller
    {
        private proyectotiendaEntities db = new proyectotiendaEntities();
        private Factura modelocontenedor = new Factura();
        Ticket Ticket = null;
        producto producto = null;
        List<producto> listaproductos = null;
        correlativos datoscorrelativo = null;
        private pagos pagos = null;
        private factura factura = null;
        private detalle_factura detallefact = null;
        private historial_inventario historialinvt = null;
        private movimientos movimientos = null;
        private creditos creditos = null;

        // GET: facturas
        public ActionResult Venta()
        {

            if (TempData["modelocontenedor"] != null) {
                modelocontenedor = (Factura)TempData["modelocontenedor"];
            }
            if (TempData["maxticket"] != null) {
                ViewBag.Mensaje = "No se puede agregar mas tickets, el limite total de tickets activos, es 6.";
            }
            if (modelocontenedor.listatickets == null || modelocontenedor.listatickets.Count() == 0)
            {

                modelocontenedor.listatickets = new List<Ticket>();
                Ticket = new Ticket();
                listaproductos = new List<producto>();
                Ticket.Numero_ticket = 1;
                Ticket.listaproductos = listaproductos;
                modelocontenedor.listatickets.Add(Ticket);
                Session["ticketactivo"] = Ticket.Numero_ticket;
                Session["calculototalpago"] = listaproductos.Sum(x => x.Impor);
                Session["cantproducto"] = listaproductos.Sum(x => x.Cant);
            }
            TempData["modelocontenedor"] = modelocontenedor;
            return View(modelocontenedor);

        }




    
        public ActionResult procesoticket()
        {
            modelocontenedor = (Factura)TempData["modelocontenedor"];

            modelocontenedor = agregar_ticket(modelocontenedor);
  

            TempData["modelocontenedor"] = modelocontenedor;

            return RedirectToAction("Venta");

        }

        public Factura agregar_ticket(Factura modelocontenedor)
        {
            if (modelocontenedor.listatickets.Count() < 6)
            {
                Ticket = new Ticket();
                List<producto> listaproductos = new List<producto>();
                if (modelocontenedor.listatickets == null)
                {
                    modelocontenedor.listatickets = new List<Ticket>();
                }

                Ticket.Numero_ticket = modelocontenedor.listatickets.Select(x => x.Numero_ticket).LastOrDefault() + 1;
                Ticket.listaproductos = listaproductos;
                modelocontenedor.listatickets.Add(Ticket);
                Session["ticketactivo"] = Ticket.Numero_ticket;
                Session["calculototalpago"] = listaproductos.Sum(x => x.Impor);
                Session["cantproducto"] = listaproductos.Sum(x => x.Cant);
            }
            else {
                TempData["maxticket"] = true;     
            }
            return modelocontenedor;
        }

        // GET: facturas/Edit/5
        public ActionResult Facturar(int? numticket)
        {
       

            modelocontenedor = (Factura)TempData["modelocontenedor"];
            ViewBag.numticket = numticket;

            if (numticket != 0 && numticket != null)
            {

                var datosfacturar = modelocontenedor.listatickets.Where(x => x.Numero_ticket == numticket).FirstOrDefault();

                if (datosfacturar != null)
                {
                    if (datosfacturar.listaproductos != null && datosfacturar.listaproductos.Count > 0)
                    {
                        var Listaclientes = db.clientes.Where(x => x.Estado == 1).ToList();

                        validaciones(datosfacturar, Listaclientes);

                        TempData["modelocontenedor"] = modelocontenedor;

                        return PartialView(Listaclientes);
                    }
                    else
                    {
                        ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>No hay productos para facturar en este ticket";
                        TempData["modelocontenedor"] = modelocontenedor;
                        return PartialView();

                    }
                }
                else
                {
                    ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>No se encontro numero de ticket";
                    TempData["modelocontenedor"] = modelocontenedor;
                    return PartialView();

                }



            }
            else
            {
                ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Numero de ticket erroneo";
                TempData["modelocontenedor"] = modelocontenedor;
                return PartialView();

            }


        }
        public void validaciones(Ticket datosfacturar,List<clientes> Listaclientes)
        {
            if (Listaclientes.Count == 0)
            {
                ViewBag.Listclientact += "<i class='bi bi-exclamation-octagon me-1'></i>No hay clientes activos con disponibilidad de credito";
            }
            List<string> listaopciones = new List<string> { "OP1_FDP", "OP2_FDP", "OP3_FDP" };
            db.opciones.Where(x => listaopciones.Contains(x.ID_OP)).ToList().ForEach(opcion => {

                if (opcion.ID_OP == "OP1_FDP" && opcion.SELECCIONADO_OP == true)
                {

                    ViewBag.cobromenorventa = "No se permite cobrar si el efecto ingresado es menor que el total de la venta, deshabilitar en configuraciones";
                    TempData["cobromenorventa"] = true;

                }
                if (opcion.ID_OP == "OP2_FDP" && opcion.SELECCIONADO_OP == false)
                {
                    ViewBag.cobrodolar = "No se puede cobrar en dolares, habilitelo en configuraciones";
                    TempData["cobrodolar"] = true;
                }
                if (opcion.ID_OP == "OP3_FDP" && opcion.SELECCIONADO_OP == false)
                {
                    ViewBag.cobrotransferencia = "No se puede cobrar por medio de transferencias, habilitelo en configuraciones";
                }

            });
            var datoscambiodolar = db.cambiodolar.Where(x => /*x.Fecha_alta.Day == DateTime.Now.Day &&*/ x.Estado == 1).FirstOrDefault();
            ViewBag.totalpago = "<input type='text' id='montototalpago' class='form-control' value='" + datosfacturar.listaproductos.Sum(x => x.Impor).ToString() + "' readonly>";
            ViewBag.totalpagodolar = "<input type='text' id='montototaldolar' class='form-control' value='" + datosfacturar.listaproductos.Sum(x => x.Impor).ToString() + "' readonly>";
            ViewBag.cambiodolar = "<input type='text' id='tipocambiodolar' class='form-control' value='" + datoscambiodolar.Monto_cambio.ToString() + "' readonly>";

        }
        // POST: facturas/Edit/5
        [HttpPost]
        public ActionResult Facturar(int? numeroticket, decimal? montopago,decimal? montovuelto,int? idcliente, decimal? montopagodolar,decimal? montovueltocordobas)
        {
            modelocontenedor = (Factura)TempData["modelocontenedor"];
            int cantidadmetodos = 0;
            ViewBag.numticket = numeroticket;
            var Listaclientes = db.clientes.Where(x => x.Estado == 1).ToList();
            var datoscambiodolar = db.cambiodolar.Where(x => /*x.Fecha_alta.Day == DateTime.Now.Day &&*/ x.Estado == 1).FirstOrDefault();

            if (numeroticket != 0 && numeroticket != null)
            {
                validaciones(modelocontenedor.listatickets.Where(x => x.Numero_ticket == numeroticket).FirstOrDefault(), Listaclientes);

                if (montopago == null && idcliente == null && montopagodolar == null)
                {
                    TempData["modelocontenedor"] = modelocontenedor;
                    ViewBag.metodospago += "<i class='bi bi-exclamation-octagon me-1'></i>Debe seleccionar un metodo de pago,favor verifique";
                    return PartialView(Listaclientes);
                }


                if ((montopago != 0 && montopago != null))
                    cantidadmetodos++;
                if ((idcliente != 0 && idcliente != null))
                    cantidadmetodos++;
                if ((montopagodolar != 0 && montopagodolar != null))
                    cantidadmetodos++;


                if (cantidadmetodos > 1)
                {
                    TempData["modelocontenedor"] = modelocontenedor;
                    ViewBag.metodospago += "<i class='bi bi-exclamation-octagon me-1'></i>Debe seleccionar solo un metodo de pago,favor verifique";
                    return PartialView(Listaclientes);
                }
                else
                {
                    decimal montototalfact = 0;
                    int prodcantanterior = 0;
                    pagos = new pagos();
                    factura = new factura();
                    datoscorrelativo = new correlativos();
                    var datosafacturar = modelocontenedor.listatickets.Where(x => x.Numero_ticket == numeroticket).FirstOrDefault();
                    montototalfact = datosafacturar.listaproductos.Sum(x => x.Impor);

                    if (TempData["cobromenorventa"] != null) {
                        if (montopago != null) {
                            if (montopago < montototalfact) {
                                TempData["modelocontenedor"] = modelocontenedor;
                                ViewBag.cobroesmenor = true;
                                return PartialView(Listaclientes);
                            }
                        }

                    }
                    if (TempData["cobromenorventa"] != null)
                    {
                                if (TempData["cobrodolar"] != null)
                            {
                                if (montopagodolar != null)
                                {
                                    if ((montopagodolar* datoscambiodolar.Monto_cambio) < montototalfact )
                                    {
                                        TempData["modelocontenedor"] = modelocontenedor;
                                    ViewBag.cobroesmenor = true;
                                    return PartialView(Listaclientes);
                                    }
                                }

                            }
                    }


                    if (idcliente != 0)
                    {
                        var datoscliente = db.clientes.Where(x => x.Idcliente == idcliente).FirstOrDefault();

                        //se valida si es tipo credito limitado
                        if (datoscliente.Id_tipocredito == 2)
                        {
                            if (datoscliente.Cantidad_credito < montototalfact)
                            {
                                TempData["modelocontenedor"] = modelocontenedor;
                               /* return Json(new { success = false, mensaje = "Cliente no tiene suficiente crédito para realizar esta compra,favor verifique" });*/
                                ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Cliente no tiene suficiente crédito para realizar esta compra,favor verifique";
                        
                                return PartialView(Listaclientes);
                            }
                            else
                            {
                                datoscliente.Cantidad_credito = Convert.ToInt32(datoscliente.Cantidad_credito - montototalfact);
                            }

                        }
                    }

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
                        datoscorrelativo.Correlativo_factura++;
                        db.SaveChanges();
                    }





                    //datos generales de Factura
                    factura.Fecha_alta = DateTime.Now;
                    factura.Usuario_alta = (string)Session["usuario_logueado"];
                    factura.Num_factura = datoscorrelativo.Correlativo_factura;
                    factura.Monto_total = montototalfact;
                    if (idcliente != 0)
                    {
                        factura.Idcliente = idcliente;
                        factura.Estado = 2;
                    }
                    else
                        factura.Estado = 1;
                    db.factura.Add(factura);

                    //Detalle de factura
                    foreach (var item in datosafacturar.listaproductos)
                    {
                        var datosproducto = db.productos.Where(x => x.Codigo_producto == item.CodProd).FirstOrDefault();
                        prodcantanterior = Convert.ToInt32(datosproducto.Cantidad_actual);
                        datosproducto.Cantidad_actual = datosproducto.Cantidad_actual - item.Cant;

                        //detalle de la factura, aqui van los productos
                        detallefact = new detalle_factura
                        {
                            Fecha_alta = DateTime.Now,
                            Usuario_alta = (string)Session["usuario_logueado"],
                            Id_factura = factura.Idfactura,
                            Idproducto = datosproducto.Idproducto,
                            Cantidad = item.Cant,
                            Monto = item.Impor,
                            Estado = 1
                        };

                        //Historial de inventario
                        historialinvt = new historial_inventario
                        {
                            Fecha_alta = DateTime.Now,
                            Usuario_alta = (string)Session["usuario_logueado"],
                            Idproducto = datosproducto.Idproducto,
                            Tipo_movimiento = 2,
                            Iddepartamento = datosproducto.Iddepartamento,
                            Cantidad_actual = Convert.ToInt32(datosproducto.Cantidad_actual),
                            Cantidad_anterior = prodcantanterior,
                            Estado = 1
                        };
                        db.historial_inventario.Add(historialinvt);

                        //Movimiento de la transaccion
                        movimientos = new movimientos
                        {
                            Fecha_alta = DateTime.Now,
                            Usuario_alta = (string)Session["usuario_logueado"],
                            Monto = montototalfact,
                            Tipo_movimiento = 1,
                            Tipo_pago = idcliente == 0 ? 2:1,
                            Idpago=pagos.Idpagos,
                            Estado = 1
                        };
                        db.movimientos.Add(movimientos);
                        factura.detalle_factura.Add(detallefact);
                    }

                    //pago realizado si idcliente es diferente de 0 es porque es una venta al credito
                    if (idcliente == 0)
                    {
                        pagos.Fecha_alta = DateTime.Now;
                        pagos.Usuario_alta = (string)Session["usuario_logueado"];
                        pagos.Id_factura = factura.Idfactura;
                        pagos.Monto_pagado = montopago;
                        pagos.Estado = 1;
                        factura.pagos.Add(pagos);
                    }
                    else
                    {
                        creditos = new creditos
                        {
                            Fecha_alta = DateTime.Now,
                            Usuario_alta = (string)Session["usuario_logueado"],
                            Idcliente = (int)idcliente,
                            Id_factura = factura.Idfactura,
                            Importe_total = factura.Monto_total,
                            Estado = 1
                        };
                        db.creditos.Add(creditos);


                    }
                    db.SaveChanges();

                    //se elimina el ticket al realizar pago de factura o puesta al credito
                    var ticketactivo = modelocontenedor.listatickets.Select(x => x.Numero_ticket).FirstOrDefault();
                    Session["ticketactivo"] = ticketactivo;
                    modelocontenedor.listatickets.Remove(datosafacturar);

                    TempData["modelocontenedor"] = modelocontenedor;

                    return Json(new { success = true, mensaje = "Se ha realizado el cobro de factura satisfactoriamente" });
                }




            }
            else
            {
                ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Numero de ticket erroneo";
                TempData["modelocontenedor"] = modelocontenedor;
                validaciones(modelocontenedor.listatickets.Where(x => x.Numero_ticket == numeroticket).FirstOrDefault(), Listaclientes);
                return PartialView(Listaclientes);

            }


        }

        // GET: facturas/Delete/5
        public ActionResult Delete(int? codigoproducto, int? numeroticket)
        {

            modelocontenedor = (Factura)TempData["modelocontenedor"];
            Factura datoseliminar = new Factura();
            ViewBag.numticket = numeroticket;
            ViewBag.codproducto = codigoproducto;
            try
            {
                if (codigoproducto != 0 && codigoproducto != null)
                {
                    if (numeroticket != 0 && numeroticket != null)
                    {
                        var ticket = modelocontenedor.listatickets.Where(x => x.Numero_ticket == numeroticket).FirstOrDefault();

                        if (ticket != null)
                        {

                           var producto = ticket.listaproductos.Where(x => x.CodProd == codigoproducto).FirstOrDefault();

                            if (producto != null)
                            {

                                datoseliminar.listatickets = new List<Ticket>
                                    {
                                        ticket
                                    };
                                foreach (var item in datoseliminar.listatickets)
                                {
                                    item.listaproductos = new List<producto>
                                    {
                                        producto
                                    };
                                }
                                TempData["modelocontenedor"] = modelocontenedor;

                                return PartialView(datoseliminar);


                            }
                            else
                            {
                                ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>No se encontro producto en la lista de compras";
                                TempData["modelocontenedor"] = modelocontenedor;
                                return PartialView(datoseliminar);
                            }
                        }
                        else {
                            ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>No se encontro numero de ticket";
                            TempData["modelocontenedor"] = modelocontenedor;
                            return PartialView(datoseliminar);

                        }
                      


                    }
                    else {
                        ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Numero de ticket erroneo";
                        TempData["modelocontenedor"] = modelocontenedor;
                        return PartialView(datoseliminar);

                    }
                }
                else
                {
                    ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Codigo de producto erroneo";
                    TempData["modelocontenedor"] = modelocontenedor;
                    return PartialView(datoseliminar);
                }


            }
            catch (Exception)
            {

                throw;
            }


        }

        // POST: facturas/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int? numeroticket, int? codigoproducto)
        {

            modelocontenedor = (Factura)TempData["modelocontenedor"];
            Factura datoseliminar = new Factura();
            ViewBag.numticket = numeroticket;
            ViewBag.codproducto = codigoproducto;
            try
            {
                if (codigoproducto != 0 && codigoproducto != null)
                {
                    if (numeroticket != 0 && numeroticket != null)
                    {
                        var ticket = modelocontenedor.listatickets.Where(x => x.Numero_ticket == numeroticket).FirstOrDefault();

                        if (ticket != null)
                        {

                            var producto = ticket.listaproductos.Where(x => x.CodProd == codigoproducto).FirstOrDefault();

                            if (producto != null)
                            {

                                if (producto.Cant == 1)
                                {
                                    ticket.listaproductos.Remove(producto);
                                }
                                else
                                {
                                    producto.Cant--;
                                    producto.Impor = producto.Cant * producto.prec_vent;
                                }
                                Session["ticketactivo"] = ticket.Numero_ticket;
                                Session["calculototalpago"] = ticket.listaproductos.Sum(x => x.Impor);
                                Session["cantproducto"] = ticket.listaproductos.Sum(x => x.Cant);
                                TempData["modelocontenedor"] = modelocontenedor;

                                return Json(new { success = true, mensaje = "Se ha eliminado producto del ticket satisfactoriamente" });


                            }
                            else
                            {
                                ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>No se encontro producto en la lista de compras";
                                TempData["modelocontenedor"] = modelocontenedor;
                                return PartialView(datoseliminar);
                            }
                        }
                        else
                        {
                            ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>No se encontro numero de ticket";
                            TempData["modelocontenedor"] = modelocontenedor;
                            return PartialView(datoseliminar);

                        }



                    }
                    else
                    {
                        ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Numero de ticket erroneo";
                        TempData["modelocontenedor"] = modelocontenedor;
                        return PartialView(datoseliminar);

                    }
                }
                else
                {
                    ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Codigo de producto erroneo";
                    TempData["modelocontenedor"] = modelocontenedor;
                    return PartialView(datoseliminar);
                }


            }
            catch (Exception)
            {

                throw;
            }

        }

        public ActionResult Deleteticket(int? numeroticket)
        {


            modelocontenedor = (Factura)TempData["modelocontenedor"];
            TempData["numticketeliminar"] = numeroticket;
            Factura datoseliminar = new Factura();
            try
            {

                    if (numeroticket != 0 && numeroticket != null)
                    {
                  
                    var ticket = modelocontenedor.listatickets.Where(x => x.Numero_ticket == numeroticket).FirstOrDefault();

                        if (ticket != null)
                        {

                        var producto = ticket.listaproductos.ToList();
                        datoseliminar.listatickets = new List<Ticket>
                            {
                                ticket
                            };
                        foreach (var item in datoseliminar.listatickets)
                        {
                            item.listaproductos = new List<producto>();
                            item.listaproductos = producto;
                        }
                        TempData["modelocontenedor"] = modelocontenedor;
                        return PartialView(datoseliminar);

                    }   
                        else
                        {
                            ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>No se encontro numero de ticket";
                            TempData["modelocontenedor"] = modelocontenedor;
                            return PartialView(datoseliminar);

                        }



                    }
                    else
                    {
                        ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Numero de ticket erroneo";
                        TempData["modelocontenedor"] = modelocontenedor;
                        return PartialView(datoseliminar);

                    }
            }
            catch (Exception)
            {

                throw;
            }
        }

        // POST: facturas/Delete/5
        [HttpPost]
        public ActionResult Deleteticket()
        {

            modelocontenedor = (Factura)TempData["modelocontenedor"];
            Ticket = new Ticket();
            Factura datoseliminar = new Factura();

            try
            {
                if (TempData["numticketeliminar"] != null)
                {

                        var ticket = modelocontenedor.listatickets.Where(x => x.Numero_ticket == (int)TempData["numticketeliminar"]).FirstOrDefault();

                        if (ticket != null)
                        {

                        Ticket.Numero_ticket = modelocontenedor.listatickets.Select(x => x.Numero_ticket).LastOrDefault() - 1;
                        modelocontenedor.listatickets.Remove(ticket);

                        Session["ticketactivo"] = Ticket.Numero_ticket;

                        TempData["modelocontenedor"] = modelocontenedor;

                        return Json(new { success = true, mensaje = "Se ha eliminado ticket satisfactoriamente" });
                    }
                        else
                        {
                            ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>No se encontro numero de ticket";
                            TempData["modelocontenedor"] = modelocontenedor;
                            return PartialView(datoseliminar);

                        }
                }
                else
                {
                    ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Numero de ticket erroneo";
                    TempData["modelocontenedor"] = modelocontenedor;
                    return PartialView(datoseliminar);
                }


            }
            catch (Exception)
            {

                throw;
            }


        }

        public ActionResult Agregarproducto(int? numticket)
        {

            if (numticket != 0 && numticket != null)
            {
                ViewBag.numticket = numticket;
                return PartialView();
            }
            else
            {
                ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Numero de ticket erroneo";
                return PartialView();

            }
        }

        [HttpPost]
        public ActionResult Agregarproducto(int? numticket, int? codproducto)
        {

 
            try
            {
                modelocontenedor = (Factura)TempData["modelocontenedor"];
                ViewBag.numticket = numticket;
                if (codproducto != 0 && codproducto != null)
                {
                    if (numticket != 0 && numticket != null)
                    {
                       
                        var existeproducto = (from x in db.productos where x.Codigo_producto == codproducto  select x).FirstOrDefault();

                        if (existeproducto != null)
                        {
                           
                            if (existeproducto.Estado == 1)
                            {
                                var ticket = modelocontenedor.listatickets.Where(x => x.Numero_ticket == numticket).FirstOrDefault();

                            if (ticket != null)
                            {

                                var listaproductos = ticket.listaproductos.Where(x => x.CodProd == codproducto).FirstOrDefault();

                                if (listaproductos != null)
                                {
                                    listaproductos.Cant++;
                                    listaproductos.Impor = listaproductos.Cant * existeproducto.Precio_venta;

                                }
                                else
                                {
                                    producto = new producto
                                    {
                                        CodProd = existeproducto.Codigo_producto,
                                        Desc = existeproducto.Descripcion,
                                        prec_vent = existeproducto.Precio_venta,
                                        Cant = 1,
                                        Impor = 1 * existeproducto.Precio_venta,
                                        existencia = 20
                                    };
                                    ticket.listaproductos.Add(producto);
                                }
                                Session["ticketactivo"] = ticket.Numero_ticket;
                                Session["calculototalpago"] = ticket.listaproductos.Sum(x => x.Impor);
                                Session["cantproducto"] = ticket.listaproductos.Sum(x => x.Cant);

                                TempData["modelocontenedor"] = modelocontenedor;

                                return Json(new { success = true, mensaje = "Se añadió producto satisfactoriamente" });

                            }
                            else
                            {
                                ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>No se encontro numero de ticket";
                                TempData["modelocontenedor"] = modelocontenedor;
                                return PartialView();

                                }
                            }
                            else
                            {
                                ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>No se encontro codigo de producto activo";
                                TempData["modelocontenedor"] = modelocontenedor;
                                return PartialView();

                            }
                        }
                        else
                        {
                            ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>No se encontro codigo de producto";
                            TempData["modelocontenedor"] = modelocontenedor;
                            return PartialView();

                        }


                    }
                    else
                    {
                        ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Numero de ticket erroneo";
                        TempData["modelocontenedor"] = modelocontenedor;
                        return PartialView();

                    }
                }
                else
                {
                    ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Codigo de producto erroneo";
                    TempData["modelocontenedor"] = modelocontenedor;
                    return PartialView();
                }


            }
            catch (Exception)
            {

                throw;
            }


        }

        public ActionResult Lista_Facturas()
        {
            return View(db.factura.ToList());
        }



        public ActionResult impresionfactura(int? idfactura)
        {
           
            if (idfactura != 0 && idfactura != null)
            {
                ViewBag.idfactura = idfactura;
                TempData["idfactura"] = idfactura;
                return PartialView();
            }
            else
            {
                ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Numero de ticket erroneo";
                TempData["idfactura"] = idfactura;
                return PartialView();

            }
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
            clientes datoscliente = null;
            string nomcliente = "Generico";
            string htmldocfactura = "";
            var formatofactura = db.formato_factura.Where(x => x.idformato_factura == 1 && x.Estado == 1).FirstOrDefault();
            var encabezadofactura = db.opciones.Where(x => x.ID_OP == "OP1_TKS").FirstOrDefault();
            var piefactura = db.opciones.Where(x => x.ID_OP == "OP2_TKS").FirstOrDefault();
            var tipocambio = db.cambiodolar.Where(x => x.Estado == 1).FirstOrDefault();
            var montodolar = Math.Round(datosfactura.Monto_total / tipocambio.Monto_cambio,2);
            if(datosfactura.Idcliente!=null)
                datoscliente =db.clientes.Where(x => x.Idcliente == datosfactura.Idcliente).FirstOrDefault();
            if (datoscliente != null)
                nomcliente = datoscliente.Primer_nombre + " " + datoscliente.Segundo_nombre + " " + datoscliente.Primer_apellido + " " + datoscliente.Segundo_apellido;

            htmldocfactura = string.Format(formatofactura.Cabeza_factura,encabezadofactura.DETALLE_EXT1.Replace("\r\n","<br>"),datosfactura.Idfactura,"Efectivo",DateTime.Now.ToShortDateString(),DateTime.Now.ToShortTimeString(), nomcliente, (string)Session["Nombreuusuario"]);

            StringBuilder tabledatosfactura = new StringBuilder();

            tabledatosfactura.Append("<table  style='width: 100%;'>");
            tabledatosfactura.Append("<thead>");
            tabledatosfactura.Append("<tr>");
            tabledatosfactura.Append("<th>Cant.</th>");
            tabledatosfactura.Append("<th>Descripción</th>");
            tabledatosfactura.Append("<th>Precio</th>");
            tabledatosfactura.Append("<th>Importe</th>");
            tabledatosfactura.Append("</tr>");
            tabledatosfactura.Append("</thead>");
            tabledatosfactura.Append("<tbody>");
       

            foreach (var item in datosfactura.detalle_factura)
            {
                tabledatosfactura.Append("<tr>");
                tabledatosfactura.Append("<td>" + item.Cantidad + "</td>");
                tabledatosfactura.Append("<td>" + item.productos.Descripcion + "</td>");
                tabledatosfactura.Append("<td>" + item.productos.Precio_venta + "</td>");
                tabledatosfactura.Append("<td>" + item.Monto + "</td>");
                tabledatosfactura.Append("</tr>");
            }
         
            tabledatosfactura.Append("</tbody>");
            tabledatosfactura.Append("</table>");
            htmldocfactura += tabledatosfactura;
            htmldocfactura +=string.Format(formatofactura.Cuerpo_factura, datosfactura.Monto_total, datosfactura.Monto_total, tipocambio.Monto_cambio, montodolar, montodolar, piefactura.DETALLE_EXT1);


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
            modelocontenedor = (Factura)TempData["modelocontenedor"];
            var datosfacturar = modelocontenedor.listatickets.Where(x => x.Numero_ticket == numticket).FirstOrDefault();
            if (datosfacturar != null)
            {
                Session["calculototalpago"] = datosfacturar.listaproductos.Sum(x => x.Impor);
                Session["cantproducto"] = datosfacturar.listaproductos.Sum(x => x.Cant);
                Session["ticketactivo"] = numticket;
            }
            else 
            {
                Session["calculototalpago"] = Convert.ToDecimal(0);
                Session["cantproducto"] = Convert.ToInt32(0);
                Session["ticketactivo"] = 1;

            }

                TempData["modelocontenedor"] = modelocontenedor;

            return Json(new { Totalpago = (decimal)Session["calculototalpago"], cantproducto = (int)Session["cantproducto"] }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult reporte_de_movimientos()
        {

            Modelo_contenedor modelo_contenedor = new Modelo_contenedor
            {
                listatipomovimiento = new List<tipo_movimento>()
            };

            try
            {

              modelo_contenedor.listatipomovimiento=cargalistatipomov();

                return View(modelo_contenedor);
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        [HttpPost]
        public ActionResult reporte_de_movimientos(tipo_movimento tipomovimiento)
        {
            Modelo_contenedor modelo_contenedor = new Modelo_contenedor
            {
                listahistorialmov = new List<historial_inventario>(),
                listatipomovimiento = new List<tipo_movimento>()
            };

            try
            {

                modelo_contenedor.listahistorialmov = (from x in db.historial_inventario where x.Estado == 1 && x.Tipo_movimiento == tipomovimiento.IdTipo_movimiento select x).ToList();

                modelo_contenedor.listatipomovimiento=cargalistatipomov();

                return View(modelo_contenedor);
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public List<tipo_movimento> cargalistatipomov()
        {
            return (from u in db.tipo_movimento where u.Estado == 1 select u).ToList();
        }


        public ActionResult Corte()
        {

            try
            {

                return View();
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public ActionResult Ventas_por_periodo()
        {

            try
            {
                Modelo_contenedor modelcontenedor = new Modelo_contenedor
                {
                    Options = crearopciones(),
                    listadetallefactura = new List<detalle_factura>()
                };
                return View(modelcontenedor);
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        [HttpPost]
        public ActionResult Ventas_por_periodo(Modelo_contenedor modelocontenedor)
        {

            try
            {
                if (modelocontenedor.SelectedValue == 1) { 
                 modelocontenedor.listadetallefactura=  db.detalle_factura.Where(x => x.Fecha_alta.Year == DateTime.Now.Year && x.Fecha_alta.Month == DateTime.Now.Month && x.Fecha_alta.Day == DateTime.Now.Day).ToList();
                }

                if (modelocontenedor.SelectedValue == 2)
                {
                    DateTime ayer = DateTime.Now.Date.AddDays(-1); // dia de ayer
                    modelocontenedor.listadetallefactura = db.detalle_factura.Where(x =>  x.Fecha_alta.Year==ayer.Year && x.Fecha_alta.Month == ayer.Month && x.Fecha_alta.Day == ayer.Day).ToList();
                }
                if (modelocontenedor.SelectedValue == 3)
                {
                    DateTime inicioSemana = DateTime.Now.Date.AddDays(-((int)DateTime.Now.DayOfWeek-1)); // Primer día de la semana actual
                    DateTime finSemana = inicioSemana.AddDays(6); // Último día de la semana actual

                    modelocontenedor.listadetallefactura= db.detalle_factura.Where(x => (x.Fecha_alta.Year == inicioSemana.Year && x.Fecha_alta.Month == inicioSemana.Month && x.Fecha_alta.Day >= inicioSemana.Day) && (x.Fecha_alta.Year == finSemana.Year && x.Fecha_alta.Month == finSemana.Month && x.Fecha_alta.Day <= finSemana.Day)).ToList();
                }
                if (modelocontenedor.SelectedValue == 4)
                {
                    DateTime inicioMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1); // Primer día del mes actual
                    DateTime finMes = inicioMes.AddMonths(1).AddDays(-1); // Último día del mes actual
                    modelocontenedor.listadetallefactura = db.detalle_factura.Where(x => (x.Fecha_alta.Year == inicioMes.Year && x.Fecha_alta.Month == inicioMes.Month && x.Fecha_alta.Day >= inicioMes.Day) && (x.Fecha_alta.Year == finMes.Year && x.Fecha_alta.Month == finMes.Month && x.Fecha_alta.Day <= finMes.Day)).ToList();
                }
                if (modelocontenedor.SelectedValue == 5 && Request.Form["consultar"]==null)
                {
                    ViewBag.fechaparticular = true;
                    
                    modelocontenedor.listadetallefactura = new List<detalle_factura>();
                }

                if (Request.Form["consultar"] != null)
                {
                    Boolean valid = true;
                    ViewBag.fechaparticular = true;

                    if (modelocontenedor.Fechadesde > modelocontenedor.Fechahasta)
                    {
                        valid = false;
                        ViewBag.Mensaje = "<i class='bi bi-exclamation-octagon me-1'></i>La fecha desde no puede ser menor a fecha hasta<br>";
                        modelocontenedor.listadetallefactura = new List<detalle_factura>();
                    }
                    if (modelocontenedor.Fechadesde== default(DateTime)) {
                        valid = false;
                        ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>La fecha desde es invalida<br>";
                        modelocontenedor.listadetallefactura = new List<detalle_factura>();

                    }

                    if (modelocontenedor.Fechahasta == default(DateTime))
                    {
                        valid = false;
                        ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>La fecha hasta es invalida";
                        modelocontenedor.listadetallefactura = new List<detalle_factura>();

                    }
                    if(valid==true)
                   {
                        modelocontenedor.listadetallefactura = db.detalle_factura.Where(x => (x.Fecha_alta.Year == modelocontenedor.Fechadesde.Year && x.Fecha_alta.Month == modelocontenedor.Fechadesde.Month && x.Fecha_alta.Day >= modelocontenedor.Fechadesde.Day) && (x.Fecha_alta.Year == modelocontenedor.Fechahasta.Year && x.Fecha_alta.Month == modelocontenedor.Fechahasta.Month && x.Fecha_alta.Day <= modelocontenedor.Fechahasta.Day)).ToList();
                    }
                    
                   
                }
                modelocontenedor.Options= crearopciones();
            
                return View(modelocontenedor);
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        // Crear una lista de opciones para el DropDownList
        public List<SelectListItem> crearopciones() {

            List<SelectListItem> options = new List<SelectListItem>
        {
            new SelectListItem { Value = "1", Text = "Hoy" },
            new SelectListItem { Value = "2", Text = "Ayer" },
            new SelectListItem { Value = "3", Text = "Esta semana" },
            new SelectListItem { Value = "4", Text = "Del mes" },
            new SelectListItem { Value = "5", Text = "Periodo en particular" }
        };


            return options;

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
