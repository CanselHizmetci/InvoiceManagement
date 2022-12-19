using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentServiceApi.Models;
using PaymentServiceApi.Services;

namespace PaymentServiceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly PaymentService _paymentService;
        private readonly BankInformationService _bankInformationService;

        public PaymentController(PaymentService paymentService, BankInformationService bankInformationService)
        {
            _paymentService = paymentService;
            _bankInformationService = bankInformationService;
        }
        [HttpPost]
        public ActionResult<Payment> Create(PayModel pay)
        {
            var bankInfo = _bankInformationService.Get()
                .FirstOrDefault(c => c.CreditCardNo == pay.CreditCardNo && c.FullName == pay.FullName);

            if (bankInfo == null)
            {
                return BadRequest(new PaymentResultModel() { ErrorMessage = "Geçersiz kredi kartı", Result = false });
            }
            var resCc = "";
            for (var i = 0; i < 16; i++)
                resCc += i is >= 4 and < 12 ? '*' : pay.CreditCardNo[i];
            if (bankInfo.ExpirationMonth != pay.ExpirationMonth || bankInfo.ExpirationYear != pay.ExpirationYear)
            {
                return BadRequest(new PaymentResultModel()
                    { ErrorMessage = "Geçersiz son kullanma tarihi", Result = false , CreditCardNo = resCc});
            }

            var expDate = new DateTime(2000 + pay.ExpirationYear, pay.ExpirationMonth, 1).AddMonths(1);
            if (expDate < DateTime.Now)
            {
                return BadRequest(new PaymentResultModel()
                    { ErrorMessage = "kartınızın son kullanma tarihi geçmiş", Result = false, CreditCardNo = resCc });
            }

            if (pay.Cvv != bankInfo.Cvv)
            {
                return BadRequest(new PaymentResultModel()
                    { ErrorMessage = "Geçersiz Cvv girdiniz", Result = false, CreditCardNo = resCc });
            }

            if (pay.Amount > bankInfo.Balance)
            {
                return BadRequest(new PaymentResultModel()
                    { ErrorMessage = "Bakiye yetersiz", Result = false, CreditCardNo = resCc });
            }

            _paymentService.Create(new Payment
            {
                CreditCardNo = pay.CreditCardNo,
                Amount = pay.Amount,
                Balance = bankInfo.Balance - pay.Amount,
                FullName = pay.FullName,
                TransactionDate = DateTime.Now

            });
            bankInfo.Balance = bankInfo.Balance - pay.Amount;
            _bankInformationService.Update(bankInfo.Id,bankInfo);

            return Ok(new PaymentResultModel()
                {Result = true, CreditCardNo = resCc });
            //_paymentService.Create(payment);

            //return CreatedAtRoute("GetBankInformation", new { id = payment.Id }, payment);
        }
    }
}
