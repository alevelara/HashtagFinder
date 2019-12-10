using NLog.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetSharp;
using TwitterAPI.Repositories.Interfaces;

namespace TwitterAPI.Services
{
    public class TweetSharpService : ITweetSharpService
    {
        private TwitterService _service;

        public TwitterService GetAuthorizeService(string apiKey, string apiSecretKey, string accessToken, string accessSecretToken) 
        {                       
            try
            {
                _service = new TwitterService(apiKey, apiSecretKey, accessToken, accessSecretToken);
            }
            catch (Exception e)
            {
                Log.Error("Error during authorization: " + e.Message);
                _service = null;
            }

            return _service;
        }

        public async Task<TwitterAsyncResult<TwitterSearchResult>> SearchByHashtagAsync(string hashtag)
        {

            return await _service.SearchAsync(new SearchOptions
            {
                Q = hashtag,
                Count = 1000
            }).ConfigureAwait(false);

        }
    }
}
