using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TradeCompanyIS.DataAccess.Postgres.Models;

namespace TradeCompanyIS.DataAccess.Postgres.Configurations
{
    public class ItemsConfiguration : IEntityTypeConfiguration<ItemsEntity>
    {
        public void Configure(EntityTypeBuilder<ItemsEntity> builder)
        {
            builder.HasKey(a => a.Id);
            builder.HasIndex(a => a.Name)
                .IsUnique();
            builder.Property(a => a.Description)
                .IsRequired();
            builder.Property(a => a.IdWareHouse)
                .IsRequired();
            builder.Property(a => a.Price)
                .IsRequired();
            builder.Property(a => a.QuantityWareHouse)
                .IsRequired();
            builder.HasMany(a => a.SuppliesRef)
                .WithOne(a => a.ItemsRef)
                .HasForeignKey(a => a.IdItem)
                .OnDelete(DeleteBehavior.Restrict); 
            builder.HasMany(a => a.OrdersRef)
                .WithOne(a => a.ItemsRef)
                .HasForeignKey(a => a.IdItem)
                .OnDelete(DeleteBehavior.Restrict); 
            builder.HasOne(a => a.WareHouseRef)
                .WithMany(a => a.ItemsRef)
                .HasForeignKey(a => a.IdWareHouse)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
