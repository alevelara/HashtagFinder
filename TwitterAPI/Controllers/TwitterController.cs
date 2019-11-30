using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TweetSharp;
using TwitterAPI.Models;
using TwitterAPI.Repository;
using TwitterAPI.Repository.Interfaces;

namespace TwitterAPI.Controllers
{
    [Route("api/Twitter")]
    [ApiController]
    public class TwitterController : ControllerBase
    {
        private readonly ISearchRepository repo;

        public TwitterController(ISearchRepository repo)
        {
            this.repo = repo;
        }

        [HttpGet("{hashtag}", Name =("SearchByHashtag"))]
        public async Task<ActionResult> SearchByHashtag(string hashtag)
        {
            TwitterService service = repo.GetAuthorizeService();

            var tweets = await service.SearchAsync(new SearchOptions
            {
                Q = hashtag, 
                Count = 1000
            }).ConfigureAwait(false);

            var items = tweets.Value.Statuses.Where(x => x.IsRetweeted == false).Select(x => x.TextDecoded);

            var historicalHastag = new HistoricalHashtag()
            {
                Hashtag = hashtag, 
                CreatedDateTime = DateTime.Now                
            };

            repo.AddHistoricalHashtag(historicalHastag);

            return Ok(items);
        }
    }
}