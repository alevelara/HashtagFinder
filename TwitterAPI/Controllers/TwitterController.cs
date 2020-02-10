using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Tweetinvi.Models;
using Tweetinvi.Parameters;
using TweetSharp;
using TwitterAPI.Dtos;
using TwitterAPI.Models;
using TwitterAPI.Repositories.Interfaces;
using TwitterAPI.Repository.Interfaces;

namespace TwitterAPI.Controllers
{
    [Route("api/Twitter")]
    [ApiController]
    public class TwitterController : ControllerBase
    {
        private readonly ISearchRepository _repo;
        private readonly IMapper _mapper;
        private readonly ITweetInviService _service;
        private readonly ITweetSharpService _serviceAsync;
        private readonly IUrlCutterService _urlCutter;

        public TwitterController (ISearchRepository repo, IMapper mapper, ITweetInviService service, ITweetSharpService serviceAsync, IUrlCutterService urlCutter)
        {
            _repo = repo;
            _mapper = mapper;
            _service = service;
            _serviceAsync = serviceAsync;
            _urlCutter = urlCutter;
        }
    
        [HttpPost("searchAsync")]
        public async Task<ActionResult> SearchByHashtagAsync(FiltersDto filter)
        {            
            var statusDtos = new List<TweetStatusDto>();

            var tweets = await _serviceAsync.SearchByHashtagAsync(filter.Hashtag).ConfigureAwait(false);

            var statuses = FilterStatuses(tweets.Value.Statuses, filter);
            
            foreach (TwitterStatus status in statuses)
            {
                var dto = _mapper.Map<TweetStatusDto>(status);
                statusDtos.Add(dto);
            }
            
            var historicalHastag = new HistoricalHashtag()
            {
                Hashtag = filter.Hashtag,
                FromDateTime = !String.IsNullOrEmpty(filter.ToDate) ? DateTime.Parse(filter.ToDate, System.Globalization.CultureInfo.InvariantCulture) : DateTime.Now,
                ToDateTime = !String.IsNullOrEmpty(filter.FromDate) ? DateTime.Parse(filter.FromDate, System.Globalization.CultureInfo.InvariantCulture) : DateTime.MinValue,
                CreatedDateTime = DateTime.Now                
            };

            _repo.AddHistoricalHashtag(historicalHastag);

            return Ok(statusDtos);
        }

        [HttpPost("search")]
        public ActionResult SearchByHashtag(FiltersDto filter)
        {
            var tweetInviDtos = new List<TweetInviDTO>();
            var parameters = CreateParameters(filter);

            var searches = _service.SearchByParameters(parameters);

            var tweets = FilterStatuses(searches, filter);

            foreach (ITweet tweet in tweets)
             {
                var dto = _mapper.Map<TweetInviDTO>(tweet);
                dto.TweetUrl = _urlCutter.ShortenIt(tweet.Url);
                tweetInviDtos.Add(dto);
             }

            var historicalHastag = new HistoricalHashtag()
            {
                Hashtag = filter.Hashtag,
                FromDateTime = !String.IsNullOrEmpty(filter.ToDate) ? DateTime.Parse(filter.ToDate, System.Globalization.CultureInfo.InvariantCulture) : DateTime.Now,
                ToDateTime = !String.IsNullOrEmpty(filter.FromDate) ? DateTime.Parse(filter.FromDate, System.Globalization.CultureInfo.InvariantCulture) : DateTime.MinValue,
                CreatedDateTime = DateTime.Now
            };

            _repo.AddHistoricalHashtag(historicalHastag);

            return Ok(tweetInviDtos);
        }

        #region Private Methods
        private IEnumerable<TwitterStatus> FilterStatuses(IEnumerable<TwitterStatus> statusWitoutFilter, FiltersDto filter)
        {
            IEnumerable<TwitterStatus> filteredStatus = new List<TwitterStatus>();
            DateTime? toDateTime = !String.IsNullOrEmpty(filter.ToDate) ? DateTime.Parse(filter.ToDate, System.Globalization.CultureInfo.InvariantCulture) : DateTime.Now;
            DateTime? fromDateTime = !String.IsNullOrEmpty(filter.FromDate) ? DateTime.Parse(filter.FromDate, System.Globalization.CultureInfo.InvariantCulture) : DateTime.MinValue;

            if (String.IsNullOrEmpty(filter.ToDate) && String.IsNullOrEmpty(filter.FromDate))
            {
                filteredStatus = statusWitoutFilter;
            }
            else if (!String.IsNullOrEmpty(filter.FromDate) && String.IsNullOrEmpty(filter.ToDate))
            {
                filteredStatus = statusWitoutFilter.Where(x => x.CreatedDate >= fromDateTime.Value).ToList();
            }
            else if (!String.IsNullOrEmpty(filter.ToDate) && String.IsNullOrEmpty(filter.FromDate))
            {
                filteredStatus = statusWitoutFilter.Where(x => x.CreatedDate <= toDateTime.Value).ToList();
            }
            else if (String.IsNullOrEmpty(filter.ToDate) && String.IsNullOrEmpty(filter.FromDate))
            {
                filteredStatus = statusWitoutFilter.Where(x => x.CreatedDate >= fromDateTime.Value && x.CreatedDate <= toDateTime.Value).ToList();
            }

            return filteredStatus;

        }

        private IEnumerable<ITweet> FilterStatuses(IEnumerable<ITweet> tweetsWithoutFilter, FiltersDto filter)
        {
            IEnumerable<ITweet> filteredStatus = new List<ITweet>();
            DateTime? toDateTime = !String.IsNullOrEmpty(filter.ToDate) ? DateTime.Parse(filter.ToDate, System.Globalization.CultureInfo.InvariantCulture) : DateTime.Now;
            DateTime? fromDateTime = !String.IsNullOrEmpty(filter.FromDate) ? DateTime.Parse(filter.FromDate, System.Globalization.CultureInfo.InvariantCulture) : DateTime.MinValue;

            if (String.IsNullOrEmpty(filter.ToDate) && String.IsNullOrEmpty(filter.FromDate))
            {
                filteredStatus = tweetsWithoutFilter;
            }
            else if (!String.IsNullOrEmpty(filter.FromDate) && String.IsNullOrEmpty(filter.ToDate))
            {
                filteredStatus = tweetsWithoutFilter.Where(x => x.CreatedAt >= fromDateTime.Value).ToList();
            }
            else if (!String.IsNullOrEmpty(filter.ToDate) && String.IsNullOrEmpty(filter.FromDate))
            {
                filteredStatus = tweetsWithoutFilter.Where(x => x.CreatedAt <= toDateTime.Value).ToList();
            }
            else if (!String.IsNullOrEmpty(filter.ToDate) && !String.IsNullOrEmpty(filter.FromDate))
            {
                filteredStatus = tweetsWithoutFilter.Where(x => x.CreatedAt >= fromDateTime.Value && x.CreatedAt <= toDateTime.Value).ToList();
            }

            return filteredStatus;
        }

        private ISearchTweetsParameters CreateParameters(FiltersDto filters)
        {
            SearchTweetsParameters searchParameters = new SearchTweetsParameters(filters.Hashtag)
            {
                MaximumNumberOfResults = 1000,
                Filters = TweetSearchFilters.Hashtags
            };

            return searchParameters;
        }
        
        #endregion
    }
}