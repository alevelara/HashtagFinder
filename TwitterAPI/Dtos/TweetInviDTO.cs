using System;
using Tweetinvi.Logic.DTO;

namespace TwitterAPI.Dtos
{
    public class TweetInviDTO : TweetDTO
    {
        public string Author { get; set; }
        
        public string ProfileImageUrl { get; set; }

        public string TweetUrl { get; set; }

        public int RetweetedCount { get; set; }

        public int FavCount { get; set; }

        public int CommentsCount { get; set; }
    }
}
