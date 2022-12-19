using InvoiceManagement.Service.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InvoiceManagement.Service.Abstracts
{
    public interface IPaymentService
    {
        Task<PaymentDTO?> GetById(int id); 
        Task<ICollection<PaymentDTO>> Get();
        Task Add(PaymentDTO dto); 
        Task Delete(int id); 
        Task Update(int id, PaymentDTO dto);
    }
}
