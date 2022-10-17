using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IRatesRepository
    {
        Task<Schema.Rates> GetAsync(DateTime id);

        Task<Schema.Rates> SaveOrUpdateAsync(Schema.Rates rate);

        Task<Schema.Rates> DeleteAsync(DateTime id);

        Task<IEnumerable<Schema.Rates>> GetByCiteria(Expression<Func<Schema.Rates, bool>> predicateSearch);
    }
}
