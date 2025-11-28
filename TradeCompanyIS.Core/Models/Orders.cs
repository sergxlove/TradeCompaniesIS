namespace TradeCompanyIS.Core.Models
{
    public class Orders
    {
        public Guid Id { get; } 
        public Guid IdClients { get; }
        public DateOnly DateReg { get; }
        public int Quantity { get; }

        public static ResultModel<Orders> Create(Guid id, Guid idClient, DateOnly dateReg,
            int quantity)
        {
            return ResultModel<Orders>.Success(new Orders(id, idClient, dateReg, quantity));
        }

        private Orders(Guid id, Guid idClient, DateOnly dateReg,
            int quantity)
        {
            Id = id;
            IdClients = idClient;
            DateReg = dateReg;
            Quantity = quantity;
        }
    }
}
