using Juego.Pages;

namespace Juego
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(LoadingPage), typeof(LoadingPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage)); 
            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
        }
    }
}
