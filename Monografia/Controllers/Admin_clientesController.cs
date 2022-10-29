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
    public class Admin_clientesController : Controller
    {
        private proyectotiendaEntities db = new proyectotiendaEntities();

        // GET: clientes
        public ActionResult Lista_clientes()
        {
            return View(db.clientes.ToList());
        }
        public ActionResult Lista_de_abonos()
        {
            try
            {
                List<pagos> pag = new List<pagos>();
                if (Session["Idfactura"] != null)
                {
                    pag = db.pagos.Where(x => x.Id_factura == (int)Session["Idfactura"]).ToList();

                }
                return PartialView(pag);
            }
            catch (Exception ex)
            {

                throw;
            }
         
        }
        
        public ActionResult Abono()
        {
           
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Abono( int? agregar_cantidad)
        {
            try
            {
                if (Session["idcliente"] != null && Session["Idfactura"] != null)
                {
                    creditos credito = new creditos();
                    pagos pago = new pagos();
                    credito = (from d in db.creditos where d.Idcliente == (int)Session["idcliente"] && d.Id_factura == (int)Session["Idfactura"] && d.Estado == 1 select d).FirstOrDefault();
                    credito.Importe_pagado = credito.Importe_pagado + agregar_cantidad;
                    pago.Fecha_alta = DateTime.Now;
                    pago.Usuario_alta = (string)Session["usuario_logueado"];
                    pago.Id_factura = (int)Session["Idfactura"];
                    pago.Monto_pagado = agregar_cantidad;
                    pago.Estado = 1;

                    db.SaveChanges();
                    return Json(new { success = true });
                }
                return PartialView();
            }
            catch (Exception)
            {

                throw;
            }
         
        }
        public ActionResult Estado_cuenta(int? id)
        {
            try
            {
                List<Modelo_contenedor> modelo_contenedor = new List<Modelo_contenedor>();

                var clientes = db.clientes.Where(x => x.Idcliente == id).FirstOrDefault();
                Session["idcliente"] = clientes.Idcliente;
                Session["nomcliente"] = clientes.Primer_nombre + " " + clientes.Primer_apellido;
                Session["limitecredito"] = clientes.tipo_credito.Descripcion;
                Session["Saldoactual"] = (from x in db.creditos where x.Idcliente == id && x.Estado == 1 select (x.Importe_total - x.Importe_pagado)).Sum() == null ? 0 : (from x in db.creditos where x.Idcliente == id && x.Estado == 1 select (x.Importe_total - x.Importe_pagado)).Sum();

                var listafacturas = (from u in db.factura
                                     join p in db.creditos on u.Idfactura equals p.Id_factura
                                     where u.Idcliente == id
                                     orderby p.Fecha_alta descending
                                     select new { u, p }).ToList();


                foreach (var item in listafacturas)
                {

                    modelo_contenedor.Add(new Modelo_contenedor
                    {
                        fechasfacturas = (item.u.Fecha_alta.ToString()).Substring(0, 9),
                        idfacturas = item.u.Idfactura,
                        idcliente2 = item.p.Idcliente
                    });
                }
                return View(modelo_contenedor);
            }
            catch (Exception)
            {

                throw;
            }
          

        }
        public JsonResult detallefactura(int? idcliente, int? idfactura)
        {
            try
            {
                Session["Idfactura"] = idfactura;
                List<Modelo_contenedor> modelo_contenedor = new List<Modelo_contenedor>();
                var listafacturas = (from f in db.factura
                                     join df in db.detalle_factura on f.Idfactura equals df.Id_factura
                                     join p in db.productos on df.Idproducto equals p.Idproducto
                                     where f.Idfactura == idfactura && f.Idcliente == idcliente
                                     orderby p.Fecha_alta descending
                                     select new { f, df, p }).ToList();


                foreach (var item in listafacturas)
                {

                    modelo_contenedor.Add(new Modelo_contenedor
                    {
                        Descripcionproducto = item.p.Descripcion,
                        Precioventa = item.p.Precio_venta,
                        cantidad = item.df.Cantidad,
                        importe = item.df.Monto
                    });
                }
                return Json(modelo_contenedor, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        

        }
        
        public ActionResult Lista_Reporte_Saldo()
        {
            try
            {
                List<Modelo_contenedor> modelo_contenedor = new List<Modelo_contenedor>();

                decimal? saldo = (from x in db.creditos where x.Estado == 1 select (x.Importe_total - x.Importe_pagado)).Sum();
                Session["cantidad_saldo"] = saldo == null ? "0": saldo.ToString();

                var clientes = db.clientes.Include(K =>K.tipo_credito).ToList();
                foreach (var item in clientes)
                {
                    DateTime fechaultimopago = (from u in db.creditos
                                     join p in db.pagos on u.Id_factura equals p.Id_factura
                                     where u.Idcliente == item.Idcliente
                                     orderby p.Idpagos descending
                                     select p.Fecha_alta).Take(1).FirstOrDefault();
                    modelo_contenedor.Add(new Modelo_contenedor
                    {
                        idcliente = item.Idcliente,
                        Nombre = item.Primer_nombre,
                        Direccion = item.Direccion,
                        telefono = item.Telefono,
                        limitecredito = item.tipo_credito.Descripcion,
                        saldo = (from x in db.creditos where x.Estado == 1 && x.Idcliente == item.Idcliente select (x.Importe_total - x.Importe_pagado)).Sum() == null ? 0 : (from x in db.creditos where x.Estado == 1 && x.Idcliente == item.Idcliente select (x.Importe_total - x.Importe_pagado)).Sum(),

                        fechapago = fechaultimopago == Convert.ToDateTime("01-01-0001") ? "": fechaultimopago.ToString()
                    });
                }

                return View(modelo_contenedor);
            }
            catch (Exception)
            {

                throw;
            }
         
        }

        // GET: clientes/Create
        public ActionResult Create()
        {
            Modelo_contenedor modelo_contenedor = new Modelo_contenedor();
            var Listtipocredito = from u in db.tipo_credito where u.Estado == 1 select u;
            modelo_contenedor.listatipocredito= new List<tipo_credito>();
            modelo_contenedor.listatipocredito=Listtipocredito.ToList();

            return PartialView(modelo_contenedor);
        }

        // POST: clientes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Modelo_contenedor clientes)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    clientes.cliente.Fecha_alta = DateTime.Now;
                    clientes.cliente.Estado = 1;
                    clientes.cliente.Usuario_alta = (string)Session["usuario_logueado"];
                    db.clientes.Add(clientes.cliente);
                    db.SaveChanges();
                    return Json(new { success = true });
                }

                return PartialView(clientes);
            }
            catch (Exception)
            {

                throw;
            }
         
        }

        // GET: clientes/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                Modelo_contenedor modelo_contenedor = null;
                if (id != null)
                {
                
                clientes clientes = db.clientes.Find(id);
                modelo_contenedor = new Modelo_contenedor();
                modelo_contenedor.cliente = new clientes();
                modelo_contenedor.cliente.Idcliente = clientes.Idcliente;
                modelo_contenedor.cliente.Primer_nombre = clientes.Primer_nombre;
                modelo_contenedor.cliente.Segundo_nombre = clientes.Segundo_nombre;
                modelo_contenedor.cliente.Primer_apellido = clientes.Primer_apellido;
                modelo_contenedor.cliente.Segundo_apellido = clientes.Segundo_apellido;
                modelo_contenedor.cliente.Direccion = clientes.Direccion;
                modelo_contenedor.cliente.Telefono = clientes.Telefono;
                modelo_contenedor.cliente.Id_tipocredito = clientes.Id_tipocredito;
                modelo_contenedor.cliente.Cantidad_credito= clientes.Cantidad_credito; 

                var Listtipocredito = from u in db.tipo_credito where u.Estado == 1 select u;
                modelo_contenedor.listatipocredito = new List<tipo_credito>();
                modelo_contenedor.listatipocredito = Listtipocredito.ToList();
                return PartialView(modelo_contenedor);
                }
                else
                {
                    return PartialView(modelo_contenedor);

                }

             
            }
            catch (Exception ex)
            {

                throw;
            }
           
        }

        // POST: clientes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Modelo_contenedor clientes)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var datosclientes = (from d in db.clientes where d.Idcliente == clientes.cliente.Idcliente select d).FirstOrDefault();
                    datosclientes.Primer_nombre = clientes.cliente.Primer_nombre;
                    datosclientes.Segundo_nombre = clientes.cliente.Segundo_nombre;
                    datosclientes.Primer_apellido = clientes.cliente.Primer_apellido;
                    datosclientes.Segundo_apellido = clientes.cliente.Segundo_apellido;
                    datosclientes.Direccion = clientes.cliente.Direccion;
                    datosclientes.Telefono = clientes.cliente.Telefono;
                    datosclientes.Id_tipocredito = clientes.cliente.Id_tipocredito;
                    datosclientes.Cantidad_credito = clientes.cliente.Id_tipocredito == 1?0: clientes.cliente.Cantidad_credito;

                    db.SaveChanges();
                    return Json(new { success = true });
                }
                return PartialView(clientes);
            }
            catch (Exception)
            {

                throw;
            }
       
        }

        // GET: clientes/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                clientes clientes = db.clientes.Find(id);
                if (clientes == null)
                {
                    return HttpNotFound();
                }
                return PartialView(clientes);
            }
            catch (Exception)
            {

                throw;
            }
          
        }

        // POST: clientes/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var datosclientes = (from d in db.clientes where d.Idcliente == id select d).FirstOrDefault();
                datosclientes.Fecha_baja = DateTime.Now;
                datosclientes.Usuario_baja = (string)Session["usuario_logueado"];
                datosclientes.Estado = 2;
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
