namespace TradeCompanyIS.DataAccess.Postgres.Models
{
    public class ClientsEntity
    {
        public Guid Id { get; set; }
        public string NameClient { get; set; } = string.Empty;
        public string NumberPhone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public Guid IdCountry { get; set; }
        public string AddressDelivery { get; set; } = string.Empty;

        public virtual CountriesEntity? CountriesRef { get; set; }
        public virtual List<OrdersEntity> OrdersRef { get; set; } = new();
    }
}
