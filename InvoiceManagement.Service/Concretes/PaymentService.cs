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
    public class PaymentService:IPaymentService
    {
        private readonly IRepository<Payment> _repository;
        private readonly IMapper _mapper;

        public PaymentService(IRepository<Payment> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<PaymentDTO> GetById(int id)
        {
            return _mapper.Map<PaymentDTO>(await _repository.GetById(id));
        }

        public async Task<ICollection<PaymentDTO>> Get()
        {
            var PaymentList = _mapper.Map<IList<PaymentDTO>>(await (await _repository.Get()).ToListAsync());
            return PaymentList;
        }

        public async Task Add(PaymentDTO payment)
        {
            await _repository.Add(_mapper.Map<Payment>(payment));
        }

        public async Task Delete(int id)
        {
            await _repository.Remove(id);
        }

        public async Task Update(int id, PaymentDTO payment)
        {
            await _repository.Update(id, _mapper.Map<Payment>(payment));
        }
    }
}
