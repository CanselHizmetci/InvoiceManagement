using InvoiceManagement.Service.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InvoiceManagement.Service.Abstracts
{
    public interface IMessageService
    {
        Task<MessageDTO?> GetById(int id);
        Task<ICollection<MessageDTO>> Get(); 
        Task Add(MessageDTO dto);
        Task Delete(int id); 
        Task Update(int id, MessageDTO dto);
    }
}
