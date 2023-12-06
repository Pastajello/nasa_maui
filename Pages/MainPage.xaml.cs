using nasa_maui.Pages;
using nasa_maui.ViewModels;

namespace nasa_maui.Pages;

public partial class MainPage : BasePage<ViewModelBase>
{
	int count = 0;

	public MainPage(ViewModelBase vm) :base(vm)
	{
		InitializeComponent();
	}

	private void OnCounterClicked(object sender, EventArgs e)
	{
		count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";

		SemanticScreenReader.Announce(CounterBtn.Text);
	}
}

