using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
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
        public  ActionResult Login(usuarios_tienda usuariologin)
        {
           
            try
            {
        
            var login =  db.usuarios_tienda.Where(x =>x.Login.Equals(usuariologin.Login)
                                && x.Contraseña.Equals(usuariologin.Contraseña) && x.Estado_usuario==1).FirstOrDefault();

                
               if (login != null)
                {
                    Session["Idusuario"] = login.Idusuario;
                    Session["usuario_logueado"] = login.Login;
                    Session["Nombreuusuario"] = login.usuario_detalle.FirstOrDefault().Primer_nombre + " " + login.usuario_detalle.FirstOrDefault().Primer_apellido;
                    Session["Perfil"] = login.usuarios_perfiles.Descripcion_perfil;

                    var sesion = login.usuario_sesion.Where(x=>x.Estado==1).FirstOrDefault();
                    if (sesion != null)
                    {
                 
                        return Json(new { success = false,mensaje="Usuario tiene sesion abierta,comuniquese con el administrador." });
                    }
                    else {
                        datosesion = new usuario_sesion();
                        datosesion.Usuario_alta = login.Login;
                        datosesion.Fecha_alta = DateTime.Now;
                        datosesion.Idusuario = login.Idusuario;
                        datosesion.Estado = 1;
                        db.usuario_sesion.Add(datosesion);
                        db.SaveChanges();
                    
                        return Json(new { success = true });
                    }
                 

                }
                else
                {
        
                  return  Json(new { success = false, mensaje = "El usuario digitado no se encuentra registrado, favor comuniquese con el administrador" ,existeusuario=false});
                }

            }
            catch (Exception ex)
            {
                return View();
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
                cantidadventas= datosfactura.Where(x => x.Fecha_alta.Month == i).Count();
                listventas.Add(cantidadventas);

                cantidadingresos = datosfactura.Where(x => x.Fecha_alta.Month == i).Select(x => x.Monto_total).Sum();
                listingresos.Add(cantidadingresos);

                cantidadclientes = datosfactura.Where(x => x.Fecha_alta.Month == i).Count();

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

        public ActionResult Delete(int idsesion)
        {
            usuario_sesion ussesiones = db.usuario_sesion.Find(idsesion);
            return PartialView(ussesiones);
        }

        // POST: clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var datosusuarios = (from d in db.usuario_sesion where d.Idusuario_sesion == id select d).FirstOrDefault();
                datosusuarios.Fecha_baja = DateTime.Now;
                datosusuarios.Usuario_baja = (string)Session["usuario_logueado"];
                datosusuarios.Estado = 2;
                db.SaveChanges();
                return Json(new { success = true, mensaje = "Se ha eliminado la sesion del usuario satisfactoriamente." });
            }
            catch (Exception ex)
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
