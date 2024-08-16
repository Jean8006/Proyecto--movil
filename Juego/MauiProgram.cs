using CommunityToolkit.Maui;
using Juego.Pages;
using Juego.Services;
using Microsoft.Extensions.Logging;
using Plugin.Maui.Audio;

namespace Juego
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkitMediaElement()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("ArcadeClassic.ttf", "CustomFont");
                });

#if DEBUG
    		builder.Logging.AddDebug();

#endif
            builder.Services.AddTransient<AuthService>(); //esto sirve para agregar los servicios al contenedor del app, AddTransient es un servicio transitorio
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddSingleton(AudioManager.Current);
            builder.Services.AddTransient<MainPage>();

            return builder.Build();
        }
    }
}
