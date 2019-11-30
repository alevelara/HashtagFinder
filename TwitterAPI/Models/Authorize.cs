using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterAPI.Models
{
    public class Authorize
    {
        private readonly IConfiguration _configuration;

        public Authorize(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string ApiKey { get => _configuration.GetValue<string>("ApiKey"); }
        public string SecretApiKey { get => _configuration.GetValue<string>("ApiSecretKey");}
        public string Token { get => _configuration.GetValue<string>("AccesToken"); }
        public string SecretToken { get => _configuration.GetValue<string>("AccesSecretToken"); }

    }
}

