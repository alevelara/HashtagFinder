using Google.Apis.Services;
using Google.Apis.Urlshortener.v1;
using Google.Apis.Urlshortener.v1.Data;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using TwitterAPI.Repositories.Interfaces;

namespace TwitterAPI.Services
{
    public class UrlCutterService : IUrlCutterService
    {
        private readonly IConfiguration _configuration;

        public UrlCutterService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string ShortenIt(string url)
        {
            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create("http://tinyurl.com/api-create.php?url=" + url);
            string strResponse = null;

            try
            {
                HttpWebResponse objResponse = objRequest.GetResponse() as HttpWebResponse;
                StreamReader stmReader = new StreamReader(objResponse.GetResponseStream());

                strResponse = stmReader.ReadToEnd();
            }
            catch { strResponse = url; }
            return strResponse;
        }
      
    }
}
