using Contracts.Providers;
using Models.Configuration;
using Models.Request;
using Models.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Net.Http.Headers;
using Common;
using Newtonsoft.Json;

namespace Engine.Providers
{
    public class RateProviderEngine: IRateProviderEngine
    {
        private readonly ILogger<RateProviderEngine> _logger;
        private readonly ProviderSettings _providerSettings;

        public RateProviderEngine(ILogger<RateProviderEngine> logger,
           ProviderSettings providerSettings)
        {
            _logger = logger;
            _providerSettings = providerSettings;
        }

        public async Task<string> GetRates(DateTime date)
        {
            var client = new HttpClient();
            string url;
            string dateformat = date.ToString("yyyy-MM-dd");
            url = $"{_providerSettings.URL.Replace(SystemParameters.ProviderDateReplace, dateformat)}";
            url = $"{url.Replace(SystemParameters.ProviderKeyReplace, _providerSettings.ProviderKey)}";



            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/x-www-form-urlencoded; charset=utf-8");

            var result = await client.GetAsync(url);
 
            if (result.IsSuccessStatusCode)
            { 
                string json = result.Content.ReadAsStringAsync().Result;
                _logger.LogInformation($"Provider response: {json}");
                return json;
            }
            else
            {
                _logger.LogWarning($"Error to get info from Provider");
                return string.Empty;
            }
        }
    }
}
