using DBC.DataAccess.ModelConfigurations;
using DBC.Infrastructure.DataAccess;
using DBC.Models;
using Microsoft.EntityFrameworkCore;

namespace DBC.DataAccess.EntityFramework
{
    public class DansBankDbContext : DbContext
    {
        private readonly IConnectionStringProvider _connectionProvider;

        public DansBankDbContext(IConnectionStringProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }

        public DbSet<Municipality> Municipalities { get; set; }
        public DbSet<TaxRule> TaxRules { get; set; }
        public DbSet<Period> Periods { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new MunicipalityConfig());
            modelBuilder.ApplyConfiguration(new TaxRuleConfig());
            modelBuilder.ApplyConfiguration(new PeriodConfig());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite(_connectionProvider.Connection());
        }
    }
}
