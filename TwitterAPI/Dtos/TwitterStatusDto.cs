using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetSharp;

namespace TwitterAPI.Dtos
{
    public class TwitterStatusDto
    {
        public ITweeter Author { get; set; }        

        public string Tweet { get; set; }

        public bool IsRetweeted { get; set; }

        public DateTime CreatedDateTime { get; set; }
    }
}
