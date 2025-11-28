namespace TradeCompanyIS.DataAccess.Postgres.Models
{
    public class ItemsEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public Guid IdWareHouse { get; set; }
        public int QuantityWareHouse { get; set; }
    }
}
