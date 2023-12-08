using CommunityToolkit.Maui;
using FFImageLoading.Maui;
using Microsoft.Extensions.Logging;
using nasa_maui.Interfaces;
using nasa_maui.Pages;
using nasa_maui.Services;
using nasa_maui.ViewModels;

namespace nasa_maui;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .UseMauiCommunityToolkitMediaElement()
            .UseFFImageLoading()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("Font Awesome 6 Free-Solid-900.otf#Font Awesome 6 Free Solid", "FA-S");
            })
			.Services.AddSingleton<INavigationService, NavigationService>()
			.AddTransient<WatchTabPageViewModel>()
			.AddTransient<MoviePageViewModel>();

		RegisterAndroidServices(builder.Services);

#if DEBUG
		builder.Logging.AddDebug();
#endif

        return builder.Build();
	}

    private static void RegisterAndroidServices(IServiceCollection services)
    {
#if __ANDROID__
		services.AddSingleton<IScreenOrientationService, ScreenOrientationService>();
#endif
	}
}
