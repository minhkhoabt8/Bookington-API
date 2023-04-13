using BCrypt.Net;
using Bookington.Infrastructure.Helpers;
using Bookington.Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.Services.Implementations
{
    public class MomoPaymentService : IMomoPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IMomoHelpers _momoHelpers;

        public MomoPaymentService(IConfiguration configuration, IMomoHelpers momoHelpers)
        {
            _configuration = configuration;
            _momoHelpers = momoHelpers;
        }

        public async Task<string> SendPaymentRequest(string endpoint, string postJsonString)
        {
            try
            {
                // Create a new HttpWebRequest object with the specified endpoint
                HttpWebRequest httpWReq = (HttpWebRequest) WebRequest.Create(endpoint);

                // Convert the JSON data to a byte array
                var postData = postJsonString;

                var data = Encoding.UTF8.GetBytes(postData);


                // Set the properties of the HttpWebRequest object
                httpWReq.ProtocolVersion = HttpVersion.Version11;
                httpWReq.Method = "POST";
                httpWReq.ContentType = "application/json";

                httpWReq.ContentLength = data.Length;
                httpWReq.ReadWriteTimeout = 30000;
                httpWReq.Timeout = 15000;

                // Get the request stream and write the JSON data to it
                using (Stream stream = await httpWReq.GetRequestStreamAsync())
                {
                    await stream.WriteAsync(data, 0, data.Length);
                    await stream.FlushAsync();
                }

                // Get the HttpWebResponse object from the HttpWebRequest object
                HttpWebResponse response = (HttpWebResponse)await  httpWReq.GetResponseAsync();

                string jsonresponse = "";

                // Read the response from the server
                using (var reader = new StreamReader(response.GetResponseStream()))
                {

                    string temp = null;
                    while ((temp = reader.ReadLine()) != null)
                    {
                        jsonresponse += temp;
                    }
                }


                // Return the response from the server as a string
                return jsonresponse;
                
            }
            catch (WebException e)
            {
                return e.Message;
            }
        }

        public async Task<string> CreatePaymentRequestToMomo(decimal amount, string orderInfo, string extraData = "")
        {

            string partnerCode = _configuration.GetValue<string>("Momo:PartnerCode");
            string accessKey = _configuration.GetValue<string>("Momo:AccessKey");
            string secretKey = _configuration.GetValue<string>("Momo:SecretKey");
            string returnUrl = _configuration.GetValue<string>("Momo:ReturnUrl");
            string notifiUrl = _configuration.GetValue<string>("Momo:NotifiUrl");
            string endpoint = _configuration.GetValue<string>("Momo:Endpoint");

            string orderId = DateTime.Now.Ticks.ToString();
            string requestId = DateTime.Now.Ticks.ToString();

            //Before sign HMAC SHA256 signature
            string rawHash = $"partnerCode={partnerCode}" +
                                $"&accessKey={accessKey}" +
                                $"&requestId={requestId}" +
                                $"&amount={amount}" +
                                $"&orderId={orderId}" +
                                $"&orderInfo={orderInfo}" +
                                $"&returnUrl={returnUrl}" +
                                $"&notifyUrl={notifiUrl}" +
                                $"&extraData={extraData}";

            string signature = _momoHelpers.signSHA256(rawHash, secretKey);

            JObject message = new JObject
            {
                { "partnerCode", partnerCode },
                { "accessKey", accessKey },
                { "requestId", requestId },
                { "amount", amount },
                { "orderId", orderId },
                { "orderInfo", orderInfo },
                { "returnUrl", returnUrl },
                { "notifyUrl", notifiUrl },
                { "extraData", extraData },
                { "requestType", "captureMoMoWallet" },
                { "signature", signature }
            };


            string responseFromMomo = await SendPaymentRequest(endpoint, message.ToString());

            JObject jmessage = JObject.Parse(responseFromMomo);

            return jmessage.GetValue("payUrl").ToString();
        }


    }
}
