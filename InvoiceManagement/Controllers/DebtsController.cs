using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using InvoiceManagement.Service.Abstracts;
using InvoiceManagement.Service.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace InvoiceManagement.Controllers
{
    [Authorize(Roles = nameof(Data.Enums.Roles.Admin))]

    public class DebtsController : Controller
    {
        private readonly IDebtService _service;
        private readonly IApartmentService _apartmentService;

        public DebtsController(IDebtService service, IApartmentService apartmentService)
        {
            _service = service;
            _apartmentService = apartmentService;
        }

        // GET: Debts
        public async Task<IActionResult> Index()
        {
            return View(await _service.Get());
        }
        
        // GET: Debts/Create
        public async Task<IActionResult> Create()
        {
            ViewData["ApartmentList"] = new SelectList((await _apartmentService.Get()).Select(c=>new{c.Id,Title=$"{c.Block.Title} No {c.ApartmentNumber}"}), "Id", "Title");

            return View();
        }

        // POST: Debts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DebtDTO debt, List<int> ApartmentList)
        {
            if (ModelState.IsValid&&ApartmentList.Count>0)
            {
                foreach (var apartmentId in ApartmentList)
                {
                    debt.ApartmentId = apartmentId;
                    await _service.Add(debt);
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApartmentList"] = new MultiSelectList((await _apartmentService.Get()).Select(c => new { c.Id, Title = $"{c.Block.Title} No {c.ApartmentNumber}" }), "Id", "Title");
            return View(debt);
        }

        // GET: Debts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var debt = await _service.GetById(id.Value);
            if (debt == null)
            {
                return NotFound();
            }
            ViewData["ApartmentList"] = new SelectList((await _apartmentService.Get()).Select(c => new { c.Id, Title = $"{c.Block.Title} No {c.ApartmentNumber}" }), "Id", "Title");
            return View(debt);
        }

        // POST: Debts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DebtDTO debt)
        {
            if (id != debt.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                ViewData["ApartmentList"] = new SelectList((await _apartmentService.Get()).Select(c => new { c.Id, Title = $"{c.Block.Title} No {c.ApartmentNumber}" }), "Id", "Title");
                await _service.Update(id, debt);
                return RedirectToAction(nameof(Index));
            }
            return View(debt);
        }

        // GET: Debts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var debt = await _service.GetById(id.Value);
            if (debt == null)
            {
                return NotFound();
            }

            return View(debt);
        }

        // POST: Debts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.Delete(id);
            return RedirectToAction(nameof(Index));
        }
        
    }
}
