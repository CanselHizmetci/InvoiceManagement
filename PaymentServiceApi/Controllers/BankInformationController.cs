using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentServiceApi.Services;
using System.Collections.Generic;
using PaymentServiceApi.Models;

namespace PaymentServiceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankInformationController : ControllerBase
    {
        private readonly BankInformationService _bankInformationService;

        public BankInformationController(BankInformationService bankInformationService)
        {
            _bankInformationService = bankInformationService;
        }
        [HttpGet]
        public ActionResult<List<BankInformation>> Get() =>
            _bankInformationService.Get();

        [HttpGet("{id:length(24)}", Name = "GetBankInformation")]
        public ActionResult<BankInformation> Get(string id)
        {
            var bankInfo = _bankInformationService.Get(id);

            if (bankInfo == null)
            {
                return NotFound();
            }

            return bankInfo;
        }
        [HttpPost]
        public ActionResult<BankInformation> Create(BankInformation bankInformation)
        {
            _bankInformationService.Create(bankInformation);

            return CreatedAtRoute("GetBankInformation", new { id = bankInformation.Id }, bankInformation);
        }
        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, BankInformation bankIn)
        {
            var bankInfo = _bankInformationService.Get(id);

            if (bankInfo == null)
            {
                return NotFound();
            }

            _bankInformationService.Update(id, bankIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var bankInfo = _bankInformationService.Get(id);

            if (bankInfo == null)
            {
                return NotFound();
            }

            _bankInformationService.Remove(id);

            return NoContent();
        }

    }
}
