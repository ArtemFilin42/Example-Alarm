using System;
using System.Net;
using System.Globalization;

namespace ExampleClock.Scripts.Services
{
    public static class NetworkTimeService
    {
        public static DateTime RequestTime(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            var response = request.GetResponse();
            string date = response.Headers["date"];
            return DateTime.ParseExact(date, "ddd, dd MMM yyyy HH:mm:ss 'GMT'", CultureInfo.InvariantCulture.DateTimeFormat, DateTimeStyles.AssumeUniversal);
        }
    }
}