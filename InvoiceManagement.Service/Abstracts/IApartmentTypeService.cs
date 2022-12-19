using InvoiceManagement.Service.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InvoiceManagement.Service.Abstracts
{
    public interface IApartmentTypeService
    {
        Task<ApartmentTypeDTO?> GetById(int id);
        Task<ICollection<ApartmentTypeDTO>> Get();
        Task Delete(int id);
        Task Update(int id, ApartmentTypeDTO dto);
        Task Add(ApartmentTypeDTO dto);
    }
}
