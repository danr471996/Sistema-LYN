//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Monografia.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class usuarios_tienda
    {
        public int Idusuario { get; set; }
        public System.DateTime Fecha_alta { get; set; }
        public string Usuario_alta { get; set; }
        public Nullable<System.DateTime> Fecha_baja { get; set; }
        public string Usuario_baja { get; set; }
        public string Login { get; set; }
        public string Contraseña { get; set; }
        public string Perfil { get; set; }
        public int Estado_usuario { get; set; }
    }
}
