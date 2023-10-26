using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Entidades
{
    public class Jugador
    {
        public string NombreUusario { get; set; }
        public string password { get; set; }
        public int puntuacion { get; set; }
        public byte[] skinActual { get; set; }
        public string skinNombre {  get; set; }
    }
}
