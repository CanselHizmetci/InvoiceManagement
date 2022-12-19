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
    public class MessageService:IMessageService
    {
        private readonly IRepository<Message> _repository;
        private readonly IMapper _mapper;

        public MessageService(IRepository<Message> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<MessageDTO> GetById(int id)
        {
            return _mapper.Map<MessageDTO>(await _repository.GetById(id));
        }

        public async Task<ICollection<MessageDTO>> Get()
        {
            var MessageList = _mapper.Map<IList<MessageDTO>>(await (await _repository.Get()).ToListAsync());
            return MessageList;
        }

        public async Task Add(MessageDTO message)
        {
            await _repository.Add(_mapper.Map<Message>(message));
        }

        public async Task Delete(int id)
        {
            await _repository.Remove(id);
        }

        public async Task Update(int id, MessageDTO message)
        {
            await _repository.Update(id, _mapper.Map<Message>(message));
        }
    }
}
