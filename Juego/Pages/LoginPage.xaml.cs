using Juego.Entidades;
using Juego.Entidades.Request;
using Juego.Services;
using Newtonsoft.Json;
using System.Text;

namespace Juego.Pages;

public partial class LoginPage : ContentPage
{
    string url = "https://webapi20231213162835.azurewebsites.net/";

    
    public LoginPage()
	{
		InitializeComponent();
	}
    private void Login_Clicked(object sender, EventArgs e)
    {
        _=this.verificarLogin();
        
    }

    private void Login_Pressed(object sender, EventArgs e)
    {
        Login.Background = Color.FromArgb("d168e0");
    }

    private void Login_Released(object sender, EventArgs e)
    {
        Login.Background = Color.FromArgb("63e7ff");
    }

    private void NewUser_Clicked(object sender, EventArgs e)
    {
        _ = this.crearUsuario();
    }

    private void NewUser_Pressed(object sender, EventArgs e)
    {
        NewUser.Background = Color.FromArgb("d168e0");
    }

    private void NewUser_Released(object sender, EventArgs e)
    {
        NewUser.Background =Color.FromArgb("63e7ff");
    }

    
    private async Task verificarLogin()
    {
        ReqLogin login = new ReqLogin();
        login.jugadorLog = new Jugador(); // Suponiendo que Jugador es la clase que tiene la propiedad NombreUsario
        login.jugadorLog.NombreUsario = Usuario.Text;
        login.jugadorLog.password = Password.Text;
        try
        {
            
                var jsonContent = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");

                HttpClient httpclient = new HttpClient();

                var response = await httpclient.PostAsync(url + "api/Login/verificarLogin", jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    ResLogin res = new ResLogin();
                    var responseContent = await response.Content.ReadAsStringAsync();
                    res = JsonConvert.DeserializeObject<ResLogin>(responseContent);

                    if (res.resultado)
                    {
                        AuthService service = new AuthService();
                        string userID = res.errorMensaje;
                        int? score = res.score;
                        service.Authenticate(login.jugadorLog.NombreUsario, userID, score);
                        var token = service.AuthToken;
                        await this.CrearSesion(userID, token);
                        await DisplayAlert("Usuario Encontrado", "El login se realizo con exito", "Aceptar");
                        //this.enviarUsuario(login.jugadorLog.NombreUsario);
                        await Navigation.PushAsync(new HomePage(token, login.jugadorLog.NombreUsario, userID, score));
                        
                }
                    else
                    {
                        await DisplayAlert("Error en backend", "Backend respondio:" + res.errorMensaje, "Aceptar");
                    }
                }
                else
                {
                    await DisplayAlert("Error conexion", "No se pudo establecer conexion", "Aceptar");
                }
            

            
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error no controlado", "Error en aplicacion " + ex, "Aceptar");
        }
    }

    private async Task crearUsuario()
    {
        ReqIngresarJugador req = new ReqIngresarJugador();
        req.elJugador = new Jugador(); // Suponiendo que Jugador es la clase que tiene la propiedad NombreUsario
        req.elJugador.NombreUsario = Usuario.Text;
        req.elJugador.password = Password.Text;
        try
        {
            loadingIndicator.IsRunning = true;
            loadingIndicator.IsVisible = true;
            var jsonContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");

            HttpClient httpclient = new HttpClient();

            var response = await httpclient.PostAsync(url + "api/jugador/ingresarJugador", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                ResIngresarJugador res = new ResIngresarJugador();
                var responseContent = await response.Content.ReadAsStringAsync();
                res = JsonConvert.DeserializeObject<ResIngresarJugador>(responseContent);

                if (res.resultado)
                {
                    AuthService service = new AuthService();
                    string userID = res.errorMensaje;
                    int? score = res.score;
                    service.Authenticate(req.elJugador.NombreUsario, userID, score);
                    var token = service.AuthToken;
                    req.session = token;
                    await this.CrearSesion(userID, token);
                    await DisplayAlert("Usuario Creado", "El usuario se realizo con exito", "Aceptar");
                    //this.enviarUsuario(login.jugadorLog.NombreUsario);
                    await Navigation.PushAsync(new HomePage(token, req.elJugador.NombreUsario, userID, score));

                }
                else
                {
                    await DisplayAlert("Error en backend", "Backend respondio:" + res.errorMensaje, "Aceptar");
                }
            }
            else
            {
                await DisplayAlert("Error conexion", "No se pudo establecer conexion", "Aceptar");
            }



        }
        catch (Exception ex)
        {
            await DisplayAlert("Error no controlado", "Error en aplicacion " + ex, "Aceptar");
        }
        finally
        {
            loadingIndicator.IsRunning = false;
            loadingIndicator.IsVisible = false;
        }
    }

    private async Task CrearSesion(string id, string sesion)
    {
        ReqCrearSesion req = new ReqCrearSesion();
        req.laSesion = new Sesion();
        req.laSesion.jugadorID = int.Parse(id);
        req.laSesion.sesion = sesion;
        req.laSesion.estado = true;
        try
        {
            var jsonContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");

            HttpClient httpclient = new HttpClient();

            var response = await httpclient.PostAsync(url + "api/sesion/CrearSesion", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                ResCrearSesion res = new ResCrearSesion();
                var responseContent = await response.Content.ReadAsStringAsync();
                res = JsonConvert.DeserializeObject<ResCrearSesion>(responseContent);

                if (res.resultado)
                {
                    await DisplayAlert("Sesion", "Sesion creada", "Aceptar");
                }
                else
                {
                    await DisplayAlert("Error en backend sesion", "Backend respondio:" + res.errorMensaje, "Aceptar");
                }
            }
            else
            {
                await DisplayAlert("Error conexion sesion", "No se pudo establecer conexion", "Aceptar");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error no controlado", "Error en aplicacion " + ex, "Aceptar");
        }
    }

}