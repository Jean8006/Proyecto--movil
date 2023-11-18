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

        public ResObtenerPartidasPorID obtenerPartidasPorID(ReqObtenerPartidasPorID req) 
        {
            ResObtenerPartidasPorID res = new ResObtenerPartidasPorID();
            try
            {
                if (String.IsNullOrEmpty(req.session))
                {
                    res.errorMensaje = "Session nula o vacia";
                    res.resultado = false;
                }
                else
                {
                    //Todos los datos estan correctos.
                    //Llamar a la base datos
                    AccesoDatosLINQDataContext miLinq = new AccesoDatosLINQDataContext();
                    List<ObtenerPartidasPorJugadorResult> miListaDeLinq = new List<ObtenerPartidasPorJugadorResult>();

                    Sesion sesion = new Sesion();
                    //Se ejecuta el Sp
                    miListaDeLinq = miLinq.ObtenerPartidasPorJugador(4).ToList();

                    //Llamar al Factory
                    res.listaDePartidasPorID = this.armarListaPublicacion(miListaDeLinq);
                    res.resultado = true;
                }
            }
            catch (Exception ex)
            {
                res.resultado = false;
                res.errorMensaje = "Error interno";
                Console.WriteLine(ex.Message);
            }
            return res;
        }

        private List<Partida> armarListaPublicacion(List<ObtenerPartidasPorJugadorResult> miListaDeLinq)
        {
            List<Partida> listaDevolver = new List<Partida>();

            foreach (ObtenerPartidasPorJugadorResult cadaLinq in miListaDeLinq)
            {
                Partida miPartida = new Partida();
                miPartida.JugadorID = cadaLinq.JugadorID;
                miPartida.Puntuacion = cadaLinq.Puntuacion;
                
                listaDevolver.Add(miPartida);
            }

            return listaDevolver;
        }
    }
    }


