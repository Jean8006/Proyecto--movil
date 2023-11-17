using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Entidades
{
    public class Sesion
    {
        public int jugadorID { get; set; }
        public string sesion {  get; set; }
        public DateTime fechaInicio { get; set; }
        public DateTime fechaFin { get;set; }
        public bool estado {  get; set; }
    }
}
