using Microsoft.Extensions.Logging;
using PocketDictionary.Interfaces;
using PocketDictionary.Services;
using PocketDictionary.ViewModels;
using PocketDictionary.Views;

namespace PocketDictionary
{
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

            builder.Services.AddSingleton<IDbService, DbService>();
            builder.Services.AddTransient<IDeckService, DeckService>();
            builder.Services.AddTransient<IFlashcardService, FlashcardService>();
            builder.Services.AddTransient<CreateFlashcardViewModel>();
            builder.Services.AddTransient<HomeViewModel>();
            builder.Services.AddTransient<DeckDetailsViewModel>();
            builder.Services.AddTransient<FlashcardDetailsViewModel>();
            builder.Services.AddTransient<CreateFlashcardPage>();
            builder.Services.AddTransient<HomePage>();
            builder.Services.AddTransient<DeckDetailsPage>();
            builder.Services.AddTransient<FlashcardDetailsPage>();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
