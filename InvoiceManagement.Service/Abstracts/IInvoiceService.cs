using InvoiceManagement.Service.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InvoiceManagement.Service.Abstracts
{
    public interface IInvoiceService
    {
        Task<InvoiceDTO?> GetById(int id);
        Task<ICollection<InvoiceDTO>> Get();
        Task Add(InvoiceDTO dto); 
        Task Delete(int id);
        Task Update(int id, InvoiceDTO dto);
    }
}
