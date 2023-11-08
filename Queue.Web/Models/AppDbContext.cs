using Microsoft.EntityFrameworkCore;
using Queue.Web.Models.Entity.Config;
using Queue.Web.Models.Entity;
using Queue.Web.Models.Seeding;
using Microsoft.Extensions.Configuration;

namespace Queue.Web.Models
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("DefaultConnection");
            }
        }



        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<DateTime>().HaveColumnType("datetime2");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CustomerConfig());
            modelBuilder.ApplyConfiguration(new CustomerQueueConfig());

            //INSERT DATA
            SeedingCustomerQueue.Seed(modelBuilder);
        }


        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<CustomerQueue> CustomersQueue { get; set; } = null!;
    }
}
