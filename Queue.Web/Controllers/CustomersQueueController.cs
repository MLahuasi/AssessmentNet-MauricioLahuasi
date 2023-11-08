using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Queue.Web.Models;
using Queue.Web.Models.Entity;

namespace Queue.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersQueueController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public CustomersQueueController(AppDbContext context)
        {
            _dbContext = context;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {

            //var lista = 
            //    _dbContext.CustomersQueue
            //    .OrderByDescending(x => x.Id)
            //    .ThenBy(t => t.Name)
            //    .ToList();

            var lista = await _dbContext.CustomersQueue
               .OrderBy(cq => cq.Duration)
               .ToListAsync();

            return StatusCode(StatusCodes.Status200OK, lista);
        }

        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar([FromBody] CustomerQueue request)
        {
            await _dbContext.CustomersQueue.AddAsync(request);
            await _dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, "ok");
        }

        [HttpDelete]
        [Route("Cerrar/{id:int}")]
        public async Task<IActionResult> Cerrar(int id)
        {
            CustomerQueue queue = _dbContext.CustomersQueue.Find(id);
            _dbContext.CustomersQueue.Remove(queue);
            await _dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, "ok");
        }
    }
}
