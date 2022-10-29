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
                var detalleusuario= db.usuario_detalle.Find(id);
                modelo_contenedor.usuario_detalle = detalleusuario;
                return PartialView(modelo_contenedor);
            }
            catch (Exception ex)
            {

                throw;
            }
           
        }

        // GET: usuarios_tienda/Create
        public ActionResult Create()
        {
            Modelo_contenedor model = new Modelo_contenedor();
            cargarperfiles(model);
            return PartialView(model);
        }

        public void cargarperfiles(Modelo_contenedor model) {
            model.listaperfiles = db.usuarios_perfiles.ToList();

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
            catch (Exception ex)
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
                var usuarios = db.usuarios_tienda.Find(id);
                modelocontenedor.usuarios_tienda = usuarios;
                modelocontenedor.usuario_detalle = usuarios.usuario_detalle.FirstOrDefault();
                cargarperfiles(modelocontenedor);

                return PartialView(modelocontenedor);

            }
            catch (Exception ex)
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




                    var usuarios_tienda = db.usuarios_tienda.Include(x => x.usuario_detalle).Include(r=>r.usuarios_perfiles).Where(x => x.Idusuario == modelocontenedor.usuarios_tienda.Idusuario).FirstOrDefault();


                    usuarios_tienda.usuario_detalle.FirstOrDefault().Primer_nombre = modelocontenedor.usuario_detalle.Primer_nombre;
                    usuarios_tienda.usuario_detalle.FirstOrDefault().Primer_apellido = modelocontenedor.usuario_detalle.Primer_apellido;
                    usuarios_tienda.usuario_detalle.FirstOrDefault().Direccion = modelocontenedor.usuario_detalle.Direccion;
                    usuarios_tienda.Login = modelocontenedor.usuarios_tienda.Login;
                    usuarios_tienda.usuario_detalle.FirstOrDefault().Telefono = modelocontenedor.usuario_detalle.Telefono;
                    usuarios_tienda.Idusuario = modelocontenedor.usuarios_tienda.Idusuario;
                    usuarios_tienda.Contraseña = modelocontenedor.usuarios_tienda.Contraseña;
                    usuarios_tienda.Id_perfil = modelocontenedor.usuarios_tienda.Id_perfil;

                    db.SaveChanges();

                    return Json(new { success = true });
                }
                return PartialView(modelocontenedor.usuarios_tienda);
            }
            catch (Exception ex)
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
                usuarios_tienda usuariostienda = db.usuarios_tienda.Find(id);

                return PartialView(usuariostienda);
            }
            catch (Exception ex)
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
                var usuarios_tienda = db.usuarios_tienda.Include(a => a.usuario_detalle).Where(x => x.Idusuario.Equals(id)).FirstOrDefault();

                usuarios_tienda.Fecha_baja = DateTime.Now;
                usuarios_tienda.Usuario_baja = (string)Session["usuario_logueado"];
                usuarios_tienda.Estado_usuario = 2;
                usuarios_tienda.Fecha_baja = DateTime.Now;
                usuarios_tienda.Usuario_baja = (string)Session["usuario_logueado"];
                db.SaveChanges();
                return Json(new { success = true });
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
