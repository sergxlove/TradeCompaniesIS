namespace TradeCompanyIS.Requests
{
    public class UpdatePriceItemRequest
    {
        public Guid ID { get; set; }
        public decimal NewPrice { get; set; }
    }
}
