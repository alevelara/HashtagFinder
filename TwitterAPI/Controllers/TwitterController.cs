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
using TwitterAPI.Repository;
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

        [HttpGet("{hashtag}", Name =("SearchByHashtag"))]
        public async Task<ActionResult> SearchByHashtag(string hashtag)
        {
            TwitterService service = _repo.GetAuthorizeService();
            var statusDtos = new List<TwitterStatusDto>();

            var tweets = await _service.SearchByHashtagAsync(hashtag).ConfigureAwait(false);

            var statuses = tweets.Value.Statuses;
            
            foreach(TwitterStatus status in statuses)
            {
                var dto = _mapper.Map<TwitterStatusDto>(status);
                statusDtos.Add(dto);
            }
            
            var historicalHastag = new HistoricalHashtag()
            {
                Hashtag = hashtag, 
                CreatedDateTime = DateTime.Now                
            };

            _repo.AddHistoricalHashtag(historicalHastag);

            return Ok(statusDtos);
        }
    }
}