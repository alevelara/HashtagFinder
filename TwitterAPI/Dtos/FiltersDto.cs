using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterAPI.Dtos
{
    public class FiltersDto
    {
        public string Hashtag { get; set; }
        public string ToDate { get; set; }
        public string FromDate { get; set; }
    }
}
