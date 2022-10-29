using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Configuration;
using MySql.Data.MySqlClient;
using Monografia.Models;

namespace Monografia.Controllers
{
    public class Config_perfilesController : Controller
    {
        private proyectotiendaEntities db = new proyectotiendaEntities();
        static Modelo_Config Modelo_actual = new Modelo_Config();
        static string coneccion = ConfigurationManager.ConnectionStrings["proyectotiendaEntities"].ConnectionString;
        static string[] coneccion_info = coneccion.Split(';');
        static string directorio_respaldo = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Respaldos\\";
        static string usuario = coneccion_info[3].Substring(8);
        static string contraseña = coneccion_info[4].Substring(9);
        static string BD = coneccion_info[6].Substring(9).TrimEnd('"');
        static string servidor = coneccion_info[2].Substring(35);
        static string mysqlconeccion = "Server=" + servidor + ";Database=" + BD + ";User ID=" + usuario + ";Password=" + contraseña + ";Pooling=false;";
        // GET: perfiles

        public ActionResult Index()
        {
            Modelo_actual.checksboxes = obtener_checkboxs();
            Modelo_actual.perfiles_usuarios = consultar_perfiles();
            return View(Modelo_actual);
        }


        [HttpPost]
        public ActionResult Index(Modelo_Config lista_check)
        {
            string perfil;
            StringBuilder cadena = new StringBuilder();
            cadena.Append("|");
            foreach (var item in lista_check.checksboxes)
            {
                if (item.seleccionado)
                {
                    cadena.Append(item.id + "|");
                }
            }
            //ViewBag.selectcheck = cadena.ToString();
            
            perfil = Request["txtperfil"].ToString();
            introducir_perfil(perfil, cadena.ToString());
            Modelo_actual.perfiles_usuarios = consultar_perfiles();
            Modelo_actual.checksboxes = obtener_checkboxs();
            return View(Modelo_actual);
        }

        private string introducir_perfil(string perfil,string codigo)
        {          
            MySqlConnection mysqlcon = new MySqlConnection(mysqlconeccion);
            mysqlcon.Open();

            try
            {              
                MySqlCommand comando = new MySqlCommand("INSERT INTO usuarios_perfiles (perfil,codigo_perfil) values ('" + perfil + "','" + codigo + "')  ON DUPLICATE KEY UPDATE codigo_perfil ='" + codigo + "'", mysqlcon);
                comando.ExecuteNonQuery();
                mysqlcon.Close();
                return ("insertado");
            }
            catch {
                mysqlcon.Close();
                return ("fallido");
            }           
        }

        private List<perfiles> consultar_perfiles()
        {          
            MySqlConnection mysqlcon = new MySqlConnection(mysqlconeccion);
            mysqlcon.Open();
            MySqlCommand comando = new MySqlCommand("SELECT Descripcion_perfil FROM proyectotienda.usuarios_perfiles", mysqlcon);
            MySqlDataReader lector = comando.ExecuteReader();
            List<perfiles> lista_perfiles = new List<perfiles>();

            List<perfiles> perfiles = new List<perfiles>();
            while (lector.Read())
            {
                perfiles.Add(new perfiles() { nom_perfil = lector["Descripcion_perfil"].ToString()});
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
                check.Add(new checkboxs() { id = lector["Id_permiso"].ToString(), texto = lector["Descripcion"].ToString(), seleccionado = Convert.ToBoolean(lector["SELECCIONADO"]), nombre_grupo = lector["GRUPO"].ToString(), inicio_grupo = Convert.ToBoolean(lector["INICIO_GRUPO"])});             
            }

            mysqlcon.Close();
            return (check);
        }

        private List<checkboxs> obtener_checkboxs_mod(string perfil)
        {
            MySqlConnection mysqlcon = new MySqlConnection(mysqlconeccion);
            mysqlcon.Open();
            MySqlCommand comando = new MySqlCommand("SELECT Id_permiso,Descripcion,((select codigo_perfil from usuarios_perfiles where perfil = '" + perfil + "') like (CONCAT('%|',Id_permiso,'|%')) ) AS SELECCIONADO,GRUPO,INICIO_GRUPO FROM lista_permisos order by Id_permiso,inicio_grupo", mysqlcon);
            MySqlDataReader lector = comando.ExecuteReader();

            List<checkboxs> check = new List<checkboxs>();
            while (lector.Read())
            {
                check.Add(new checkboxs() { id = lector["Id_permiso"].ToString(), texto = lector["Descripcion"].ToString(), seleccionado = Convert.ToBoolean(lector["SELECCIONADO"]), nombre_grupo = lector["GRUPO"].ToString(), inicio_grupo = Convert.ToBoolean(lector["INICIO_GRUPO"]) });
            }

            mysqlcon.Close();
            return (check);
        }
        public ActionResult Modificar(string nombre_perfil)
        {
            Modelo_actual.checksboxes = obtener_checkboxs_mod(nombre_perfil);
            ViewData["txtperfil"] = nombre_perfil;
            return View("Index",Modelo_actual);
        }
 
        public ActionResult Eliminar(string nombre_perfil)
        {
            Modelo_actual.perfiles_usuarios = consultar_perfiles();
            return View("Index", Modelo_actual);
        }

    }
}