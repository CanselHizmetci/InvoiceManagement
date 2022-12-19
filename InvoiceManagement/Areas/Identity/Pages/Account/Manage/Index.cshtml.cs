using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using InvoiceManagement.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InvoiceManagement.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }
        [TempData]
        public string UserNameChangeLimitMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "First Name")]
            public string Name { get; set; }
            [Required]
            [Display(Name = "Last Name")]
            public string Surname { get; set; }
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }
            [Required]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            [Required]
            [Display(Name = "Have a vehicle")]
            public bool HaveAVehicle { get; set; }

            [Display(Name = "License plate")]
            public string LicensePlate { get; set; }
            

        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var name = user.Name;
            var surname = user.Surname;
            var licensePlate = user.LicensePlate;
            var haveAVehicle = user.HaveAVehicle;
            

            Input = new InputModel
            {
             HaveAVehicle   = haveAVehicle,
             LicensePlate = licensePlate,
             PhoneNumber = phoneNumber,
             Email = email,
             Name = name,
             Surname = surname
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }
            var email = await _userManager.GetEmailAsync(user);
            if (Input.Email != email)
            {
                var setEmailResult = await _userManager.SetEmailAsync(user, Input.Email);
                if (!setEmailResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set email adress.";
                    return RedirectToPage();
                }
            }
            var name = user.Name;
            var surname = user.Surname;
            var licensePlate = user.LicensePlate;
            var haveAVehicle = user.HaveAVehicle;
            if (Input.Name != name)
            {

                user.Name = Input.Name;
                await _userManager.UpdateAsync(user);
            }
            if (Input.Surname != surname)
            {
                user.Surname = Input.Surname;
                await _userManager.UpdateAsync(user);
            }
            if (Input.HaveAVehicle != haveAVehicle)
            {
                user.HaveAVehicle = Input.HaveAVehicle;
                await _userManager.UpdateAsync(user);
            }
            if (Input.LicensePlate != licensePlate)
            {
                user.LicensePlate = haveAVehicle? Input.LicensePlate:"";
                await _userManager.UpdateAsync(user);
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
