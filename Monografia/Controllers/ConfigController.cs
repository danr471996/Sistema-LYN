using Monografia.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using static System.Data.Entity.Infrastructure.Design.Executor;

namespace Monografia.Controllers
{
    public class ConfigController : Controller
    {
        private proyectotiendaEntities db = new proyectotiendaEntities();
        private Modelo_Config Modelo_actual = new Modelo_Config();
        static string coneccion = ConfigurationManager.ConnectionStrings["proyectotiendaEntities"].ConnectionString;
        static string[] coneccion_info = coneccion.Split(';');
        static string directorio_respaldo = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Respaldos\\";
        static string usuario = coneccion_info[3].Substring(8);
        static string contraseña = coneccion_info[4].Substring(9);
        static string BD = coneccion_info[6].Substring(9).TrimEnd('"');
        static string servidor = coneccion_info[2].Substring(35);
        static string mysqlconeccion = "Server=" + servidor + ";Database=" + BD + ";User ID=" + usuario + ";Password=" + contraseña + ";Pooling=false;";
        static string mysqldump = directoriodump(usuario, contraseña, BD, servidor);
        // GET: Config_foliotickets
        public ActionResult Folio()
        {
            Modelo_actual.Lista_opciones = Obtener_opciones_fo();
            return View("Folio", Modelo_actual);
        }
        [HttpPost]
        public ActionResult Folio(Modelo_Config Modelo)
        {
            foreach (var item in Modelo.Lista_opciones)
            {

                if (item.ID_OP.Contains("OP1_FDT")) { item.DETALLE_EXT1 = Request["txtfolio"].ToString(); }
                Actualizar_datos_fo(item.ID_OP, item.DETALLE_EXT1);
            }
            Modelo_actual.Lista_opciones = Obtener_opciones_fo();
            return View("Folio", Modelo_actual);
        }

        private List<Opcion> Obtener_opciones_fo()
        {
            MySqlConnection mysqlcon = new MySqlConnection(mysqlconeccion);
            mysqlcon.Open();
            MySqlCommand comando = new MySqlCommand(@"select ID_OP,NOMBRE_OP,DESCRIPCION_OP,DETALLE_EXT1 from opciones where ID_OP LIKE ('%_FDT');", mysqlcon);
            MySqlDataReader lector = comando.ExecuteReader();

            List<Opcion> op = new List<Opcion>();
            while (lector.Read())
            {
                op.Add(new Opcion() { ID_OP = lector["ID_OP"].ToString(), NOMBRE_OP = lector["NOMBRE_OP"].ToString(), DESCRIPCION_OP = lector["DESCRIPCION_OP"].ToString(),DETALLE_EXT1 = lector["DETALLE_EXT1"].ToString()});
            }

            mysqlcon.Close();
            return (op);
        }

        private string Actualizar_datos_fo(string ID, string EXT1)
        {
            MySqlConnection mysqlcon = new MySqlConnection(mysqlconeccion);
            mysqlcon.Open();

            try
            {
                MySqlCommand comando = new MySqlCommand("UPDATE OPCIONES SET DETALLE_EXT1 = '" + EXT1 + "' WHERE ID_OP = '" + ID + "'", mysqlcon);
                comando.ExecuteNonQuery();
                mysqlcon.Close();
                return ("guardado");
            }
            catch
            {
                mysqlcon.Close();
                return ("fallido");
            }
        }


        //************************************* UNIDADES DE MEDIDA

        public ActionResult Unidadesdemedida()
        {
            Modelo_actual.Lista_opciones = Obtener_opciones_ud();
            return View("Unidadesdemedida", Modelo_actual);
        }

        [HttpPost]
        public ActionResult Unidadesdemedida(Modelo_Config Modelo)
        {
            for (var i = 0; i < Modelo.Lista_opciones.Count; i++)
            {
                Modelo.Lista_opciones[i].DETALLE_EXT1 = Request["txtunidad" + i.ToString()].ToString();
                Actualizar_datos_ud(Modelo.Lista_opciones[i].ID_OP, Modelo.Lista_opciones[i].DETALLE_EXT1);
            }
            Modelo_actual.Lista_opciones = Obtener_opciones_ud();
            return View("Unidadesdemedida", Modelo_actual);
        }

