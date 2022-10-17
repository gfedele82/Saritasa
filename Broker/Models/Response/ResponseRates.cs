using Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Response
{
    public class ResponseRates
    {
        public List<RatesDTO> Rates { get; set; }

        public DateTime buyDate { get; set; }

        public DateTime sellDate { get; set; }

        public string tool { get; set; }

        public decimal revenue { get; set; }
    }
}
