using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Linde.App.FacturacionMasiva.Objetos
{
    public class FacturaRemito
    {
        public int Id { get; set; }
        //public string IdProducto { get; set; }
        //public string NombreProducto { get; set; }
        public string IdCliente { get; set; }
        public string NombreCliente { get; set; }
        public string IdRemito { get; set; }
        public DateTime Fecha { get; set; }
        public string FechaTexto { get => Fecha.ToShortDateString(); }
        public string Estado { get; set; }
        public string EstadoDian { get; set; }
        public string Factura { get; set; }


    }
}