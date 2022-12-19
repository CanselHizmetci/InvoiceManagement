using InvoiceManagement.Service.Abstracts;
using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvoiceManagement.Service.DTOs;

namespace InvoiceManagement.Service.Concretes
{
    public class DebtReminderService
    {
        private readonly IDebtService _debtService;
        private readonly IEmailSender _emailSender;

        public DebtReminderService(IDebtService debtService, IEmailSender emailSender)
        {
            _debtService = debtService;
            _emailSender = emailSender;
        }

        public async Task SendDebtReminderMail()
        {
            foreach (var debt in (await _debtService.Get()).Where(c => !c.IsPaid))
            {
                await _emailSender.SendEmailAsync(debt.Apartment.User.Email, "Unpaid debt reminder",
                    $"You owe {debt.Amount} $ ({debt.Title}), please pay as soon as possible.");
            }
        }
    }
}
