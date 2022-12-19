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
    public class ApartmentTypeService:IApartmentTypeService
    {
        private readonly IRepository<ApartmentType> _repository;
        private readonly IMapper _mapper;

        public ApartmentTypeService(IRepository<ApartmentType> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApartmentTypeDTO> GetById(int id)
        {
            return _mapper.Map<ApartmentTypeDTO>(await _repository.GetById(id));
        }

        public async Task<ICollection<ApartmentTypeDTO>> Get()
        {
            var apartmentTypeList = _mapper.Map<IList<ApartmentTypeDTO>>(await (await _repository.Get()).ToListAsync());
            return apartmentTypeList;
        }
        

        public async Task Add(ApartmentTypeDTO apartmentType)
        {
            await _repository.Add(_mapper.Map<ApartmentType>(apartmentType));
        }

        public async Task Delete(int id)
        {
            await _repository.Remove(id);
        }

        public async Task Update(int id, ApartmentTypeDTO apartmentType)
        {
            await _repository.Update(id, _mapper.Map<ApartmentType>(apartmentType));
        }
        
    }
}
