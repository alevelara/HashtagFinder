using NLog.Fluent;
using System;
using TwitterAPI.Contexts;
using TwitterAPI.Models;
using TwitterAPI.Repository.Interfaces;

namespace TwitterAPI.Repository
{
    public class SearchRepository : ISearchRepository
    {        
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
