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
    
    public partial class proveedor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public proveedor()
        {
            this.facturas_proveedor = new HashSet<facturas_proveedor>();
            this.productos = new HashSet<productos>();
        }
    
        public int IdProveedor { get; set; }
        public System.DateTime Fecha_alta { get; set; }
        public string Usuario_alta { get; set; }
        public Nullable<System.DateTime> Fecha_baja { get; set; }
        public string Usuario_baja { get; set; }
        public string Descripcion { get; set; }
        public Nullable<int> Telefono { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }
        public int Estado { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<facturas_proveedor> facturas_proveedor { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<productos> productos { get; set; }
    }
}
