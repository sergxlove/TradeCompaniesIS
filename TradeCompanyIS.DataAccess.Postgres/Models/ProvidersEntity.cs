namespace TradeCompanyIS.DataAccess.Postgres.Models
{
    public class ProvidersEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string NumberPhone { get; set; } = string.Empty;
        public Guid IdCountry { get; set; }
        public string Address { get; set; } = string.Empty;
    }
}
