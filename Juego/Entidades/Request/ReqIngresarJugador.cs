using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juego.Entidades.Request
{
    public class ReqIngresarJugador : ReqBase
    {
        public Jugador elJugador { get; set; }
    }
}
