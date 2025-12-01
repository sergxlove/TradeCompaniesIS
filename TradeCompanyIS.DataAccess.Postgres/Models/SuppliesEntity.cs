namespace TradeCompanyIS.DataAccess.Postgres.Models
{
    public class SuppliesEntity
    {
        public Guid Id { get; set; }
        public Guid IdProvider { get; set; }
        public Guid IdWarehouse { get; set; }
        public Guid IdItem { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public virtual ProvidersEntity? ProvidersRef { get; set; }
        public virtual WareHousesEntity? WareHousesRef { get; set; }
        public virtual ItemsEntity? ItemsRef { get; set; }
    }
}
