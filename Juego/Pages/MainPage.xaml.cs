using App.Pages;
using Juego.Pages;
using Juego.Services;

namespace Juego
{
    
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
  
        }
        //protected async override void OnNavigatedTo(NavigatedToEventArgs args)
        //{
        //    base.OnNavigatedTo(args);

        //    if (await _authService.IsAuthenticatedAsync())
        //    {
        //        // El usuario está logueado, dirigir a la página de inicio (Home) dentro de la MainPage
        //        await Shell.Current.GoToAsync($"{nameof(HomePage)}");
        //    }
        //    else
        //    {
        //        // No está logueado, dirigir a la página de login
        //        await Shell.Current.GoToAsync($"{nameof(LoginPage)}");
        //    }
        //}

    }

       

}
