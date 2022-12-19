using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using InvoiceManagement.Domain.Entities;
using InvoiceManagement.Service.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceManagement.Controllers
{
    [Authorize(Roles = nameof(Data.Enums.Roles.User))]
    public class UserHomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserHomeController( UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await GetCurrentUser();
            var apartmentList= user.Apartments.Where(c=>!c.IsDeleted).Select(c=>new UserHomeViewModel
            {
                ApartmentId = c.Id,
                Block=c.Block.Title,
                ApartmentNumber = c.ApartmentNumber,
                TotalDebt=c.Debts.Where(d=>!d.IsPaid&&!d.IsDeleted).Sum(d=>d.Amount)
            });

            return View(apartmentList);
        }

        private async Task<ApplicationUser> GetCurrentUser()
        {
            return await _userManager.FindByNameAsync(User.Identity?.Name??"");
        }

        public class UserHomeViewModel
        {
            public int ApartmentId { get; set; }
            public string Block { get; set; }
            public decimal TotalDebt { get; set; }
            public int ApartmentNumber { get; set; }
        }
    }
}
