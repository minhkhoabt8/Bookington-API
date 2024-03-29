﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.Helpers
{
    public interface IMomoHelpers
    {
        Task<string> GetHash(string partnerCode, string merchantRefId,
           string amount, string paymentCode, string storeId, string storeName, string publicKeyXML);

        Task<string> BuildQueryHash(string partnerCode, string merchantRefId,
             string requestid, string publicKey);

        Task<string> BuildRefundHash(string partnerCode, string merchantRefId,
            string momoTranId, long amount, string description, string publicKey);

        Task<string> SignSHA256(string message, string key);
    }
}
