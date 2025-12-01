using System.ComponentModel.Design;

namespace TradeCompanyIS.DataAccess.Postgres.Models
{
    public class WareHousesEntity
    {
        public Guid Id { get; set; }
        public string Address { get; set; } = string.Empty;
        public Guid IdCountry { get; set; }

        public virtual CountriesEntity? CountriesRef { get; set; }
        public virtual List<ItemsEntity> ItemsRef { get; set; } = new();
        public virtual List<SuppliesEntity> SuppliesRef { get; set; } = new();
    }
}
