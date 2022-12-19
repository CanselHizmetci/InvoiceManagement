using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using InvoiceManagement.Data.Abstract;
using InvoiceManagement.Domain.Entities;
using InvoiceManagement.Service.Abstracts;
using InvoiceManagement.Service.DTOs;
using Microsoft.EntityFrameworkCore;

namespace InvoiceManagement.Service.Concretes
{
    public class BlockService:IBlockService
    {
        private readonly IRepository<Block> _repository;
        private readonly IMapper _mapper;

        public BlockService(IRepository<Block> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BlockDTO> GetById(int id)
        {
            return _mapper.Map<BlockDTO>(await _repository.GetById(id));
        }

        public async Task<ICollection<BlockDTO>> Get()
        {
            var blockList = _mapper.Map<IList<BlockDTO>>(await (await _repository.Get()).ToListAsync());
            return blockList;
        }

        public async Task Add(BlockDTO block)
        {
            await _repository.Add(_mapper.Map<Block>(block));
        }

        public async Task Delete(int id)
        {
            await _repository.Remove(id);
        }

        public async Task Update(int id, BlockDTO block)
        {
            await _repository.Update(id, _mapper.Map<Block>(block));
        }
    }
}
