namespace TradeCompanyIS.Requests
{
    public class RegClientRequest
    {
        public string NameClient { get; set; } = string.Empty;
        public string NumberPhone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public Guid IdCountry { get; set; }
        public string AddressDelivery { get; set; } = string.Empty;
    }
}
