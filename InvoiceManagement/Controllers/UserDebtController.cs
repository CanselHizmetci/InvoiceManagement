using System.Linq;
using System.Threading.Tasks;
using InvoiceManagement.Domain.Entities;
using InvoiceManagement.Service.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceManagement.Controllers
{
    [Authorize(Roles = "User")]
    public class UserDebtController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IDebtService _debtService;

        public UserDebtController(UserManager<ApplicationUser> userManager, IDebtService debtService)
        {
            _userManager = userManager;
            _debtService = debtService;
        }

        public async Task<IActionResult> Index(int? ApartmentId)
        {
            var user = await GetCurrentUser();
            if (ApartmentId is not null)
                return View((await _debtService.Get()).Where(c => c.ApartmentId == ApartmentId &&
                    user.Apartments.Select(a => a.Id).ToList().Contains(c.ApartmentId)));
            return View((await _debtService.Get()).Where(c =>
                user.Apartments.Select(a => a.Id).ToList().Contains(c.ApartmentId)));
        }
        private async Task<ApplicationUser> GetCurrentUser()
        {
            return await _userManager.FindByNameAsync(User.Identity?.Name ?? "");
        }
    }
}

