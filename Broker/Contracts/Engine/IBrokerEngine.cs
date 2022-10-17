using Models.Response;
using System;
using System.Threading.Tasks;

namespace Contracts.Engine
{
    public interface IBrokerEngine
    {
        Task<ResponseRates> GetBestRate(DateTime startDate, DateTime endDate, decimal moneyUsd);
    }
}
