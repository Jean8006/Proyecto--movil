using Backend.Entidades;
using Backend.Entidades.Request;
using Backend.Entidades.Response;
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
            LogPartida logica = new LogPartida();
            return logica.ingresarPartida(req);
        }
    }
}