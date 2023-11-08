using Queue.Web.Models.Entity;
using Queue.Web.Models;
using Microsoft.EntityFrameworkCore;
using Queue.Web.Models.Entity.DTOs;

namespace Queue.Web.Services
{
    public class CustomerServices
    {
        private readonly AppDbContext _context;

        public CustomerServices(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            return await _context.Customers
                .Include(c => c.Queue)
                .Where(c => c.Status == Transacctions.Command.Action.NoAttend)
                .ToListAsync();
        }


        public async Task<CustomerQueue> GetQueueById(int id)
        {
            var CustomersQueue = await _context.CustomersQueue
                .Where(cq => cq.Id == id)
                .FirstAsync();

            return CustomersQueue;
        }

        public async Task<List<QueueInfo>?> GetMinQueueWithCustomer()
        {
            var results = await _context.CustomersQueue
            .Select(cq => new QueueInfo
            {
                QueueId = cq.Id,
                QueueName = cq.Name,
                CustomerCount = cq.Customers.Count(c => c.Status == Transacctions.Command.Action.NoAttend),
                TotalQueueDuration = cq.Customers.Where(c => c.Status == Transacctions.Command.Action.NoAttend).Sum(c => c.Queue.Duration)
            })
            .ToListAsync();

            return results;
        }

        public async Task<CustomerQueue?> GetMinQueueWithOutCustomer()
        {
            var result = await _context.CustomersQueue
                .OrderBy(cq => cq.Duration)
                .FirstAsync();

            return result;
        }

        public async Task<CustomerQueue?> GetMinCustomerQueue()
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

        public async Task<IEnumerable<QueueCustomerInfo>?> GetQueueCustomerList()
        {
            var result = await _context.CustomersQueue
            .Where(cq => cq.Customers.Any(c => c.Status == Transacctions.Command.Action.NoAttend))
            .Select(cq => new
            {
                Customers = cq.Customers.Where(c => c.Status == Transacctions.Command.Action.NoAttend)
                    .Select(c => new
                    {
                        QueueId = cq.Id,
                        QueueName = cq.Name,
                        QueueDuration = cq.Duration,
                        CustomerId = c.Id,
                        CustomerName = c.Name,
                        CustomerStatus = c.Status,
                        CustomerStartDate = c.StartDate,
                        CustomerFinishDate = c.FinishDate
                    })
            })
            .ToListAsync();

            return null;
        }

        public async Task<Customer?> Add(string ci, string name)
        {
            var minQueue = await GetMinCustomerQueue();

            if (minQueue == null)
                return null;

            var customer = new Customer(ci, name);
            customer.Status = Transacctions.Command.Action.NoAttend;
            customer.StartDate = DateTime.Now;
            customer.Queue = minQueue;

            _context.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<bool> Delete(string ci, string name)
        {
            var currentCustomer = await _context.Customers.AsTracking()
                .FirstOrDefaultAsync(a => a.Ci == ci && a.Name == name);

            if (currentCustomer is null)
                return false;

            currentCustomer.FinishDate = DateTime.Now;
            currentCustomer.Status = Transacctions.Command.Action.Attend;

            _context.Update(currentCustomer);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
