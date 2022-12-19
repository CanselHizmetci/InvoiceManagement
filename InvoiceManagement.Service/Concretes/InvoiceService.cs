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
    public class InvoiceService : IInvoiceService
    {
        private readonly IRepository<Invoice> _repository;
        private readonly IMapper _mapper;

        public InvoiceService(IRepository<Invoice> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<InvoiceDTO> GetById(int id)
        {
            return _mapper.Map<InvoiceDTO>(await _repository.GetById(id));
        }

        public async Task<ICollection<InvoiceDTO>> Get()
        {
            var invoiceList = _mapper.Map<IList<InvoiceDTO>>(await (await _repository.Get()).ToListAsync());
            return invoiceList;
        }

        public async Task Add(InvoiceDTO invoice)
        {
            await _repository.Add(_mapper.Map<Invoice>(invoice));
        }

        public async Task Delete(int id)
        {
            await _repository.Remove(id);
        }

        public async Task Update(int id, InvoiceDTO invoice)
        {
            await _repository.Update(id, _mapper.Map<Invoice>(invoice));
        }
    }
}
