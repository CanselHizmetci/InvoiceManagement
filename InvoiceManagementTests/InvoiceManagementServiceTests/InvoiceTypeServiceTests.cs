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
    public class InvoiceTypeServiceTests
    {
        private readonly Mock<IInvoiceTypeService> _invoiceTypeMockService;

        public InvoiceTypeServiceTests()
        {
            _invoiceTypeMockService = new Mock<IInvoiceTypeService>();
        }
        [Fact]
        public async void GetAll_ShouldBeReturnAListOfInvoiceType_WhenInvoiceTypesExist()
        {
            var invoiceTypeList = CreateInvoiceTypeList();

            _invoiceTypeMockService.Setup(c => c.Get()).ReturnsAsync(invoiceTypeList);

            var result = await _invoiceTypeMockService.Object.Get();

            Assert.NotNull(result);
            Assert.IsType<List<InvoiceTypeDTO>>(result);
        }
        [Fact]
        public async void GetById_ShouldBeReturnAInvoiceType_WhenInvoiceTypesExist()
        {
            var id = 1; // id 1 already exist;

            _invoiceTypeMockService.Setup(c => c.GetById(id)).ReturnsAsync(GetInvoiceTypeById(id));

            var result = await _invoiceTypeMockService.Object.GetById(id);

            Assert.NotNull(result);
            Assert.IsType<InvoiceTypeDTO>(result);
        }
        [Fact]
        public async void GetById_ShouldBeReturnNull_WhenInvoiceTypeNotExist()
        {
            var id = 2; // id 2 not exist;

            _invoiceTypeMockService.Setup(c => c.GetById(id)).ReturnsAsync(GetInvoiceTypeById(id));

            var result = await _invoiceTypeMockService.Object.GetById(id);

            Assert.Null(result);
        }
        [Fact]
        public async void AddInvoiceType_ShouldBeSuccess_WhenInvoiceTypeNotExist()
        {
            var dto = new InvoiceTypeDTO
            {
                Id = 2,
                Title = "TestTitle"
            };

            _invoiceTypeMockService.Setup(c => c.Add(dto)).Returns(AddInvoiceType(dto));
            await _invoiceTypeMockService.Object.Add(dto);

        }
        [Fact]
        public async void AddInvoiceType_ShouldBeThrowException_WhenInvoiceTypeExist()
        {
            var dto = new InvoiceTypeDTO
            {
                Id = 1,
                Title = "TestTitle"
            };

            _invoiceTypeMockService.Setup(c => c.Add(dto)).Returns(AddInvoiceType(dto));

            await Assert.ThrowsAsync<ArgumentException>(async () => await _invoiceTypeMockService.Object.Add(dto));
        }
        [Fact]
        public async void UpdateInvoiceType_ShouldBeSuccess_WhenInvoiceTypeUpdated()
        {
            var id = 1;
            var dto = new InvoiceTypeDTO
            {
                Id = 1,
                Title = "TestTitle"
            };

            _invoiceTypeMockService.Setup(c => c.Update(id, dto)).Returns(UpdateInvoiceType(id, dto));
            await _invoiceTypeMockService.Object.Update(id, dto);

        }
        [Fact]
        public async void UpdateInvoiceType_ShouldBeThrowException_WhenInvoiceTypeNotExist()
        {

            var id = 2;
            var dto = new InvoiceTypeDTO
            {
                Id = 2,
                Title = "TestTitle"
            };

            _invoiceTypeMockService.Setup(c => c.Update(id, dto)).Returns(UpdateInvoiceType(id, dto));

            await Assert.ThrowsAsync<ArgumentException>(async () => await _invoiceTypeMockService.Object.Update(id, dto));
        }
        [Fact]
        public async void DeleteInvoiceType_ShouldBeSuccess_WhenInvoiceTypeExist()
        {
            var id = 1;
            _invoiceTypeMockService.Setup(c => c.Delete(id)).Returns(DeleteInvoiceType(id));
            await _invoiceTypeMockService.Object.Delete(id);

        }
        [Fact]
        public async void DeleteInvoiceType_ShouldBeThrowException_WhenInvoiceTypeNotExist()
        {

            var id = 2;
            _invoiceTypeMockService.Setup(c => c.Delete(id)).Returns(DeleteInvoiceType(id));

            await Assert.ThrowsAsync<ArgumentException>(async () => await _invoiceTypeMockService.Object.Delete(id));
        }

        private ICollection<InvoiceTypeDTO> CreateInvoiceTypeList()
        {
            return new List<InvoiceTypeDTO>
            {
                new()
                {
                    Id = 1,
                    Title = "TestTitle"
                }
            };
        }

        private InvoiceTypeDTO GetInvoiceTypeById(int id)
        {
            return CreateInvoiceTypeList().FirstOrDefault(c => c.Id == id);
        }

        private async Task AddInvoiceType(InvoiceTypeDTO dto)
        {
            if (CreateInvoiceTypeList().Any(c => c.Id == dto.Id))
                throw new ArgumentException();
        }
        private async Task UpdateInvoiceType(int id, InvoiceTypeDTO dto)
        {
            if (CreateInvoiceTypeList().Any(c => c.Id != id))
                throw new ArgumentException();
        }
        private async Task DeleteInvoiceType(int id)
        {
            if (CreateInvoiceTypeList().Any(c => c.Id != id))
                throw new ArgumentException();
        }
    }
}
