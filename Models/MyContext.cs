using Microsoft.EntityFrameworkCore;

namespace ElysianMotors.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options) { }
        public DbSet<Customer> Customers {get;set;}
        public DbSet<Vehicle> Vehicles {get;set;}
        public DbSet<Order> Orders {get;set;}
    }
}