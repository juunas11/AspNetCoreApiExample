using Microsoft.EntityFrameworkCore;
using ElectronicsStoreApi.DomainModels;

namespace ElectronicsStoreApi.DAL
{
    public class StoreDataContext : DbContext
    {
        public StoreDataContext(DbContextOptions<StoreDataContext> options)
            : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderRow> OrderRows { get; set; }
    }
}
