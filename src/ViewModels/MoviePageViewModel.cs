using CommunityToolkit.Mvvm.ComponentModel;
using HtmlAgilityPack;
using nasa_maui.Services;
using System.Collections.ObjectModel;

namespace nasa_maui.ViewModels
{
    public partial class MoviePageViewModel : ViewModelBase
    {
        [ObservableProperty]
        public Video video;

        [ObservableProperty]
        public string videoUrl;

        public MoviePageViewModel(INavigationService navigationService) : base(navigationService)
        {
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
            base.OnAppearing();
        }
    }
}
