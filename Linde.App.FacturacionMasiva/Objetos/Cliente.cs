using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Linde.App.FacturacionMasiva.Objetos
{
    public class Cliente
    {
        public int? Compañia { get; set; }
        public string Agencia { get; set; }
        public string CodCliente { get; set; }
        public string NombreCliente { get; set; }
    }
}