
using Juego.Entidades;
using Juego.Services;
using Newtonsoft.Json;
using System.Text;

namespace Juego.Pages;

public partial class LogoutPage : ContentPage
{
    string url = "https://webapi20231213162835.azurewebsites.net/";
    private string _token;
    public LogoutPage(string token)
	{
		InitializeComponent();
        _token = token;
	}

    private void Logout_Clicked(object sender, EventArgs e)
    {
        AuthService service = new AuthService();
        service.LogOut();
        _ = this.cerrarSesion(_token);
        DisplayAlert("Sesion cerrada", "Sesion cerrada", "Aceptar");
        Navigation.PushAsync(new LoginPage());
        //Navigation.PopToRootAsync();
    }

    private void Logout_Pressed(object sender, EventArgs e)
    {
        Logout.Background = Color.FromArgb("d168e0");
    }

    private void Logout_Released(object sender, EventArgs e)
    {
        Logout.Background = Color.FromArgb("63e7ff");
    }

    private async Task cerrarSesion(string token)
    {
        ReqCrearSesion req = new ReqCrearSesion();
        req.laSesion = new Sesion();
        req.laSesion.sesion = token;
        req.laSesion.estado = true;
        try
        {
            var jsonContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");

            HttpClient httpclient = new HttpClient();

            var response = await httpclient.PostAsync(url + "api/sesion/CambiarEstado", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                ResCrearSesion res = new ResCrearSesion();
                var responseContent = await response.Content.ReadAsStringAsync();
                res = JsonConvert.DeserializeObject<ResCrearSesion>(responseContent);

                if (res.resultado)
                {
                    await DisplayAlert("Sesion cerrada", "Sesion cerrada", "Aceptar");
                }
                else
                {
                    await DisplayAlert("Error en backend al cerrar sesion", "Backend respondio:" + res.errorMensaje, "Aceptar");
                }
            }
            else
            {
                await DisplayAlert("Error conexion sesion", "No se pudo establecer conexion", "Aceptar");
            }
        }
        catch(Exception ex)
        {
            await DisplayAlert("Error no controlado", "Error en aplicacion " + ex, "Aceptar");
        }
    }
}