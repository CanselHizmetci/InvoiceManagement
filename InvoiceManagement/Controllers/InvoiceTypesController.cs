using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InvoiceManagement.Service.Abstracts;
using InvoiceManagement.Service.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace InvoiceManagement.Controllers
{
    [Authorize(Roles = nameof(Data.Enums.Roles.Admin))]

    public class InvoiceTypesController : Controller
    {
        private readonly IInvoiceTypeService _service;

        public InvoiceTypesController(IInvoiceTypeService service)
        {
            _service = service;
        }

        // GET: ApartmentTypes
        public async Task<IActionResult> Index()
        {
            return View(await _service.Get());
        }

        // GET: ApartmentTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ApartmentTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( InvoiceTypeDTO invoiceType)
        {
            if (ModelState.IsValid)
            {
                await _service.Add(invoiceType);
                return RedirectToAction(nameof(Index));
            }
            return View(invoiceType);
        }

        // GET: ApartmentTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceType = await _service.GetById(id.Value);
            if (invoiceType == null)
            {
                return NotFound();
            }
            return View(invoiceType);
        }

        // POST: ApartmentTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, InvoiceTypeDTO invoiceType)
        {
            if (id != invoiceType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                    await _service.Update(id,invoiceType);
                    return RedirectToAction(nameof(Index));
            }
            return View(invoiceType);
        }

        // GET: ApartmentTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceType = await _service.GetById(id.Value);
            if (invoiceType == null)
            {
                return NotFound();
            }

            return View(invoiceType);
        }

        // POST: ApartmentTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
