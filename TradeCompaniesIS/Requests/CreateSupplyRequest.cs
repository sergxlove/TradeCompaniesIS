namespace TradeCompanyIS.Requests
{
    public class CreateSupplyRequest
    {
        public Guid IdProvider { get; set; }
        public Guid IdWarehouse { get; set; }
        public Guid IdItem { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
