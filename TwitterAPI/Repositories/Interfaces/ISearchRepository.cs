using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetSharp;
using TwitterAPI.Models;

namespace TwitterAPI.Repository.Interfaces
{
    public interface ISearchRepository
    {
        TwitterService GetAuthorizeService();
        public void AddHistoricalHashtag(HistoricalHashtag historicalHashtag);
    }
}
