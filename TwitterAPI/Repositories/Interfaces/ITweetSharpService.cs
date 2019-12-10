using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetSharp;

namespace TwitterAPI.Repositories.Interfaces
{
    public interface ITweetSharpService
    {
        TwitterService GetAuthorizeService();
        Task<TwitterAsyncResult<TwitterSearchResult>> SearchByHashtagAsync(string hashtag);
    }
}
