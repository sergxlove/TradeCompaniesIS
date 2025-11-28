namespace TradeCompanyIS.Core.Models
{
    public class Items
    {
        public Guid Id { get; }
        public string Name { get; } = string.Empty;
        public string Description { get; } = string.Empty;
        public decimal Price { get; } 
        public Guid IdWareHouse { get; }
        public int QuantityWareHouse { get; }

        public static ResultModel<Items> Create(Guid id, string name, string description, 
            decimal price, Guid IdWareHouse, int quantityWareHouse)
        {
            return ResultModel<Items>.Success(new Items(id, name, description, price, IdWareHouse, 
                quantityWareHouse));
        }

        private Items(Guid id, string name, string description, decimal price,
            Guid idWareHouse, int quantityWareHouse)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            IdWareHouse = idWareHouse;
            QuantityWareHouse = quantityWareHouse;
        }
    }
}
