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

namespace Monografia.Controllers
{
    [ValidateSession]
    public class Admin_clientesController : Controller
    {
        private proyectotiendaEntities db = new proyectotiendaEntities();
        string patronsindecimales = @"^\d+$";
        string patronConDecimales = @"^\d+(\.\d+)?$";
        // GET: clientes
    
        public ActionResult Lista_clientes()
        {
            return View(db.clientes.ToList());
        }
    
        public ActionResult Lista_de_abonos()
        {
            try
            {
                List<pagos> pag = null ;
                if (TempData["Idfactura"] != null)
                {
                    var idfactura = (int)TempData["Idfactura"];
                    TempData["Idfactura"] = idfactura;
                    pag = db.pagos.Where(x => x.Id_factura == idfactura).ToList();
                    return PartialView(pag);
                }
                else{
                    ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>No se ha seleccionado factura pendiente para listar los abonos,favor seleccione una.";
                    return PartialView(pag);
                }
                
            }
            catch (Exception ex)
            {

                throw;
            }
         
        }
        
        public ActionResult Abono()
        {
            if (TempData["idcliente"] == null || TempData["Idfactura"] == null)
            {
                ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>No se ha seleccionado factura para realizar abono,favor seleccione una.";
            
            }
            TempData["idcliente"] = TempData["idcliente"];
            TempData["Idfactura"] = TempData["Idfactura"];
            return PartialView();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Abono( int? agregar_cantidad)
        {
            try
            {
       
                if (TempData["idcliente"] != null && TempData["Idfactura"] != null)
                {
                    TempData["idcliente"] = TempData["idcliente"];
                    TempData["Idfactura"] = TempData["Idfactura"];
                    if (agregar_cantidad == null || agregar_cantidad == 0)
                    {
                 
                        ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Favor ingrese una cantidad para abonar.";
                        ViewBag.Abonovacio = true;
                        return PartialView();
                    }
                    else
                    {
                        creditos credito = new creditos();
                        pagos pago = new pagos();
                        factura factura = new factura();
                        int idcliente = (int)TempData["idcliente"];
                        int idfactura = (int)TempData["Idfactura"];
                        credito = (from d in db.creditos where d.Idcliente == idcliente && d.Id_factura == idfactura && d.Estado == 1 select d).FirstOrDefault();
                        if (credito != null)
                        {
                            if(credito.Importe_pagado==null)
                            credito.Importe_pagado = agregar_cantidad;
                            else
                                credito.Importe_pagado += agregar_cantidad;

                            if (credito.Importe_pagado >= credito.Importe_total)
                            {
                                factura = (from d in db.factura where d.Idfactura == idfactura && d.Estado == 2 select d).FirstOrDefault();
                                factura.Estado = 1;
                            }

                            pago.Fecha_alta = DateTime.Now;
                            pago.Usuario_alta = (string)Session["usuario_logueado"];
                            pago.Id_factura = idfactura;
                            pago.Monto_pagado = agregar_cantidad;
                            pago.Estado = 1;
                            db.pagos.Add(pago);

                            movimientos movimientos = new movimientos
                            {
                                Fecha_alta = DateTime.Now,
                                Usuario_alta = (string)Session["usuario_logueado"],
                                Monto = Convert.ToDecimal(agregar_cantidad),
                                Tipo_movimiento = 1,
                                Tipo_pago = 2,
                                Idpago=pago.Idpagos,
                                Estado = 1
                            };
                            db.movimientos.Add(movimientos);

                            db.SaveChanges();
                            return Json(new { success = true, mensaje = "Se ha abonado a la deuda satisfactoriamente." });
                        }
                        else {
                            ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>No se encontro credito";
                            ViewBag.Abonovacio = true;
                            return PartialView();

                        }
                     
                    }
                   
                }
                else
                {
                
                    ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>No se ha seleccionado factura para realizar abono,favor seleccione una.";
                    return PartialView();
                }
            }
            catch (Exception)
            {

                throw;
            }
         
        }
        // GET: clientes/Edit/5
        public ActionResult Editabono(int? id)
        {
            try
            {
                pagos datospagos = null;
                if (id != 0 && id != null)
                {

                     datospagos = db.pagos.Find(id);
                    if (datospagos == null)
                    {

                        ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>No se encontro pago";

                        return PartialView(datospagos);

                    }
                    else
                    {
                        return PartialView(datospagos);
                    }
                }
                else
                {
                    ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Id de pago erroneo";

                    return PartialView(datospagos);

                }


            }
            catch (Exception ex)
            {

                throw;
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editabono(pagos datosabonoedit)
        {
            try
            {

                if (validadinputs(null, datosabonoedit))
                {

                    var datospagos = db.pagos.Find(datosabonoedit.Idpagos);
                    if (datospagos == null)
                    {
                        ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>No se encontro pago";
                        return PartialView(datosabonoedit);
                    }
                    else
                    {
                        datospagos.Monto_pagado = datosabonoedit.Monto_pagado;
                        var movimientos=db.movimientos.Where(x => x.Idpago == datospagos.Idpagos).FirstOrDefault();
                        movimientos.Monto =Convert.ToDecimal(datosabonoedit.Monto_pagado);
                        db.SaveChanges();
                        var datoscreditos = db.creditos.Where(x => x.Id_factura == datospagos.Id_factura).FirstOrDefault();
                        datoscreditos.Importe_pagado = db.pagos.Where(k => k.Id_factura == datospagos.Id_factura).Sum(x => (decimal?)x.Monto_pagado) ?? 0;

                        db.SaveChanges();

                        return Json(new { success = true, mensaje = "Se ha actualizado la informacion del pago satisfactoriamente." });
                    }
                }
                else
                {
                    return PartialView(datosabonoedit);
                }

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
                TempData.Remove("Idfactura");
                TempData.Remove("idcliente");
                if (id != 0 && id != null)
                {

                    var clientes = db.clientes.Where(x => x.Idcliente == id).FirstOrDefault();
                    if (clientes != null)
                    {
                        
                        ViewBag.nomcliente= clientes.Primer_nombre + " " + clientes.Primer_apellido;
                        ViewBag.limitecredito = clientes.tipo_credito.Descripcion;
                        ViewBag.Saldoactual = (from x in db.creditos where x.Idcliente == id && x.Estado == 1 select ((x.Importe_total) - (x.Importe_pagado == null ? 0 : x.Importe_pagado))).Sum() == null ? (0).ToString() : ((from x in db.creditos where x.Idcliente == id && x.Estado == 1 select ((x.Importe_total) - (x.Importe_pagado == null ? 0 : x.Importe_pagado))).Sum()).ToString();

                        var listafacturas = (from u in db.factura
                                             join p in db.creditos on u.Idfactura equals p.Id_factura
                                             where u.Idcliente == id && u.Estado == 2
                                             orderby p.Fecha_alta descending
                                             select new { u, p }).ToList();


                        foreach (var item in listafacturas)
                        {

                            modelo_contenedor.Add(new Modelo_contenedor
                            {
                                fechasfacturas = item.u.Fecha_alta.ToShortDateString(),
                                idfacturas = item.u.Idfactura,
                                idcliente2 = item.p.Idcliente
                            });
                        }
                        return View(modelo_contenedor);
                    }
                    else {
                        ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>No se encontro cliente";
                        return View(modelo_contenedor);
                    }
                }
                else {
                    ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Id de cliente erroneo";
                    return View(modelo_contenedor);
                }
              
            }
            catch (Exception)
            {

                throw;
            }
          

        }
        public JsonResult detallefactura(int? idcliente, int? idfactura,Boolean filaseleccionada)
        {
            try
            {
                List<Modelo_contenedor> modelo_contenedor = new List<Modelo_contenedor>();
                if (filaseleccionada)
                {

                    if ((idcliente != 0 && idcliente != null) && (idfactura != 0 && idfactura != null))
                    {


                        TempData["Idfactura"] = idfactura;
                        TempData["idcliente"] = idcliente;
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
                    else
                    {

                        string Mensaje = "<div class='alert alert-danger bg-danger text-light border-0 alert-dismissible fade show' style='display: block;' role='alert'>" +
                                         "<i class='bi bi-exclamation-octagon me-1'></i>Id de cliente erroneo o idfactura erronea" +
                                         "<button type ='button' class='btn-close btn-close-white' data-bs-dismiss='alert' aria-label='Close'></button>" +
                                         "</div>";
                        // objeto anónimo con las variables
                        var data = new
                        {
                            modelo_contenedor = modelo_contenedor,
                            Mensaje = Mensaje
                        };

                        return Json(data, JsonRequestBehavior.AllowGet);
                    }
                }
                else {
                    TempData.Remove("Idfactura");
                    TempData.Remove("idcliente");
                    return Json(modelo_contenedor, JsonRequestBehavior.AllowGet);
                }
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

                decimal? saldo = (from x in db.creditos where x.Estado == 1 select (((x.Importe_total) - (x.Importe_pagado==null?0:x.Importe_pagado)))).Sum();
                Session["cantidad_saldo"] = saldo == null ? "0": saldo.ToString();

                var clientes = db.clientes.Include(K =>K.tipo_credito).ToList();
                foreach (var item in clientes)
                {
                    DateTime fechaultimopago = (from u in db.creditos
                                     join p in db.pagos on u.Id_factura equals p.Id_factura
                                     where u.Idcliente == item.Idcliente && p.Estado==1
                                     orderby p.Idpagos descending
                                     select p.Fecha_alta).Take(1).FirstOrDefault();
                    modelo_contenedor.Add(new Modelo_contenedor
                    {
                        idcliente = item.Idcliente,
                        Nombre = item.Primer_nombre,
                        Direccion = item.Direccion,
                        telefono = item.Telefono,
                        limitecredito = item.tipo_credito.Descripcion,
                        saldo = (from x in db.creditos where x.Estado == 1 && x.Idcliente == item.Idcliente select ((x.Importe_total) - (x.Importe_pagado == null ? 0 : x.Importe_pagado))).Sum() == null ? 0 : (from x in db.creditos where x.Estado == 1 && x.Idcliente == item.Idcliente select ((x.Importe_total) - (x.Importe_pagado == null ? 0 : x.Importe_pagado))).Sum(),

                        fechapago = fechaultimopago == Convert.ToDateTime("01-01-0001") ? "": fechaultimopago.ToString()
                    });
                }
                TempData["SaldoClientes"] = modelo_contenedor;
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
            Modelo_contenedor modelo_contenedor = new Modelo_contenedor
            {
                listatipocredito = new List<tipo_credito>()
            };
            modelo_contenedor.listatipocredito = GetTipo_Creditos();

            return PartialView(modelo_contenedor);
        }


      
        // POST: clientes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Modelo_contenedor datoscliente)
        {
            try
            {

              
                if (validadinputs(datoscliente,null))
                {
                    if (db.clientes.Where(x => (x.Primer_nombre + x.Segundo_nombre + x.Primer_apellido + x.Segundo_apellido).ToUpper() == (datoscliente.cliente.Primer_nombre + datoscliente.cliente.Segundo_nombre + datoscliente.cliente.Primer_apellido + datoscliente.cliente.Segundo_apellido).ToUpper() && x.Estado==1).FirstOrDefault() == null)
                    {
                        datoscliente.cliente.Fecha_alta = DateTime.Now;
                        datoscliente.cliente.Estado = 1;
                        datoscliente.cliente.Usuario_alta = (string)Session["usuario_logueado"];
                        db.clientes.Add(datoscliente.cliente);
                        db.SaveChanges();
                        return Json(new { success = true, mensaje = "Se ha creado cliente satisfactoriamente." });
                    }
                    else {
                        datoscliente.listatipocredito = new List<tipo_credito>();
                        datoscliente.listatipocredito = GetTipo_Creditos();
                        ViewBag.Mensaje = "<i class='bi bi-exclamation-octagon me-1'></i>Ya existe un cliente con el mismo nombre<br>";
                        return PartialView(datoscliente);
                    }
                }
                else {

                    datoscliente.listatipocredito = new List<tipo_credito>();
                    datoscliente.listatipocredito = GetTipo_Creditos();

                    return PartialView(datoscliente);
                }

              
            }
            catch (Exception)
            {

                throw;
            }
         
        }

        public Boolean validadinputs( Modelo_contenedor datoscliente,pagos datospagos) {
            Boolean valid = true;
            if (datoscliente != null)
            {
                if (datoscliente.cliente.Primer_nombre == null)
                {
                    ViewBag.Mensaje = "<i class='bi bi-exclamation-octagon me-1'></i>Debe ingresar el primer nombre del cliente<br>";
                    valid = false;
                }
                if (datoscliente.cliente.Primer_nombre != null)
                    if (!sololetras(datoscliente.cliente.Primer_nombre))
                    {
                        ViewBag.Mensaje = "<i class='bi bi-exclamation-octagon me-1'></i>Debe ingresar solo letras en primer nombre del cliente<br>";
                        valid = false;
                    }
                //if (datoscliente.cliente.Segundo_nombre == null)
                //{
                //    ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Debe ingresar el Segundo nombre del cliente<br>";
                //    valid = false;
                //}
                if (datoscliente.cliente.Segundo_nombre != null)
                    if (!sololetras(datoscliente.cliente.Segundo_nombre))
                    {
                        ViewBag.Mensaje = "<i class='bi bi-exclamation-octagon me-1'></i>Debe ingresar solo letras en Segundo nombre del cliente<br>";
                        valid = false;
                    }
                if (datoscliente.cliente.Primer_apellido == null)
                {
                    ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Debe ingresar el primer apellido del cliente<br>";
                    valid = false;
                }
                if (datoscliente.cliente.Primer_apellido != null)
                    if (!sololetras(datoscliente.cliente.Primer_apellido))
                    {
                        ViewBag.Mensaje = "<i class='bi bi-exclamation-octagon me-1'></i>Debe ingresar solo letras en primer apellido del cliente<br>";
                        valid = false;
                    }
                //if (datoscliente.cliente.Segundo_apellido == null)
                //{
                //    ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Debe ingresar el segundo apellido del cliente<br>";
                //    valid = false;
                //}
                if (datoscliente.cliente.Segundo_apellido != null)
                    if (!sololetras(datoscliente.cliente.Segundo_apellido))
                    {
                        ViewBag.Mensaje = "<i class='bi bi-exclamation-octagon me-1'></i>Debe ingresar solo letras en segundo apellido del cliente<br>";
                        valid = false;
                    }
                if (datoscliente.cliente.Telefono == null)
                {
                    ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Debe ingresar el número telefonico del cliente<br>";
                    valid = false;
                }
                else
                {
                    if (datoscliente.cliente.Telefono.ToString().Length < 8)
                    {
                        ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Debe ingresar un número telefonico Válido<br>";
                        valid = false;
                    }

                    if (!Regex.IsMatch(datoscliente.cliente.Telefono.ToString(), patronsindecimales))
                    {
                        ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Debe ingresar solo números en número telefonico<br>";
                        valid = false;
                    }
                }


                if (datoscliente.cliente.Id_tipocredito == 0)
                {
                    ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Debe ingresar el tipo de credito del cliente<br>";
                    valid = false;
                }
                else
                {
                    if ((datoscliente.cliente.Cantidad_credito == null || datoscliente.cliente.Cantidad_credito == 0) && datoscliente.cliente.Id_tipocredito == 2)
                    {
                        ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Debe ingresar Cantidad de crédito para el cliente<br>";
                        valid = false;
                    }

                    if (!Regex.IsMatch(datoscliente.cliente.Cantidad_credito.ToString(), patronsindecimales) && datoscliente.cliente.Id_tipocredito == 2)
                    {
                        ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Debe ingresar solo números en número cantidad de credito<br>";
                        valid = false;
                    }

                }
                if (datoscliente.cliente.Direccion == null)
                {
                    ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Debe ingresar la dirección del cliente<br>";
                    valid = false;

                }
                if (datoscliente.cliente.Cedula == null)
                {
                    ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Debe ingresar la cedula del cliente<br>";
                    valid = false;

                } else {
                    Boolean valida = IsValidCedulaNicaraguense(datoscliente.cliente.Cedula);
                    if (valida == false) {
                        ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>El formato de la cedula es invalido <br>";
                        valid = false;
                    }
                }
            }
            if (datospagos != null)
            {

                if ((datospagos.Monto_pagado == null || datospagos.Monto_pagado == 0))
                {
                    ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Debe ingresar Cantidad del nuevo monto de pago<br>";
                    valid = false;
                }
                if (!Regex.IsMatch(datospagos.Monto_pagado.ToString(), patronConDecimales))
                {
                    ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Debe ingresar solo números en nuevo monto de pago<br>";
                    valid = false;
                }

            }
            return valid;
        }

        public static bool IsValidCedulaNicaraguense(string cedula)
        {
            // Expresión regular para el formato de la cédula y la estructura básica de la fecha (DDMMYY)
            // NNN-DDMMYY-NNNNA
            // NNN: 3 dígitos
            // DD: Día (01-31)
            // MM: Mes (01-12)
            // YY: Año (2 dígitos)
            // NNNN: 4 dígitos
            // A: Una letra mayúscula
            string pattern = @"^\d{3}-(?:0[1-9]|[12]\d|3[01])(?:0[1-9]|1[0-2])\d{2}-\d{4}[A-Z]$";

            // Verificar el formato con la expresión regular
            Match match = Regex.Match(cedula, pattern);

            if (!match.Success)
            {
                return false; // El formato de la cédula no coincide
            }

            // Si el formato es correcto, ahora extraemos la parte de la fecha
            // para una validación más estricta con DateTime.
            string[] parts = cedula.Split('-');
            string datePart = parts[1]; // Los 6 dígitos de la fecha (DDMMYY)

            int day;
            int month;
            int year;

            // Intentar parsear el día, mes y año de la parte de la fecha
            if (!int.TryParse(datePart.Substring(0, 2), out day) ||
                !int.TryParse(datePart.Substring(2, 2), out month) ||
                !int.TryParse(datePart.Substring(4, 2), out year))
            {
                return false; // Error al parsear las partes de la fecha
            }

            // Ajustar el año de 2 dígitos a 4 dígitos.
            // Esto es una heurística y podría necesitar un ajuste más específico
            // basado en el rango de años esperados para las cédulas en Nicaragua.
            // Por ejemplo, si el año es 95, asumimos 1995. Si es 05, asumimos 2005.
            // Esta lógica es simplificada y puede requerir refinamiento.
            int fullYear = (year > DateTime.Now.Year % 100) ? (1900 + year) : (2000 + year);

            // Validar la fecha completa usando DateTime
            try
            {
                // Intenta crear un objeto DateTime. Si los valores (día, mes, año)
                // no forman una fecha válida (ej. 31 de febrero), lanzará una excepción.
                DateTime dateOfBirth = new DateTime(fullYear, month, day);

                // Opcional: Podrías añadir validación para asegurar que la fecha de nacimiento
                // no sea en el futuro, o dentro de un rango de edad razonable para una cédula.
                // Ejemplo: if (dateOfBirth > DateTime.Today) return false;

                return true; // La cédula es válida (formato y fecha)
            }
            catch (ArgumentOutOfRangeException)
            {
                return false; // La fecha no es válida (ej. 31 de febrero, mes 13, etc.)
            }
        }

        public Boolean sololetras(string datoingresado) {
            if (datoingresado.All(char.IsLetter))
            {
                return true;
            }
            else {

                return false;
            }
        
        }


  


        // GET: clientes/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                Modelo_contenedor modelo_contenedor = null;
                if (id != 0 && id != null)
                {

                    clientes datoscliente = db.clientes.Find(id);
                    if (datoscliente == null) {

                        ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>No se encontro cliente";
                
                        return PartialView(modelo_contenedor);

                    }
                    else {
                            modelo_contenedor = new Modelo_contenedor
                            {
                                cliente = new clientes(),
                                listatipocredito = new List<tipo_credito>()
                            };
                            modelo_contenedor.cliente = datoscliente;

                            modelo_contenedor.listatipocredito = GetTipo_Creditos();
                            return PartialView(modelo_contenedor);
                    
                     }
                }
                else
                {
                    ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Id de cliente erroneo";

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
        public ActionResult Edit(Modelo_contenedor datosclienteedit)
        {
            try
            {
                if (validadinputs(datosclienteedit, null))
                {
                    var datoscliente = (from d in db.clientes where d.Idcliente == datosclienteedit.cliente.Idcliente select d).FirstOrDefault();
                    if (datoscliente == null)
                    {
                        ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>No se encontro cliente";
                        datosclienteedit.listatipocredito = new List<tipo_credito>();
                        datosclienteedit.listatipocredito = GetTipo_Creditos();

                        return PartialView(datosclienteedit);
                    }
                    else
                    {
                        var existeClienteConMismoNombreYDiferenteId = db.clientes
                                                                    .Where(x => (x.Primer_nombre + x.Segundo_nombre + x.Primer_apellido + x.Segundo_apellido).ToUpper() ==
                                                                    (datosclienteedit.cliente.Primer_nombre + datosclienteedit.cliente.Segundo_nombre + datosclienteedit.cliente.Primer_apellido + datosclienteedit.cliente.Segundo_apellido).ToUpper() && x.Idcliente != datosclienteedit.cliente.Idcliente)
                                                                    .Any();
                        if (!existeClienteConMismoNombreYDiferenteId)
                        {
                        datoscliente.Primer_nombre = datosclienteedit.cliente.Primer_nombre;
                        datoscliente.Segundo_nombre = datosclienteedit.cliente.Segundo_nombre;
                        datoscliente.Primer_apellido = datosclienteedit.cliente.Primer_apellido;
                        datoscliente.Segundo_apellido = datosclienteedit.cliente.Segundo_apellido;
                        datoscliente.Direccion = datosclienteedit.cliente.Direccion;
                        datoscliente.Telefono = datosclienteedit.cliente.Telefono;
                        datoscliente.Id_tipocredito = datosclienteedit.cliente.Id_tipocredito;
                        datoscliente.Cantidad_credito = datosclienteedit.cliente.Id_tipocredito == 1 ? 0 : datosclienteedit.cliente.Cantidad_credito;

                        db.SaveChanges();
                        return Json(new { success = true, mensaje = "Se ha actualizado la informacion del cliente satisfactoriamente." });
                        }
                        else
                        {
                            datosclienteedit.listatipocredito = new List<tipo_credito>();
                            datosclienteedit.listatipocredito = GetTipo_Creditos();
                            ViewBag.Mensaje = "<i class='bi bi-exclamation-octagon me-1'></i>Ya existe un cliente con el mismo nombre<br>";
                            return PartialView(datosclienteedit);
                        }
                    }
                }
                else
                {

                    datosclienteedit.listatipocredito = new List<tipo_credito>();
                    datosclienteedit.listatipocredito = GetTipo_Creditos();

                    return PartialView(datosclienteedit);
                }
            }
            catch (Exception)
            {

                throw;
            }
       
        }

        // GET: clientes/Delete/5
        public ActionResult Delete(int? id)
        {
            clientes datosclientes = null;
            try
            {
                if (id != 0 && id != null)
                {
                    datosclientes = db.clientes.Find(id);

                    if (datosclientes != null)
                    {
                        return PartialView(datosclientes);
                    }
                    else {
                        ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>No se encontro cliente";
                        return PartialView(datosclientes);
                    }
                }
                else {
                    ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Id de cliente erroneo";
                    return PartialView(datosclientes);
                }
            
               
            }
            catch (Exception)
            {

                throw;
            }
          
        }

        // POST: clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            try
            {
                clientes datosclientes = null;

                if (id != 0 && id!=null)
                {
                    datosclientes = db.clientes.Find(id);

                    if (datosclientes != null)
                    {
                        if(!db.factura.Where(x=> x.Idcliente==datosclientes.Idcliente && x.Estado==2).Any())
                        {
                            datosclientes.Fecha_baja = DateTime.Now;
                            datosclientes.Usuario_baja = (string)Session["usuario_logueado"];
                            datosclientes.Estado = 2;
                            db.SaveChanges();
                            return Json(new { success = true, mensaje = "Se ha inactivado el cliente satisfactoriamente." });
                        }
                        else{
                            ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>No se puede inactivar un cliente que tiene facturas pendientes de pago";
                            return PartialView(datosclientes);
                        }
                 
                    }
                    else
                    {
                        ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>No se encontro cliente";
                        return PartialView(datosclientes);
                    }
                }
                else
                {
                    ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Id de cliente erroneo";
                    return PartialView(datosclientes);
                }


            }
            catch (Exception)
            {

                throw;
            }
          
        }

        public ActionResult reporte_SaldoClientes_Preview()
        {

            try
            {
                var SaldoClientes = (List<Modelo_contenedor>)TempData["SaldoClientes"];
                string reportPath = Server.MapPath("~/Reportes/ReportSaldo.rdlc");

                ViewBag.ReportViewer = ReportCreate.GetReport(reportPath, SaldoClientes);
                return View("reporte_Preview");
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public List<tipo_credito> GetTipo_Creditos()
        {
            return (from u in db.tipo_credito where u.Estado == 1 select u).ToList();
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
