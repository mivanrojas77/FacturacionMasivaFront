using Linde.App.FacturacionMasiva.Objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Linde.App.FacturacionMasiva.App_Start
{
    public class InitCliente
    {
        private static List<Objetos.Cliente> _listaCliente;

        public static List<Cliente> ListaCliente {
            get
            {
                if (_listaCliente == null) {
                    _listaCliente = Servicios.RemitoServices.GetCliente();
                }
                return _listaCliente;
            }
        }
    }
}