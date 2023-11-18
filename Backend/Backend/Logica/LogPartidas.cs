using Backend.AccesoDatos;
using Backend.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Logica
{
    public class LogPartidas
    {
        public ResIngresarPartida ingresarPartida(ReqIngresarPartida req)
        {
            ResIngresarPartida res = new ResIngresarPartida();

            try
            {
                if (req.laPartida.JugadorID == 0)
                {
                    res.resultado = false;
                    res.errorMensaje = "Id de jugador faltante ";

                }
                else if (req.laPartida.Puntuacion < 0)
                {
                    res.resultado = false;
                    res.errorMensaje = "Puntuacion no válida";

                }
                else
                {
                    AccesoDatosLINQDataContext miLinq = new AccesoDatosLINQDataContext();
                    miLinq.RegistrarPartida(req.laPartida.JugadorID, req.laPartida.Puntuacion);
                    res.resultado = true;
                }
            }
            catch (Exception ex)
            {
                res.resultado = false;
                res.errorMensaje = ex.Message;
                Console.WriteLine(ex.ToString());
            }
            return res;
        }
    }
}
