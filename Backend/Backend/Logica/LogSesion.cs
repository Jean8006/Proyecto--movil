using Backend.AccesoDatos;
using Backend.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Logica
{
    public class LogSesion
    {
        public ResCrearSesion crearSesion(ReqCrearSesion req)
        {
            ResCrearSesion res = new ResCrearSesion();

            try
            {
                if (req.laSesion.jugadorID == 0)
                {
                    res.resultado = false;
                    res.errorMensaje = "JugadorID faltante";
                }
                else if (String.IsNullOrEmpty(req.laSesion.sesion))
                {
                    res.resultado = false;
                    res.errorMensaje = "sesion faltante";

                }
                else if(req.laSesion.estado == false)
                {
                    res.resultado = false;
                    res.errorMensaje = "Sesion ya cerrada";
                }
                else
                {
                    AccesoDatosLINQDataContext miLinq = new AccesoDatosLINQDataContext();
                    miLinq.CrearSesion(req.laSesion.jugadorID, req.laSesion.sesion, req.laSesion.estado);
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

