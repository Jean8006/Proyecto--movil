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
            LogPartidas logica = new LogPartidas();
            return logica.ingresarPartida(req);
        }

        [HttpGet]
        [Route("api/partida/ObtenerPartidasPorJugador")]
        public ResObtenerPartidasPorID obtenerPartidasPorID()
        {
            ReqObtenerPartidasPorID req = new ReqObtenerPartidasPorID();
            req.session = "sample string 2";
            LogPartidas miLogica = new LogPartidas();
            return miLogica.obtenerPartidasPorID(req);
        }
    }
}