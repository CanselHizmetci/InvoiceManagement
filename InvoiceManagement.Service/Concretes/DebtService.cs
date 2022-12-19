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
    public class DebtService:IDebtService
    {
        private readonly IRepository<Debt> _repository;
        private readonly IMapper _mapper;

        public DebtService(IRepository<Debt> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<DebtDTO> GetById(int id)
        {
            return _mapper.Map<DebtDTO>(await _repository.GetById(id));
        }

        public async Task<ICollection<DebtDTO>> Get()
        {
            var DebtList = _mapper.Map<IList<DebtDTO>>(await (await _repository.Get()).ToListAsync());
            return DebtList;
        }

        public async Task Add(DebtDTO debt)
        {
            await _repository.Add(_mapper.Map<Debt>(debt));
        }

        public async Task Delete(int id)
        {
            await _repository.Remove(id);
        }

        public async Task Update(int id, DebtDTO debt)
        {
            await _repository.Update(id, _mapper.Map<Debt>(debt));
        }
    }
}
