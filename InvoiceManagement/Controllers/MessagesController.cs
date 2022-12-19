using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InvoiceManagement.Service.Abstracts;
using InvoiceManagement.Service.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace InvoiceManagement.Controllers
{
    [Authorize(Roles = nameof(Data.Enums.Roles.Admin))]

    public class MessagesController : Controller
    {
        private readonly IMessageService _service;

        public MessagesController(IMessageService service)
        {
            _service = service;
        }

        // GET: Messages
        public async Task<IActionResult> Index()
        {
            return View(await _service.Get());
        }
        

        // GET: Messages/Create
        public IActionResult Send(string SenderId)
        {
            return View();
        }

        // POST: Messages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Send(MessageDTO message)
        {
            message.IsOutgoing = false;
            message.SendDate=DateTime.Now;
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
            var message=await _service.GetById(Id);
            message.IsReaded = true;
            await _service.Update(message.Id, message);
            return RedirectToAction(nameof(Index));
        }
        
    }
}
