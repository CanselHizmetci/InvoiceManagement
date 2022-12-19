using Hangfire;

namespace InvoiceManagement.Service.Abstracts
{
    public interface IHangfireService
    {
        [JobDisplayName("That sends daily mail when no payment is made job")]
        void Run();
    }
}
