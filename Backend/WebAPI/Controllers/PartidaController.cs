using Backend.Entidades;
using Backend.Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace WebAPI.Controllers
{
    public class PartidaController : ApiController
    {
        [HttpPost]
        [Route("api/partida/ingresarPartida")]
        public ResIngresarPartida ingresarPartida(ReqIngresarPartida req)
        {
            LogPartidas logica = new LogPartidas();
            return logica.ingresarPartida(req);
        }
    }
}