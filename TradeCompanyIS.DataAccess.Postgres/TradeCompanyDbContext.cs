using Microsoft.EntityFrameworkCore;
using TradeCompanyIS.DataAccess.Postgres.Configurations;
using TradeCompanyIS.DataAccess.Postgres.Models;

namespace TradeCompanyIS.DataAccess.Postgres
{
    public class TradeCompanyDbContext : DbContext
    {
        public TradeCompanyDbContext(DbContextOptions<TradeCompanyDbContext> options) 
            : base(options) { }

        public DbSet<ClientsEntity> ClientsTable { get; set; }
        public DbSet<CountriesEntity> CountriesTable { get; set; }
        public DbSet<ItemsEntity> ItemsTable { get; set; }
        public DbSet<OrdersEntity> OredersTable { get; set; }
        public DbSet<ProvidersEntity> ProvidersTable { get; set; }
        public DbSet<SuppliesEntity> SuppliesTable { get; set; }
        public DbSet<UsersEntity> UsersTable { get; set; }
        public DbSet<WareHousesEntity> WareHousesTable { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClientsConfiguration());
            modelBuilder.ApplyConfiguration(new CountriesConfiguration());
            modelBuilder.ApplyConfiguration(new ItemsConfiguration());
            modelBuilder.ApplyConfiguration(new OrdersConfiguration());
            modelBuilder.ApplyConfiguration(new ProvidersConfiguration());
            modelBuilder.ApplyConfiguration(new SuppliersConfiguration());
            modelBuilder.ApplyConfiguration(new UsersConfiguration());
            modelBuilder.ApplyConfiguration(new WarehousesConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
