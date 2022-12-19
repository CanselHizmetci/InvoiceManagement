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
    public class UserService:IUserService
    {
        private readonly IRepository<User> _repository;
        private readonly IMapper _mapper;

        public UserService(IRepository<User> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<UserDTO> GetById(int id)
        {
            return _mapper.Map<UserDTO>(await _repository.GetById(id));
        }

        public async Task<ICollection<UserDTO>> Get()
        {
            var UserList = _mapper.Map<IList<UserDTO>>(await (await _repository.Get()).ToListAsync());
            return UserList;
        }

        public async Task Add(UserDTO user)
        {
            await _repository.Add(_mapper.Map<User>(user));
        }

        public async Task Delete(int id)
        {
            await _repository.Remove(id);
        }

        public async Task Update(int id, UserDTO user)
        {
            await _repository.Update(id, _mapper.Map<User>(user));
        }
    }
}
