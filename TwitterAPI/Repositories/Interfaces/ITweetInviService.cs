using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tweetinvi.Models;

namespace TwitterAPI.Repositories.Interfaces
{
    public interface ITweetInviService 
    {
        IEnumerable<ITweet> SearchByHashtag(string hashtag);
        IEnumerable<ITweet> SearchReplyTo(ITweet tweet);
    }
}
