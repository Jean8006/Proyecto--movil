using Backend.Entidades;
using Backend.Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace WebAPI.Controllers
{
    public class SesionController : ApiController
    {
        [HttpPost]
        [Route("api/sesion/CrearSesion")]
        public ResCrearSesion crearSesion(ReqCrearSesion req)
        {
            LogSesion logica = new LogSesion();
            return logica.crearSesion(req);
        }
    }
}