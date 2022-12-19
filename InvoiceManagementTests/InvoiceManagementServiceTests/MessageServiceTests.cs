using InvoiceManagement.Service.Abstracts;
using InvoiceManagement.Service.DTOs;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace InvoiceManagementTests.InvoiceManagementServiceTests
{
    public class MessageServiceTests
    {
        private readonly Mock<IMessageService> _messageMockService;

        public MessageServiceTests()
        {
            _messageMockService = new Mock<IMessageService>();
        }
        [Fact]
        public async void GetAll_ShouldBeReturnAListOfMessage_WhenMessagesExist()
        {
            var messageList = CreateMessageList();

            _messageMockService.Setup(c => c.Get()).ReturnsAsync(messageList);

            var result = await _messageMockService.Object.Get();

            Assert.NotNull(result);
            Assert.IsType<List<MessageDTO>>(result);
        }
        [Fact]
        public async void GetById_ShouldBeReturnAMessage_WhenMessagesExist()
        {
            var id = 1; // id 1 already exist;

            _messageMockService.Setup(c => c.GetById(id)).ReturnsAsync(GetMessageById(id));

            var result = await _messageMockService.Object.GetById(id);

            Assert.NotNull(result);
            Assert.IsType<MessageDTO>(result);
        }
        [Fact]
        public async void GetById_ShouldBeReturnNull_WhenMessageNotExist()
        {
            var id = 2; // id 2 not exist;

            _messageMockService.Setup(c => c.GetById(id)).ReturnsAsync(GetMessageById(id));

            var result = await _messageMockService.Object.GetById(id);

            Assert.Null(result);
        }
        [Fact]
        public async void AddMessage_ShouldBeSuccess_WhenMessageNotExist()
        {
            var dto = new MessageDTO
            {
                Id = 2,
                Title = "TestTitle",
                Content = "ContentTest",
                IsReaded = true,
                SendDate = DateTime.Now
            };

            _messageMockService.Setup(c => c.Add(dto)).Returns(AddMessage(dto));
            await _messageMockService.Object.Add(dto);

        }
        [Fact]
        public async void AddMessage_ShouldBeThrowException_WhenMessageExist()
        {
            var dto = new MessageDTO
            {
                Id = 1,
                Title = "TestTitle",
                Content = "ContentTest",
                IsReaded = true,
                SendDate = DateTime.Now
            };

            _messageMockService.Setup(c => c.Add(dto)).Returns(AddMessage(dto));

            await Assert.ThrowsAsync<ArgumentException>(async () => await _messageMockService.Object.Add(dto));
        }
        [Fact]
        public async void UpdateMessage_ShouldBeSuccess_WhenMessageUpdated()
        {
            var id = 1;
            var dto = new MessageDTO
            {
                Id = 1,
                Title = "TestTitle",
                Content = "ContentTest",
                IsReaded = true,
                SendDate = DateTime.Now
            };

            _messageMockService.Setup(c => c.Update(id, dto)).Returns(UpdateMessage(id, dto));
            await _messageMockService.Object.Update(id, dto);

        }
        [Fact]
        public async void UpdateMessage_ShouldBeThrowException_WhenMessageNotExist()
        {

            var id = 2;
            var dto = new MessageDTO
            {
                Id = 2,
                Title = "TestTitle",
                Content = "ContentTest",
                IsReaded = true,
                SendDate = DateTime.Now
            };

            _messageMockService.Setup(c => c.Update(id, dto)).Returns(UpdateMessage(id, dto));

            await Assert.ThrowsAsync<ArgumentException>(async () => await _messageMockService.Object.Update(id, dto));
        }
        [Fact]
        public async void DeleteMessage_ShouldBeSuccess_WhenMessageExist()
        {
            var id = 1;
            _messageMockService.Setup(c => c.Delete(id)).Returns(DeleteMessage(id));
            await _messageMockService.Object.Delete(id);

        }
        [Fact]
        public async void DeleteMessage_ShouldBeThrowException_WhenMessageNotExist()
        {

            var id = 2;
            _messageMockService.Setup(c => c.Delete(id)).Returns(DeleteMessage(id));

            await Assert.ThrowsAsync<ArgumentException>(async () => await _messageMockService.Object.Delete(id));
        }

        private ICollection<MessageDTO> CreateMessageList()
        {
            return new List<MessageDTO>
            {
                new()
                {
                    Id = 1,
                    Title = "TestTitle",
                    Content = "ContentTest",
                    IsReaded = true,
                    SendDate = DateTime.Now
                }
            };
        }

        private MessageDTO GetMessageById(int id)
        {
            return CreateMessageList().FirstOrDefault(c => c.Id == id);
        }

        private async Task AddMessage(MessageDTO dto)
        {
            if (CreateMessageList().Any(c => c.Id == dto.Id))
                throw new ArgumentException();
        }
        private async Task UpdateMessage(int id, MessageDTO dto)
        {
            if (CreateMessageList().Any(c => c.Id != id))
                throw new ArgumentException();
        }
        private async Task DeleteMessage(int id)
        {
            if (CreateMessageList().Any(c => c.Id != id))
                throw new ArgumentException();
        }
    }
}
