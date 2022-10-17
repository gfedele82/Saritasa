using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Request
{
    public class RequestRates
    {
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public decimal moneyUsd { get; set; }
    }
}
