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

                Modelo_contenedor modelo_contenedor = null;
                if (id != 0 && id != null)
                {

                    usuario_detalle detalleusuario = db.usuario_detalle.Find(id);
                    if (detalleusuario == null)
                    {
                        ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>No se encontro información de usuario";
                        return PartialView(modelo_contenedor);

                    }
                    else
                    {
                        modelo_contenedor = new Modelo_contenedor();
                        modelo_contenedor.usuario_detalle = detalleusuario;
                        return PartialView(modelo_contenedor);
                    }
                }
                else
                {
                    ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Id de usuario erroneo";
                    return PartialView(modelo_contenedor);

                }

            }
            catch (Exception ex)
            {

                throw;
            }
           
        }

        // GET: usuarios_tienda/Create
        public ActionResult Create()
        {
            Modelo_contenedor modelocontenedor = new Modelo_contenedor();
            modelocontenedor.listaperfiles= getperfiles();
            return PartialView(modelocontenedor);
        }

        public List<usuarios_perfiles> getperfiles() {
           return db.usuarios_perfiles.ToList();

        }
        // POST: usuarios_tienda/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Modelo_contenedor modelocontenedor)
        {

            try
            {
    

                if (validadinputs(modelocontenedor))
                {
                    var datosusuarios = db.usuarios_tienda.Include(a => a.usuario_detalle).Where(x =>x.Estado_usuario == 1).ToList();
                    if (datosusuarios.Where(x => x.Login.ToUpper() == modelocontenedor.usuarios_tienda.Login.ToUpper()).FirstOrDefault()==null)
                    {
                      
                        if (datosusuarios.SelectMany(usuario => usuario.usuario_detalle).Where(detalle => (detalle.Primer_nombre + detalle.Segundo_nombre + detalle.Primer_apellido + detalle.Segundo_apellido).ToUpper() != (modelocontenedor.usuario_detalle.Primer_nombre + modelocontenedor.usuario_detalle.Segundo_nombre + modelocontenedor.usuario_detalle.Primer_apellido + modelocontenedor.usuario_detalle.Segundo_apellido).ToUpper()).FirstOrDefault()==null)
                        {
                            modelocontenedor.usuarios_tienda.Fecha_alta = DateTime.Now;
                            modelocontenedor.usuarios_tienda.Usuario_alta = (string)Session["usuario_logueado"];
                            modelocontenedor.usuarios_tienda.Estado_usuario = 1;
                            db.usuarios_tienda.Add(modelocontenedor.usuarios_tienda);
                            db.SaveChanges();
                            modelocontenedor.usuario_detalle.Idusuario = modelocontenedor.usuarios_tienda.Idusuario;
                            modelocontenedor.usuario_detalle.Usuario_alta = (string)Session["usuario_logueado"];
                            modelocontenedor.usuario_detalle.Fecha_alta = DateTime.Now;
                            db.usuario_detalle.Add(modelocontenedor.usuario_detalle);
                            db.SaveChanges();
                            return Json(new { success = true, mensaje = "Se ha creado usuario satisfactoriamente." });
                        }
                        else {
                            modelocontenedor = new Modelo_contenedor();
                            modelocontenedor.listaperfiles = getperfiles();
                            ViewBag.Mensaje = "<i class='bi bi-exclamation-octagon me-1'></i>Ya existe un usuario con el mismo nombre<br>";
                            return PartialView(modelocontenedor);

                        }
                    }
                    else {
                        modelocontenedor = new Modelo_contenedor();
                        modelocontenedor.listaperfiles = getperfiles();
                        ViewBag.Mensaje = "<i class='bi bi-exclamation-octagon me-1'></i>Ya existe un usuario con el mismo login<br>";
                        return PartialView(modelocontenedor);
                    }
                }
                else
                {

                    modelocontenedor = new Modelo_contenedor();
                    modelocontenedor.listaperfiles = getperfiles();

                    return PartialView(modelocontenedor);
                }

            }
            catch (Exception ex)
            {

                throw;
            }
          
        }

        public Boolean validadinputs(Modelo_contenedor datoscliente)
        {
            Boolean valid = true;
            
            if (datoscliente.usuario_detalle.Primer_nombre == null)
            {
                ViewBag.Mensaje = "<i class='bi bi-exclamation-octagon me-1'></i>Debe ingresar el primer nombre del usuario<br>";
                valid = false;
            }
            if (datoscliente.usuario_detalle.Segundo_nombre == null)
            {
                ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Debe ingresar el Segundo nombre del usuario<br>";
                valid = false;
            }
            if (datoscliente.usuario_detalle.Primer_apellido == null)
            {
                ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Debe ingresar el primer apellido del usuario<br>";
                valid = false;
            }
            if (datoscliente.usuario_detalle.Segundo_apellido == null)
            {
                ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Debe ingresar el segundo apellido del usuario<br>";
                valid = false;
            }
            if (datoscliente.usuario_detalle.Telefono == null)
            {
                ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Debe ingresar el número telefonico del usuario<br>";
                valid = false;
            }
            else
            {
                if (datoscliente.usuario_detalle.Telefono.ToString().Length < 8)
                {
                    ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Debe ingresar un número telefonico Válido<br>";
                    valid = false;
                }
            }

            if (datoscliente.usuario_detalle.Direccion == null)
            {
                ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Debe ingresar la dirección del usuario<br>";
                valid = false;
            }
            if (datoscliente.usuarios_tienda.Id_perfil == 0)
            {
                ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Debe ingresar el tipo de perfil del usuario<br>";
                valid = false;
            }
        
            if (datoscliente.usuarios_tienda.Login == null)
            {
                ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Debe ingresar el login del usuario<br>";
                valid = false;

            }

            if (datoscliente.usuarios_tienda.Contraseña == null)
            {
                ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Debe ingresar la contraseña del usuario";
                valid = false;

            }
            return valid;
        }

        // GET: usuarios_tienda/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {

                Modelo_contenedor modelo_contenedor = null;
                usuarios_tienda usuarios = null;
                if (id != 0 && id != null)
                {

                    usuarios = db.usuarios_tienda.Find(id);
                    if (usuarios == null)
                    {

                        ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>No se encontro usuario";
                        return PartialView(modelo_contenedor);

                    }
                    else
                    {
                        modelo_contenedor = new Modelo_contenedor();
                        modelo_contenedor.usuarios_tienda = usuarios;
                        modelo_contenedor.usuario_detalle = usuarios.usuario_detalle.FirstOrDefault();
                        modelo_contenedor.listaperfiles = getperfiles();
                        return PartialView(modelo_contenedor);
                    }
                }
                else
                {
                    ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Id de usuario erroneo";
                    return PartialView(modelo_contenedor);

                }

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
                if (validadinputs(modelocontenedor))
                {
                    var usuarios_tienda = db.usuarios_tienda.Include(x => x.usuario_detalle).Include(r => r.usuarios_perfiles).Where(x => x.Idusuario == modelocontenedor.usuarios_tienda.Idusuario).FirstOrDefault();
                    if (usuarios_tienda == null)
                    {
                        ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>No se encontro usuario";
                        modelocontenedor.listaperfiles = new List<usuarios_perfiles>();
                        modelocontenedor.listaperfiles = getperfiles();

                        return PartialView(modelocontenedor);
                    }
                    else
                    {
                        var datosusuarios = db.usuarios_tienda.Include(a => a.usuario_detalle).Where(x => x.Estado_usuario == 1).ToList();
                        if (datosusuarios.SelectMany(usuario => usuario.usuario_detalle).Where(detalle => (detalle.Primer_nombre + detalle.Segundo_nombre + detalle.Primer_apellido + detalle.Segundo_apellido).ToUpper() != (modelocontenedor.usuario_detalle.Primer_nombre + modelocontenedor.usuario_detalle.Segundo_nombre + modelocontenedor.usuario_detalle.Primer_apellido + modelocontenedor.usuario_detalle.Segundo_apellido ).ToUpper() && detalle.Idusuariodetalle != modelocontenedor.usuarios_tienda.Idusuario).FirstOrDefault() == null)
                        {
                        usuarios_tienda.usuario_detalle.FirstOrDefault().Primer_nombre = modelocontenedor.usuario_detalle.Primer_nombre;
                        usuarios_tienda.usuario_detalle.FirstOrDefault().Primer_apellido = modelocontenedor.usuario_detalle.Primer_apellido;
                        usuarios_tienda.usuario_detalle.FirstOrDefault().Direccion = modelocontenedor.usuario_detalle.Direccion;
                        usuarios_tienda.Login = modelocontenedor.usuarios_tienda.Login;
                        usuarios_tienda.usuario_detalle.FirstOrDefault().Telefono = modelocontenedor.usuario_detalle.Telefono;
                        usuarios_tienda.Idusuario = modelocontenedor.usuarios_tienda.Idusuario;
                        usuarios_tienda.Contraseña = modelocontenedor.usuarios_tienda.Contraseña;
                        usuarios_tienda.Id_perfil = modelocontenedor.usuarios_tienda.Id_perfil;

                        db.SaveChanges();

                        return Json(new { success = true, mensaje = "Se ha actualizado la información usuario satisfactoriamente." });
                            }
                            else
                            {
                                modelocontenedor = new Modelo_contenedor();
                                modelocontenedor.listaperfiles = getperfiles();
                                ViewBag.Mensaje = "<i class='bi bi-exclamation-octagon me-1'></i>Ya existe un usuario con el mismo nombre<br>";
                                return PartialView(modelocontenedor);

                            }
                    }
                }
                else
                {
                    modelocontenedor.listaperfiles = new List<usuarios_perfiles>();
                    modelocontenedor.listaperfiles = getperfiles();

                    return PartialView(modelocontenedor);
                }



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

                usuarios_tienda usuariostienda = null;
                if (id != 0 && id != null)
                {
                    usuariostienda = db.usuarios_tienda.Find(id);

                    if (usuariostienda != null)
                    {
                        return PartialView(usuariostienda);
                    }
                    else
                    {
                        ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>No se encontro usuario";
                        return PartialView(usuariostienda);
                    }
                }
                else
                {
                    ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Id de usuario erroneo";
                    return PartialView(usuariostienda);
                }


            }
            catch (Exception ex)
            {

                throw;
            }
          
        }

        // POST: usuarios_tienda/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            try
            {
                usuarios_tienda usuarios_tienda = null;
                if (id != 0 && id != null)
                {
                    usuarios_tienda = db.usuarios_tienda.Include(a => a.usuario_detalle).Where(x => x.Idusuario==id).FirstOrDefault();

                    if (usuarios_tienda != null)
                    {

                        usuarios_tienda.Fecha_baja = DateTime.Now;
                        usuarios_tienda.Usuario_baja = (string)Session["usuario_logueado"];
                        usuarios_tienda.Estado_usuario = 2;
                        usuarios_tienda.Fecha_baja = DateTime.Now;
                        usuarios_tienda.Usuario_baja = (string)Session["usuario_logueado"];
                        usuarios_tienda.usuario_detalle.FirstOrDefault().Fecha_baja = DateTime.Now;
                        usuarios_tienda.usuario_detalle.FirstOrDefault().Usuario_baja= (string)Session["usuario_logueado"];
                        db.SaveChanges();
                        return Json(new { success = true, mensaje = "Se ha inactivado el usuario satisfactoriamente." });
                    }
                    else
                    {
                        ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>No se encontro usuario";
                        return PartialView(usuarios_tienda);
                    }
                }
                else
                {
                    ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Id de usuario erroneo";
                    return PartialView(usuarios_tienda);
                }


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
