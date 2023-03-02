using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Linde.App.FacturacionMasiva.Objetos
{
    public class Agencia
    {
        public int Id { get; set; }
        public Compañia ObtenerCompañia { get; set; }
        public string NombreAgencia { get; set; }
    }
}