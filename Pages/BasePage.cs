using nasa_maui.ViewModels;
using System.Diagnostics;

namespace nasa_maui.Pages
{
    public abstract class BasePage<TViewModel> : ContentPage where TViewModel : IViewModel
    {
        public IViewModel VM => (IViewModel)BindingContext;
        protected BasePage(object? viewModel = null)
        {
            BindingContext = (TViewModel)Activator.CreateInstance(typeof(TViewModel));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Debug.WriteLine($"OnAppearing: {Title}");

            VM.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            Debug.WriteLine($"OnDisappearing: {Title}");

            VM.OnDisappearing();
        }
    }
}
