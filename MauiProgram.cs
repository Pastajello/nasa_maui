using CommunityToolkit.Maui;
using FFImageLoading.Maui;
using Microsoft.Extensions.Logging;
using nasa_maui.Pages;
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
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif
        builder.Services.AddTransientWithShellRoute<MainPage,ViewModelBase>();
        builder.Services.AddTransientWithShellRoute<WatchTabPage, WatchTabPageViewModel>();


        return builder.Build();
	}

    static IServiceCollection AddTransientWithShellRoute<TPage, TViewModel>(this IServiceCollection services)
		where TPage : BasePage<TViewModel>
		where TViewModel : ViewModelBase
    {
        return services.AddTransientWithShellRoute<TPage, TViewModel>(typeof(TPage).Name);
    }
}
