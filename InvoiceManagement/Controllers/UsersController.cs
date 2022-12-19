using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InvoiceManagement.Data.Context;
using InvoiceManagement.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;

namespace InvoiceManagement.Controllers
{
    [Authorize(Roles = nameof(Data.Enums.Roles.Admin))]

    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public UsersController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            ViewData["UserManager"] = _userManager;
            return View(await _userManager.Users.Where(c=>!c.IsDeleted).ToListAsync());

        }

        // GET: Users/Create
        public async Task<IActionResult> Create()
        {
            ViewData["RoleList"] = new MultiSelectList(await _roleManager.Roles.ToListAsync(), "Name", "Name");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ApplicationUser user, IList<string> Roles)
        {
            user.UserName = user.Email;
            ViewData["RoleList"] = new MultiSelectList(await _roleManager.Roles.ToListAsync(), "Name", "Name");
            if (ModelState.IsValid)
            {
                var result=await _userManager.CreateAsync(user, _configuration["IdentityServerPasswords:NewUserPassword"]);
                await _userManager.AddToRolesAsync(user, Roles);
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(string id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null||user.IsDeleted)
            {
                return NotFound();
            }

            ViewData["RoleList"] = new MultiSelectList(await _roleManager.Roles.ToListAsync(), "Name", "Name", await _userManager.GetRolesAsync(user));
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, ApplicationUser user, IList<string> Roles)
        {
            user.UserName = user.Email;

            if (id != user.Id)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                ViewData["RoleList"] = new MultiSelectList(await _roleManager.Roles.ToListAsync(), "Name", "Name", await _userManager.GetRolesAsync(user));
                var currentUser = await _userManager.FindByIdAsync(id);
                if (currentUser != null&&!currentUser.IsDeleted)
                {
                    await TryUpdateModelAsync(currentUser);
                    await _userManager.RemoveFromRolesAsync(currentUser, await _userManager.GetRolesAsync(currentUser));
                    await _userManager.AddToRolesAsync(currentUser, Roles);
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null||user.IsDeleted)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            user.IsDeleted = true;
            await _userManager.UpdateAsync(user);
            return RedirectToAction(nameof(Index));
        }
    }
}
