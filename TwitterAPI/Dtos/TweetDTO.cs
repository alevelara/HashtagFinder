using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterAPI.Dtos
{
    public class TweetDTO
    {
        public string Tweet { get; set; }

        public bool IsRetweeted { get; set; }

        public DateTime CreatedDateTime { get; set; }
    }
}
