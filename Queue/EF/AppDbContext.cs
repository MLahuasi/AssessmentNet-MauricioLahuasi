using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Queue.EF.EntityConfig;
using Queue.EF.Seeding;
using Queue.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue.EF
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer("Server=ELESSAR;Database=AssessmentNet;Integrated Security=False;User Id=sa;Password=P@$$w0rd;TrustServerCertificate=True;");
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
