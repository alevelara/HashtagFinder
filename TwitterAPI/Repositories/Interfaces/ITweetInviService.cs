using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tweetinvi.Models;
using Tweetinvi.Parameters;

namespace TwitterAPI.Repositories.Interfaces
{
    public interface ITweetInviService 
    {
        IEnumerable<ITweet> SearchByHashtag(string hashtag);
        IEnumerable<ITweet> SearchReplyTo(ITweet tweet);
        IEnumerable<ITweet> SearchByParameters(ISearchTweetsParameters parameters);

    }
}
