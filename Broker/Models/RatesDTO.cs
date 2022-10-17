using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class RatesDTO
    {
        public DateTime date { get; set; }

        public decimal rub { get; set; }

        public decimal eur { get; set; }

        public decimal gbp { get; set; }

        public decimal jpy { get; set; }
    }
}
