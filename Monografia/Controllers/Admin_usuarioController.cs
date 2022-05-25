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
    public class Admin_usuarioController : Controller
    {
        private proyectotiendaEntities db = new proyectotiendaEntities();

        // GET: usuarios_tienda
        public ActionResult Lista_usuario()
        {
            return View(db.usuarios_tienda.ToList());
        }

        // GET: usuarios_tienda/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Modelo_contenedor modelo_contenedor = new Modelo_contenedor();
                modelo_contenedor.usuario_detalle = db.usuario_detalle.Find(id);
                modelo_contenedor.usuarios_tienda = db.usuarios_tienda.Find(id);
                if (modelo_contenedor == null)
                {
                    return HttpNotFound();
                }
                return PartialView(modelo_contenedor);
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        // GET: usuarios_tienda/Create
        public ActionResult Create()
        {
            return PartialView();
        }

        // POST: usuarios_tienda/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(usuarios_tienda usuarios_tienda, usuario_detalle usuario_detalle)
        {
            try
            {
                if (ModelState.IsValid)
                {


                    usuarios_tienda.Fecha_alta = DateTime.Now;
                    usuarios_tienda.Usuario_alta = (string)Session["usuario_logueado"];
                    usuarios_tienda.Estado_usuario = 1;
                    db.usuarios_tienda.Add(usuarios_tienda);
                    db.SaveChanges();
                    usuario_detalle.Idusuario = usuarios_tienda.Idusuario;
                    usuario_detalle.Usuario_alta = (string)Session["usuario_logueado"];
                    usuario_detalle.Fecha_alta = DateTime.Now;
                    db.usuario_detalle.Add(usuario_detalle);
                    db.SaveChanges();
                    return Json(new { success = true });
                }

                return PartialView(usuarios_tienda);
            }
            catch (Exception)
            {

                throw;
            }
          
        }

        // GET: usuarios_tienda/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Modelo_contenedor modelocontenedor = new Modelo_contenedor();
                modelocontenedor.usuarios_tienda = db.usuarios_tienda.Find(id);
                modelocontenedor.usuario_detalle = db.usuario_detalle.Find(id);
                if (modelocontenedor == null)
                {
                    return HttpNotFound();
                }
                return PartialView(modelocontenedor);

            }
            catch (Exception)
            {

                throw;
            }
        

        }

        // POST: usuarios_tienda/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Modelo_contenedor modelocontenedor)
        {
            try
            {
                if (ModelState.IsValid)
                {




                    var usuarios_tienda = db.usuarios_tienda.Where(x => x.Idusuario == modelocontenedor.usuarios_tienda.Idusuario).FirstOrDefault();
                    var usuarios_detalle = db.usuario_detalle.Where(x => x.Idusuario == modelocontenedor.usuarios_tienda.Idusuario).FirstOrDefault();
                    var datosusuarios = (from u in db.usuarios_tienda
                                         join u2 in db.usuario_detalle on u.Idusuario equals u2.Idusuario
                                         where u.Idusuario == modelocontenedor.usuarios_tienda.Idusuario
                                         select new
                                         {
                                             u,
                                             u2
                                         }).FirstOrDefault();

                    datosusuarios.u2.Nombre = modelocontenedor.usuario_detalle.Nombre;
                    datosusuarios.u2.Apellido = modelocontenedor.usuario_detalle.Apellido;
                    datosusuarios.u2.Direccion = modelocontenedor.usuario_detalle.Direccion;
                    datosusuarios.u.Login = modelocontenedor.usuarios_tienda.Login;
                    datosusuarios.u2.Telefono = modelocontenedor.usuario_detalle.Telefono;
                    datosusuarios.u.Idusuario = modelocontenedor.usuarios_tienda.Idusuario;
                    datosusuarios.u.Contraseña = modelocontenedor.usuarios_tienda.Contraseña;
                    datosusuarios.u.Perfil = modelocontenedor.usuarios_tienda.Perfil;

                    db.SaveChanges();

                    return Json(new { success = true });
                }
                return PartialView(modelocontenedor.usuarios_tienda);
            }
            catch (Exception)
            {

                throw;
            }
           
        }


        // GET: usuarios_tienda/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                usuarios_tienda usuarios_tienda = db.usuarios_tienda.Find(id);
                if (usuarios_tienda == null)
                {
                    return HttpNotFound();
                }
                return PartialView(usuarios_tienda);
            }
            catch (Exception)
            {

                throw;
            }
          
        }

        // POST: usuarios_tienda/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var usuarios_tienda = db.usuarios_tienda.Join(db.usuario_detalle,
                                          a => a.Idusuario,
                                          b => b.Idusuario,
                                          (x, y) => new { x, y }).Where(x => x.x.Idusuario.Equals(id)).FirstOrDefault();

                usuarios_tienda.x.Fecha_baja = DateTime.Now;
                usuarios_tienda.x.Usuario_baja = (string)Session["usuario_logueado"];
                usuarios_tienda.x.Estado_usuario = 2;
                usuarios_tienda.y.Fecha_baja = DateTime.Now;
                usuarios_tienda.y.Usuario_baja = (string)Session["usuario_logueado"];
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
