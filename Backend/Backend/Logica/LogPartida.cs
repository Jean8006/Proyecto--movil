using Backend.AccesoDatos;
using Backend.Entidades;
using Backend.Entidades.Request;
using Backend.Entidades.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Logica
{
    public class LogPartida
    {

        public ResIngresarPartida ingresarPartida(ReqIngresarPartida req)
        {
            ResIngresarPartida res = new ResIngresarPartida();
            

            try 
            {

                DateTime fecha;

                if (req.laPartida.JugadorID == 0)
                {
                    res.resultado = false;
                    res.errorMensaje = "Id de jugador faltante";

                } else if (req.laPartida.Puntuacion == 0)
                {
                    res.resultado = false;
                    res.errorMensaje = "Puntuacion faltante";

                } else if (DateTime.TryParse(req.laPartida.FechaHora.ToString(), out fecha))
                { 
                   res.resultado = false;
                   res.errorMensaje = "Fecha faltante";

                }
                else
                {
                    AccesoDatosLINQDataContext miLinq = new AccesoDatosLINQDataContext();
                    miLinq.RegistrarPartida(req.laPartida.JugadorID,req.laPartida.Puntuacion,req.laPartida.FechaHora);
                    res.resultado = true;
                }
             
            }
            catch(Exception ex)
            {
                res.resultado = false;
                res.errorMensaje = ex.Message;
                Console.WriteLine(ex.ToString());

            }
        
         return res;
        }
    }
}
