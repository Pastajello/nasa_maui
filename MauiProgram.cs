using CommunityToolkit.Maui;
using FFImageLoading.Maui;
using Microsoft.Extensions.Logging;
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
			})
			.Services.AddSingleton<INavigationService, NavigationService>()
			.AddTransient<WatchTabPageViewModel>()
			.AddTransient<MoviePageViewModel>();



#if DEBUG
		builder.Logging.AddDebug();
#endif

        return builder.Build();
	}
}
