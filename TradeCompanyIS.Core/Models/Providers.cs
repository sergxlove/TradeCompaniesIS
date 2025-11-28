namespace TradeCompanyIS.Core.Models
{
    public class Providers
    {
        public Guid Id { get; }
        public string Name { get; } = string.Empty;
        public string NumberPhone { get; } = string.Empty;
        public Guid IdCountry { get; } 
        public string Address { get; } = string.Empty;

        public static ResultModel<Providers> Create(Guid id, string name, string numberPhone,
            Guid idCountry, string address)
        {
            return ResultModel<Providers>.Success(new Providers(id, name, numberPhone, idCountry, 
                address));
        }

        private Providers(Guid id, string name, string numberPhone, Guid idCountry, string address)
        {
            Id = id;
            Name = name;
            NumberPhone = numberPhone;
            IdCountry = idCountry;
            Address = address;
        }
    }
}
