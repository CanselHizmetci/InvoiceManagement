using InvoiceManagement.Service.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InvoiceManagement.Service.Abstracts
{
    public interface IInvoiceTypeService
    {
        Task<InvoiceTypeDTO?> GetById(int id);
        Task<ICollection<InvoiceTypeDTO>> Get();
        Task Delete(int id);
        Task Update(int id, InvoiceTypeDTO dto);
        Task Add(InvoiceTypeDTO dto);
    }
}
