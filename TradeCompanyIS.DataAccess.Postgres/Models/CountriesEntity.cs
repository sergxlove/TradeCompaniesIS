namespace TradeCompanyIS.DataAccess.Postgres.Models
{
    public class CountriesEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public virtual List<WareHousesEntity> WareHousesRef { get; set; } = new();
        public virtual List<ProvidersEntity> ProvidersRef { get; set; } = new();
        public virtual List<ClientsEntity> ClientsRef { get; set; } = new();
    }
}
