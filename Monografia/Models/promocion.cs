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
    
    public partial class promocion
    {
        public int Idpromocion { get; set; }
        public System.DateTime Fecha_alta { get; set; }
        public Nullable<System.DateTime> Fecha_baja { get; set; }
        public string Usuario_baja { get; set; }
        public string Nombre_promocion { get; set; }
        public int Cod_producto { get; set; }
        public int Cant_desde { get; set; }
        public int Cant_hasta { get; set; }
        public int Precio_unitario { get; set; }
        public int Estado { get; set; }
    }
}
