using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TradeCompanyIS.DataAccess.Postgres.Models;

namespace TradeCompanyIS.DataAccess.Postgres.Configurations
{
    public class WarehousesConfiguration : IEntityTypeConfiguration<WareHousesEntity>
    {
        public void Configure(EntityTypeBuilder<WareHousesEntity> builder)
        {
            throw new NotImplementedException();
        }
    }
}
