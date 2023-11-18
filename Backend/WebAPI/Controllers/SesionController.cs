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

        [HttpPost]
        [Route("api/sesion/CambiarEstado")]
        public ResCrearSesion cambiarSesion(ReqCrearSesion req)
        {
            LogSesion cambiado = new LogSesion();
            return cambiado.cambiarEstado(req);
        }
    }
}