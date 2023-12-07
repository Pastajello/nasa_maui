using CommunityToolkit.Mvvm.ComponentModel;
using nasa_maui.ViewModels;
using System.Diagnostics;

namespace nasa_maui.ViewModels
{

    internal interface IInitializable
    {
        void Init(object? navigationParameter);
    }

    public interface IViewModel
    {
        bool IsInitilized { get; set; }

        void OnAppearing();
        void OnDisappearing();
    }
    public abstract partial class ViewModelBase : ObservableObject, IViewModel, IInitializable
    {
        public ViewModelBase() { }

        public bool IsInitilized { get; set; }

        async void IInitializable.Init(object? navigationParameter)
        {
            IsInitilized = true;
            Initialize(navigationParameter);
        }

        public virtual async void Initialize(object? navigationParameter)
        {
            IsInitilized = true;
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
