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
    public class UsuarioServices
    {
        public static Objetos.Usuario Autenticar(string dominio, string usuario)
        {
            var url = System.Configuration.ConfigurationManager.AppSettings["ServicioAutenticar"];
            url = url.Replace("*", "&");
            url = url.Replace("{0}", dominio);
            url = url.Replace("{1}", usuario);
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
                            return new Objetos.Usuario();
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();
                            // Do something with responseBody
                            //Console.WriteLine(responseBody);
                            Objetos.Usuario salida = JsonSerializer.Deserialize<Objetos.Usuario>(responseBody);
                            return salida;
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                // Handle error
                return new Objetos.Usuario();
            }
        }

        public static List<Objetos.Usuario> BuscarUsuarios()
        {
            var url = System.Configuration.ConfigurationManager.AppSettings["ServicioBuscarUsuarios"];
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
                            return new List<Objetos.Usuario>();
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();
                            // Do something with responseBody
                            //Console.WriteLine(responseBody);
                            List<Objetos.Usuario> salida = JsonSerializer.Deserialize<List<Objetos.Usuario>>(responseBody);
                            return salida;
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                // Handle error
                return new List<Objetos.Usuario>();
            }
        }

        public static bool CrearUsuario(string dominio, string usuario, int rol)
        {
            string postData = "";
            byte[] data = Encoding.ASCII.GetBytes(postData);

            var url = System.Configuration.ConfigurationManager.AppSettings["ServicioAdicionarUsuarios"];
            url = url.Replace('*', '&');
            url = url.Replace("{0}", dominio);
            url = url.Replace("{1}", usuario);
            url = url.Replace("{2}", rol.ToString());

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
                            // Do something with responseBody
                            //Console.WriteLine(responseBody);
                            bool salida = JsonSerializer.Deserialize<bool>(responseBody);
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

        public static bool InactivarUsuario(int id)
        {
            string postData = "";
            byte[] data = Encoding.ASCII.GetBytes(postData);

            var url = System.Configuration.ConfigurationManager.AppSettings["ServicioInactivarUsuarios"];
            url = url.Replace("{0}", id.ToString());

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
                            // Do something with responseBody
                            //Console.WriteLine(responseBody);
                            bool salida = JsonSerializer.Deserialize<bool>(responseBody);
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

        public static bool EditarUsuario(int id, int rol)
        {
            string postData = "";
            byte[] data = Encoding.ASCII.GetBytes(postData);

            var url = System.Configuration.ConfigurationManager.AppSettings["ServicioEditarUsuarios"];
            url = url.Replace("*", "&");
            url = url.Replace("{0}", id.ToString());
            url = url.Replace("{1}", rol.ToString());

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
                            // Do something with responseBody
                            //Console.WriteLine(responseBody);
                            bool salida = JsonSerializer.Deserialize<bool>(responseBody);
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
    }
}