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
    public class DebtServiceTests
    {
        private readonly Mock<IDebtService> _debtMockService;

        public DebtServiceTests()
        {
            _debtMockService = new Mock<IDebtService>();
        }
        [Fact]
        public async void GetAll_ShouldBeReturnAListOfDebt_WhenDebtsExist()
        {
            var debtList = CreateDebtList();

            _debtMockService.Setup(c => c.Get()).ReturnsAsync(debtList);

            var result = await _debtMockService.Object.Get();

            Assert.NotNull(result);
            Assert.IsType<List<DebtDTO>>(result);
        }
        [Fact]
        public async void GetById_ShouldBeReturnADebt_WhenDebtsExist()
        {
            var id = 1; // id 1 already exist;

            _debtMockService.Setup(c => c.GetById(id)).ReturnsAsync(GetDebtById(id));

            var result = await _debtMockService.Object.GetById(id);

            Assert.NotNull(result);
            Assert.IsType<DebtDTO>(result);
        }
        [Fact]
        public async void GetById_ShouldBeReturnNull_WhenDebtNotExist()
        {
            var id = 2; // id 2 not exist;

            _debtMockService.Setup(c => c.GetById(id)).ReturnsAsync(GetDebtById(id));

            var result = await _debtMockService.Object.GetById(id);

            Assert.Null(result);
        }
        [Fact]
        public async void AddDebt_ShouldBeSuccess_WhenDebtNotExist()
        {
            var dto = new DebtDTO
            {
                Id = 2,
                Title = "TestTitle",
                Amount = 123,
                ApartmentId = 1,
                DueTime = DateTime.Now,
                IsPaid = true
            };

            _debtMockService.Setup(c => c.Add(dto)).Returns(AddDebt(dto));
            await _debtMockService.Object.Add(dto);

        }
        [Fact]
        public async void AddDebt_ShouldBeThrowException_WhenDebtExist()
        {
            var dto = new DebtDTO
            {
                Id = 1,
                Title = "TestTitle",
                Amount = 123,
                ApartmentId = 1,
                DueTime = DateTime.Now,
                IsPaid = true
            };

            _debtMockService.Setup(c => c.Add(dto)).Returns(AddDebt(dto));

            await Assert.ThrowsAsync<ArgumentException>(async () => await _debtMockService.Object.Add(dto));
        }
        [Fact]
        public async void UpdateDebt_ShouldBeSuccess_WhenDebtUpdated()
        {
            var id = 1;
            var dto = new DebtDTO
            {
                Id = 1,
                Title = "TestTitle",
                Amount = 123,
                ApartmentId = 1,
                DueTime = DateTime.Now,
                IsPaid = true
            };

            _debtMockService.Setup(c => c.Update(id, dto)).Returns(UpdateDebt(id, dto));
            await _debtMockService.Object.Update(id, dto);

        }
        [Fact]
        public async void UpdateDebt_ShouldBeThrowException_WhenDebtNotExist()
        {

            var id = 2;
            var dto = new DebtDTO
            {
                Id = 2,
                Title = "TestTitle",
                Amount = 123,
                ApartmentId = 1,
                DueTime = DateTime.Now,
                IsPaid = true
            };

            _debtMockService.Setup(c => c.Update(id, dto)).Returns(UpdateDebt(id, dto));

            await Assert.ThrowsAsync<ArgumentException>(async () => await _debtMockService.Object.Update(id, dto));
        }
        [Fact]
        public async void DeleteDebt_ShouldBeSuccess_WhenDebtExist()
        {
            var id = 1;
            _debtMockService.Setup(c => c.Delete(id)).Returns(DeleteDebt(id));
            await _debtMockService.Object.Delete(id);

        }
        [Fact]
        public async void DeleteDebt_ShouldBeThrowException_WhenDebtNotExist()
        {

            var id = 2;
            _debtMockService.Setup(c => c.Delete(id)).Returns(DeleteDebt(id));

            await Assert.ThrowsAsync<ArgumentException>(async () => await _debtMockService.Object.Delete(id));
        }

        private ICollection<DebtDTO> CreateDebtList()
        {
            return new List<DebtDTO>
            {
                new()
                {
                    Id = 1,
                    Title = "TestTitle",
                    Amount = 123,
                    ApartmentId = 1,
                    DueTime = DateTime.Now,
                    IsPaid = true
                }
            };
        }

        private DebtDTO GetDebtById(int id)
        {
            return CreateDebtList().FirstOrDefault(c => c.Id == id);
        }

        private async Task AddDebt(DebtDTO dto)
        {
            if (CreateDebtList().Any(c => c.Id == dto.Id))
                throw new ArgumentException();
        }
        private async Task UpdateDebt(int id, DebtDTO dto)
        {
            if (CreateDebtList().Any(c => c.Id != id))
                throw new ArgumentException();
        }
        private async Task DeleteDebt(int id)
        {
            if (CreateDebtList().Any(c => c.Id != id))
                throw new ArgumentException();
        }
    }
}
