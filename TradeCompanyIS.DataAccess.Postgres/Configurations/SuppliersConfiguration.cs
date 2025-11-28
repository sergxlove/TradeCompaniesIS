using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TradeCompanyIS.DataAccess.Postgres.Models;

namespace TradeCompanyIS.DataAccess.Postgres.Configurations
{
    public class SuppliersConfiguration : IEntityTypeConfiguration<SuppliesEntity>
    {
        public void Configure(EntityTypeBuilder<SuppliesEntity> builder)
        {
            throw new NotImplementedException();
        }
    }
}
