using CommunityToolkit.Mvvm.ComponentModel;
using nasa_maui.ViewModels;
using System.Diagnostics;

namespace nasa_maui.ViewModels
{
    public interface IViewModel
    {
        bool IsInitilized { get; set; }

        void Init(object? navigationParameter);
        void OnAppearing();
        void OnDisappearing();
    }
    public class ViewModelBase : ObservableObject, IViewModel
    {
        public ViewModelBase() { }

        public bool IsInitilized { get; set; }

        public virtual async void Init(object? navigationParameter)
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
