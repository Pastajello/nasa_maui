using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HtmlAgilityPack;
using nasa_maui.Interfaces;
using nasa_maui.Services;
using System.Collections.ObjectModel;

namespace nasa_maui.ViewModels
{
    public partial class MoviePageViewModel : ViewModelBase
    {
        private readonly IScreenOrientationService _screenOrientationService;
        [ObservableProperty]
        public Video video;

        [ObservableProperty]
        public string videoUrl;

        public MoviePageViewModel(INavigationService navigationService,
            IScreenOrientationService screenOrientationService) : base(navigationService)
        {
            _screenOrientationService = screenOrientationService;
        }

        public override async Task Initialize(object? navigationParameter)
        {
            base.Initialize(navigationParameter);

            Video = navigationParameter as Video;

            var web = new HtmlWeb();
            var doc = await web.LoadFromWebAsync(Video.Url);
            var node = doc.DocumentNode.SelectNodes("//a[@class='usa-button usa-button--outline usa-button--inverse button--round icon--download']");
            VideoUrl = node[0].Attributes["href"].Value;
        }

        public override void OnAppearing()
        {
            _screenOrientationService.SetScreenOrientation(ScreenOrientation.Landscape);
            base.OnAppearing();
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();
            _screenOrientationService.SetScreenOrientation(ScreenOrientation.Portrait);
        }

        [RelayCommand]
        public async Task NavigateBack()
        {
            await NavigationService.PopAsync();
        }
    }
}
