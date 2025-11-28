using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TradeCompanyIS.DataAccess.Postgres.Models;

namespace TradeCompanyIS.DataAccess.Postgres.Configurations
{
    public class OrdersConfiguration : IEntityTypeConfiguration<OrdersEntity>
    {
        public void Configure(EntityTypeBuilder<OrdersEntity> builder)
        {
            throw new NotImplementedException();
        }
    }
}
