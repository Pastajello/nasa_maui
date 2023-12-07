using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HtmlAgilityPack;
using nasa_maui.Pages;
using System.Collections.ObjectModel;

namespace nasa_maui.ViewModels
{
    public class Video
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string ThumbnailUrl { get; set; }
        public string Duration { get; set; }
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

            //TODO load different series 
            //videoslists.Add(GetVideoListFromHtml(liveAndUpcoming));
            //videoslists.Add(GetVideoListFromHtml(nasaSeries));


            foreach (var section in series)
            {
                var list = GetSeriesVideos(section);
                videoslists.Add(list);
            }
            
            Sections = new ObservableCollection<VideoList>(videoslists);
        }

        static VideoList GetSeriesVideos(HtmlNode section)
        {
            var videoList = new VideoList { Videos = new List<Video>() };
            var titleNode = section.SelectSingleNode("header/h3").InnerText;
            videoList.Title = titleNode;
            // Example: Iterate through each article tag representing a video
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
                        // Extract properties from the videoNode here
                        Title = title,
                        ThumbnailUrl = thumbnail,
                        Duration = duration,
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
            int i = 5;
            var navigation = Application.Current.MainPage.Navigation;
            var page = new MoviePage() ;
            page.NavigationParameter = video;
            await navigation.PushAsync(page);
        }
    }
}
