using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Linde.App.FacturacionMasiva.Objetos
{
    public class Remito
    {
        public int Id { get; set; }

        public string TipoRemito { get; set; }
        public string Documento { get; set; }
        public int Compañia { get; set; }
        public string Agencia { get; set; }
        public Cliente ObtenerCliente { get; set; }
        public List<Producto> ObtenerProductos { get; set; }
        public DateTime FechaRemito { get; set; }
        public double Total { get; set; }
        public string NombreDocumento { get; set; }
        public string HtmlProductos { get; set; }
        public string FechaRemitoCadena {
            get { return FechaRemito.ToShortDateString(); }
        }

        public string Link { get; set; }
        public int TipoFacturacion { get; set; }
        public string DesTipoFacturacion { get; set; }
        public int CondPago { get; set; }
        public string DesCondPago { get; set; }
        public double Valor { get; set; }
        public double Iva { get; set; }
        public string Mail { get; set; }
        public string Observacion { get; set; }
        public string Remision { get; set; } 
        public string Talonario { get; set; }
        public string Cantidad { get; set; }

    }
}