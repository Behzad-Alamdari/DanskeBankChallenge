using DBC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DBC.DataAccess.ModelConfigurations
{
    public class TaxRuleConfig : IEntityTypeConfiguration<TaxRule>
    {
        public void Configure(EntityTypeBuilder<TaxRule> builder)
        {
            builder
                .HasOne(r => r.Municipality)
                .WithMany(m => m.TaxRules)
                .HasForeignKey(f => f.MunicipalityId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(r => r.Periods)
                .WithOne(p => p.TaxRule)
                .HasForeignKey(f => f.TaxRuleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
