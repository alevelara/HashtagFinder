using Microsoft.Extensions.Configuration;
using NLog.Fluent;
using System;
using TweetSharp;
using TwitterAPI.Contexts;
using TwitterAPI.Models;
using TwitterAPI.Repositories.Interfaces;
using TwitterAPI.Repository.Interfaces;
using TwitterAPI.Services;

namespace TwitterAPI.Repository
{
    public class SearchRepository : ISearchRepository
    {
        private readonly IConfiguration _configuration;
        private readonly HistoricalHashtagContext _context;
        private readonly ITweetSharpService _service;
        private readonly Authorize authorize;

        public SearchRepository(IConfiguration configuration, HistoricalHashtagContext context, ITweetSharpService service)
        {
            _configuration = configuration;
            _context = context;
            _service = service;

            authorize = new Authorize(_configuration);
        }        

        public TwitterService GetAuthorizeService()
        {
            var apiKey = authorize.ApiKey;
            var apiSecretKey = authorize.SecretApiKey;

            var accessToken = authorize.Token;
            var accesSecretToken = authorize.SecretToken;

            return _service.GetAuthorizeService(apiKey, apiSecretKey, accessToken, accesSecretToken);
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
