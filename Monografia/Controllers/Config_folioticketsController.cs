using Monografia.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Monografia.Controllers
{
    public class Config_folioticketsController : Controller
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
        // GET: Config_foliotickets
        public ActionResult Index()
        {
            Modelo_actual.Lista_opciones = Obtener_opciones();
            return View("Index", Modelo_actual);
        }
        [HttpPost]
        public ActionResult Index(Modelo_Config Modelo)
        {
            foreach (var item in Modelo.Lista_opciones)
            {

                if (item.ID_OP.Contains("OP1_FDT")) { item.DETALLE_EXT1 = Request["txtfolio"].ToString(); }
                Actualizar_datos(item.ID_OP, item.DETALLE_EXT1);
            }
            Modelo_actual.Lista_opciones = Obtener_opciones();
            return View("Index", Modelo_actual);
        }

        private List<Opcion> Obtener_opciones()
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

        private string Actualizar_datos(string ID, string EXT1)
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
    }
}