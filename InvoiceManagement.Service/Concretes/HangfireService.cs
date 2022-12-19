using Hangfire;
using InvoiceManagement.Service.Abstracts;

namespace InvoiceManagement.Service.Concretes
{
    public class HangfireService:IHangfireService
    {
        private readonly DebtReminderService _debtReminder;

        public HangfireService(DebtReminderService debtReminder)
        {
            _debtReminder = debtReminder;
        }
        
        public void Run()
        {
            RecurringJob.AddOrUpdate("SendDebtReminderMail", () => _debtReminder.SendDebtReminderMail(), Cron.Daily);
        }
    }
}
