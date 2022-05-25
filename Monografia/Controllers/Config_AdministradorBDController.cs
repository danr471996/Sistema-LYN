using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using MySql.Data.MySqlClient;
using Monografia.Models;

namespace Monografia.Controllers
{
    public class Config_AdministradorBDController : Controller
    {
        
        static string coneccion = ConfigurationManager.ConnectionStrings["proyectotiendaEntities"].ConnectionString;
        static string[] coneccion_info = coneccion.Split(';');
        static string directorio_respaldo = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Respaldos\\";       
        static string usuario = coneccion_info[3].Substring(8);
        static string contraseña = coneccion_info[4].Substring(9);
        static string BD = coneccion_info[6].Substring(9).TrimEnd('"');
        static string servidor = coneccion_info[2].Substring(35);
        static string mysqldump = directoriodump(usuario, contraseña, BD, servidor);
        private proyectotiendaEntities db = new proyectotiendaEntities();


        private static string directoriodump(string user,string pass,string database,string server)
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



       
        
        
        public ActionResult AdministracionBD_principal()
        {
            archivos();
            return View("inicio_bd");
        }

        [HttpPost]
        public ActionResult subir(HttpPostedFileBase file)
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

            archivos();
            return View("inicio_bd");
        }

        public ActionResult Restaurar(string nombre_archivo)
        {
                string consulta = string.Format("mysql --host={0} --default-character-set=utf8  --port=3306 -u{1} -p{2} ", servidor, usuario, contraseña);
                string archivo = directorio_respaldo + nombre_archivo;
                string cmd = consulta + BD + " < \"" + archivo;
                string result = ejecutar(mysqldump, cmd);
                archivos();
                return View("inicio_bd");
        }
        public ActionResult Eliminar(string nombre_archivo)
        {
            string archivo = directorio_respaldo + nombre_archivo;
            if (System.IO.File.Exists(archivo))
            {
                    System.IO.File.Delete(archivo);
            }
            archivos();
            return View("inicio_bd");
        }

        public ActionResult Descargar(string nombre_archivo)
        {
            string archivo = directorio_respaldo + nombre_archivo;
            archivos();
            if (System.IO.File.Exists(archivo))
            {
                byte[] bytes = System.IO.File.ReadAllBytes(archivo);
                return File(bytes, "application/octet-stream", nombre_archivo);
            }
            return View("inicio_bd");
        }

        public ActionResult Respaldar()
        {
            
            string consulta = string.Format("mysqldump --host={0} --default-character-set=utf8 --lock-tables --routines --force --port=3306 -u{1} -p{2} --quick ",servidor, usuario, contraseña);            
            if (!Directory.Exists(directorio_respaldo)){Directory.CreateDirectory(directorio_respaldo);}
            string directorio = directorio_respaldo + DateTime.Now.ToString("yyyy-MM-dd(HH-mm-ss)") + BD + ".sql";
            string cmd = consulta + BD + " >" + directorio;
            string result = ejecutar(mysqldump, cmd);

            if (System.IO.File.Exists(directorio))
            {
                FileInfo file = new FileInfo(directorio);
                long valor = file.Length;
                if (valor != 0)
                {
                    //CORRECTO
                    archivos();
                    return View("inicio_bd");
                }
                else {
                    //ERROR
                    archivos();
                    return View("inicio_bd");
                }
               
            }
            else
            {
                //ERROR
                archivos();
                return View("inicio_bd");
            }
           
        }

        private string ejecutar(string directorio_dump, string consulta)
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

        public ActionResult archivos() {
            string[] directorios = Directory.GetFiles(directorio_respaldo);
            List<Modelo_Config> archivos = new List<Modelo_Config>();
            foreach (string diretorio in directorios)
            {
                archivos.Add(new Modelo_Config { filename = Path.GetFileName(diretorio) });
            }
            return View(archivos);
        }


    }
}