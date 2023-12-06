﻿using CommunityToolkit.Mvvm.ComponentModel;
using HtmlAgilityPack;
using System.Collections.ObjectModel;

namespace nasa_maui.ViewModels
{
    public partial class MoviePageViewModel : ViewModelBase
    {
        [ObservableProperty]
        public Video video;

        [ObservableProperty]
        public string videoUrl;
        public override async void Init(object? navigationParameter)
        {
            base.Init(navigationParameter);

            Video = navigationParameter as Video;

            var web = new HtmlWeb();
            var doc = web.Load(Video.Url);
            var node = doc.DocumentNode.SelectNodes("//a[@class='usa-button usa-button--outline usa-button--inverse button--round icon--download']");
            VideoUrl = node[0].Attributes["href"].Value;
        }

        public override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}
