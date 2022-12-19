using InvoiceManagement.Service.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using System.Linq;
using InvoiceManagement.Domain.Entities;
using InvoiceManagement.Service.Abstracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace InvoiceManagement.Controllers
{
    [Authorize(Roles = nameof(Data.Enums.Roles.User))]
    public class UserMessagesController : Controller
    {
        private readonly IMessageService _service;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserMessagesController(IMessageService service, UserManager<ApplicationUser> userManager)
        {
            _service = service;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var currentUser = await GetCurrentUser();
            return View((await _service.Get()).Where(c=>c.SenderId==currentUser.Id));

        }
        public IActionResult Send(string SenderId)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Send(MessageDTO message)
        {
            var user = await _userManager.FindByNameAsync(User.Identity?.Name ?? "");
            message.IsOutgoing = true;
            message.SenderId = user.Id; 
            message.SendDate = DateTime.Now;
            message.IsReaded = false;
            if (ModelState.IsValid)
            {
                await _service.Add(message);
                return RedirectToAction(nameof(Index));
            }
            return View(message);
        }

        public async Task<IActionResult> IsRead(int Id)
        {
            var message = await _service.GetById(Id);
            message.IsReaded = true;
            await _service.Update(message.Id, message);
            return RedirectToAction(nameof(Index));
        }
        private async Task<ApplicationUser> GetCurrentUser()
        {
            return await _userManager.FindByNameAsync(User.Identity?.Name ?? "");
        }
    }
}
