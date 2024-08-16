namespace Juego.Pages;


using global::App.Pages;
using Juego.Entidades;
using Juego.Pages;
using Juego.Services;
using Newtonsoft.Json;
using Plugin.Maui.Audio;
using System.ComponentModel;
using System.Globalization;
using CommunityToolkit.Maui.Converters;


public partial class HomePage : TabbedPage
{
    // Añadir propiedades para almacenar el token y el nombre de usuario
    private string _token;
    private string _username;
    private string _userID;
    public int _id;
    private int? _score;
    public string imagePath;
    public int Position { get; set; }


    public HomePage(string token, string username, string id, int? score)
	{
		InitializeComponent();

        _token = token;
        _username = username;
        _userID = id;
        _score = score;
        _id = int.Parse(id);
        imagePath = "nave.png";
        NombreUsuario.Text = username;
        Score.Text = score.ToString();
        BindingContext = this; 

        homeSound.Play();

        

    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await obtenerLeaderboard();
        homeSound.Play();
    }

    private void OnCounterBtn_Clicked_1(object sender, EventArgs e)
    {

        JuegoPage juegoPage = new JuegoPage(imagePath, _id);
        Navigation.PushAsync(new JuegoPage(imagePath, _id));
        homeSound.Stop();
        
    }

    private void Tabbed_Clicked(object sender, EventArgs e)
    {
        CounterBtn.Background = Color.FromArgb("00BFFF");
    }

    private void CounterBtn_Pressed(object sender, EventArgs e)
    {
        CounterBtn.Background = Color.FromArgb("d168e0");
    }

    private void CounterBtn_Released(object sender, EventArgs e)
    {
        CounterBtn.Background = Color.FromArgb("63e7ff");
    }

    private void Login_Clicked(object sender, EventArgs e)
    {

    }

   
    private void OnPerfilButtonClicked(object sender, EventArgs e)
    {
        // Lógica que se ejecutará cuando se haga clic en el botón
        Navigation.PushAsync(new LogoutPage(_token));
    }

    

    private void OnButtonClicked(object sender, EventArgs e)
    {
        if (sender is Button button)
        {
            imagePath = button.CommandParameter.ToString();
            naveView.Source = imagePath;
            
            JuegoPage juegop = new JuegoPage(imagePath, _id);
        }
        
    }


    private async Task obtenerLeaderboard()
    {
        try
        {
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync("https://webapi20231213162835.azurewebsites.net/api/partida/ObtenerPartidasPorJugador");
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                ResObtenerPartidasPorID res = new ResObtenerPartidasPorID();
                res = JsonConvert.DeserializeObject<ResObtenerPartidasPorID>(responseContent);
                if (res.resultado)
                {
                   
                    lvScores.ItemsSource = res.listaDePartidas;

                }
                else
                {
                    await DisplayAlert("Error", "Error en backend leaderboard", "Aceptar");
                }

            }
            else
            {
                await DisplayAlert("No se encontro", "Error", "Aceptar");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error interno", "Error en la aplicacion: " + ex.Message.ToString(), "Resignarse");
        }
    }

    private void btnDefault_Clicked(object sender, EventArgs e)
    {

    }
}
