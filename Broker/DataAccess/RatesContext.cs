using Common;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class RatesContext: DbContext
    {
        public RatesContext(DbContextOptions<RatesContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Schema.Rates>().ToTable(DBTables.DBRates);
            //modelBuilder.Entity<Schema.Rates>().Property(x => x.EUR).HasPrecision(10,10);
            //modelBuilder.Entity<Schema.Rates>().Property(x => x.GBP).HasPrecision(10,10);
            //modelBuilder.Entity<Schema.Rates>().Property(x => x.JPY).HasPrecision(10,10);
            //modelBuilder.Entity<Schema.Rates>().Property(x => x.RUB).HasPrecision(10,10);
        }



        public virtual DbSet<Schema.Rates> Rates { get; set; }
    }
}
