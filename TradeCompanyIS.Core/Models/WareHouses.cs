namespace TradeCompanyIS.Core.Models
{
    public class WareHouses
    {
        public Guid Id { get; }
        public string Address { get; } = string.Empty;
        public Guid IdCountry { get; } 

        public static ResultModel<WareHouses> Create(Guid id, string address, Guid idCountry)
        {
            return ResultModel<WareHouses>.Success(new WareHouses(id, address, idCountry));
        }

        private WareHouses(Guid id, string address, Guid idCountry)
        {
            Id = id;
            Address = address;
            IdCountry = idCountry;
        }
    }
}
