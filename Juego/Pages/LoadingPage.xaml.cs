using Juego.Services;

namespace Juego.Pages;

public partial class LoadingPage : ContentPage
{
   // private readonly AuthService _authService;

    public LoadingPage(AuthService authService)
	{
		InitializeComponent();
		//_authService = authService;
	}

    //protected async override void OnNavigatedTo(NavigatedToEventArgs args)
    //{
    //    base.OnNavigatedTo(args);

    //    if (await _authService.IsAuthenticatedAsync())
    //    {
    //        // El usuario está logueado, dirigir a la página de inicio (Home) dentro de la MainPage
    //        await Shell.Current.GoToAsync($"{nameof(MainPage)}");
    //    }
    //    else
    //    {
    //        // No está logueado, dirigir a la página de login
    //        await Shell.Current.GoToAsync($"{nameof(LoginPage)}");
    //    }
    //}
}