using System;
using System.Threading.Tasks;

namespace Contracts.Providers
{
    public interface IRateProviderEngine
    {
        Task<string> GetRates(DateTime date);
    }
}
