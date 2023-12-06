using nasa_maui.Pages;
using nasa_maui.ViewModels;

namespace nasa_maui;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new NavigationPage(new WatchTabPage()) {};
	}
}
