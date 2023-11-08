using Microsoft.AspNetCore.Mvc;
using Queue.Web.Transacctions;

namespace Queue.Web.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ClientAdvisor _queueService;

        public CustomerController(ClientAdvisor queueService)
        {
            _queueService = queueService;
        }

        public IActionResult Index()
        {

            return View();
        }

        /** Se usa _queueService para invocar a la cola **/
    }
}
