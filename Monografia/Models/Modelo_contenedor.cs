using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Monografia.Models
{
    public class Modelo_contenedor
    {
        #region Modelos_contenidos
        public usuarios_tienda usuarios_tienda { get; set; }
        public usuario_detalle usuario_detalle { get; set; }
        public productos productos { get; set; }
        public departamento departamento { get; set; }
        public promocion promocion { get; set; }
        public creditos creditos { get; set; }

        public usuario_sesion datosesion { get; set; }

        #endregion

        public List<departamento> listadepartamento { get; set; }
        public List<productos> listaproductos { get; set; }



        #region lista_productos_bajo_inventario
        public int codigo_producto1 { get; set; }
        public string descripcionproducto1 { get; set; }
        public decimal preciodeventa1 { get; set; }
        public int? cantidaactual1 { get; set; }
        public int? cantidaminima1 { get; set; }
        public string descripciondepartamento { get; set; }
        #endregion

        //#region lista_productos_reporte_inventario
        //public int idproducto { get; set; }
        //public int codigo_producto2 { get; set; }
        //public string descripcionproducto1 { get; set; }
        //public decimal? preciocosto1 { get; set; }
        //public decimal? preciodeventa1 { get; set; }
        //public int? cantidaactual1 { get; set; }
        //public int? cantidaminima1 { get; set; }
        //#endregion

        #region lista_reporte_saldo
        public int idcliente { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public int? telefono { get; set; }
        public int? limitecredito { get; set; }
        public decimal? saldo { get; set; }
        public DateTime fechapago { get; set; }
        #endregion

        #region Lista_facturas
        public string fechasfacturas { get; set; }
        public int idfacturas { get; set; }
        public int idcliente2 { get; set; }
        public string Descripcionproducto { get; set; }
        public decimal Precioventa { get; set; }
        public int cantidad { get; set; }
        public decimal importe { get; set; }

        #endregion
   


    #region Facturacion
    public class  facturacion
    {
        public int CodProd { get; set; }
        public string Desc { get; set; }
        public decimal prec_vent { get; set; }
        public int Cant { get; set; }
        public decimal Impor { get; set; }
        public int existencia { get; set; }

    }
        #endregion
    }

}