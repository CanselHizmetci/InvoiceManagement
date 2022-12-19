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
    public class ApartmentService:IApartmentService
    {
        private readonly IRepository<Apartment> _repository;
        private readonly  IMapper _mapper;

        public ApartmentService(IMapper mapper, IRepository<Apartment> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<ApartmentDTO> GetById(int id)
        {
            return _mapper.Map<ApartmentDTO>(await _repository.GetById(id));
        }

        public async Task<ICollection<ApartmentDTO>> Get()
        {
            var apartmentList = await (await _repository.Get()).ToListAsync();
            return  _mapper.Map<IList<ApartmentDTO>>(apartmentList);
        }

        public async Task Add(ApartmentDTO apartment)
        {
            await _repository.Add(_mapper.Map<Apartment> (apartment));
        }

        public async Task Delete(int id)
        {
            await _repository.Remove(id);
        }

        public async Task Update(int id, ApartmentDTO apartment)
        {
            await _repository.Update(id, _mapper.Map<Apartment>(apartment));
        }
    }
}
