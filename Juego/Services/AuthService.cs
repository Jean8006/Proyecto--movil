using Juego.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juego.Services
{


    public class AuthService
    {
        private static AuthService _instance;
        private string _authToken;

        public static AuthService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AuthService();
                }
                return _instance;
            }
        }

        public bool ValidateToken()
        {
            // Verifica si el token existe y no está vacío
            return !string.IsNullOrEmpty(_authToken);
        }

        //public bool IsUserAuthenticated
        //{
        //    get { return _isUserAuthenticated; }
        //    set { _isUserAuthenticated = value; }
        //}

        public bool IsUserAuthenticated
        {
            get { return !string.IsNullOrEmpty(_authToken); }
            set { /* Puedes agregar lógica adicional si es necesario */ }
        }


        public string AuthToken
        {
            get { return _authToken; }
            private set { _authToken = value; }
        }

        public string User { get; internal set; }

        public bool Authenticate(string userName, string userID, int? score)
        {
            // Lógica de autenticación simulada
           
                _authToken = GenerateToken(userID, userName, score);
                Console.WriteLine("Token generado: " + _authToken);
                Microsoft.Maui.Storage.Preferences.Set("AuthToken", _authToken);
                Microsoft.Maui.Storage.Preferences.Set("Username", userName);
                Microsoft.Maui.Storage.Preferences.Set("UserID", userID);
                Microsoft.Maui.Storage.Preferences.Set("Score", score.ToString());
            // Asignar el nombre de usuario a la propiedad Username
                User = userName;


            return true;
        }

        public void SignOut()
        {
            // Lógica para cerrar sesión
            _authToken = null;
        }

        public string GenerateToken(string userId, string username, int? score)
        {
            string token = $"{userId}:{username}:{Guid.NewGuid()}";
            return token;
        }

        public void AuthenticateWithToken(string token)
        {
            _authToken = token;
            IsUserAuthenticated = true;
        }

        public void LogOut()
        {
            _authToken = null;
            Microsoft.Maui.Storage.Preferences.Set("AuthToken", _authToken);
        }
         
       
    }
}
