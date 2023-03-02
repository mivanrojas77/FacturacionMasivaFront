using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Linde.App.FacturacionMasiva.Objetos
{
    public class Producto
    {
        public string Id { get; set; }
        public string NombreProducto { get; set; }
        public Compañia ObtenerCompañia { get; set; }
        public string Articulo { get; set; }
        public int? Total { get; set; }
        public bool? AplicaFlete { get; set; }
        public bool? AplicaFacturacion { get; set; }
        public string OrdenRemision { get; set; }
        public float Cantidad { get; set; }

    }
}