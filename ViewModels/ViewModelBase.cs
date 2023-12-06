using CommunityToolkit.Mvvm.ComponentModel;
using nasa_maui.ViewModels;
using System.Diagnostics;

namespace nasa_maui.ViewModels
{
    public interface IViewModel
    {
        void OnAppearing();
        void OnDisappearing();
    }
    public class ViewModelBase : ObservableObject, IViewModel
    {
        public ViewModelBase() { }

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
