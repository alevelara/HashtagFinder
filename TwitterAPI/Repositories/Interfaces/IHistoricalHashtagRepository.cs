using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterAPI.Contexts;
using TwitterAPI.Models;

namespace TwitterAPI.Repositories.Interfaces
{
    public interface IHistoricalHashtagRepository
    {              
        public Task<List<HistoricalHashtag>> GetAsyncHistoricalHashtags();
    }
}
