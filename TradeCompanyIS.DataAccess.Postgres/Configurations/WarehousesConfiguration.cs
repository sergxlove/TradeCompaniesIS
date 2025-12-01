using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TradeCompanyIS.DataAccess.Postgres.Models;

namespace TradeCompanyIS.DataAccess.Postgres.Configurations
{
    public class WarehousesConfiguration : IEntityTypeConfiguration<WareHousesEntity>
    {
        public void Configure(EntityTypeBuilder<WareHousesEntity> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.IdCountry)
                .IsRequired();
            builder.Property(a => a.Address)
                .IsRequired();
            builder.HasMany(a => a.SuppliesRef)
                .WithOne(a => a.WareHousesRef)
                .HasForeignKey(a => a.IdWarehouse)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(a => a.ItemsRef)
                .WithOne(a => a.WareHouseRef)
                .HasForeignKey(a => a.IdWareHouse)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(a => a.CountriesRef)
                .WithMany(a => a.WareHousesRef)
                .HasForeignKey(a => a.IdCountry)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
