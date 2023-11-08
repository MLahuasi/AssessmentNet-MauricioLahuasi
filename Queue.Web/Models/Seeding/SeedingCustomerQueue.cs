using Microsoft.EntityFrameworkCore;
using Queue.Web.Models.Entity;

namespace Queue.Web.Models.Seeding
{
    public static class SeedingCustomerQueue
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            var queue_1 = new CustomerQueue { Id = 1, Duration = 2000, Name = "Cola#1" };
            var queue_2 = new CustomerQueue { Id = 2, Duration = 3000, Name = "Cola#2" };

            modelBuilder.Entity<CustomerQueue>().HasData(queue_1, queue_2);
        }
    }
}
