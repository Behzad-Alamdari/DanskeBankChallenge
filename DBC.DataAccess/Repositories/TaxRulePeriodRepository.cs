using DBC.DataAccess.EntityFramework;
using DBC.Infrastructure.DataAccess.Repositories;
using DBC.Models;

namespace DBC.DataAccess.Repositories
{
    public class TaxRulePeriodRepository : Repository<Period>, ITaxRulePeriodRepository
    {
        public TaxRulePeriodRepository(DansBankDbContext context)
            : base(context)
        {
        }


    }
}
