using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using InvoiceManagement.Domain.Entities;
using InvoiceManagement.Service.Abstracts;
using InvoiceManagement.Service.Concretes;
using InvoiceManagement.Service.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace InvoiceManagement.Controllers
{
    [Authorize(Roles = nameof(Data.Enums.Roles.User))]
    public class UserPaymentController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPaymentService _paymentService;
        private readonly IDebtService _debtService;

        public UserPaymentController(IPaymentService paymentService, UserManager<ApplicationUser> userManager, IDebtService debtService)
        {
            _paymentService = paymentService;
            _userManager = userManager;
            _debtService = debtService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await GetCurrentUser();
            View((await _paymentService.Get()).Where(c => c.UserId==user.Id));
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Pay(PayModel data)
        {
            var user = await GetCurrentUser();
            var debt = await _debtService.GetById(data.DebtId);
            using var client = new HttpClient();
            var responseTask = await client.PostAsync(new Uri("https://localhost:44352/api/Payment"),new StringContent(JsonConvert.SerializeObject(new
            {
                fullName= data.FullName,
                creditCardNo= data.CreditCardNo,
                cvv= data.Cvv,
                expirationMonth= data.ExpirationMonth,
                expirationYear= data.ExpirationYear,
                amount= debt.Amount
            }),Encoding.UTF8,"application/json"));
            if (responseTask.Content.Headers.ContentType.MediaType == "application/json")
            {
                var result = JsonConvert.DeserializeObject<PayResult>(await responseTask.Content.ReadAsStringAsync());
                if (string.IsNullOrEmpty(result.CreditCardNo))
                    return RedirectToAction("Payment", new { data.DebtId, result.ErrorMessage });
                await _paymentService.Add(new PaymentDTO
                {
                    CreditCardNo = result.CreditCardNo,
                    UserId = user.Id,
                    ErrorMessage = result.ErrorMessage,
                    PaymentDate = DateTime.Now,
                    PaymentStatus = result.Result,
                    DebtId = data.DebtId
                });
                if(result.Result)
                {
                    debt.IsPaid = true;
                    await _debtService.Update(data.DebtId, debt);
                }
                return result.Result
                    ?  RedirectToAction("Index")
                    : RedirectToAction("Payment",new{data.DebtId, result.ErrorMessage});
            }

            return await Payment(data.DebtId, "Unexpected error occured");
        }
        public async Task<IActionResult> Payment(int debtId, string ErrorMessage="")
        {
            ViewBag.DebtId = debtId;
            ViewBag.ErrorMessage = ErrorMessage;
            var debt = await _debtService.GetById(debtId);
            if(debt==null)
                return NotFound();
            ViewBag.DebtInfo = $"{debt.Apartment.Block.Title} No {debt.Apartment.ApartmentNumber} ({debt.Title})";
            ViewBag.Amount = debt.Amount.ToString("N2");
            return View();
        }
        private async Task<ApplicationUser> GetCurrentUser()
        {
            return await _userManager.FindByNameAsync(User.Identity?.Name ?? "");
        }
        public class PayModel
        {
            public string CreditCardNo { get; set; }
            public int DebtId { get; set; }
            public string FullName { get; set; } = null!;
            public int Cvv { get; set; }
            public int ExpirationMonth { get; set; }
            public int ExpirationYear { get; set; }
        }

        public class PayResult
        {
           public string ErrorMessage { get; set; }
            public bool Result { get; set; }
            public string CreditCardNo { get; set; }
        }
    }
}
