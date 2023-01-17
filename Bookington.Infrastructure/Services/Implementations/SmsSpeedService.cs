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
    public class SmsSpeedService :ISmsService
    {
        private readonly Uri rootUrl = new Uri("https://api.speedsms.vn/index.php/sms/send");
        private readonly string AuthenticationToken = "Amu3akGoFIWKPLNcBBth94IAWTy549NK";
        private readonly string Sender = "083c019d6f27c92b";

        public SmsSpeedService(Uri rootUrl, string authenticationToken, string sender)
        {
            this.rootUrl = rootUrl;
            AuthenticationToken = authenticationToken;
            Sender = sender;
        }

        public SmsSpeedService()
        {
        }

        public async Task sendSmsAsync(string phones, string otp)
        {
           
            string content = $"Your Otp is: {otp}. Please don't share this to anyone";

            NetworkCredential myCreds = new NetworkCredential(AuthenticationToken, ":x");

            WebClient client = new WebClient();

            client.Credentials = myCreds;

            client.Headers[HttpRequestHeader.ContentType] = "application/json";

            var builder = "{\"to\":[\""+phones+"\"], \"content\": \"" 
                + content
                + "\", \"type\":" + 5 
                + ", \"sender\": \"" 
                + Sender + "\"}";
            string json = builder.ToString();
            client.UploadStringAsync(rootUrl, json);
        }
    }
}
