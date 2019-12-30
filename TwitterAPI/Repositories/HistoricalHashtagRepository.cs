using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterAPI.Contexts;
using TwitterAPI.Models;
using TwitterAPI.Repositories.Interfaces;

namespace TwitterAPI.Repositories
{
    public class HistoricalHashtagRepository : IHistoricalHashtagRepository
    {
        private readonly HistoricalHashtagContext _context;

        public HistoricalHashtagRepository(HistoricalHashtagContext context)
        {
            _context = context;
        }       

        public async Task<List<HistoricalHashtag>> GetAsyncHistoricalHashtags()
        {
            return  await (from h in _context.HistoricalHashtags orderby h.CreatedDateTime descending select h)
                .ToListAsync()
                .ConfigureAwait(false);
        }
    }
}
