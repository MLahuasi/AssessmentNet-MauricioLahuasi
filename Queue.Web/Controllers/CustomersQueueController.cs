using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Queue.Web.Models;
using Queue.Web.Models.Entity;
using Queue.Web.Models.Entity.DTOs;
using Queue.Web.Services;

namespace Queue.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersQueueController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly CustomerServices _srv;

        public CustomersQueueController(AppDbContext context)
        {
            _dbContext = context;
            _srv = new CustomerServices(_dbContext);
        }

        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> List()
        {

            var lista = await _dbContext.CustomersQueue
               .Include(cq => cq.Customers)
               .OrderBy(cq => cq.Duration)
               .ToListAsync();

            return StatusCode(StatusCodes.Status200OK, lista);
        }

        [HttpPost]
        [Route("Save")]
        public async Task<IActionResult> Save([FromBody] CustomerQueue request)
        {
            await _dbContext.CustomersQueue.AddAsync(request);
            await _dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, "ok");
        }

        [HttpDelete]
        [Route("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            CustomerQueue queue = _dbContext.CustomersQueue.Find(id);
            _dbContext.CustomersQueue.Remove(queue);
            await _dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, "ok");
        }

        [HttpPost]
        [Route("SaveCustomer")]
        public async Task<IActionResult> SaveCustomer([FromBody] AddCustomer request)
        {
            if( string.IsNullOrEmpty(request.Ci) || string.IsNullOrEmpty(request.Name))
                return StatusCode(StatusCodes.Status400BadRequest, "error");

            var result = await _srv.AddCusomer(request.Ci, request.Name);
            if(result)
                return StatusCode(StatusCodes.Status200OK, "ok");
            else
                return StatusCode(StatusCodes.Status400BadRequest, "error");
        }

        
    }
}
