using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls;
using System;
using System.Threading.Tasks;
using Microsoft.Maui.Devices.Sensors;
using Microsoft.Maui.Devices;
using System.Timers;
using System.Collections.Generic;
using Juego.Entidades;
using Newtonsoft.Json;
using System.Text;
using Juego.Pages;

namespace App.Pages;
public partial class JuegoPage : ContentPage
{
    //private System.Timers.Timer timer;
    private Random random = new Random();
    private System.Timers.Timer timer;
    private double screenHeight;
    private List<Image> imagenes;
    List<BoxView> listaNavesEnemigas = new List<BoxView>();
    int puntuacion = 0;
    int vida = 1;
    int tiempo = 3000;// 3000 milisegundos = 3 segundos
    int contador = 10;
    int velocidad = 5;// Asegúrate de inicializar y gestionar esta lista correctamente
    public ImageSource MainImageSource { get; set; }
    int _id;
    string url = "https://webapi20231213162835.azurewebsites.net/";
    public JuegoPage(string imagePath, int id)
    {
        InitializeComponent();
        naveImage.TranslationX = 141;
        naveImage.TranslationY = 640;
        Temporizador(tiempo);
        screenHeight = DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density;
        imagenes = new List<Image> { ovni1, ovni2, ovni3, ovni4, meteorito1, meteorito2, planeta1, planeta2 };
        listaNavesEnemigas = new List<BoxView> { hitboxCae1, hitboxCae2, hitboxCae3, hitboxCae4, hitboxCae5, hitboxCae6, hitboxCae7, hitboxCae8 };
        // Configurar temporizador para manejar la aparición y el movimiento de las imágenes
        MainImageSource = ImageSource.FromFile(imagePath);
        naveImage.Source = MainImageSource;
        imageVida.Source = MainImageSource;
        _id = id;
        

    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Iniciar la escucha del giroscopio cuando la página aparece
        Gyroscope.ReadingChanged += OnGyroscopeReadingChanged;
        Gyroscope.Start(SensorSpeed.Game);
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        // Detener la escucha del giroscopio cuando la página desaparece
        Gyroscope.ReadingChanged -= OnGyroscopeReadingChanged;
        Gyroscope.Stop();
    }

    private void OnGyroscopeReadingChanged(object sender, GyroscopeChangedEventArgs e)
    {
        var lectura = e.Reading;
        lblvidas.Text = vida.ToString();
        lblPuntuacion.Text = puntuacion.ToString();
        // Tu lógica para manejar el evento del giroscopio aquí
        MainThread.BeginInvokeOnMainThread(() =>
        {
            double PosX = naveImage.TranslationX + lectura.AngularVelocity.Y * 5;
            double PosY = 640;

            //Definicion de margenes

            double MargenI = -13;
            double MargenD = 284;
            double MargenAr = 0;
            double MargenAb = 650;


            PosX = Math.Max(MargenI, Math.Min(PosX, MargenD));
            PosY = Math.Max(MargenAr, Math.Min(PosY, MargenAb));


            naveImage.TranslationX = PosX;
            naveImage.TranslationY = PosY;




            naveBoxView.TranslationX = naveImage.TranslationX;
            naveBoxView.TranslationY = naveImage.TranslationY;
            // Verificar colisiones con las naves enemigas
            foreach (var enemigoBoxView in listaNavesEnemigas)
            {
                if (HayColision(enemigoBoxView))
                {
                    lblvidas.Text = "0";
                    naveImage.TranslationX = 141;
                    naveImage.TranslationY = 640;
                    naveImage.Source = "explosion.png";
                    gameSound.Stop();
                    RegistrarPartida(_id, puntuacion);
                    DisplayAlert("Game Over", "Se termino el juego con una puntuación de: " + puntuacion, "Ok");
                    Navigation.PopAsync();


                }
                //
            }
        });
        // Comprueba la colisión con cada nave que cae
    }

    private void Temporizador(int tiempo)
    {

        // Configurar temporizador para manejar la aparición y el movimiento de las imágenes
        timer = new System.Timers.Timer(tiempo);
        timer.Elapsed += (sender, e) => Device.BeginInvokeOnMainThread(MoverImagen);
        timer.Start();

    }

