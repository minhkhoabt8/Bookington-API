using Azure.Core;
using Bookington.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.Services.Implementations
{
    public class SmsSpeedService : ISmsService
    {
        private readonly IConfiguration _configuration;

        public SmsSpeedService(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public async Task sendSmsAsync(string phones, string otp)
        {
            var access = _configuration["SmsSpeed:AccessToken"];

            var auToken = _configuration["SmsSpeed:SenderId"];

            Uri root =  new Uri(_configuration["SmsSpeed:ApiEndpoint"]);

            string content = $"Your Otp is: {otp}. Please don't share this to anyone";

            NetworkCredential myCreds = new NetworkCredential(access, ":x");

            WebClient client = new WebClient();

            client.Credentials = myCreds;

            client.Headers[HttpRequestHeader.ContentType] = "application/json";

            var builder = "{\"to\":[\"" + phones + "\"], \"content\": \"" 
                + content
                + "\", \"type\":" + 5 
                + ", \"sender\": \"" 
                + auToken + "\"}";

            string json = builder.ToString();

            client.UploadStringAsync(root, json);
        }
    }
}
