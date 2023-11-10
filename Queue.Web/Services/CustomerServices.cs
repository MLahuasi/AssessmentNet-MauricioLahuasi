using Queue.Web.Models.Entity;
using Queue.Web.Models;
using Microsoft.EntityFrameworkCore;
using Queue.Web.Models.Entity.DTOs;

namespace Queue.Web.Services
{
    public class CustomerServices : IService
    {
        private readonly AppDbContext _dbContext;

        public CustomerServices(AppDbContext context)
        {
            _dbContext = context;
        }

        public async Task<bool> Add(string ci, string name)
        {
            try
            {
                var minQueue = await GetMinCustomerQueue();

                if (minQueue == null)
                    return false;

                var customer = new Customer(ci, name);
                customer.Status = Transacctions.Action.NoAttend;
                customer.StartDate = DateTime.Now;
                customer.Queue = minQueue;

                _dbContext.Add(customer);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                return false;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                //var currentCustomer = await _dbContext.Customers.AsTracking()
                //.FirstOrDefaultAsync(a => a.Id == id);

                //if (currentCustomer is null)
                //    return false;

                //currentCustomer.FinishDate = DateTime.Now;
                //currentCustomer.Status = Transacctions.Action.Attend;

                //_dbContext.Update(currentCustomer);
                

                Customer customer = _dbContext.Customers.Find(id);
                _dbContext.Customers.Remove(customer);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                return false;
            }
        }

        private async Task<CustomerQueue?> GetMinCustomerQueue()
        {

            var results = await GetMinQueueWithCustomer();


            if (results == null)
                return null;

            if (results.Count != 0)
            {
                QueueInfo minDurationItem = results.OrderBy(qi => qi.TotalQueueDuration).FirstOrDefault();

                if (minDurationItem == null)
                    return null;

                return await GetQueueById(minDurationItem.QueueId);

            }
            else
            {
                return await GetMinQueueWithOutCustomer();
            }
        }

        private async Task<List<QueueInfo>?> GetMinQueueWithCustomer()
        {
            var results = await _dbContext.CustomersQueue
            .Select(cq => new QueueInfo
            {
                QueueId = cq.Id,
                QueueName = cq.Name,
                CustomerCount = cq.Customers.Count(c => c.Status == Transacctions.Action.NoAttend),
                TotalQueueDuration = cq.Customers.Where(c => c.Status == Transacctions.Action.NoAttend).Sum(c => c.Queue.Duration)
            })
            .ToListAsync();

            return results;
        }

        private async Task<CustomerQueue> GetQueueById(int id)
        {
            var CustomersQueue = await _dbContext.CustomersQueue
                .Where(cq => cq.Id == id)
                .FirstAsync();

            return CustomersQueue;
        }

        public async Task<Customer> GetCustomerById(int id)
        {
            var customer = await _dbContext.Customers
                .Include(c => c.Queue)
                .Where(c => c.Id == id)
                .FirstAsync();

            return customer;
        }

        private async Task<CustomerQueue?> GetMinQueueWithOutCustomer()
        {
            var result = await _dbContext.CustomersQueue
                .OrderBy(cq => cq.Duration)
                .FirstAsync();

            return result;
        }

        public Task<List<CustomerQueue>?> Get()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Add(CustomerQueue queue)
        {
            throw new NotImplementedException();
        }
    }
}
