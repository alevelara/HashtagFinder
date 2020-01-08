using Google.Apis.Services;
using Google.Apis.Urlshortener.v1;
using Google.Apis.Urlshortener.v1.Data;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            var service = GetUrlShortenerService();

            var dataUrl = new Url();
            dataUrl.LongUrl = url;

            return service.Url.Insert(dataUrl).Execute().Id;
        }

        private UrlshortenerService GetUrlShortenerService()
        {
            UrlshortenerService service = new UrlshortenerService(new BaseClientService.Initializer()
            {
                ApiKey = _configuration.GetValue<string>("UrlCutterApiKey"),
                ApplicationName = "UrlCutter ApiKey"
            });

            return service;
        }
    }
}
