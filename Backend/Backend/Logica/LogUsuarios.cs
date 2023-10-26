using Backend.Entidades;
using Backend.StringConexion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Logica
{
    public class LogUsuarios
    {
        public ResIngresarUsuario ingresarUsuario(ReqUsuario req)
        {
            ResIngresarUsuario res = new ResIngresarUsuario();

            try
            {
                if(req.ElUsuario.Id == 0)
                {
                    res.resultado = false;
                    res.errorMensaje = "Id faltante";
                }
                else if (String.IsNullOrEmpty(req.ElUsuario.NombreUsuario))
                {
                    res.resultado = false;
                    res.errorMensaje = "Nombre de ususario faltante";
                }
                else if (String.IsNullOrEmpty(req.ElUsuario.password))
                {
                    res.resultado= false;
                    res.errorMensaje = "Password faltante";
                }
                else
                {
                    
                }
            }
            catch(Exception ex)
            {

            }
        }
    }
}
