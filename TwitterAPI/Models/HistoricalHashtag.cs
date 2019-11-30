using System;

namespace TwitterAPI.Models
{
    public class HistoricalHashtag
    {
        public int HistoricalHashtagID { get; set; }
        public string Hashtag { get; set; }        
        public DateTime? FromDateTime { get; set; }
        public DateTime? ToDateTime { get; set; }
        public DateTime CreatedDateTime { get; set; }        
    }
}
