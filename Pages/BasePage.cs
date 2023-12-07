using CommunityToolkit.Mvvm.DependencyInjection;
using nasa_maui.ViewModels;
using System.Diagnostics;

namespace nasa_maui.Pages
{
    public abstract class BasePage : ContentPage
    {
        public object NavigationParameter { get; set; }
    }

    public abstract class BasePage<TViewModel> : BasePage  where TViewModel : IViewModel
    {
        public IViewModel VM => (IViewModel)BindingContext;
        protected BasePage()
        {
            BindingContext = (TViewModel)Ioc.Default.GetService(typeof(TViewModel));
        }

        protected override async void OnAppearing()
        {
            if(VM is IInitializable initializable && !initializable.IsInitilized)
            {
                initializable.Init(NavigationParameter);
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
