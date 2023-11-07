using Microsoft.EntityFrameworkCore;
using Queue.Entity;
using Queue.Transacction.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue.EF.Seeding
{
    public static class SeedingCustomerQueue
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            var queue_1 = new CustomerQueue {Id = 1, Duration = 2000, Name = "Cola#1" };
            var queue_2 = new CustomerQueue {Id = 2, Duration = 3000, Name = "Cola#2" };

            modelBuilder.Entity<CustomerQueue>().HasData(queue_1, queue_2);
        }
    }
}
