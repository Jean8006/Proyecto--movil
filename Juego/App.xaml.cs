using Juego.Pages;
using Juego.Services;


namespace Juego

{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            string storedToken = Microsoft.Maui.Storage.Preferences.Get("AuthToken", string.Empty);
            string storedUsername = Microsoft.Maui.Storage.Preferences.Get("Username", string.Empty);
            string storedUserID = Microsoft.Maui.Storage.Preferences.Get("UserID", string.Empty);
            string storedScore = Microsoft.Maui.Storage.Preferences.Get("Score", string.Empty);

            int? score = null;

            if (int.TryParse(storedScore, out int parsedScore))
            {
                // La conversión fue exitosa, asignamos el valor a la variable nullable
                score = parsedScore;
            }
            else
            {
                // La conversión no fue exitosa, puedes manejar el error según sea necesario
                // Puedes asignar un valor predeterminado o realizar otra lógica de manejo de errores
                score = 0;
            }

            if (!string.IsNullOrEmpty(storedToken))
            {
                // Validar el token y establecer el estado de autenticación
                AuthService.Instance.AuthenticateWithToken(storedToken);

                if (AuthService.Instance.ValidateToken())
                {
                    string username = AuthService.Instance.User;
                    // Si el usuario está autenticado, navegar a la HomePage
                    MainPage = new NavigationPage(new HomePage(storedToken, storedUsername, storedUserID, score));
                    return; // Salir del constructor para evitar configurar la LoginPage
                }
            }

            // Si no hay token almacenado o la autenticación falla, mostrar la LoginPage
            MainPage = new NavigationPage(new LoginPage());
        }

        private bool _isUserAuthenticated;

        public bool IsUserAuthenticated
        {
            get { return _isUserAuthenticated; }
            set
            {
                _isUserAuthenticated = value;
                // Almacenar el estado de autenticación en SecureStorage u otro método seguro aquí si es necesario
            }
        }

        protected override void OnSleep()
        {
            // Simular guardar el estado de autenticación al cerrar la aplicación
            SecureStorage.SetAsync("IsUserAuthenticated", AuthService.Instance.IsUserAuthenticated.ToString());
            
        }


    }
}
