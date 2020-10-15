using DBC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DBC.DataAccess.ModelConfigurations
{
    public class MunicipalityConfig : IEntityTypeConfiguration<Municipality>
    {
        public void Configure(EntityTypeBuilder<Municipality> builder)
        {
            builder
                .HasAlternateKey(m => m.Name);

            builder
                .HasMany(m => m.TaxRules)
                .WithOne(t => t.Municipality)
                .HasForeignKey(f => f.MunicipalityId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