        private string Actualizar_datos_ud(string ID, string EXT1)
        {
            MySqlConnection mysqlcon = new MySqlConnection(mysqlconeccion);
            mysqlcon.Open();

            try
            {
                MySqlCommand comando = new MySqlCommand("UPDATE OPCIONES SET DETALLE_EXT1 = '" + EXT1 + "' WHERE ID_OP = '" + ID + "'", mysqlcon);
                comando.ExecuteNonQuery();
                mysqlcon.Close();
                return ("guardado");
            }
            catch
            {
                mysqlcon.Close();
                return ("fallido");
            }
        }

        private List<Opcion> Obtener_opciones_ud()
        {
            MySqlConnection mysqlcon = new MySqlConnection(mysqlconeccion);
            mysqlcon.Open();
            MySqlCommand comando = new MySqlCommand(@"SELECT * FROM OPCIONES WHERE ID_OP LIKE ('%_UDM')", mysqlcon);
            MySqlDataReader lector = comando.ExecuteReader();

            List<Opcion> op = new List<Opcion>();
            while (lector.Read())
            {
                op.Add(new Opcion() { ID_OP = lector["ID_OP"].ToString(), NOMBRE_OP = lector["NOMBRE_OP"].ToString(), DESCRIPCION_OP = lector["DESCRIPCION_OP"].ToString(), SELECCIONADO_OP = Convert.ToBoolean(lector["SELECCIONADO_OP"]), DETALLE_EXT1 = lector["DETALLE_EXT1"].ToString(), DETALLE_EXT2 = lector["DETALLE_EXT2"].ToString(), DETALLE_EXT3 = lector["DETALLE_EXT3"].ToString() });
            }

            mysqlcon.Close();
            return (op);
        }


        //************************************* FORMAS DE PAGO


        public ActionResult Formasdepago()
        {
            Modelo_actual.Lista_opciones = Obtener_opciones_fp();
            return View("Formasdepago", Modelo_actual);
        }

        [HttpPost]
        public ActionResult Formasdepago(Modelo_Config Modelo)
        {
            foreach (var item in Modelo.Lista_opciones)
            {
                Actualizar_datos_fp(item.ID_OP, item.SELECCIONADO_OP);
            }
            Modelo_actual.Lista_opciones = Obtener_opciones_fp();
            return View("Formasdepago", Modelo_actual);
        }

        private List<Opcion> Obtener_opciones_fp()
        {
            MySqlConnection mysqlcon = new MySqlConnection(mysqlconeccion);
            mysqlcon.Open();
            MySqlCommand comando = new MySqlCommand(@"SELECT * FROM OPCIONES WHERE ID_OP LIKE ('%_FDP')", mysqlcon);
            MySqlDataReader lector = comando.ExecuteReader();

            List<Opcion> op = new List<Opcion>();
            while (lector.Read())
            {
                op.Add(new Opcion() { ID_OP = lector["ID_OP"].ToString(), NOMBRE_OP = lector["NOMBRE_OP"].ToString(), DESCRIPCION_OP = lector["DESCRIPCION_OP"].ToString(), SELECCIONADO_OP = Convert.ToBoolean(lector["SELECCIONADO_OP"]), DETALLE_EXT1 = lector["DETALLE_EXT1"].ToString(), DETALLE_EXT2 = lector["DETALLE_EXT2"].ToString(), DETALLE_EXT3 = lector["DETALLE_EXT3"].ToString() });
            }

            mysqlcon.Close();
            return (op);
        }

        private string Actualizar_datos_fp(string ID, bool SELECCIONADO)
        {
            MySqlConnection mysqlcon = new MySqlConnection(mysqlconeccion);
            mysqlcon.Open();

            try
            {
                MySqlCommand comando = new MySqlCommand("UPDATE OPCIONES SET SELECCIONADO_OP = " + Convert.ToInt32(SELECCIONADO) + " WHERE ID_OP = '" + ID + "'", mysqlcon);
                comando.ExecuteNonQuery();
                mysqlcon.Close();
                return ("guardado");
            }
            catch
            {
                mysqlcon.Close();
                return ("fallido");
            }
        }

        //************************************* IMPUESTOS


        public ActionResult Impuestos()
        {
            Modelo_actual.Lista_opciones = Obtener_opciones_ip();
            return View("Impuestos", Modelo_actual);
        }

        [HttpPost]
        public ActionResult Impuestos(Modelo_Config Modelo)
        {
            foreach (var item in Modelo.Lista_opciones)
            {
                Actualizar_datos_ip(item.ID_OP, item.SELECCIONADO_OP);
            }
            Modelo_actual.Lista_opciones = Obtener_opciones_ip();
            return View("Impuestos", Modelo_actual);
        }

