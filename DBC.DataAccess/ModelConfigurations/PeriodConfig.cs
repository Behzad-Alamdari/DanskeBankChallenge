using DBC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DBC.DataAccess.ModelConfigurations
{
    public class PeriodConfig : IEntityTypeConfiguration<Period>
    {
        public void Configure(EntityTypeBuilder<Period> builder)
        {
            builder
                .HasOne(p => p.TaxRule)
                .WithMany(t => t.Periods)
                .HasForeignKey(f => f.TaxRuleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
