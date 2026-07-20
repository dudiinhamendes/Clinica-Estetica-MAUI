using Microsoft.Extensions.Logging;
using SistemaClinica.Services;
using SistemaClinica.Views;

namespace SistemaClinica;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // Registra o HttpClient apontando para o endereco da API
        // Toda requisicao feita pelo ApiService usara esse endereco como base
        builder.Services.AddHttpClient<ApiService>(client =>
        {
            client.BaseAddress = new Uri("http://localhost:5000");
        });

        // Registra a LoginPage para que ela receba o ApiService automaticamente
        builder.Services.AddTransient<LoginPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}