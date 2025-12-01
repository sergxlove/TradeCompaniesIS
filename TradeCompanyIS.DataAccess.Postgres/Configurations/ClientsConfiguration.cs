using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TradeCompanyIS.DataAccess.Postgres.Models;

namespace TradeCompanyIS.DataAccess.Postgres.Configurations
{
    public class ClientsConfiguration : IEntityTypeConfiguration<ClientsEntity>
    {
        public void Configure(EntityTypeBuilder<ClientsEntity> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.IdCountry)
                .IsRequired();
            builder.Property(a => a.NameClient)
                .IsRequired();
            builder.Property(a => a.NumberPhone)
                .IsRequired();
            builder.Property(a => a.AddressDelivery)
                .IsRequired();
            builder.HasIndex(a => a.Email)
                .IsUnique();
            builder.HasMany(a => a.OrdersRef)
                .WithOne(a => a.ClientsRef)
                .HasForeignKey(a => a.IdClients)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(a => a.CountriesRef)
                .WithMany(a => a.ClientsRef)
                .HasForeignKey(a => a.IdCountry)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
