using System.Data.Entity;
using WpfApplicationEntity.API;

namespace WpfApplicationEntity.API
{
    class MyDBContext : DbContext
    {
        public MyDBContext() : base("DbConnectString")
        {
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order_Product> Order_Products { get; set; }
        public DbSet<Plan_Product> Plan_Products { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Product_in_stock> Product_in_stocks  { get; set; }
        public DbSet<Product_Type> Product_types{ get; set; }
        public DbSet<Production_plan> Production_plans { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<Shipment_Product> Shipment_Products{ get; set; }
        public DbSet<Shop> Shops{ get; set; }
    }
}
