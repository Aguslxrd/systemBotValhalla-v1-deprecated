using System;
using System.Collections.Generic;
using System.Linq;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using System.Text;
using System.Threading.Tasks;

namespace ValhallaBOT.StreamersNotifys
{
    public class YTEngine
    {

        public string channelId = "UC4w-kAV7u95ZVcHZdjerglg"; //channel ID del streamer
        public string apiKey = "";
        public YTVideoAlert _video = new YTVideoAlert(); // Llamando a la otra clase

        public YTVideoAlert GetLatestVideo(string channelId, string apiKey)
        {
            string videoId; //Temporary variables for video details
            string videoUrl;
            string videoTitle;
            DateTime? videoPublishedAt;

            var youtubeService = new YouTubeService(new BaseClientService.Initializer() //Initialising our API
            {
                ApiKey = apiKey,
                ApplicationName = "MyApp"
            });

            var searchListRequest = youtubeService.Search.List("snippet"); //Setting up our search
            searchListRequest.ChannelId = channelId;
            searchListRequest.MaxResults = 1;
            searchListRequest.Order = SearchResource.ListRequest.OrderEnum.Date;

            var searchListResponse = searchListRequest.Execute(); //Executing the search

            foreach (var searchResult in searchListResponse.Items)
            {
                if (searchResult.Id.Kind == "youtube#video") //We are looking for a youtube video here
                {
                    videoId = searchResult.Id.VideoId; //Setting our details
                    videoUrl = $"https://www.youtube.com/watch?v={videoId}";
                    videoTitle = searchResult.Snippet.Title;
                    videoPublishedAt = searchResult.Snippet.PublishedAt;
                    var thumbnail = searchResult.Snippet.Thumbnails.Default__.Url;

                    return new YTVideoAlert() //Storing in a class for use in the bot
                    {
                        videoId = videoId,
                        videoUrl = videoUrl,
                        videoTitle = videoTitle,
                        thumbnail = thumbnail,
                        PublishedVideoAt = (DateTime)videoPublishedAt
                    };
                }
                else
                {
                    return null;
                }
            }
            return null;
        }
    }
}