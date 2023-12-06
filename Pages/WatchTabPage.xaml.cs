using nasa_maui.Pages;
using nasa_maui.ViewModels;

namespace nasa_maui.Pages;

public partial class WatchTabPage : BasePage<WatchTabPageViewModel>
{
	public WatchTabPage()
	{
		InitializeComponent();
		NavigationPage.SetHasNavigationBar(this,false);
	}

}

