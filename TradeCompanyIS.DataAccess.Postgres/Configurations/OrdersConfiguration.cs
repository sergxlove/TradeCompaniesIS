using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TradeCompanyIS.DataAccess.Postgres.Models;

namespace TradeCompanyIS.DataAccess.Postgres.Configurations
{
    public class OrdersConfiguration : IEntityTypeConfiguration<OrdersEntity>
    {
        public void Configure(EntityTypeBuilder<OrdersEntity> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.IdClients)
                .IsRequired();
            builder.Property(a => a.IdItem)
                .IsRequired();
            builder.Property(a => a.DateReg)
                .IsRequired();
            builder.Property(a => a.Quantity)
                .IsRequired();
            builder.HasOne(a => a.ClientsRef)
                .WithMany(a => a.OrdersRef)
                .HasForeignKey(a => a.IdClients)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(a => a.ItemsRef)
                .WithMany(a => a.OrdersRef)
                .HasForeignKey(a => a.IdItem)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
