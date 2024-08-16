using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juego.Entidades
{
    public class Partida
    {
        public int? JugadorID { get; set; }
        public int? Puntuacion { get; set; }
        public string jugador { get; set; }
        public int Posicion { get; set; }
    }
}
