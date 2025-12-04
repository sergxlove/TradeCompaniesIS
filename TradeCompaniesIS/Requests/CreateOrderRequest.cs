namespace TradeCompanyIS.Requests
{
    public class CreateOrderRequest
    {
        public Guid IdClients { get; set; }
        public Guid IdItem { get; set; }
        public int Quantity { get; set; }
    }
}
