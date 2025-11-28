using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TradeCompanyIS.DataAccess.Postgres.Models;

namespace TradeCompanyIS.DataAccess.Postgres.Configurations
{
    public class CountriesConfiguration : IEntityTypeConfiguration<CountriesEntity>
    {
        public void Configure(EntityTypeBuilder<CountriesEntity> builder)
        {
            throw new NotImplementedException();
        }
    }
}
