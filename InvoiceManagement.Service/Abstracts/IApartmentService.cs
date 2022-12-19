using InvoiceManagement.Service.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InvoiceManagement.Service.Abstracts
{
    public interface IApartmentService
    {
        Task<ApartmentDTO?> GetById(int id);
        Task<ICollection<ApartmentDTO>> Get();
        Task Add(ApartmentDTO dto);
        Task Delete(int id);
        Task Update(int id, ApartmentDTO dto);
    }
}
