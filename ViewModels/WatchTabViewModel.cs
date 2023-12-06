using CommunityToolkit.Mvvm.ComponentModel;
using HtmlAgilityPack;
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

        public override async void OnAppearing()
        {
            base.OnAppearing();

            var client = new HttpClient();

            var a = await client.GetStringAsync("https://plus.nasa.gov/video/we-make-history-here/");
            var web = new HtmlWeb();
            var doc = web.Load(" https://plus.nasa.gov/");
            var mainArticles = doc.DocumentNode.SelectNodes("//main/section/article").ToList();
            var series = doc.DocumentNode.SelectNodes("//main/section/div/article").ToList();

            var videoslists = new List<VideoList>();

            var liveAndUpcoming = mainArticles.FirstOrDefault();

            //videoslists.Add(GetVideoListFromHtml(liveAndUpcoming));

            var nasaSeries = mainArticles.LastOrDefault();
            //videoslists.Add(GetVideoListFromHtml(nasaSeries));


            foreach (var section in series)
            {
                var list = GetSeriesVideos(section);
                videoslists.Add(list);
            }

            Device.BeginInvokeOnMainThread(() => {
            Sections = new ObservableCollection<VideoList>(videoslists);
            });
            Console.WriteLine(videoslists.Count);
            int i = 5;
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

     
    }
}
