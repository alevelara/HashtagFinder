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
        
        public SearchRepository(HistoricalHashtagContext context)
        {            
            _context = context;                  
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