        private List<Opcion> Obtener_opciones_ip()
        {
            MySqlConnection mysqlcon = new MySqlConnection(mysqlconeccion);
            mysqlcon.Open();
            MySqlCommand comando = new MySqlCommand(@"SELECT * FROM OPCIONES WHERE ID_OP LIKE ('%_IMP')", mysqlcon);
            MySqlDataReader lector = comando.ExecuteReader();

            List<Opcion> op = new List<Opcion>();
            while (lector.Read())
            {
                op.Add(new Opcion() { ID_OP = lector["ID_OP"].ToString(), NOMBRE_OP = lector["NOMBRE_OP"].ToString(), DESCRIPCION_OP = lector["DESCRIPCION_OP"].ToString(), SELECCIONADO_OP = Convert.ToBoolean(lector["SELECCIONADO_OP"]), DETALLE_EXT1 = lector["DETALLE_EXT1"].ToString(), DETALLE_EXT2 = lector["DETALLE_EXT2"].ToString(), DETALLE_EXT3 = lector["DETALLE_EXT3"].ToString() });
            }

            mysqlcon.Close();
            return (op);
        }

        private string Actualizar_datos_ip(string ID, bool SELECCIONADO)
        {
            MySqlConnection mysqlcon = new MySqlConnection(mysqlconeccion);
            mysqlcon.Open();

            try
            {
                MySqlCommand comando = new MySqlCommand("UPDATE OPCIONES SET SELECCIONADO_OP = " + Convert.ToInt32(SELECCIONADO) + " WHERE ID_OP = '" + ID + "'", mysqlcon);
                comando.ExecuteNonQuery();
                mysqlcon.Close();
                return ("guardado");
            }
            catch
            {
                mysqlcon.Close();
                return ("fallido");
            }
        }

      

        //************************************* OPCIONES HABILIDATAS

        public ActionResult Opcioneshabilitadas()
        {
            Modelo_actual.Lista_opciones = Obtener_opciones_oh();
            return View("Opcioneshabilitadas", Modelo_actual);
        }


        [HttpPost]
        public ActionResult Opcioneshabilitadas(Modelo_Config Modelo)
        {
            foreach (var item in Modelo.Lista_opciones)
            {

                if (item.ID_OP.Contains("OP4_OPH")) { item.DETALLE_EXT1 = Request["txtporcentaje"].ToString(); }
                else if (item.ID_OP.Contains("OP5_OPH")) { item.DETALLE_EXT1 = Request["slcopcion"].ToString(); }
                Actualizar_datos_oh(item.ID_OP, item.SELECCIONADO_OP, item.DETALLE_EXT1);
            }
            Modelo_actual.Lista_opciones = Obtener_opciones_oh();
            return View("Opcioneshabilitadas", Modelo_actual);
        }

        private string Actualizar_datos_oh(string ID, bool SELECCIONADO, string EXT1)
        {
            MySqlConnection mysqlcon = new MySqlConnection(mysqlconeccion);
            mysqlcon.Open();

            try
            {
                MySqlCommand comando = new MySqlCommand("UPDATE OPCIONES SET SELECCIONADO_OP = " + Convert.ToInt32(SELECCIONADO) + ",DETALLE_EXT1 = '" + EXT1 + "' WHERE ID_OP = '" + ID + "'", mysqlcon);
                comando.ExecuteNonQuery();
                mysqlcon.Close();
                return ("guardado");
            }
            catch
            {
                mysqlcon.Close();
                return ("fallido");
            }
        }

        private List<Opcion> Obtener_opciones_oh()
        {
            MySqlConnection mysqlcon = new MySqlConnection(mysqlconeccion);
            mysqlcon.Open();
            MySqlCommand comando = new MySqlCommand(@"SELECT * FROM OPCIONES WHERE ID_OP LIKE ('%_OPH')", mysqlcon);
            MySqlDataReader lector = comando.ExecuteReader();

            List<Opcion> op = new List<Opcion>();
            while (lector.Read())
            {
                op.Add(new Opcion() { ID_OP = lector["ID_OP"].ToString(), NOMBRE_OP = lector["NOMBRE_OP"].ToString(), DESCRIPCION_OP = lector["DESCRIPCION_OP"].ToString(), SELECCIONADO_OP = Convert.ToBoolean(lector["SELECCIONADO_OP"]), DETALLE_EXT1 = lector["DETALLE_EXT1"].ToString(), DETALLE_EXT2 = lector["DETALLE_EXT2"].ToString(), DETALLE_EXT3 = lector["DETALLE_EXT3"].ToString() });
            }

            mysqlcon.Close();
            return (op);
        }
        //************************************* PERFILES
        public ActionResult Lista_perfiles()
        {
            Modelo_actual.perfiles_usuarios = consultar_perfiles();
            return View(Modelo_actual);
        }


