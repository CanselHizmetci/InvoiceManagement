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
    public class InvoiceServiceTests
    {
        private readonly Mock<IInvoiceService> _invoiceMockService;

        public InvoiceServiceTests()
        {
            _invoiceMockService = new Mock<IInvoiceService>();
        }
        [Fact]
        public async void GetAll_ShouldBeReturnAListOfInvoice_WhenInvoicesExist()
        {
            var invoiceList = CreateInvoiceList();

            _invoiceMockService.Setup(c => c.Get()).ReturnsAsync(invoiceList);

            var result = await _invoiceMockService.Object.Get();

            Assert.NotNull(result);
            Assert.IsType<List<InvoiceDTO>>(result);
        }
        [Fact]
        public async void GetById_ShouldBeReturnAInvoice_WhenInvoicesExist()
        {
            var id = 1; // id 1 already exist;

            _invoiceMockService.Setup(c => c.GetById(id)).ReturnsAsync(GetInvoiceById(id));

            var result = await _invoiceMockService.Object.GetById(id);

            Assert.NotNull(result);
            Assert.IsType<InvoiceDTO>(result);
        }
        [Fact]
        public async void GetById_ShouldBeReturnNull_WhenInvoiceNotExist()
        {
            var id = 2; // id 2 not exist;

            _invoiceMockService.Setup(c => c.GetById(id)).ReturnsAsync(GetInvoiceById(id));

            var result = await _invoiceMockService.Object.GetById(id);

            Assert.Null(result);
        }
        [Fact]
        public async void AddInvoice_ShouldBeSuccess_WhenInvoiceNotExist()
        {
            var dto = new InvoiceDTO
            {
                Id = 2,
                Amount = 123,
                DueTime = DateTime.Now,
                InvoiceReadDate = DateTime.Now,
                InvoiceTypeId = 1
            };

            _invoiceMockService.Setup(c => c.Add(dto)).Returns(AddInvoice(dto));
            await _invoiceMockService.Object.Add(dto);

        }
        [Fact]
        public async void AddInvoice_ShouldBeThrowException_WhenInvoiceExist()
        {
            var dto = new InvoiceDTO
            {
                Id = 1,
                Amount = 123,
                DueTime = DateTime.Now,
                InvoiceReadDate = DateTime.Now,
                InvoiceTypeId = 1
            };

            _invoiceMockService.Setup(c => c.Add(dto)).Returns(AddInvoice(dto));

            await Assert.ThrowsAsync<ArgumentException>(async () => await _invoiceMockService.Object.Add(dto));
        }
        [Fact]
        public async void UpdateInvoice_ShouldBeSuccess_WhenInvoiceUpdated()
        {
            var id = 1;
            var dto = new InvoiceDTO
            {
                Id = 1,
                Amount = 123,
                DueTime = DateTime.Now,
                InvoiceReadDate = DateTime.Now,
                InvoiceTypeId = 1
            };

            _invoiceMockService.Setup(c => c.Update(id, dto)).Returns(UpdateInvoice(id, dto));
            await _invoiceMockService.Object.Update(id, dto);

        }
        [Fact]
        public async void UpdateInvoice_ShouldBeThrowException_WhenInvoiceNotExist()
        {

            var id = 2;
            var dto = new InvoiceDTO
            {
                Id = 2,
                Amount = 123,
                DueTime = DateTime.Now,
                InvoiceReadDate = DateTime.Now,
                InvoiceTypeId = 1
            };

            _invoiceMockService.Setup(c => c.Update(id, dto)).Returns(UpdateInvoice(id, dto));

            await Assert.ThrowsAsync<ArgumentException>(async () => await _invoiceMockService.Object.Update(id, dto));
        }
        [Fact]
        public async void DeleteInvoice_ShouldBeSuccess_WhenInvoiceExist()
        {
            var id = 1;
            _invoiceMockService.Setup(c => c.Delete(id)).Returns(DeleteInvoice(id));
            await _invoiceMockService.Object.Delete(id);

        }
        [Fact]
        public async void DeleteInvoice_ShouldBeThrowException_WhenInvoiceNotExist()
        {

            var id = 2;
            _invoiceMockService.Setup(c => c.Delete(id)).Returns(DeleteInvoice(id));

            await Assert.ThrowsAsync<ArgumentException>(async () => await _invoiceMockService.Object.Delete(id));
        }

        private ICollection<InvoiceDTO> CreateInvoiceList()
        {
            return new List<InvoiceDTO>
            {
                new()
                {
                    Id = 1,
                    Amount = 123,
                    DueTime = DateTime.Now,
                    InvoiceReadDate = DateTime.Now,
                    InvoiceTypeId = 1
                }
            };
        }

        private InvoiceDTO GetInvoiceById(int id)
        {
            return CreateInvoiceList().FirstOrDefault(c => c.Id == id);
        }

        private async Task AddInvoice(InvoiceDTO dto)
        {
            if (CreateInvoiceList().Any(c => c.Id == dto.Id))
                throw new ArgumentException();
        }
        private async Task UpdateInvoice(int id, InvoiceDTO dto)
        {
            if (CreateInvoiceList().Any(c => c.Id != id))
                throw new ArgumentException();
        }
        private async Task DeleteInvoice(int id)
        {
            if (CreateInvoiceList().Any(c => c.Id != id))
                throw new ArgumentException();
        }
    }
}
