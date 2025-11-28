using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TradeCompanyIS.DataAccess.Postgres.Models;

namespace TradeCompanyIS.DataAccess.Postgres.Configurations
{
    public class ClientsConfiguration : IEntityTypeConfiguration<ClientsEntity>
    {
        public void Configure(EntityTypeBuilder<ClientsEntity> builder)
        {
            throw new NotImplementedException();
        }
    }
}
