using nasa_maui.ViewModels;
using System.Diagnostics;

namespace nasa_maui.Pages
{
    public abstract class BasePage<TViewModel> : ContentPage where TViewModel : IViewModel
    {
        public object NavigationParameter { get; set; }
        public IViewModel VM => (IViewModel)BindingContext;
        protected BasePage()
        {
            BindingContext = (TViewModel)Activator.CreateInstance(typeof(TViewModel));
        }

        protected override async void OnAppearing()
        {
            if(!VM.IsInitilized)
            {
                VM?.Init(NavigationParameter);
            }

            base.OnAppearing();

            Debug.WriteLine($"OnAppearing: {Title}");

            VM?.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            Debug.WriteLine($"OnDisappearing: {Title}");

            VM?.OnDisappearing();
        }
    }
}
