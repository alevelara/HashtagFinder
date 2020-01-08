using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetSharp;

namespace TwitterAPI.Dtos
{
    public class TweetStatusDto : TweetDTO
    {
        public ITweeter Author { get; set; }                
    }
}
