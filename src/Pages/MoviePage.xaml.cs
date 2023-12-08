using CommunityToolkit.Maui.Views;
using nasa_maui.Pages;
using nasa_maui.ViewModels;

namespace nasa_maui.Pages;

public partial class MoviePage : BasePage<MoviePageViewModel>
{
	public MoviePage()
	{
		InitializeComponent();
        this.Unloaded += MoviePage_Unloaded;
	}

    private void MoviePage_Unloaded(object? sender, EventArgs e)
    {
        this.Unloaded -= MoviePage_Unloaded;
        //mediaElement.Handler?.DisconnectHandler();
    }
}