        public ActionResult Create()
        {
            Modelo_actual.checksboxes = obtener_checkboxs();
            return PartialView(Modelo_actual);
        }


        [HttpPost]
        public ActionResult Create(Modelo_Config lista_check)
        {
            string perfil= Request["txtperfil"].ToString();
            ViewBag.nomperfil = perfil;
            if (validadinputs(perfil)) {

                if (db.usuarios_perfiles.Where(x => x.Descripcion_perfil.ToUpper() == perfil.ToUpper()).FirstOrDefault() == null)
                {
                    StringBuilder cadena = new StringBuilder();
                cadena.Append("|");
                foreach (var item in lista_check.checksboxes)
                {
                    if (item.seleccionado)
                    {
                        cadena.Append(item.id + "|");
                    }
                }

                var resultado = introducir_perfil(perfil, cadena.ToString(), DateTime.Now, (string)Session["usuario_logueado"]);
                if (resultado == "insertado")
                {
                    return Json(new { success = true, mensaje = "Se ha creado perfil satisfactoriamente." });
                }
                else
                {

                    return PartialView(lista_check);
                    }
                }
                else
                {
                    ViewBag.Mensaje = "<i class='bi bi-exclamation-octagon me-1'></i>Ya existe un perfil con la misma descripcion<br>";
                    return PartialView(lista_check);
                }
            } else {
                return PartialView(lista_check);
            }

         
           
        }

        public ActionResult Edit(int? id,string nomperfil)
        {
            ViewBag.nomperfil = nomperfil;
            Modelo_actual = null;
        

            if (id != 0 && id != null)
                {
                var datosperfiles = db.usuarios_perfiles.Find(id);
                if (datosperfiles != null)
                {

                    Modelo_actual = new Modelo_Config
                    {
                        perfiles_usuarios = new List<perfiles>(),
                        checksboxes = new List<checkboxs>()
                    };

                    Modelo_actual.perfiles_usuarios.Add(new perfiles
                    {
                        Idperfil = Convert.ToInt32(id)
                    });

                    Modelo_actual.checksboxes = obtener_checkboxs_mod(Convert.ToInt32(id));

                    return PartialView(Modelo_actual);
                }
                else
                {

                    ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>No se encontro perfil";
                    return PartialView(Modelo_actual);
                }
            }
                else
                {

                ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Id de perfil erroneo";
                    return PartialView(Modelo_actual);
                }
        
          

        }

        [HttpPost]
        public ActionResult Edit(Modelo_Config lista_check,perfiles datoperfil)
        {
            string perfil = Request["txtperfil"].ToString();
            ViewBag.nomperfil = perfil;

            lista_check.perfiles_usuarios = new List<perfiles> {
                                                                 new perfiles
                                                                  {
                                                                   Idperfil = Convert.ToInt32(datoperfil.Idperfil)
                                                                   }
                                                                   };
            if (validadinputs(perfil))
                {
                var datosperfiles = db.usuarios_perfiles.Find(datoperfil.Idperfil);
                        if (datosperfiles != null)
                        {

                    if (db.usuarios_perfiles.Where(x => x.Descripcion_perfil.ToUpper() == perfil.ToUpper() && x.Id_perfil!=datoperfil.Idperfil).FirstOrDefault() == null)
                    {
                        StringBuilder cadena = new StringBuilder();
                            cadena.Append("|");
                            foreach (var item in lista_check.checksboxes)
                            {
                                if (item.seleccionado)
                                {
                                    cadena.Append(item.id + "|");
                                }
                            }

                            var resultado = Actualizar_perfil(Convert.ToInt32(datoperfil), perfil, cadena.ToString());
                            if (resultado == "actualizado")
                            {
                                return Json(new { success = true, mensaje = "Se ha actualizado perfil satisfactoriamente." });
                            }
                            else
                            {

                                return PartialView(lista_check);
                            }
                    }
                    else
                    {
                        ViewBag.Mensaje = "<i class='bi bi-exclamation-octagon me-1'></i>Ya existe un perfil con la misma descripcion<br>";
                        return PartialView(lista_check);
                    }
                }
                        else {
                             ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>No se encontro perfil";
                            return PartialView(lista_check);
                        }
                }
                else
                {
                return PartialView(lista_check);
                }
   



        }


