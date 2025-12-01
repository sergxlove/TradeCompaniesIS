using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TradeCompanyIS.DataAccess.Postgres.Models;

namespace TradeCompanyIS.DataAccess.Postgres.Configurations
{
    public class ProvidersConfiguration : IEntityTypeConfiguration<ProvidersEntity>
    {
        public void Configure(EntityTypeBuilder<ProvidersEntity> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Name)
                .IsRequired();
            builder.Property(a => a.NumberPhone)
                .IsRequired();
            builder.Property(a => a.IdCountry)
                .IsRequired();
            builder.Property(a => a.Address)
                .IsRequired();
            builder.HasMany(a => a.SuppliesRef)
                .WithOne(a => a.ProvidersRef)
                .HasForeignKey(a => a.IdProvider)
                .OnDelete(DeleteBehavior.Restrict); 
            builder.HasOne(a => a.CountriesRef)
                .WithMany(a => a.ProvidersRef)
                .HasForeignKey(a => a.IdCountry)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
