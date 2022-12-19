using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using InvoiceManagement.Service.Abstracts;
using InvoiceManagement.Service.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace InvoiceManagement.Controllers
{
    [Authorize(Roles = nameof(Data.Enums.Roles.Admin))]

    public class InvoicesController : Controller
    {
        private readonly IInvoiceService _service;
        private readonly IInvoiceTypeService _typeService;

        public InvoicesController(IInvoiceService service, IInvoiceTypeService typeService)
        {
            _service = service;
            _typeService = typeService;
        }

        // GET: Invoices
        public async Task<IActionResult> Index()
        {
            return View(await _service.Get());
        }

        // GET: Invoices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _service.GetById(id.Value);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // GET: Invoices/Create
        public async Task<IActionResult> Create()
        {
            ViewData["TypeList"] = new SelectList((await _typeService.Get()), "Id", "Title");
            return View();
        }

        // POST: Invoices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InvoiceDTO invoice)
        {
            if (ModelState.IsValid)
            {
                await _service.Add(invoice);
                return RedirectToAction(nameof(Index));
            }
            ViewData["TypeList"] = new SelectList((await _typeService.Get()), "Id", "Title"); 
            return View(invoice);
        }

        // GET: Invoices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _service.GetById(id.Value);
            if (invoice == null)
            {
                return NotFound();
            }
            ViewData["TypeList"] = new SelectList((await _typeService.Get()), "Id", "Title", invoice.InvoiceTypeId);
            return View(invoice);
        }

        // POST: Invoices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, InvoiceDTO invoice)
        {
            if (id != invoice.Id)
            {
                return NotFound();
            }
            ViewData["TypeList"] = new SelectList((await _typeService.Get()), "Id", "Title",invoice.InvoiceTypeId);

            if (ModelState.IsValid)
            {
                await _service.Update(id, invoice);
                return RedirectToAction(nameof(Index));
            }
            return View(invoice);
        }

        // GET: Invoices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _service.GetById(id.Value);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.Delete(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
