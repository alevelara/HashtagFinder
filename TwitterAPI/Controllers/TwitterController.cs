using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
        private readonly ITweetSharpService _service;

        public TwitterController(ISearchRepository repo, IMapper mapper, ITweetSharpService service)
        {
            _repo = repo;
            _mapper = mapper;
            _service = service;
        }

        [HttpPost("search")]
        public async Task<ActionResult> SearchByHashtag(FiltersDto filter)
        {            
            var statusDtos = new List<TwitterStatusDto>();

            var tweets = await _service.SearchByHashtagAsync(filter.Hashtag).ConfigureAwait(false);

            var statuses = FilterStatuses(tweets.Value.Statuses, filter);
            
            foreach (TwitterStatus status in statuses)
            {
                var dto = _mapper.Map<TwitterStatusDto>(status);
                statusDtos.Add(dto);
            }
            
            var historicalHastag = new HistoricalHashtag()
            {
                Hashtag = filter.Hashtag, 
                CreatedDateTime = DateTime.Now                
            };

            _repo.AddHistoricalHashtag(historicalHastag);

            return Ok(statuses);
        }

        private IEnumerable<TwitterStatus> FilterStatuses(IEnumerable<TwitterStatus> statusWitoutFilter, FiltersDto filter)
        {
            IEnumerable<TwitterStatus> filteredStatus = new List<TwitterStatus>();
            DateTime? toDateTime = !String.IsNullOrEmpty(filter.ToDate) ? DateTime.Parse(filter.ToDate, System.Globalization.CultureInfo.InvariantCulture) : DateTime.Now;
            DateTime? fromDateTime = !String.IsNullOrEmpty(filter.FromDate) ? DateTime.Parse(filter.FromDate, System.Globalization.CultureInfo.InvariantCulture) : DateTime.MinValue;

            if (!toDateTime.HasValue && !fromDateTime.HasValue)
            {
                filteredStatus = statusWitoutFilter;
            }
            else if (fromDateTime.HasValue && !toDateTime.HasValue)
            {
                filteredStatus = statusWitoutFilter.Where(x => x.CreatedDate >= fromDateTime.Value).ToList();
            }
            else if (toDateTime.HasValue && !fromDateTime.HasValue)
            {
                filteredStatus = statusWitoutFilter.Where(x => x.CreatedDate <= toDateTime.Value).ToList();
            }
            else if (fromDateTime.HasValue && toDateTime.HasValue)
            {
                filteredStatus = statusWitoutFilter.Where(x => x.CreatedDate >= fromDateTime.Value && x.CreatedDate <= toDateTime.Value).ToList();
            }

            return filteredStatus;

        }
    }
}