        public ActionResult Delete(int? id)
        {
            Modelo_actual = null;


            if (id != 0 && id != null)
            {
                var datosperfiles = db.usuarios_perfiles.Find(id);
                if (datosperfiles != null)
                {

                    Modelo_actual = new Modelo_Config
                    {
                        perfiles_usuarios = new List<perfiles>(),
                    };

                    Modelo_actual.perfiles_usuarios.Add(new perfiles
                    {
                        Idperfil = Convert.ToInt32(datosperfiles.Id_perfil),
                        nom_perfil = datosperfiles.Descripcion_perfil
                    }) ;

                    return PartialView(Modelo_actual);
                }
                else
                {

                    ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>No se encontro perfil";
                    return PartialView(Modelo_actual);
                }
            }
            else
            {

                ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Id de perfil erroneo";
                return PartialView(Modelo_actual);
            }


        }

        // POST: clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {

            try
            {
                Modelo_actual = null;
                if (id != 0 && id != null)
                {
                    var datosperfiles = db.usuarios_perfiles.Find(id);

                    if (datosperfiles != null)
                    {

                        datosperfiles.Fecha_baja = DateTime.Now;
                        datosperfiles.Usuario_baja = (string)Session["usuario_logueado"];
                        datosperfiles.Estado = 2;
                        db.SaveChanges();
                        return Json(new { success = true, mensaje = "Se ha inactivado el perfil satisfactoriamente." });
                    }
                    else
                    {
                        ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>No se encontro perfil";
                        return PartialView(Modelo_actual);
                    }
                }
                else
                {
                    ViewBag.Mensaje += "<i class='bi bi-exclamation-octagon me-1'></i>Id de perfil erroneo";
                    return PartialView(Modelo_actual);
                }


            }
            catch (Exception)
            {

                throw;
            }


        }

        public Boolean validadinputs(string datosperfil)
        {
            Boolean valid = true;
            if (String.IsNullOrEmpty(datosperfil))
            {
                ViewBag.Mensaje = "<i class='bi bi-exclamation-octagon me-1'></i>Debe ingresar la descripción del perfil<br>";
                valid = false;
            }


            return valid;
        }
        private string introducir_perfil(string perfil, string codigo,DateTime fechaalta,string usuarioalta)
        {
            MySqlConnection mysqlcon = new MySqlConnection(mysqlconeccion);
            mysqlcon.Open();

            try
            {
             
                string query = "INSERT INTO usuarios_perfiles (Fecha_alta,Usuario_alta,Descripcion_perfil,Codigo_accesos_perfil,Estado) values (@FechaAlta,@UsuarioAlta,@DescripcionPerfil,@CodigoAccesosPerfil,1)";
                using (MySqlCommand comando = new MySqlCommand(query, mysqlcon))
                {
                    comando.Parameters.AddWithValue("@FechaAlta", fechaalta.ToString("yyyy-MM-dd HH:mm:ss"));
                    comando.Parameters.AddWithValue("@UsuarioAlta", usuarioalta);
                    comando.Parameters.AddWithValue("@DescripcionPerfil", perfil);
                    comando.Parameters.AddWithValue("@CodigoAccesosPerfil", codigo);

                    comando.ExecuteNonQuery();
                    
                }
                mysqlcon.Close();

                return ("insertado");
            }
            catch
            {
                mysqlcon.Close();
                return ("fallido");
            }
        }

        private string Actualizar_perfil(int idperfil,string perfil, string codigo)
        {
            MySqlConnection mysqlcon = new MySqlConnection(mysqlconeccion);
            mysqlcon.Open();

            try
            {
           
                string query = "UPDATE usuarios_perfiles SET Descripcion_perfil = @DescripcionPerfil, Codigo_accesos_perfil = @CodigoAccesosPerfil WHERE Id_perfil=@idperfil";
                using (MySqlCommand comando = new MySqlCommand(query, mysqlcon))
                {
                    comando.Parameters.AddWithValue("@DescripcionPerfil", perfil);
                    comando.Parameters.AddWithValue("@CodigoAccesosPerfil", codigo);
                    comando.Parameters.AddWithValue("@idperfil", idperfil);
                    comando.ExecuteNonQuery();

                }
                mysqlcon.Close();

                return ("actualizado");
            }
            catch
            {
                mysqlcon.Close();
                return ("fallido");
            }
        }

