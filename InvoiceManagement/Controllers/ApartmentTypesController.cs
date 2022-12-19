using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InvoiceManagement.Service.Abstracts;
using InvoiceManagement.Service.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace InvoiceManagement.Controllers
{
    [Authorize(Roles = nameof(Data.Enums.Roles.Admin))]

    public class ApartmentTypesController : Controller
    {
        private readonly IApartmentTypeService _service;

        public ApartmentTypesController(IApartmentTypeService service)
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
        public async Task<IActionResult> Create(/*[Bind("Title,Id,IsDeleted,CreateDateTime,UpdateDateTime")]*/ ApartmentTypeDTO apartmentType)
        {
            if (ModelState.IsValid)
            {
                await _service.Add(apartmentType);
                return RedirectToAction(nameof(Index));
            }
            return View(apartmentType);
        }

        // GET: ApartmentTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apartmentType = await _service.GetById(id.Value);
            if (apartmentType == null)
            {
                return NotFound();
            }
            return View(apartmentType);
        }

        // POST: ApartmentTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ApartmentTypeDTO apartmentType)
        {
            if (id != apartmentType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                    await _service.Update(id,apartmentType);
                    return RedirectToAction(nameof(Index));
            }
            return View(apartmentType);
        }

        // GET: ApartmentTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apartmentType = await _service.GetById(id.Value);
            if (apartmentType == null)
            {
                return NotFound();
            }

            return View(apartmentType);
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
