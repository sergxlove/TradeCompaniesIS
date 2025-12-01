using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TradeCompanyIS.DataAccess.Postgres.Models;

namespace TradeCompanyIS.DataAccess.Postgres.Configurations
{
    public class SuppliersConfiguration : IEntityTypeConfiguration<SuppliesEntity>
    {
        public void Configure(EntityTypeBuilder<SuppliesEntity> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.IdProvider)
                .IsRequired();
            builder.Property(a => a.IdItem)
                .IsRequired();
            builder.Property(a => a.IdWarehouse)
                .IsRequired();
            builder.Property(a => a.Quantity)
                .IsRequired();
            builder.Property(a => a.Price)
                .IsRequired();
            builder.HasOne(a => a.ProvidersRef)
                .WithMany(a => a.SuppliesRef)
                .HasForeignKey(a => a.IdProvider)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(a => a.WareHousesRef)
                .WithMany(a => a.SuppliesRef)
                .HasForeignKey(a => a.IdProvider)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(a => a.WareHousesRef)
                .WithMany(a => a.SuppliesRef)
                .HasForeignKey(a => a.IdProvider)
                .OnDelete(DeleteBehavior.Restrict); 
        }
    }
}
