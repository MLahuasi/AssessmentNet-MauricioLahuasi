using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Queue.Web.Models;
using Queue.Web.Models.Entity;
using Queue.Web.Models.Entity.DTOs;
using System.Collections.ObjectModel;

namespace Queue.Web.Services
{
    public class CustomerQueueService : IService
    {
        private readonly AppDbContext _dbContext;

        public CustomerQueueService(AppDbContext context)
        {
            _dbContext = context;
        }

        public async Task<List<CustomerQueue>?> Get()
        {
            try
            {
                var results = await _dbContext.CustomersQueue
                   .Include(cq => cq.Customers)
                   .OrderBy(cq => cq.Duration)
                   .ToListAsync();


                return results;
            }
            catch (Exception err)
            {
                Console.Write(err.Message);
                return null;
            }
        }

        public async Task<bool> Add(CustomerQueue queue)
        {
            try
            {
                await _dbContext.CustomersQueue.AddAsync(queue);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception err)
            {
                Console.Write(err.Message);
                return false;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                CustomerQueue queue = _dbContext.CustomersQueue.Find(id);
                _dbContext.CustomersQueue.Remove(queue);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception err)
            {
                Console.Write(err.Message);
                return false;
            }
        }
    }
}
