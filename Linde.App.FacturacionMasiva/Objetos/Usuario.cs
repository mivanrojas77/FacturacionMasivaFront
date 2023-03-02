using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Linde.App.FacturacionMasiva.Objetos
{
    public class Usuario
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public Rol RolUsuario { get; set; }
    }
}