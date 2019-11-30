using Microsoft.Extensions.Configuration;
using NLog.Fluent;
using System;
using TweetSharp;
using TwitterAPI.Contexts;
using TwitterAPI.Models;
using TwitterAPI.Repository.Interfaces;

namespace TwitterAPI.Repository
{
    public class SearchRepository : ISearchRepository
    {
        private readonly IConfiguration _configuration;
        private readonly HistoricalHashtagContext _context;
        private readonly Authorize authorize;
        public SearchRepository(IConfiguration configuration, HistoricalHashtagContext context)
        {
            _configuration = configuration;
            _context = context;

            authorize = new Authorize(_configuration);
        }        

        public TwitterService GetAuthorizeService()
        {
            TwitterService service;

            var apiKey = authorize.ApiKey;
            var apiSecretKey = authorize.SecretApiKey;

            var accessToken = authorize.Token;
            var accesSecretToken = authorize.SecretToken;

            try
            {
                service = new TwitterService(apiKey, apiSecretKey, accessToken, accesSecretToken);                
            } 
            catch (Exception e)
            {
                Log.Error("Error during authorization: " + e.Message);
                service = null;
            }

            return service;
        }

        public void AddHistoricalHashtag(HistoricalHashtag historicalHashtag)
        {
            try
            {
                _context.HistoricalHashtags.Add(historicalHashtag);
                _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Log.Error("Error adding new HistoricalHashtag:" + e.Message);
            }
        }
    }
}
