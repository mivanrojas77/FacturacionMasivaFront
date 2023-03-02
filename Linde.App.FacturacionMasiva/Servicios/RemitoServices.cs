using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Web;

namespace Linde.App.FacturacionMasiva.Servicios
{
    public class RemitoServices
    {
        public static List<Objetos.ClienteRemito> GetCliente(int idEmpresa, string sucur)        {
            var url = System.Configuration.ConfigurationManager.AppSettings["ServicioClienteCompania"] ;
            url = url.Replace("*", "&");
            url = url.Replace("{0}", idEmpresa.ToString());
            url = url.Replace("{1}", sucur.ToString());
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";

            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null)
                            return new List<Objetos.ClienteRemito>();
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();
                            // Do something with responseBody
                            //Console.WriteLine(responseBody);
                            List<Objetos.ClienteRemito> salida = JsonSerializer.Deserialize<List<Objetos.ClienteRemito>>(responseBody);
                            return salida;
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                // Handle error
                return new List<Objetos.ClienteRemito>();
            }
        }

        public static List<Objetos.ClienteRemito> GetClienteAgencia(int idEmpresa, string cliente)
        {
            var url = System.Configuration.ConfigurationManager.AppSettings["ServicioClienteCompania"];
            url = url.Replace("*", "&");
            url = url.Replace("{0}", idEmpresa.ToString());
            url = url.Replace("{1}", cliente.ToString());
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";

            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null)
                            return new List<Objetos.ClienteRemito>();
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();
                            // Do something with responseBody
                            //Console.WriteLine(responseBody);
                            List<Objetos.ClienteRemito> salida = JsonSerializer.Deserialize<List<Objetos.ClienteRemito>>(responseBody);
                            return salida;
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                // Handle error
                return new List<Objetos.ClienteRemito>();
            }
        }

        public static List<Objetos.Cliente> GetCliente()
        {
            var url = System.Configuration.ConfigurationManager.AppSettings["ServicioClientes"];
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";

            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null)
                            return new List<Objetos.Cliente>();
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();
                            // Do something with responseBody
                            //Console.WriteLine(responseBody);
                            List<Objetos.Cliente> salida = JsonSerializer.Deserialize<List<Objetos.Cliente>>(responseBody);
                            return salida;
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                // Handle error
                return new List<Objetos.Cliente>();
            }
        }

        public static List<Objetos.Remito> GetProductosCliente(string cliente)
        {
            var url = System.Configuration.ConfigurationManager.AppSettings["ServicioInfoRemito"];
            url = url.Replace("{0}", cliente.ToString());
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            request.Timeout = 120000;

            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null)
                            return new List<Objetos.Remito>();
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();
                            List<Objetos.Remito> salida = JsonSerializer.Deserialize<List<Objetos.Remito>>(responseBody);
                            return salida;
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                // Handle error
                return new List<Objetos.Remito>();
            }
        }

        public static bool PostFacturacion(string remito, string cliente, string producto, string remi, string tremi, string tfac, string cond, double valor, double iva, double total, string mail, string obs, string talo, string cantidad)
        {
            string postData = "";
            byte[] data = Encoding.ASCII.GetBytes(postData);

            bool salida = false;
            var url = System.Configuration.ConfigurationManager.AppSettings["ServicioFacturacion"];
            url = url.Replace('*', '&');
            url = url.Replace("{0}", remito.ToString());
            url = url.Replace("{1}", cliente.ToString());
            url = url.Replace("{2}", producto.ToString());
            url = url.Replace("{3}", remi.ToString());
            url = url.Replace("{4}", tremi.ToString());
            url = url.Replace("{5}", tfac.ToString());
            url = url.Replace("{6}", cond.ToString());
            url = url.Replace("{7}", valor.ToString());
            url = url.Replace("{8}", iva.ToString());
            url = url.Replace("{9}", total.ToString());
            url = url.Replace("{10}", mail.ToString());
            url = url.Replace("{11}", obs.ToString());
            url = url.Replace("{12}", talo.ToString());
            url = url.Replace("{13}", cantidad.ToString());

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";

            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            Stream requestStream = request.GetRequestStream();
            requestStream.Write(data, 0, data.Length);
            requestStream.Close();

            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null)
                            return false;
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();
                            salida = bool.Parse(responseBody);
                            return salida;
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                // Handle error
                return false;
            }
        }

        public static List<Objetos.Agencia> GetAgencias(int compania)
        {
            var url = System.Configuration.ConfigurationManager.AppSettings["ServicioAgenciasPorCompania"];
            url = url.Replace("{0}", compania.ToString());
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";

            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null)
                            return new List<Objetos.Agencia>();
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();
                            List<Objetos.Agencia> salida = JsonSerializer.Deserialize<List<Objetos.Agencia>>(responseBody);
                            return salida;
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                // Handle error
                return new List<Objetos.Agencia>();
            }
        }

        public static List<Objetos.FacturaRemito> GetFacturaRemito(DateTime fechaInicial, DateTime fechaFinal)
        {
            var url = System.Configuration.ConfigurationManager.AppSettings["ServicioFacturaRemito"];
            url = url.Replace("*", "&");
            url = url.Replace("{0}", fechaInicial.ToShortDateString());
            url = url.Replace("{1}", fechaFinal.ToShortDateString());
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            request.Timeout = 5000000;

            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null)
                            return new List<Objetos.FacturaRemito>();
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();
                            // Do something with responseBody
                            //Console.WriteLine(responseBody);
                            List<Objetos.FacturaRemito> salida = JsonSerializer.Deserialize<List<Objetos.FacturaRemito>>(responseBody);
                            return salida;
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                // Handle error
                return new List<Objetos.FacturaRemito>();
            }
        }

        public static string GetExportarRemito(DateTime fechaInicial, DateTime fechaFinal)
        {
            var url = System.Configuration.ConfigurationManager.AppSettings["ServicioExpFacturaRemito"];
            url = url.Replace("*", "&");
            url = url.Replace("{0}", fechaInicial.ToShortDateString());
            url = url.Replace("{1}", fechaFinal.ToShortDateString());
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            request.Timeout = 500000;

            try
            {
                using (WebResponse response = request.GetResponse())
                {

                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null)
                            return "#";
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();
                            // Do something with responseBody
                            //Console.WriteLine(responseBody);
                            string salida = JsonSerializer.Deserialize<string>(responseBody);
                            return salida;
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                // Handle error
                return "#";
            }
        }

        public static List<Objetos.Talonario> GetListaTalonarios(int  empId, int agenciaId)
        {
            var url = System.Configuration.ConfigurationManager.AppSettings["ServicioTalonarios"];
            url = url.Replace("*", "&");
            url = url.Replace("{0}", empId.ToString());
            url = url.Replace("{1}", agenciaId.ToString());
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            request.Timeout = 500000;

            try
            {
                using (WebResponse response = request.GetResponse())
                {

                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null)
                            return null;
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();
                            List<Objetos.Talonario> salida = JsonSerializer.Deserialize<List<Objetos.Talonario>>(responseBody);
                            return salida;
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                // Handle error
                return null;
            }
        }
    }
}