using Backend.Entidades;
using Backend.Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace WebAPI.Controllers
{
    public class JugadorController : ApiController
    {
        [HttpPost]
        [Route("api/jugador/ingresarJugador")]
        public ResIngresarJugador ingresarJugador(ReqIngresarJugador req)
        {
            LogJugador logica = new LogJugador();
            return logica.ingresarJugador(req);
        }
    }
}