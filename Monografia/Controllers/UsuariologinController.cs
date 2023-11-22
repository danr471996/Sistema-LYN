using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using Monografia.Models;
using System.Web.Security;



namespace Monografia.Controllers
{
    public class UsuariologinController : Controller
    {

       private proyectotiendaEntities db = new proyectotiendaEntities();
       usuario_sesion datosesion = null;

        // GET: Usuariologin
        public ActionResult Login()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult Login(usuarios_tienda usuariologin)
        {
           
            try
            {
                if (usuariologin.Login==null && usuariologin.Contraseña == null) { 
                    ViewBag.mensaje = "Debe ingresar usuario y contraseña para ingresar al sistema,Favor verifique";
                    return View();
                }

                if (usuariologin.Login == null )
                {
                    ViewBag.mensaje = "Debe ingresar usuario para ingresar al sistema,Favor verifique";
                    return View();
                }

                if (usuariologin.Contraseña == null)
                {
                    ViewBag.mensaje = "Debe ingresar contraseña para ingresar al sistema,Favor verifique";
                    return View();
                }

                var datoslogin =  db.usuarios_tienda.Where(x =>x.Login.Equals(usuariologin.Login)
                                && x.Contraseña.Equals(usuariologin.Contraseña) && x.Estado_usuario==1).FirstOrDefault();

                
               if (datoslogin != null)
                {
                    Session["Idusuario"] = datoslogin.Idusuario;
                    Session["usuario_logueado"] = datoslogin.Login;
                    Session["Nombreuusuario"] = datoslogin.usuario_detalle.FirstOrDefault().Primer_nombre + " " + datoslogin.usuario_detalle.FirstOrDefault().Primer_apellido;
                    Session["Perfil"] = datoslogin.usuarios_perfiles.Descripcion_perfil;
           
                    var sesion = datoslogin.usuario_sesion.Where(x=>x.Estado==1).FirstOrDefault();
                    if (sesion != null)
                    {
                        ViewBag.mensaje = "Usuario tiene sesion abierta,comuniquese con el administrador.";
                        return View();
                    }
                    else {
                        datosesion = new usuario_sesion();
                        datosesion.Usuario_alta = datoslogin.Login;
                        datosesion.Fecha_alta = DateTime.Now;
                        datosesion.Idusuario = datoslogin.Idusuario;
                        datosesion.Estado = 1;
                        db.usuario_sesion.Add(datosesion);
                        db.SaveChanges();
                    
                        return RedirectToAction("Paginainicio");
                    }
                 

                }
                else
                {
                    ViewBag.mensaje = "El usuario digitado no se encuentra registrado, favor comuniquese con el administrador";
                    return View();
                 
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }
  
        public ActionResult Paginainicio(string filtroventas,string filtroingresos,string filtroclientes)
        {
            int cantidadventas = 0,cantidadclientes=0;
            decimal cantidadingresos = 0;
            var datosfactura = db.factura.ToList();

            if (filtroventas == null)
            {
                cantidadventas = datosfactura.Count();
                ViewBag.tipofiltroventas = "Todos los años";
                ViewBag.cantidadventas = cantidadventas;
            }
            else {
         
                ViewBag.tipofiltroventas = filtroventas=="hoy"? filtroventas:"este "+ filtroventas;
                if (filtroventas == "hoy")
                {
                    cantidadventas = datosfactura.Where(x =>x.Fecha_alta.Day==DateTime.Now.Day).Count();
                    
                }
                else if (filtroventas == "mes")
                {
                    cantidadventas = datosfactura.Where(x => x.Fecha_alta.Month == DateTime.Now.Month).Count();
                }
                else {
                    cantidadventas = datosfactura.Where(x => x.Fecha_alta.Year == DateTime.Now.Year).Count();
                }
                ViewBag.cantidadventas = cantidadventas;
            }

            if (filtroingresos == null)
            {
                cantidadingresos = datosfactura.Select(x => x.Monto_total).Sum();
                ViewBag.tipofiltroingresos = "Todos los años";
                ViewBag.cantidadingresos = cantidadingresos;
            }
            else
            {
                ViewBag.tipofiltroingresos = filtroingresos == "hoy" ? filtroingresos : "este " + filtroingresos; 
                if (filtroingresos == "hoy")
                {
                    cantidadingresos = datosfactura.Where(x => x.Fecha_alta.Day == DateTime.Now.Day).Select(x => x.Monto_total).Sum();
                }
                else if (filtroingresos == "mes")
                {
                    cantidadingresos = datosfactura.Where(x => x.Fecha_alta.Month == DateTime.Now.Month).Select(x => x.Monto_total).Sum();
                }
                else
                {
                    cantidadingresos = datosfactura.Where(x => x.Fecha_alta.Year == DateTime.Now.Year).Select(x => x.Monto_total).Sum();
                }
                ViewBag.cantidadingresos = cantidadingresos;
            }

            if (filtroclientes == null)
            {
                cantidadclientes = datosfactura.Count();
                ViewBag.tipofiltroclientes = "Todos los años";
                ViewBag.cantidadclientes = cantidadclientes;
            }
            else
            { 

                ViewBag.tipofiltroclientes = filtroclientes == "hoy" ? filtroclientes : "este " + filtroclientes; 
                if (filtroclientes == "hoy")
                {
                    cantidadclientes = datosfactura.Where(x => x.Fecha_alta.Day == DateTime.Now.Day).Count();
                }
                else if (filtroclientes == "mes")
                {
                    cantidadclientes = datosfactura.Where(x => x.Fecha_alta.Month == DateTime.Now.Month).Count();
                }
                else
                {
                    cantidadclientes = datosfactura.Where(x => x.Fecha_alta.Year == DateTime.Now.Year).Count();
                }
                ViewBag.cantidadclientes = cantidadclientes;
            }
            return View();
        }

        public ActionResult ventasporperiodo()
        {

            List<int> listventas = new List<int>();
            List<decimal> listingresos = new List<decimal>();
            List<int> listclientes = new List<int>();
            int cantidadventas = 0, cantidadclientes = 0;
            decimal cantidadingresos = 0;
            var datosfactura = db.factura.ToList();


            for (int i = 1; i < 13; i++)
            {
                cantidadventas= datosfactura.Where(x => x.Fecha_alta.Month == i && x.Fecha_alta.Year==DateTime.Now.Year).Count();
                listventas.Add(cantidadventas);

                cantidadingresos = datosfactura.Where(x => x.Fecha_alta.Month == i && x.Fecha_alta.Year == DateTime.Now.Year).Select(x => x.Monto_total).Sum();
                listingresos.Add(cantidadingresos);

                cantidadclientes = datosfactura.Where(x => x.Fecha_alta.Month == i && x.Fecha_alta.Year == DateTime.Now.Year).Count();

                listclientes.Add(cantidadclientes);
            }



            return Json(new { ventas = listventas, ingresos = listingresos,clientes= listclientes }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Logout()
        {
            int idusuario = Convert.ToInt32(Session["Idusuario"]);
            try
            {
                FormsAuthentication.SignOut();
                Session.Abandon();

                var sesion = db.usuario_sesion.Where(x => x.Idusuario == idusuario && x.Estado == 1).FirstOrDefault();

                if (sesion != null)
                {

                    sesion.Fecha_baja = DateTime.Now;
                    sesion.Usuario_baja = Session["usuario_logueado"].ToString();
                    sesion.Estado = 2;
                    db.SaveChanges();

                }
                return RedirectToAction("Login", "Usuariologin");
            }
            catch (Exception ex)
            {
                throw;
            }
          
        }
        [ChildActionOnly]
        public ActionResult mostrarnotificaciones()
        {
            var listaprodbajosinvent= db.productos.Where(x => x.Cantidad_actual < x.Cantidad_minima).ToList();
            ViewBag.cantidadprodbajos = listaprodbajosinvent.Count();
            return PartialView("_notificaciones");
        }
        public ActionResult Sesiones_usuario() {
         
            return View(db.usuario_sesion.Where(x =>x.Estado==1).ToList());
        }

        public ActionResult Delete(int? idsesion)
        {
        
            usuario_sesion ussesiones = null;
            try
            {
                if (idsesion != 0 && idsesion != null)
                {
                    ussesiones = db.usuario_sesion.Find(idsesion);

                    if (ussesiones != null)
                    {
                        return PartialView(ussesiones);
                    }
                    else
                    {
                        ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>No se encontro sesion de usuario";
                        return PartialView(ussesiones);
                    }
                }
                else
                {
                    ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Id de sesion usuario erroneo";
                    return PartialView(ussesiones);
                }


            }
            catch (Exception ex)
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
                usuario_sesion datosusuariossesion = null;
                if (id != 0 && id != null)
                {
                    datosusuariossesion=(from d in db.usuario_sesion where d.Idusuario_sesion == id select d).FirstOrDefault();

                    if (datosusuariossesion != null)
                    {

                        datosusuariossesion.Fecha_baja = DateTime.Now;
                        datosusuariossesion.Usuario_baja = (string)Session["usuario_logueado"];
                        datosusuariossesion.Estado = 2;
                        db.SaveChanges();
                        return Json(new { success = true, mensaje = "Se ha eliminado la sesion del usuario satisfactoriamente." });
                    }
                    else
                    {
                        ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>No se encontro sesion de usuario";
                        return PartialView(datosusuariossesion);
                    }
                }
                else
                {
                    ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Id de sesion usuario erroneo";
                    return PartialView(datosusuariossesion);
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
