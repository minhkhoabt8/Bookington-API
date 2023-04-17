using BCrypt.Net;
using Bookington.Infrastructure.DTOs.Momo;
using Bookington.Infrastructure.Helpers;
using Bookington.Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
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
                HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(endpoint);

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

                // Get the request stream and write the JSON data to it asynchronously
                using (Stream stream = await httpWReq.GetRequestStreamAsync())
                {
                    await stream.WriteAsync(data, 0, data.Length);
                }

                // Get the HttpWebResponse object from the HttpWebRequest object
                HttpWebResponse response = (HttpWebResponse)await httpWReq.GetResponseAsync();

                string jsonresponse = "";

                // Read the response from the server asynchronously
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    string temp = null;
                    while ((temp = await reader.ReadLineAsync()) != null)
                    {
                        jsonresponse += temp;
                    }
                }

                // Return the response from the server as a string
                return jsonresponse;
            }
            catch (WebException ex)
            {
                // Handle any exceptions here
                throw;
            }

        }

        public async Task<MomoResponseDTO> CreatePaymentRequestToMomo(int amount, string orderInfo, string extraData = "")
        {

            string partnerCode = _configuration.GetValue<string>("Momo:PartnerCode");
            string accessKey = _configuration.GetValue<string>("Momo:AccessKey");
            string secretKey = _configuration.GetValue<string>("Momo:SecretKey");
            string returnUrl = _configuration.GetValue<string>("Momo:ReturnUrl");
            string notifiUrl = _configuration.GetValue<string>("Momo:NotifiUrl");
            string endpoint = _configuration.GetValue<string>("Momo:Endpoint");
            string requestType = "captureWallet";

            string orderId = Guid.NewGuid().ToString();
            string requestId = Guid.NewGuid().ToString();
            //Before sign HMAC SHA256 signature

            string rawHash = "accessKey=" + accessKey +
                "&amount=" + amount.ToString() +
                "&extraData=" + extraData +
                "&ipnUrl=" + notifiUrl +
                "&orderId=" + orderId +
                "&orderInfo=" + orderInfo +
                "&partnerCode=" + partnerCode +
                "&redirectUrl=" + returnUrl +
                "&requestId=" + requestId +
                "&requestType=" + requestType
                ;

            System.Diagnostics.Debug.WriteLine("\n[MOMO Log] - RAW HASH: " + rawHash + "\n\n");

            string signature = await _momoHelpers.SignSHA256(rawHash, secretKey);

            System.Diagnostics.Debug.WriteLine("\n[MOMO Log] - SIGNATURE: " + signature + "\n\n");

            //build body json request
            JObject message = new JObject
            {
                { "partnerCode", partnerCode },
                { "partnerName", "Test" },
                { "storeId", "MomoTestStore" },
                { "requestId", requestId },
                { "amount", amount },
                { "orderId", orderId },
                { "orderInfo", orderInfo },
                { "redirectUrl", returnUrl },
                //{ "redirectUrl", redirectUrl },
                //{ "ipnUrl", ipnUrl },
                { "ipnUrl", notifiUrl },
                { "lang", "en" },
                { "extraData", extraData },
                { "requestType", requestType },
                { "signature", signature }
            };

            System.Diagnostics.Debug.WriteLine("\n[MOMO Log] - JSON REQUEST TO MOMO: " + message + "\n\n");
            
            var responseFromMomo = await SendPaymentRequest(endpoint, message.ToString());

            JObject jmessage = JObject.Parse(responseFromMomo);

            System.Diagnostics.Debug.WriteLine("\n[MOMO Log] - RESPONSE FROM MOMO: " + message + "\n\n");

            MomoResponseDTO momoResponse = JsonConvert.DeserializeObject<MomoResponseDTO>(responseFromMomo);

            return momoResponse;
        }

    }
}
