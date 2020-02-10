using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetSharp;
using TwitterAPI.Repositories.Interfaces;
using Tweetinvi;
using Tweetinvi.Models;
using Microsoft.Extensions.Configuration;
using TwitterAPI.Models;
using NLog.Fluent;
using Tweetinvi.Parameters;

namespace TwitterAPI.Services
{
    public class TweetInviService : ITweetInviService
    {
        private readonly IConfiguration _configuration;
        private readonly Authorize authorize;

        public TweetInviService(IConfiguration configuration)
        {
            _configuration = configuration;
            authorize = new Authorize(_configuration);
            SetTwitterAuthUser();
        }

        public IEnumerable<ITweet> SearchByHashtag(string hashtag)
        {
            return Search.SearchTweets(hashtag);
        }

        public IEnumerable<ITweet> SearchByParameters(ISearchTweetsParameters parameters)
        {
            return Search.SearchTweets(parameters);
        }

        public IEnumerable<ITweet> SearchReplyTo(ITweet tweet)
        {
            return Search.SearchRepliesTo(tweet, true);
        }

        private void SetTwitterAuthUser()
        {
            var apiKey = authorize.ApiKey;
            var apiSecretKey = authorize.SecretApiKey;

            var accessToken = authorize.Token;
            var accessSecretToken = authorize.SecretToken;

            try
            {
                var credentials = Auth.SetUserCredentials(apiKey, apiSecretKey, accessToken, accessSecretToken);
                Auth.SetCredentials(credentials);
            }
            catch (Exception e)
            {
                Log.Error("Error during authorization: " + e.Message);
            }

        }
    }
}
