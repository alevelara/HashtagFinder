using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetSharp;
using TwitterAPI.Dtos;

namespace TwitterAPI.Repositories.Interfaces
{
    public interface ITweetSharpService
    {        
        Task<TwitterAsyncResult<TwitterSearchResult>> SearchByHashtagAsync(string hashtag);
        
    }
}
