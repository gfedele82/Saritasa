using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.DTOAdapters
{
    public static class RatesAdapters
    {
        public static void ToDBModel(this Schema.Rates rate, DateTime date, string json)
        {
            if (rate == null || string.IsNullOrEmpty(json))
                return;
            try
            {
                dynamic data = JObject.Parse(json);
                rate.RUB = data.rates.RUB;
                rate.EUR = data.rates.EUR;
                rate.GBP = data.rates.GBP;
                rate.JPY = data.rates.JPY;
                rate.Response = json;
                rate.Id = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
            }
            catch
            {
                return;
            }

        }
    }
}
