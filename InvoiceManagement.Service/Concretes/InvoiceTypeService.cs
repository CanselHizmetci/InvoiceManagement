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
    public class InvoiceTypeService : IInvoiceTypeService
    {
        private readonly IRepository<InvoiceType> _repository;
        private readonly IMapper _mapper;

        public InvoiceTypeService(IRepository<InvoiceType> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<InvoiceTypeDTO> GetById(int id)
        {
            return _mapper.Map<InvoiceTypeDTO>(await _repository.GetById(id));
        }

        public async Task<ICollection<InvoiceTypeDTO>> Get()
        {
            var invoiceTypeList = _mapper.Map<IList<InvoiceTypeDTO>>(await (await _repository.Get()).ToListAsync());
            return invoiceTypeList;
        }
        

        public async Task Add(InvoiceTypeDTO invoiceType)
        {
            await _repository.Add(_mapper.Map<InvoiceType>(invoiceType));
        }

        public async Task Delete(int id)
        {
            await _repository.Remove(id);
        }

        public async Task Update(int id, InvoiceTypeDTO invoiceType)
        {
            await _repository.Update(id, _mapper.Map<InvoiceType>(invoiceType));
        }
        
    }
}
