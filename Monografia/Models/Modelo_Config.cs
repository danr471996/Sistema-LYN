using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Monografia.Models
{
    public class Modelo_Config
    {
        public List<checkboxs> checksboxes { get; set; }
        public List<perfiles> perfiles_usuarios { get; set; }
        public List<Opcion> Lista_opciones { get; set; }
        public string filename { get; set; }
    }


    public class checkboxs
    {
        public string id { get; set; }
        public string texto { get; set; }
        public bool seleccionado { get; set; }
        public string nombre_grupo { get; set; }
        public bool inicio_grupo { get; set; }

    }

    public class perfiles
    {
        public string nom_perfil { get; set; }
    }

    public class Opcion
    {
        public string ID_OP { get; set; }
        public string NOMBRE_OP { get; set; }
        public string DESCRIPCION_OP { get; set; }
        public bool SELECCIONADO_OP { get; set; }
        public string DETALLE_EXT1 { get; set; }
        public string DETALLE_EXT2 { get; set; }
        public string DETALLE_EXT3 { get; set; }
    }
}