        private List<perfiles> consultar_perfiles()
        {
            MySqlConnection mysqlcon = new MySqlConnection(mysqlconeccion);
            mysqlcon.Open();
            MySqlCommand comando = new MySqlCommand("SELECT Id_perfil,Descripcion_perfil,Estado FROM proyectotienda.usuarios_perfiles", mysqlcon);
            MySqlDataReader lector = comando.ExecuteReader();

            List<perfiles> perfiles = new List<perfiles>();
            while (lector.Read())
            {
                perfiles.Add(new perfiles() { Idperfil= Convert.ToInt32(lector["Id_perfil"]), nom_perfil = lector["Descripcion_perfil"].ToString(), estado = Convert.ToInt32( lector["Estado"]) });
            }

            mysqlcon.Close();
            return (perfiles);

        }

        private List<checkboxs> obtener_checkboxs()
        {
            MySqlConnection mysqlcon = new MySqlConnection(mysqlconeccion);
            mysqlcon.Open();
            MySqlCommand comando = new MySqlCommand("SELECT * FROM lista_permisos order by Id_permiso,inicio_grupo", mysqlcon);
            MySqlDataReader lector = comando.ExecuteReader();

            List<checkboxs> check = new List<checkboxs>();
            while (lector.Read())
            {
                check.Add(new checkboxs() { id = lector["Id_permiso"].ToString(), texto = lector["Descripcion"].ToString(), seleccionado = Convert.ToBoolean(lector["SELECCIONADO"]), nombre_grupo = lector["GRUPO"].ToString(), inicio_grupo = Convert.ToBoolean(lector["INICIO_GRUPO"]) });
            }

            mysqlcon.Close();
            return (check);
        }

        private List<checkboxs> obtener_checkboxs_mod(int idperfil)
        {
          
                MySqlConnection mysqlcon = new MySqlConnection(mysqlconeccion);
                mysqlcon.Open();
                MySqlCommand comando = new MySqlCommand("SELECT Id_permiso,Descripcion,((select codigo_accesos_perfil from usuarios_perfiles where Id_perfil = '" + idperfil + "') like (CONCAT('%|',Id_permiso,'|%')) ) AS SELECCIONADO,GRUPO,INICIO_GRUPO FROM lista_permisos order by Id_permiso,inicio_grupo", mysqlcon);
                MySqlDataReader lector = comando.ExecuteReader();

                List<checkboxs> check = new List<checkboxs>();

                while (lector.Read())
                {
                    check.Add(new checkboxs() { id = lector["Id_permiso"].ToString(), texto = lector["Descripcion"].ToString(), seleccionado = Convert.ToBoolean(lector["SELECCIONADO"]), nombre_grupo = lector["GRUPO"].ToString(), inicio_grupo = Convert.ToBoolean(lector["INICIO_GRUPO"]) });

                }

                mysqlcon.Close();
                return (check);
          
        }
    

        //************************************* SIMBOLODEMONEDA


        public ActionResult Simbolodemoneda()
        {
            Modelo_actual.Lista_opciones = Obtener_opciones_sm();
            return View("Simbolodemoneda", Modelo_actual);
        }

        [HttpPost]
        public ActionResult Simbolodemoneda(Modelo_Config Modelo)
        {
            for (var i = 0; i < Modelo.Lista_opciones.Count; i++)
            {
                Modelo.Lista_opciones[i].DETALLE_EXT1 = Request["txtsimbolo" + i.ToString()].ToString();
                Actualizar_datos_sm(Modelo.Lista_opciones[i].ID_OP, Modelo.Lista_opciones[i].DETALLE_EXT1);
            }
            Modelo_actual.Lista_opciones = Obtener_opciones_sm();
            return View("Simbolodemoneda", Modelo_actual);
        }

