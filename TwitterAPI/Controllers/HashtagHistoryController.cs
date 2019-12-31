using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TwitterAPI.Models;
using TwitterAPI.Repositories.Interfaces;

namespace TwitterAPI.Controllers
{
    [Route("api/HashtagHistory")]
    [ApiController]
    public class HashtagHistoryController : ControllerBase
    {
        private readonly IHistoricalHashtagRepository _repo;

        public HashtagHistoryController(IHistoricalHashtagRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult> GetHistorical()
        {
            var historical =  await _repo.GetAsyncHistoricalHashtags().ConfigureAwait(false);

            return Ok(historical);
        }

    }
}