    [Obsolete]
    private void MoverImagen()
    {
        Image imagen = ObtenerImagenAleatoria();

        int spotIndex = random.Next(1, 5); // Obtener un número aleatorio de 1 a 4

        // Obtener el spot correspondiente según el índice
        BoxView spot = spotsGrid.Children.ElementAt(spotIndex - 1) as BoxView;

        // Configurar la posición inicial de la imagen y el BoxView en el spot
        imagen.TranslationX = spot.X;
        imagen.TranslationY = 0;

        BoxView hitboxCae = ObtenerHitboxCae(imagen);
        hitboxCae.TranslationX = spot.X;
        hitboxCae.TranslationY = 0;

        // Mostrar la imagen y el BoxView
        imagen.IsVisible = true;
        hitboxCae.IsVisible = true;

        if (puntuacion > contador)
        {
            contador += 5;

            //TOPE DE VELOCIAD
            if (velocidad == 20)
            {
                velocidad = random.Next(5, 20);
            }
            //SEGUIR AUMENTANDO LA VELOCIDAD
            else
            {
                velocidad += 5;
            }

            if (tiempo <= 1000)
            { }
            else
            {
                tiempo -= 300;
                Temporizador(tiempo);
            }

        }

        Device.StartTimer(TimeSpan.FromMilliseconds(30), () =>
        {
            imagen.TranslationY += velocidad;
            hitboxCae.TranslationY = imagen.TranslationY;

            if (imagen.TranslationY > screenHeight)
            {

                // La imagen ha salido de la pantalla, reinicia su posición en el spot
                imagen.IsVisible = false;
                hitboxCae.IsVisible = false;
                imagen.TranslationY = spot.Y;
                hitboxCae.TranslationY = spot.Y;
                //SUMAR PUNTUACION 
                puntuacion += 1;
                lblPuntuacion.Text = puntuacion.ToString();
                return false; // Detener el temporizador
            }

            return true; // Continuar el temporizador
        });
    }
    private BoxView ObtenerHitboxCae(Image imagen)
    {
        switch (imagen)
        {
            case var _ when imagen == ovni1:
                return hitboxCae1;
            case var _ when imagen == ovni2:
                return hitboxCae2;
            case var _ when imagen == ovni3:
                return hitboxCae3;
            case var _ when imagen == ovni4:
                return hitboxCae4;
            case var _ when imagen == meteorito1:
                return hitboxCae5;
            case var _ when imagen == meteorito2:
                return hitboxCae6;
            case var _ when imagen == planeta1:
                return hitboxCae7;
            case var _ when imagen == planeta2:
                return hitboxCae8;
            default:
                return null;
        }
    }
    private Image ObtenerImagenAleatoria()
    {
        // Obtener una imagen aleatoria
        int indiceImagen = random.Next(1, 9);
        Image imagen = null;

        switch (indiceImagen)
        {
            case 1:
                imagen = ovni1;
                break;
            case 2:
                imagen = ovni2;
                break;
            case 3:
                imagen = ovni3;
                break;
            case 4:
                imagen = ovni4;
                break;
            case 5:
                imagen = meteorito1;
                break;
            case 6:
                imagen = meteorito2;
                break;
            case 7:
                imagen = planeta1;
                break;
            case 8:
                imagen = planeta2;
                break;
        }

        return imagen;
    }


    private bool HayColision(BoxView enemigoBoxView)
    {
        double x1 = Math.Round(naveBoxView.TranslationX);
        double y1 = Math.Round(naveBoxView.TranslationY);
        double width1 = Math.Round(naveBoxView.WidthRequest);
        double height1 = Math.Round(naveBoxView.HeightRequest);

        double xN = enemigoBoxView.TranslationX;
        double yN = enemigoBoxView.TranslationY;
        double widthN = enemigoBoxView.WidthRequest;
        double heightN = enemigoBoxView.HeightRequest;

        // Lógica para verificar colisiones entre las dos áreas

        return (x1 < xN + widthN && x1 + width1 > xN && y1 < yN + heightN && y1 + height1 > yN)
          ;
    }

    private async Task RegistrarPartida(int id, int puntuacion)
    {
        ReqIngresarPartida req = new ReqIngresarPartida();
        req.laPartida = new Partida();
        req.laPartida.JugadorID = id;
        req.laPartida.Puntuacion = puntuacion;
        try
        {
            var jsonContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");

            HttpClient httpclient = new HttpClient();

            var response = await httpclient.PostAsync(url + "api/partida/ingresarPartida", jsonContent);

            if(response.IsSuccessStatusCode)
            {
                ResIngresarPartida res = new ResIngresarPartida();
                var responseContent = await response.Content.ReadAsStringAsync();
                res = JsonConvert.DeserializeObject<ResIngresarPartida>(responseContent);

                if (res.resultado)
                {
                    await DisplayAlert("Game Over", "Se termino el juego con una puntuación de: " + puntuacion, "Ok");
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

}
