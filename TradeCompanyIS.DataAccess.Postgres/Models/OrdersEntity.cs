namespace TradeCompanyIS.DataAccess.Postgres.Models
{
    public class OrdersEntity
    {
        public Guid Id { get; set; }
        public Guid IdClients { get; set; }
        public DateOnly DateReg { get; set; }
        public int Quantity { get; set; }
    }
}