        private List<Opcion> Obtener_opciones_sm()
        {
            MySqlConnection mysqlcon = new MySqlConnection(mysqlconeccion);
            mysqlcon.Open();
            MySqlCommand comando = new MySqlCommand(@"SELECT * FROM OPCIONES WHERE ID_OP LIKE ('%_SDM')", mysqlcon);
            MySqlDataReader lector = comando.ExecuteReader();

            List<Opcion> op = new List<Opcion>();
            while (lector.Read())
            {
                op.Add(new Opcion() { ID_OP = lector["ID_OP"].ToString(), NOMBRE_OP = lector["NOMBRE_OP"].ToString(), DESCRIPCION_OP = lector["DESCRIPCION_OP"].ToString(), SELECCIONADO_OP = Convert.ToBoolean(lector["SELECCIONADO_OP"]), DETALLE_EXT1 = lector["DETALLE_EXT1"].ToString(), DETALLE_EXT2 = lector["DETALLE_EXT2"].ToString(), DETALLE_EXT3 = lector["DETALLE_EXT3"].ToString() });
            }

            mysqlcon.Close();
            return (op);
        }

        private string Actualizar_datos_sm(string ID, string EXT1)
        {
            MySqlConnection mysqlcon = new MySqlConnection(mysqlconeccion);
            mysqlcon.Open();

            try
            {
                MySqlCommand comando = new MySqlCommand("UPDATE OPCIONES SET DETALLE_EXT1 = '" + EXT1 + "' WHERE ID_OP = '" + ID + "'", mysqlcon);
                comando.ExecuteNonQuery();
                mysqlcon.Close();
                return ("guardado");
            }
            catch
            {
                mysqlcon.Close();
                return ("fallido");
            }
        }

        //************************************* TICKETS

        public ActionResult Tickets()
        {
            Modelo_actual.Lista_opciones = Obtener_opciones_tk();
            return View("Tickets", Modelo_actual);
        }

        [HttpPost]
        public ActionResult Tickets(Modelo_Config Modelo)
        {
            foreach (var item in Modelo.Lista_opciones)
            {
                if (item.ID_OP.Contains("OP1_TKS")) { item.DETALLE_EXT1 = Request["textarea1"].ToString(); }
                else if (item.ID_OP.Contains("OP2_TKS")) { item.DETALLE_EXT1 = Request["textarea2"].ToString(); }
                Actualizar_datos_tk(item.ID_OP, item.SELECCIONADO_OP, item.DETALLE_EXT1);
            }
            Modelo_actual.Lista_opciones = Obtener_opciones_tk();
            return View("Tickets", Modelo_actual);
        }

        private string Actualizar_datos_tk(string ID, bool SELECCIONADO, string EXT1)
        {
            MySqlConnection mysqlcon = new MySqlConnection(mysqlconeccion);
            mysqlcon.Open();

            try
            {
                MySqlCommand comando = new MySqlCommand("UPDATE OPCIONES SET SELECCIONADO_OP = " + Convert.ToInt32(SELECCIONADO) + ",DETALLE_EXT1 = '" + EXT1 + "' WHERE ID_OP = '" + ID + "'", mysqlcon);
                comando.ExecuteNonQuery();
                mysqlcon.Close();
                return ("guardado");
            }
            catch
            {
                mysqlcon.Close();
                return ("fallido");
            }
        }

        private List<Opcion> Obtener_opciones_tk()
        {
            MySqlConnection mysqlcon = new MySqlConnection(mysqlconeccion);
            mysqlcon.Open();
            MySqlCommand comando = new MySqlCommand(@"select ID_OP,NOMBRE_OP,DESCRIPCION_OP,DETALLE_EXT1 from opciones where ID_OP LIKE ('%_TKS') Order by ID_OP;", mysqlcon);
            MySqlDataReader lector = comando.ExecuteReader();

            List<Opcion> op = new List<Opcion>();
            while (lector.Read())
            {
                op.Add(new Opcion() { ID_OP = lector["ID_OP"].ToString(), NOMBRE_OP = lector["NOMBRE_OP"].ToString(), DESCRIPCION_OP = lector["DESCRIPCION_OP"].ToString(), DETALLE_EXT1 = lector["DETALLE_EXT1"].ToString() });
            }

            mysqlcon.Close();
            return (op);
        }

        //************************************* ADMINISTRADORBD

        private static string directoriodump(string user, string pass, string database, string server)
        {
            string retorno = "Vacio";
            string mysqlconeccion = "Server=" + server + ";Database=" + database + ";User ID=" + user + ";Password=" + pass + ";Pooling=false;";
            MySqlConnection mysqlcon = new MySqlConnection(mysqlconeccion);
            mysqlcon.Open();
            MySqlCommand comando = new MySqlCommand("show variables where variable_name = 'basedir'", mysqlcon);
            MySqlDataReader lector = comando.ExecuteReader();

            while (lector.Read())
            {
                retorno = lector["Value"].ToString() + "bin";
            }

            mysqlcon.Close();
            return retorno;
        }


