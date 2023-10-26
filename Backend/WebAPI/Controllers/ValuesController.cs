using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace WebAPI.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
        public IHttpActionResult GetValues()
        {
            string clientIPAddress = GetClientIPAddress();

            string[] allowedIPs = new string[] { "186.15.205.40", "192.168.0.7" };

            if (IsIPInWhitelist(clientIPAddress, allowedIPs))
            {
                // La solicitud proviene de una IP permitida, no se requiere autenticación adicional
                // Realiza las operaciones de la API
                return Ok("Solicitud exitosa sin autenticación.");
            }
            else
            {
                // La solicitud proviene de una IP no permitida, se puede requerir autenticación aquí
                return Unauthorized(); // Devuelve un código 401 Unauthorized
            }
        }

        private string GetClientIPAddress()
        {
            // Obtiene la dirección IP del cliente desde la solicitud
            string clientIPAddress = HttpContext.Current.Request.UserHostAddress;
            return clientIPAddress;
        }

        private bool IsIPInWhitelist(string clientIPAddress, string[] allowedIPs)
        {
            // Verifica si la dirección IP del cliente está en la lista blanca
            return allowedIPs.Contains(clientIPAddress);
        }
    }
}
