using Microsoft.Extensions.Configuration;
using NLog.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetSharp;
using TwitterAPI.Models;
using TwitterAPI.Repositories.Interfaces;

namespace TwitterAPI.Services
{
    public class TweetSharpService : ITweetSharpService
    {
        private readonly IConfiguration _configuration;
        private readonly Authorize authorize;

        private TwitterService _service;
        
        public TweetSharpService(IConfiguration configuration)
        {
            _configuration = configuration;
            authorize = new Authorize(_configuration);

            _service = GetAuthorizeService();
        }
       
        public async Task<TwitterAsyncResult<TwitterSearchResult>> SearchByHashtagAsync(string hashtag)
        {

            return await _service.SearchAsync(new SearchOptions
            {
                Q = hashtag,
                Count = 1000
            }).ConfigureAwait(false);

        }

        private TwitterService GetAuthorizeService()
        {
            var apiKey = authorize.ApiKey;
            var apiSecretKey = authorize.SecretApiKey;

            var accessToken = authorize.Token;
            var accessSecretToken = authorize.SecretToken;

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
    }
}
