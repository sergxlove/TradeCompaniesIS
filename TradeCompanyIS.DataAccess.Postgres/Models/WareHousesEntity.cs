namespace TradeCompanyIS.DataAccess.Postgres.Models
{
    public class WareHousesEntity
    {
        public Guid Id { get; set; }
        public string Address { get; set; } = string.Empty;
        public Guid IdCountry { get; set; }
    }
}
