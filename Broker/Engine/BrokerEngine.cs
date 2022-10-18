using Common;
using Contracts.Engine;
using Contracts.Providers;
using DataAccess.DTOAdapters;
using DataAccess.Interfaces;
using DataAccess.Schema;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Models;
using Models.Enums;
using Models.Request;
using Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Engine
{
    public class BrokerEngine : IBrokerEngine
    {
        private readonly IValidator<RequestRates> _validator;
        private readonly IRatesRepository _repository;
        private readonly IRateProviderEngine _rateProvider;
        private readonly ILogger<BrokerEngine> _logger;

        public BrokerEngine (IValidator<RequestRates> validator,
           IRatesRepository repository,
           IRateProviderEngine rateProvider,
           ILogger<BrokerEngine> logger)
        {
            _validator = validator;
            _repository = repository;
            _rateProvider = rateProvider;
            _logger = logger;

        }
        public async Task<ResponseRates> GetBestRate(DateTime startDate, DateTime endDate, decimal moneyUsd)
        {
            ResponseRates _response = new ResponseRates();
            RequestRates request = new RequestRates()
            {
                startDate = startDate,
                endDate = endDate,
                moneyUsd = moneyUsd
            };

            var valid = await _validator.ValidateAsync(request).ConfigureAwait(false);
            if (!valid.IsValid)
            {
                throw new ArgumentException(string.Join("\n", valid.Errors));
            }

            List<Rates> dbListRates =  _repository.GetByCiteria(p => p.Id >= startDate && p.Id <= endDate).Result.ToList();

            while (startDate <= endDate)
            {
                if (!dbListRates.Any(p => p.Id == startDate))
                {
                    string json = await _rateProvider.GetRates(startDate);
                    Rates dbRates = new Rates();
                    dbRates.ToDBModel(startDate, json);
                    if(dbRates == null)
                    {
                        throw new ArgumentException(ExcepcionsMenssages.ProviderError);
                    }
                    await _repository.SaveOrUpdateAsync(dbRates);
                    dbListRates.Add(dbRates);
                }
                startDate = startDate.AddDays(1);
            }

            dbListRates = dbListRates.OrderBy(p => p.Id).ToList();

            for (int i = 1; i < dbListRates.Count; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    decimal revenue = 0;
                    Coins tool = Calculate(ref revenue, dbListRates[i], dbListRates[j], moneyUsd);

                    if (_response.revenue == 0 || _response.revenue < revenue)
                    {
                        _response.revenue = revenue;
                        _response.buyDate = dbListRates[j].Id;
                        _response.sellDate = dbListRates[i].Id;
                        _response.Rates = new List<RatesDTO>();
                        _response.tool = tool.ToString();
                        Parallel.ForEach(dbListRates.GetRange(j, (i - j + 1)), rate =>
                        {
                            _response.Rates.Add(new RatesDTO()
                            {
                                date = rate.Id,
                                eur = rate.EUR,
                                gbp = rate.GBP,
                                jpy = rate.JPY,
                                rub = rate.RUB
                            });
                         });
                    }
                }

            }

            return _response;
        }

        private Coins Calculate (ref decimal revenue, Rates rateFrom, Rates rateTo, decimal moneyUsd)
        {
            decimal temprevenue;
            Coins tempcoins = Coins.RUB;

            foreach (string coin in Enum.GetNames(typeof(Coins)))
            {
                decimal rateToTemp = (decimal)(GetPropValue(rateTo, coin));
                decimal rateFromTemp = (decimal)(GetPropValue(rateFrom, coin));
                if (rateToTemp != 0 && rateFromTemp != 0)
                {
                    temprevenue = ((rateToTemp * moneyUsd / rateFromTemp) - (rateFrom.Id - rateTo.Id).Days) - moneyUsd;
                    if (revenue == 0 || revenue < temprevenue)
                    {
                        tempcoins = (Coins)Enum.Parse(typeof(Coins), coin);
                        revenue = temprevenue;
                    }
                }
            }
            return tempcoins;
        }

        private Object GetPropValue(Object obj, String name)
        {
            foreach (String part in name.Split('.'))
            {
                if (obj == null) { return null; }

                Type type = obj.GetType();
                PropertyInfo info = type.GetProperty(part);
                if (info == null) { return null; }

                obj = info.GetValue(obj, null);
            }
            return obj;
        }
    }
}
