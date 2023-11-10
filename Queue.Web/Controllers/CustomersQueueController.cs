using Azure.Core;
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
        private readonly CustomerQueueService _srvQueue;
        public CustomersQueueController(AppDbContext context)
        {
            _dbContext = context;
            _srv = new CustomerServices(_dbContext);
            _srvQueue = new CustomerQueueService(_dbContext);
        }

        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> List()
        {
            var lista = await _srvQueue.Get();
            return StatusCode(StatusCodes.Status200OK, lista);
        }

        [HttpPost]
        [Route("Save")]
        public async Task<IActionResult> Save([FromBody] CustomerQueue request)
        {
            if (request.Duration > 1 || string.IsNullOrEmpty(request.Name))
                return StatusCode(StatusCodes.Status400BadRequest, "error");

            var result = await _srvQueue.Add(request);
            if (result)
                return StatusCode(StatusCodes.Status200OK, "ok");
            else
                return StatusCode(StatusCodes.Status400BadRequest, "error");
        }

        [HttpDelete]
        [Route("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _srvQueue.Delete(id);

            if (result)
                return StatusCode(StatusCodes.Status200OK, "ok");
            else
                return StatusCode(StatusCodes.Status400BadRequest, "error");
        }

        [HttpPost]
        [Route("SaveCustomer")]
        public async Task<IActionResult> SaveCustomer([FromBody] AddCustomer request)
        {
            if( string.IsNullOrEmpty(request.Ci) || string.IsNullOrEmpty(request.Name))
                return StatusCode(StatusCodes.Status400BadRequest, "error");

            var result = await _srv.Add(request.Ci, request.Name);
            if(result)
                return StatusCode(StatusCodes.Status200OK, "ok");
            else
                return StatusCode(StatusCodes.Status400BadRequest, "error");
        }

        [HttpDelete]
        [Route("DeleteCustomer/{id:int}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var result = await _srv.Delete(id);
            if (result)
                return StatusCode(StatusCodes.Status200OK, "ok");
            else
                return StatusCode(StatusCodes.Status400BadRequest, "error");
        }

    }
}
