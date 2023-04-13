using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.Helpers
{
    public interface IMomoHelpers
    {
        string GetHash(string partnerCode, string merchantRefId,
            string amount, string paymentCode, string storeId, string storeName, string publicKeyXML);
        string BuildQueryHash(string partnerCode, string merchantRefId,
            string requestid, string publicKey);
        string BuildRefundHash(string partnerCode, string merchantRefId,
            string momoTranId, long amount, string description, string publicKey);
        string signSHA256(string message, string key);
    }
}
