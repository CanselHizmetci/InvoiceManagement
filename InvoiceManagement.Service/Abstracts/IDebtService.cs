using InvoiceManagement.Service.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InvoiceManagement.Service.Abstracts
{
    public interface IDebtService
    {
        Task<DebtDTO?> GetById(int id);
        Task<ICollection<DebtDTO>> Get();
        Task Add(DebtDTO dto);
        Task Delete(int id);
        Task Update(int id, DebtDTO dto);
    }
}
