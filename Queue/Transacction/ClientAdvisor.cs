using Queue.EF;
using Queue.EF.Services;
using Queue.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue.Transacction
{
    public partial class ClientAdvisor
    {
        private Queue<Customer> _customers;

        private readonly CustomerServices _services;

        public Task<bool> ChargeCustomers { get; private set; }

        public ClientAdvisor()
        {
            _customers = new Queue<Customer>();
            _services = new CustomerServices(new AppDbContext());
            ChargeCustomers = Task.Run(() => GetCustomers());
        }

        public async Task<bool> GetCustomers ()
        {
            var currentCustomers = await _services.GetCustomers();
            foreach (var customer in currentCustomers)
            {
                _customers.Enqueue(customer);
            }

            return true;
        }

        public async Task<Customer?> AddCustomer(Customer customer)
        {
            var newCustomer = await _services.Add(customer.Ci, customer.Name);

            if (newCustomer != null)
            {
                _customers.Enqueue(newCustomer);
                return newCustomer;
            }
            
            return null;
        }

        public async Task<bool> RemoveCustomer(Customer customer)
        {
            Queue<Customer> currentCustomers = new Queue<Customer>();
            bool result = false;

            foreach (Customer _customer in _customers)
            {
                if (customer.Name != _customer.Name
                    && customer.Id != _customer.Id)
                {
                    var deleteCustomer = await _services.Delete(customer.Ci, customer.Name);
                    if(deleteCustomer)
                    {
                        currentCustomers.Enqueue(customer);
                        return true;
                    }                    
                }
            }
            _customers = currentCustomers;
            return result;
        }


        public Dictionary<string, Queue<Customer>> GetQueues()
        {
            var groupedQueues = _customers.GroupBy(c => c.Queue.Name);

            Dictionary<string, Queue<Customer>> queuesByName = new Dictionary<string, Queue<Customer>>();

            foreach (var group in groupedQueues)
            {
                Queue<Customer> subQueue = new Queue<Customer>(group);
                queuesByName.Add(group.Key, subQueue);
            }
            return queuesByName;
        }

        public Queue<Customer> GetMainQueue()
        {
            return _customers;
        }

        public async Task ProcessCustomersAsync()
        {
            while (_customers.Count > 0)
            {
                Customer customer = _customers.Dequeue();
                await Task.Delay(TimeSpan.FromSeconds(customer.Queue.Duration));
                await RemoveCustomer(customer);
                Console.WriteLine($"Eliminado: {customer.ToString()}");
            }
        }
    }
}
