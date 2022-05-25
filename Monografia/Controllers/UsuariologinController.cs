using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Monografia.Models;
using System.Web.Security;

namespace Monografia.Controllers
{
    public class UsuariologinController : Controller
    {

        proyectotiendaEntities db = new proyectotiendaEntities();
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

                var login =  db.usuarios_tienda.Join( db.usuario_detalle,
                            a => a.Idusuario,
                            b => b.Idusuario,
                            (x, y) => new { x,y }).Where(x =>x.x.Login.Equals(usuariologin.Login)
                                && x.x.Contraseña.Equals(usuariologin.Contraseña) && x.x.Estado_usuario==1).FirstOrDefault();

                Session["Idusuario"] = login.x.Idusuario;
                Session["usuario_logueado"] =login.x.Login;
                Session["Nombreuusuario"] = login.y.Nombre;
                if (login != null && login.x.Perfil == "Admin")
                {
                    var sesion = db.usuario_sesion.Where(x => x.Id_usuario == login.x.Idusuario && x.Estado == 1).FirstOrDefault();
                    if (sesion != null)
                    {
                        return RedirectToAction("Login", "Usuariologin");
                    }
                    else {
                        datosesion = new usuario_sesion();
                        datosesion.Usuario_alta = login.x.Login;
                        datosesion.Fecha_alta = DateTime.Now;
                        datosesion.Id_usuario = login.x.Idusuario;
                        datosesion.Estado = 1;
                        db.usuario_sesion.Add(datosesion);
                        db.SaveChanges();
                        return RedirectToAction("Paginainicio", "Usuariologin");
                    }
                 

                }
                else
                {
                    return RedirectToAction("Login", "Usuariologin");
                }

            }
            catch (Exception ex)
            {
                return View();
            }
        }

        public ActionResult Paginainicio()
        {
            return View();
        }
        public ActionResult Logout()
        {
            int idusuario = Convert.ToInt32(Session["Idusuario"]);
            try
            {
                FormsAuthentication.SignOut();
                Session.Abandon();

                var sesion = db.usuario_sesion.Where(x => x.Id_usuario == idusuario && x.Estado == 1).FirstOrDefault();

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
                return Json(new { success = true });
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
