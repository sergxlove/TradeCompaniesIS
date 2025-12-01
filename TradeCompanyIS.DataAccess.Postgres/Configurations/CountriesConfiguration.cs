using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TradeCompanyIS.DataAccess.Postgres.Models;

namespace TradeCompanyIS.DataAccess.Postgres.Configurations
{
    public class CountriesConfiguration : IEntityTypeConfiguration<CountriesEntity>
    {
        public void Configure(EntityTypeBuilder<CountriesEntity> builder)
        {
            builder.HasKey(a => a.Id);
            builder.HasIndex(a => a.Name)
                .IsUnique();
            builder.HasMany(a => a.ClientsRef)
                .WithOne(a => a.CountriesRef)
                .HasForeignKey(a => a.IdCountry)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(a => a.ProvidersRef)
                .WithOne(a => a.CountriesRef)
                .HasForeignKey(a => a.IdCountry)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(a => a.WareHousesRef)
                .WithOne(a => a.CountriesRef)
                .HasForeignKey(a => a.IdCountry)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
