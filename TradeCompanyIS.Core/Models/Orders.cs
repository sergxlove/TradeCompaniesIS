namespace TradeCompanyIS.Core.Models
{
    public class Orders
    {
        public Guid Id { get; } 
        public Guid IdClients { get; }
        public Guid IdItem { get; }
        public DateOnly DateReg { get; }
        public int Quantity { get; }

        public static ResultModel<Orders> Create(Guid id, Guid idClient, Guid idItem, 
            DateOnly dateReg, int quantity)
        {
            return ResultModel<Orders>.Success(new Orders(id, idClient, idItem, dateReg, quantity));
        }

        private Orders(Guid id, Guid idClient, Guid idItem, DateOnly dateReg, 
            int quantity)
        {
            Id = id;
            IdClients = idClient;
            IdItem = idItem;
            DateReg = dateReg;
            Quantity = quantity;
        }
    }
}
