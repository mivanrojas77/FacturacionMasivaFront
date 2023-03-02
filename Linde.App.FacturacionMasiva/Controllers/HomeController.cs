using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;

namespace Linde.App.FacturacionMasiva.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            int idx = -1;
            var temp = User.Identity.Name;
            if (User.Identity.Name.IndexOf('\\') > 0)
                temp = User.Identity.Name.Split('\\')[0] + "\\\\" + User.Identity.Name.Split('\\')[1];
            else
                temp = "";

            ViewBag.usuario = temp;

            if (ViewBag.usuario == string.Empty)
                ViewBag.usuario = @"LINDE\\c7ss74";

            return View();
        }


        public ActionResult BuscarCliente(int empresa, string sucur)
        {
            var salida = Servicios.RemitoServices.GetCliente(empresa, sucur);
            return base.Json(salida, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetClienteAgencia(int empresa, string cliente)
        {
            var salida = Servicios.RemitoServices.GetClienteAgencia(empresa, cliente);
            return base.Json(salida, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BuscarProductos(string cliente)
        {
            var salida = new List<Objetos.Remito>();
            salida = Servicios.RemitoServices.GetProductosCliente(cliente);
            foreach (var s in salida) {
                string mensaje = "<table><thead><tr><th>Check</th><th>Producto</th><th>Cantidad</th><th>Total</th></tr></thead><tbody>";
                foreach (var p in s.ObtenerProductos)
                {
                    mensaje += "<tr><td><input type='checkbox' onclick='ManejarValor(this)' id='chkProd_" + s.Documento + "_" + p.Id + "' name='" + s.Documento + "_" + p.Id + "'></td><td>" + p.NombreProducto + "</td><td>" + p.Cantidad + "</td><td>" + s.Total + "</td></tr>";
                    //mensaje += "<tr><td><input type='checkbox' onclick='ManejarValor(this)' id='chkProd_" + s.Documento + "_" + p.Id + "' name='" + s.Documento + "_" + p.Id + "'></td><td>" + p.NombreProducto + "</td><td>" + p.Cantidad + "</td><td><input type='text' id='Remision_" + s.Documento + "_" + p.Id + "'/></td><td>" + s.Total + "</td></tr>";
                }
                mensaje += "</tbody></table>";
                s.HtmlProductos = mensaje;
                if (!s.Link.Equals("#"))
                    s.Link = "<a href='" + s.Link + "'>Enlace</a>";
                else
                    s.Link = "<p>sin enlace</p>";

            }
            return base.Json(salida, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BuscarProductosFacturacion(string cliente)
        {
            string real = "";
            var salida = Servicios.RemitoServices.GetProductosCliente(cliente);
            foreach (var s in salida)
            {
                foreach (var p in s.ObtenerProductos)
                {
                    bool t = Servicios.RemitoServices.PostFacturacion(s.Documento, cliente, p.Id, s.Remision, s.TipoRemito, s.TipoFacturacion.ToString(), s.CondPago.ToString(), s.Valor, s.Iva, s.Total, s.Mail, s.Observacion, s.Talonario, s.Cantidad);
                    if (t) {
                        real += "<p><b>Cliente:</b>" + cliente +  " <b>Documento:</b>" + s.Documento + " <b>Producto:</b>" + p.Id + "</p>";
                    }
                }

            }
            return base.Json(real, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FacturarRemito(string remito, string cliente, string producto, string remi, string tremi, string tfac, string cond, double valor, double iva, double total, string mail, string obs, string talo, string cantidad) {
            var salida = Servicios.RemitoServices.PostFacturacion(remito, cliente, producto, remi, tremi, tfac, cond, valor, iva, total, mail, obs, talo, cantidad);
            return base.Json(salida, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FacturarRemitoGrupo(string cliente, List<Objetos.FacturaRemitoGrupo> lObjetos)
        {
            string salida = "";
            if (lObjetos == null)
                salida = "Debe seleccionar por lo menos un producto de los remitos del cliente <b>" + cliente + "</b>";
            else {
                foreach (var f in lObjetos)
                {
                    if (f.Obs == null)
                        f.Obs = " ";
                    if (f.Remi == null)
                        f.Remi = "-1";

                    var s = Servicios.RemitoServices.PostFacturacion(f.Rem, cliente, f.Pro, f.Remi, f.Tremi, f.Tfac, f.Cond, double.Parse(f.Valor), double.Parse(f.Iva), double.Parse(f.Total), f.Mail, f.Obs, f.Talo, f.Cantidad);
                    if (s)
                        salida += f.Rem + "-" + f.Pro + " ";
                }
            }
            
            return base.Json(salida, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FacturarRemitoDesagregado(string remito, string cliente, string producto, string remi, string tremi, string tfac, string cond, double valor, double iva, double total, string mail, string obs, string talo, string cantidad)
        {
            string salida = "";
            var s = Servicios.RemitoServices.PostFacturacion(remito, cliente, producto, remi, tremi, tfac, cond, valor, iva, total, mail, obs, talo, cantidad);
            if (s)
            salida += remito + "-" + producto + " <br>";
        
            return base.Json(salida, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListarAgencias(int id) {
            var salida = Servicios.RemitoServices.GetAgencias(id);
            return base.Json(salida, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListarTalonarios(int empId, int agenciaId)
        {
            var salida = Servicios.RemitoServices.GetListaTalonarios(empId, agenciaId);
            return base.Json(salida, JsonRequestBehavior.AllowGet);
        }



        [System.Web.Http.HttpPost]
        public ActionResult AutocompletarCliente(string termino, string sucursal)
        {

           List<Objetos.Cliente> salida = new List<Objetos.Cliente>();
            if (termino != null)
            {
                if (termino.Length > 3)
                {
                    salida = App_Start.InitCliente.ListaCliente.Where(x => x.NombreCliente.ToUpper().Contains(termino) && x.Agencia.Equals(sucursal.PadLeft(5,'0') )).ToList();

                }
                else
                {
                    salida.Add(new Objetos.Cliente { NombreCliente = "" });
                }
            }
            else
            {
                salida.Add(new Objetos.Cliente { NombreCliente = "" });
            }
            return base.Json(salida, JsonRequestBehavior.AllowGet);
        }


        public ActionResult ListarFacturasRemito(DateTime fechaI,DateTime fechaF)
        {
            var salida = Servicios.RemitoServices.GetFacturaRemito(fechaI, fechaF);
            return base.Json(salida, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ExportarFacturasRemito(DateTime fechaI, DateTime fechaF)
        {
            var salida = Servicios.RemitoServices.GetExportarRemito(fechaI, fechaF);
            return base.Json(salida, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Autenticar(string dominio, string usuario)
        {
            var salida = Servicios.UsuarioServices.Autenticar(dominio, usuario);
            return base.Json(salida, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BuscarUsuarios()
        {
            var salida = Servicios.UsuarioServices.BuscarUsuarios();
            return base.Json(salida, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CrearUsuario(string dominio, string usuario, int rol)
        {
            var salida = Servicios.UsuarioServices.CrearUsuario(dominio, usuario, rol);
            return base.Json(salida, JsonRequestBehavior.AllowGet);
        }

        public ActionResult InactivarUsuario(int id) {
            var salida = Servicios.UsuarioServices.InactivarUsuario(id);
            return base.Json(salida, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditarUsuario(int id, int rol)
        {
            var salida = Servicios.UsuarioServices.EditarUsuario(id, rol);
            return base.Json(salida, JsonRequestBehavior.AllowGet);
        }
    }
}