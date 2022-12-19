using InvoiceManagement.Service.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InvoiceManagement.Service.Abstracts
{
    public interface IUserService
    {
        Task<UserDTO?> GetById(int id); 
        Task<ICollection<UserDTO>> Get();
        Task Add(UserDTO dto);
        Task Delete(int id);
        Task Update(int id, UserDTO dto);
    }
}
