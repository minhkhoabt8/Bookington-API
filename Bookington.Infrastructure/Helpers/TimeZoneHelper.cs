using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.Helpers
{
    public static class TimeZoneHelper
    {
        private static string CURRENT_TIME_ZONE = "North Asia Standard Time";

        public static DateTime convertToCurrentTimeZone(DateTime deployerCurrDateTime)
        {
            TimeZoneInfo currTimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(CURRENT_TIME_ZONE);

            //Convert to UTC+0 before converting to UTC+7
            DateTime convertedDateTime = TimeZoneInfo.ConvertTimeFromUtc(deployerCurrDateTime.ToUniversalTime(), currTimeZoneInfo);

            return convertedDateTime;
        }
    }
}
