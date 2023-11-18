using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Entidades.Response
{
    public class ResObtenerPartidasPorID : ResBase
    {
        public List<Partida> listaDePartidasPorID { get; set; }
    }
}
