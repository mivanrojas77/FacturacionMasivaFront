using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Linde.App.FacturacionMasiva.Objetos
{
    public class FacturaRemitoGrupo
    {
        public FacturaRemitoGrupo()
        {

        }
        string rem = "";
        string pro = "";
        string remi = "";
        string tremi = "";
        string tfac = "";
        string cond = "";
        string valor = "";
        string iva = "";
        string total = "";
        string mail = "";
        string obs = "";
        string talo = "";
        string cantidad = "";

        public string Rem { get => rem; set => rem = value; }
        public string Pro { get => pro; set => pro = value; }
        public string Remi { get => remi; set => remi = value; }
        public string Tremi { get => tremi; set => tremi = value; }
        public string Tfac { get => tfac; set => tfac = value; }
        public string Cond { get => cond; set => cond = value; }
        public string Valor { get => valor; set => valor = value; }
        public string Iva { get => iva; set => iva = value; }
        public string Total { get => total; set => total = value; }
        public string Mail { get => mail; set => mail = value; }
        public string Obs { get => obs; set => obs = value; }
        public string Talo { get => talo; set => talo = value; }
        public string Cantidad { get => cantidad; set => cantidad = value; }
    }
}