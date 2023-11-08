using Queue.Web.Models.Entity;
using Queue.Web.Models;
using Queue.Web.Services;

namespace Queue.Web.Transacctions
{
    public partial class ClientAdvisor
    {
        private Queue<Customer> _customers;

        private readonly CustomerServices _services;

        public Task<bool> ChargeCustomers { get; private set; }

        private readonly AppDbContext _context;

        public ClientAdvisor(AppDbContext context)
        {
            _context = context;
            _customers = new Queue<Customer>();
            _services = new CustomerServices(_context);
            ChargeCustomers = Task.Run(() => GetCustomers());
        }

        public async Task<bool> GetCustomers()
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
                    if (deleteCustomer)
                    {
                        currentCustomers.Enqueue(customer);
                        return true;
                    }
                }
            }
            _customers = currentCustomers;
            return result;
        }

        public IEnumerable<IGrouping<string?, Customer>>? GetGroupQueues()
        {
            var groupedQueues = _customers.GroupBy(c => c.Queue.Name);
            return groupedQueues;
        }

        public Dictionary<string, Queue<Customer>> GetQueues()
        {
            var groupedQueues = GetGroupQueues();
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
            var nextCustomers = GetQueues();
            //List<Task> tasks = new List<Task>();
            List<Customer> tasks = new List<Customer>();

            foreach (var entry in nextCustomers)
            {
                Console.WriteLine($"Key: {entry.Key}, Value: {entry.Value}");
                foreach (var customer in entry.Value)
                {
                    tasks.Add(customer);
                    await RemoveNextCustomer(customer);

                    //tasks.Add(RemoveNextCustomer(customer));

                    Console.WriteLine($"Se ejecutará: {customer.ToString()}");
                    break;
                }
            }

            //await Task.WhenAll(tasks);
        }

        public async Task RemoveNextCustomer(Customer customer)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(customer.Queue.Duration));
            await RemoveCustomer(customer);
            Console.WriteLine($"Eliminado: {customer.ToString()}");
        }
    }
}
