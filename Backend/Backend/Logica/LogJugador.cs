using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Entidades;
using Backend.AccesoDatos;

namespace Backend.Logica
{
    public class LogJugador
    {
        public ResIngresarJugador ingresarJugador (ReqIngresarJugador req)
        {
            ResIngresarJugador res = new ResIngresarJugador ();

            try
            {
                if(String.IsNullOrEmpty(req.elJugador.NombreUsario))
                {
                    res.resultado = false;
                    res.errorMensaje = "Usuario faltante";
                }
                else if (String.IsNullOrEmpty(req.elJugador.password))
                {
                    res.resultado = false;
                    res.errorMensaje = "Password faltante";

                }
                else
                {
                    AccesoDatosLINQDataContext miLinq = new AccesoDatosLINQDataContext ();
                    miLinq.InsertarJugador(req.elJugador.NombreUsario, req.elJugador.password);
                    res.resultado = true;
                }
            }
            catch (Exception ex) 
            {
                res.resultado=false;
                res.errorMensaje = ex.Message;
                Console.WriteLine(ex.ToString());
            }
            return res;
        }
    }
}