        public ActionResult Administradorbd()
        {
            archivos_bd();
            return View("Administradorbd");
        }

        [HttpPost]
        public ActionResult Subir_bd(HttpPostedFileBase file)
        {

            if (file != null && file.ContentLength > 0)
                try
                {
                    var extension = Path.GetExtension(file.FileName).ToLower();
                    if (extension.Equals(".sql"))
                    {
                        string directorio_archivo = Path.Combine(directorio_respaldo, Path.GetFileName(file.FileName));
                        if (System.IO.File.Exists(directorio_archivo))
                        {
                            System.IO.File.Delete(directorio_archivo);
                        }
                        file.SaveAs(directorio_archivo);
                        ViewBag.Message = "Archivo cargado con exito";
                    }
                    else
                    {
                        ViewBag.Message = "Por favor seleccione un archivo valido";
                    }
                }

                catch (Exception ex)
                {
                    ViewBag.Message = "Error:" + ex.Message.ToString();
                }
            else
            {
                ViewBag.Message = "No has seleccionado un archivo";
            }

            archivos_bd();
            return View("Administradorbd");
        }

        public ActionResult Restaurar_bd(string nombre_archivo)
        {
            string consulta = string.Format("mysql --host={0} --default-character-set=utf8  --port=3306 -u{1} -p{2} ", servidor, usuario, contraseña);
            string archivo = directorio_respaldo + nombre_archivo;
            string cmd = consulta + BD + " < \"" + archivo;
            string result = ejecutar_bd(mysqldump, cmd);
            archivos_bd();
            return View("Administradorbd");
        }
        public ActionResult Eliminar_bd(string nombre_archivo)
        {
            string archivo = directorio_respaldo + nombre_archivo;
            if (System.IO.File.Exists(archivo))
            {
                System.IO.File.Delete(archivo);
            }
            archivos_bd();
            return View("Administradorbd");
        }

        public ActionResult Descargar_bd(string nombre_archivo)
        {
            string archivo = directorio_respaldo + nombre_archivo;
            archivos_bd();
            if (System.IO.File.Exists(archivo))
            {
                byte[] bytes = System.IO.File.ReadAllBytes(archivo);
                return File(bytes, "application/octet-stream", nombre_archivo);
            }
            return View("Administradorbd");
        }

        public ActionResult Respaldar_bd()
        {

            string consulta = string.Format("mysqldump --host={0} --default-character-set=utf8 --lock-tables --routines --force --port=3306 -u{1} -p{2} --quick ", servidor, usuario, contraseña);
            if (!Directory.Exists(directorio_respaldo)) { Directory.CreateDirectory(directorio_respaldo); }
            string directorio = directorio_respaldo + DateTime.Now.ToString("yyyy-MM-dd(HH-mm-ss)") + BD + ".sql";
            string cmd = consulta + BD + " >" + directorio;
            string result = ejecutar_bd(mysqldump, cmd);

            if (System.IO.File.Exists(directorio))
            {
                FileInfo file = new FileInfo(directorio);
                long valor = file.Length;
                if (valor != 0)
                {
                    //CORRECTO
                    archivos_bd();
                    return View("Administradorbd");
                }
                else
                {
                    //ERROR
                    archivos_bd();
                    return View("Administradorbd");
                }

            }
            else
            {
                //ERROR
                archivos_bd();
                return View("Administradorbd");
            }

        }

        private string ejecutar_bd(string directorio_dump, string consulta)
        {
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.WorkingDirectory = directorio_dump;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            p.StandardInput.WriteLine(consulta);
            p.StandardInput.WriteLine("exit");
            return p.StandardError.ReadToEnd();
        }

        public ActionResult archivos_bd()
        {
            System.IO.Directory.CreateDirectory(directorio_respaldo);
            string[] directorios = Directory.GetFiles(directorio_respaldo);
            List<Modelo_Config> archivos = new List<Modelo_Config>();
            foreach (string diretorio in directorios)
            {
                archivos.Add(new Modelo_Config { filename = Path.GetFileName(diretorio) });
            }
            return View(archivos);
        }

        //************************************* MENU

        public ActionResult Menu()
        {
            return View("Menu");
        }

    }
}