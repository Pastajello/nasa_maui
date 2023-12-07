using CommunityToolkit.Mvvm.ComponentModel;
using nasa_maui.Services;
using nasa_maui.ViewModels;
using System.Diagnostics;

namespace nasa_maui.ViewModels
{

    internal interface IInitializable
    {
        void Init(object? navigationParameter);
        bool IsInitilized { get; set; }
    }

    public interface IViewModel
    {
        void OnAppearing();
        void OnDisappearing();
    }

    public abstract partial class ViewModelBase : ObservableObject, IViewModel, IInitializable
    {
        public INavigationService NavigationService { get; set; }

        public ViewModelBase(Services.INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        public bool IsInitilized { get; set; }

        [ObservableProperty]
        private bool _isLoading;

        void IInitializable.Init(object? navigationParameter)
        {
            IsInitilized = true;
            Task.Run(async()=>
            {
                IsLoading = true;
                await Initialize(navigationParameter);
                IsLoading = false;
            }
                );
        }

        public virtual async Task Initialize(object? navigationParameter)
        {
        }

        public virtual void OnAppearing()
        {
            Debug.WriteLine($"VM OnAppearing");
        }

        public virtual void OnDisappearing()
        {
            Debug.WriteLine($"VM OnDisappearing");
        }
    }

   
}
