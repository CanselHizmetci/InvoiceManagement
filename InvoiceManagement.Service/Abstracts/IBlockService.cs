using InvoiceManagement.Service.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InvoiceManagement.Service.Abstracts
{
    public interface IBlockService
    {
        Task<BlockDTO?> GetById(int id);
        Task<ICollection<BlockDTO>> Get();
        Task Add(BlockDTO dto);
        Task Delete(int id);
        Task Update(int id, BlockDTO dto);
    }
}
