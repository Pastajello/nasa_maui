using nasa_maui.Pages;
using nasa_maui.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nasa_maui.Services
{
    public interface INavigationService
    {
        Task PushAsync<T>(object parameter = null) where T : ViewModelBase;
        Task PopAsync();
    }

    public class NavigationService : INavigationService
    {
        Dictionary<Type, Type> _navigationPairs = new Dictionary<Type, Type>()
        {
            {typeof(WatchTabPageViewModel), typeof(WatchTabPage) },
            {typeof(MoviePageViewModel), typeof(MoviePage) },
        };

        public INavigation Navigation => Application.Current?.MainPage?.Navigation;

        public Task PopAsync()
        {
            throw new NotImplementedException();
        }

        public async Task PushAsync<T>(object parameter = null) where T : ViewModelBase
        {
            if(Navigation == null && Application.Current != null)
            {
                var page = (BasePage)Activator.CreateInstance(_navigationPairs[typeof(T)]);
                page.NavigationParameter = parameter;
                Application.Current.MainPage = new NavigationPage(page);
            }
            else
            {
                var page = (BasePage)Activator.CreateInstance(_navigationPairs[typeof(T)]);
                page.NavigationParameter = parameter;
                await Navigation.PushAsync(page);
            }
        }
    }
}
