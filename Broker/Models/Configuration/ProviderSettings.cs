using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Configuration
{
    public class ProviderSettings
    {
        public const string KEY = "ProviderConf";

        public string ProviderKey { get; set; }

        public string URL { get; set; }
    }
}
