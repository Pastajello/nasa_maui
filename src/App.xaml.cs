using CommunityToolkit.Mvvm.DependencyInjection;
using nasa_maui.Pages;
using nasa_maui.Services;
using nasa_maui.ViewModels;

namespace nasa_maui;

public partial class App : Application
{
	public App(IServiceProvider serviceProvider)
	{
		Ioc.Default.ConfigureServices(serviceProvider);

		InitializeComponent();

		var navigationService = Ioc.Default.GetService<INavigationService>();

		navigationService.PushAsync<WatchTabPageViewModel>();
	}
}
