namespace TradeCompanyIS.Core.Models
{
    public class Clients
    {
        public Guid Id { get; }
        public string NameClient { get; } = string.Empty;
        public string NumberPhone { get; } = string.Empty;
        public string Email { get; } = string.Empty;
        public Guid IdCountry { get; }
        public string AddressDelivery {  get; } = string.Empty;

        public static ResultModel<Clients> Create(Guid id, string nameClient, string numberPhone,
            string email, Guid idCountry, string addressDelivery)
        {

            return ResultModel<Clients>.Success(new Clients(id, nameClient, numberPhone, email, 
                idCountry, addressDelivery));
        }

        private Clients(Guid id, string nameClient, string numberPhone, string email,
            Guid idCountry, string addressDelivery)
        {
            Id = id;
            NameClient = nameClient;
            NumberPhone = numberPhone;
            Email = email;
            IdCountry = idCountry;
            AddressDelivery = addressDelivery;
        }
    }
}
