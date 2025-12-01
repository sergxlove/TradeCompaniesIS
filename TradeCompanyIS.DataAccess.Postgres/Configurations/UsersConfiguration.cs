using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TradeCompanyIS.DataAccess.Postgres.Models;

namespace TradeCompanyIS.DataAccess.Postgres.Configurations
{
    public class UsersConfiguration : IEntityTypeConfiguration<UsersEntity>
    {
        public void Configure(EntityTypeBuilder<UsersEntity> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Username)
                .IsRequired();
            builder.Property(a => a.HashPassword)
                .IsRequired();
            builder.Property(a => a.Role)
                .IsRequired();
        }
    }
}
