using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InvoiceManagement.Domain.Entities;
using InvoiceManagement.Service.Abstracts;
using InvoiceManagement.Service.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace InvoiceManagement.Controllers
{
    [Authorize(Roles = nameof(Data.Enums.Roles.Admin))]
    public class ApartmentsController : Controller
    {
        private readonly IApartmentService _service;
        private readonly IApartmentTypeService _typeService;
        private readonly IBlockService _blockService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ApartmentsController(IApartmentService service, IBlockService blockService, IApartmentTypeService typeService, UserManager<ApplicationUser> userManager)
        {
            _service = service;
            _blockService = blockService;
            _typeService = typeService;
            _userManager = userManager;
        }

        // GET: Apartments
        public async Task<IActionResult> Index()
        {
            return View(await _service.Get());
        }

        // GET: Apartments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apartment = await _service.GetById(id.Value);
            if (apartment == null)
            {
                return NotFound();
            }

            return View(apartment);
        }

        // GET: Apartments/Create
        public async Task<IActionResult> Create()
        {
            ViewData["BlockList"] = new SelectList(await _blockService.Get(), "Id", "Title");
            ViewData["TypeList"] = new SelectList(await _typeService.Get(), "Id", "Title");
            ViewData["UserList"] = new SelectList(await _userManager.Users.Select(c => new { Id = c.Id, Name = $"{c.Name} {c.Surname}" }).ToListAsync(), "Id", "Name");
            return View();
        }

        // POST: Apartments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ApartmentDTO apartment)
        {
            if (ModelState.IsValid)
            {
                ViewData["BlockList"] = new SelectList(await _blockService.Get(), "Id", "Title");
                ViewData["TypeList"] = new SelectList(await _typeService.Get(), "Id", "Title");
                ViewData["UserList"] = new SelectList(await _userManager.Users.Select(c => new { Id = c.Id, Name = $"{c.Name} {c.Surname}" }).ToListAsync(), "Id", "Name");


                await _service.Add(apartment);
                return RedirectToAction(nameof(Index));
            }
            return View(apartment);
        }

        // GET: Apartments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apartment = await _service.GetById(id.Value);
            if (apartment == null)
            {
                return NotFound();
            }
            ViewData["BlockList"] = new SelectList(await _blockService.Get(), "Id", "Title");
            ViewData["TypeList"] = new SelectList(await _typeService.Get(), "Id", "Title");
            ViewData["UserList"] = new SelectList(await _userManager.Users.Select(c => new { Id = c.Id, Name = $"{c.Name} {c.Surname}" }).ToListAsync(), "Id", "Name");
            return View(apartment);
        }

        // POST: Apartments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ApartmentDTO apartment)
        {
            if (id != apartment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                ViewData["BlockList"] = new SelectList(await _blockService.Get(), "Id", "Title");
                ViewData["TypeList"] = new SelectList(await _typeService.Get(), "Id", "Title");
                ViewData["UserList"] = new SelectList(await _userManager.Users.Select(c => new { Id = c.Id, Name = $"{c.Name} {c.Surname}" }).ToListAsync(), "Id", "Name");

                await _service.Update(id, apartment);
                return RedirectToAction(nameof(Index));
            }
            return View(apartment);
        }

        // GET: Apartments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apartment = await _service.GetById(id.Value);
            if (apartment == null)
            {
                return NotFound();
            }

           
            return View(apartment);
        }

        // POST: Apartments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        //private bool ApartmentExists(int id)
        //{
        //    return _service.Apartments.Any(e => e.Id == id);
        //}
    }
}
