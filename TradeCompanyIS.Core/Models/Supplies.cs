namespace TradeCompanyIS.Core.Models
{
    public class Supplies
    {
        public Guid Id { get; }
        public Guid IdProvider { get; }
        public Guid IdWarehouse { get; }
        public Guid IdItem { get; }
        public int Quantity { get; }
        public decimal Price { get; }

        public static ResultModel<Supplies> Create(Guid id, Guid idProvider, Guid idWarehouse,
            Guid idItem, int quantity, decimal price)
        {
            return ResultModel<Supplies>.Success(new Supplies(id, idProvider, idWarehouse, idItem,
                quantity, price));
        }

        private Supplies(Guid id, Guid idProvider, Guid idWarehouse, Guid idItem, int quantity, 
            decimal price)
        {
            Id = id;
            IdProvider = idProvider;
            IdWarehouse = idWarehouse;
            IdItem = idItem;
            Quantity = quantity;
            Price = price;
        }
    }
}
