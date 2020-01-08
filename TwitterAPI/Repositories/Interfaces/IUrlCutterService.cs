using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterAPI.Repositories.Interfaces
{
    public interface IUrlCutterService
    {
        public string ShortenIt(string url);
    }
}
