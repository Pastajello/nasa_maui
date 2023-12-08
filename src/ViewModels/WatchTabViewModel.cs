using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HtmlAgilityPack;
using nasa_maui.Pages;
using nasa_maui.Services;
using System.Collections.ObjectModel;

namespace nasa_maui.ViewModels
{
    public class Video
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string ThumbnailUrl { get; set; }
        public string TimeInformation { get; internal set; }
    }

    public class VideoList
    {
        public List<Video> Videos { get; set; }
        public string Title { get; internal set; }
    }
    public partial class WatchTabPageViewModel : ViewModelBase
    {
        [ObservableProperty]
        public ObservableCollection<VideoList> sections;

        public WatchTabPageViewModel(INavigationService navigationService) : base(navigationService)
        {
                
        }

        public override async Task Initialize(object? navigationParameter)
        {
            base.Initialize(navigationParameter);

            var web = new HtmlWeb();
            var doc = await web.LoadFromWebAsync(" https://plus.nasa.gov/");
            var mainArticles = doc.DocumentNode.SelectNodes("//main/section/article").ToList();
            var series = doc.DocumentNode.SelectNodes("//main/section/div/article").ToList();

            var videoslists = new List<VideoList>();

            var liveAndUpcoming = mainArticles.FirstOrDefault();
            var nasaSeries = mainArticles.LastOrDefault();

            videoslists.Add(GetLiveSeries(liveAndUpcoming));

            //TODO load different series 
            //videoslists.Add(GetVideoListFromHtml(nasaSeries));


            foreach (var section in series)
            {
                var list = GetSeriesVideos(section);
                videoslists.Add(list);
            }
            
            Sections = new ObservableCollection<VideoList>(videoslists);
        }

        private VideoList GetLiveSeries(HtmlNode? section)
        {
            var videoList = new VideoList { Videos = new List<Video>() };
            var titleNode = section.SelectSingleNode("header/div/h3").InnerText;
            videoList.Title = titleNode;

            var videoNodes = section.SelectNodes("section/div/div//article");
            if (videoNodes != null)
            {
                foreach (var videoNode in videoNodes)
                {
                    var urlNode = videoNode.SelectSingleNode("div/div");
                    var url = urlNode.SelectSingleNode("div/a").Attributes["href"].Value;
                    var thumbnail = urlNode.SelectSingleNode("div/a/figure").Attributes.Last().Value.Replace("background-image:url(", "").Replace(");", "");
                    var descriptionNode = videoNode.SelectSingleNode("div/div/div[2]");

                    var title = descriptionNode.SelectSingleNode("h4/a").InnerText;

                    var startDate = descriptionNode.SelectSingleNode("div/span").InnerText;
                    var video = new Video
                    {
                        // Extract properties from the videoNode here
                        Title = title,
                        ThumbnailUrl = thumbnail,
                        TimeInformation = startDate,
                        Url = url
                    };

                    videoList.Videos.Add(video);
                }
            }

            return videoList;
        }


        static VideoList GetSeriesVideos(HtmlNode section)
        {
            var videoList = new VideoList { Videos = new List<Video>() };
            var titleNode = section.SelectSingleNode("header/h3").InnerText;
            videoList.Title = titleNode;
            var videoNodes = section.SelectNodes("section/div/div//article");
            if (videoNodes != null)
            {
                foreach (var videoNode in videoNodes)
                {
                    var innerNode = videoNode.SelectSingleNode("a/figure");
                    var url = videoNode.SelectSingleNode("a").Attributes["href"].Value;
                    var attributes = innerNode.Attributes;
                    var thumbnail = attributes.Last().Value.Replace("background-image:url(", "").Replace(");", "");
                    var title = innerNode.SelectSingleNode("div/h4").InnerText;
                    var duration = innerNode.SelectSingleNode("div/p").InnerText;
                    var video = new Video
                    {
                        Title = title,
                        ThumbnailUrl = thumbnail,
                        TimeInformation = duration,
                        Url = url
                    };

                    videoList.Videos.Add(video);
                }
            }

            return videoList;
        }

        [RelayCommand]
        public async Task NavigateToVideo(Video video)
        {
            await NavigationService.PushAsync<MoviePageViewModel>(parameter: video);
        }
    }
}
