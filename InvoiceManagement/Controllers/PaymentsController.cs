using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InvoiceManagement.Service.Abstracts;
using Microsoft.AspNetCore.Authorization;

namespace InvoiceManagement.Controllers
{
    [Authorize(Roles = nameof(Data.Enums.Roles.Admin))]

    public class PaymentsController : Controller
    {
        private readonly IPaymentService _service;

        public PaymentsController(IPaymentService service)
        {
            _service = service;
        }

        // GET: Payments
        public async Task<IActionResult> Index()
        {
            return View(await _service.Get());
        }
        
        
    }
}
