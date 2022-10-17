using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class RatesRepository : IRatesRepository
    {
        private readonly RatesContext _dbContext;

        public RatesRepository(RatesContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Schema.Rates> GetAsync(DateTime id)
        {
            return await _dbContext.Rates.Where(p => p.Id == id).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<Schema.Rates> SaveOrUpdateAsync(Schema.Rates rate)
        {
            _dbContext.ChangeTracker.Clear();
            var entity = await _dbContext.Rates.FindAsync(rate.Id);
            if (entity == null)
            {
                await _dbContext.Rates.AddAsync(rate);
            }
            else
            {
                _dbContext.Rates.Update(rate);
            }
            _dbContext.SaveChanges();
            return rate;
        }

        public async Task<Schema.Rates> DeleteAsync(DateTime id)
        {
            _dbContext.ChangeTracker.Clear();
            var entity = await _dbContext.Rates.FindAsync(id);
            if (entity == null)
            {
                return null;
            }
            else
            {
                _dbContext.Rates.Remove(entity);
            }
            _dbContext.SaveChanges();
            return entity;
        }

        public async Task<IEnumerable<Schema.Rates>> GetByCiteria(Expression<Func<Schema.Rates, bool>> predicateSearch)
        {
            return await _dbContext.Rates.Where(predicateSearch).AsNoTracking().ToListAsync();
        }
    }
}
