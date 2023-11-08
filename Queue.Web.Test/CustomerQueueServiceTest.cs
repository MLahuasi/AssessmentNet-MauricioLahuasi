using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Queue.Web.Models;
using Queue.Web.Models.Entity;
using Queue.Web.Services;
using System.Net;

namespace Queue.Web.Test
{
    public class CustomerQueueServiceTest
    {
        private CustomerQueue _queue;
        private Customer _customer;

        /// <summary>
        /// Crear Mock DbContext
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private IServiceProvider CreateContext(string name)
        {
            var services = new ServiceCollection();
            services.AddDbContext<AppDbContext> ( options => options.UseInMemoryDatabase(databaseName:name), ServiceLifetime.Scoped, ServiceLifetime.Scoped);
            return services.BuildServiceProvider();
        }

        [SetUp]
        public void Setup()
        {
            _queue = new CustomerQueue()
            {
                Name = "Cola#3",
                Duration = 5000
            };
            _customer = new Customer("123456", "Juan")
            {
                StartDate = DateTime.Now,
                FinishDate = null,
                Status = Queue.Web.Transacctions.Action.NoAttend
            };
        }


        //Servicio
        [Test]
        public async Task  When_Add_New_Queue_Service_Test()
        {
            //Arrage
            var dbName = Guid.NewGuid().ToString();
            var serviceProvider = CreateContext(dbName);
            var db = serviceProvider.GetRequiredService<AppDbContext>();
            db.Add(_queue);

            //Act
            CustomerQueueService srv = new CustomerQueueService(db);
            var responseServices = await srv.Add(_queue);

            //Assert
            Assert.AreEqual(true, responseServices);
        }
    }
}