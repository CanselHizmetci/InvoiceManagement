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
    public class PaymentServiceTests
    {
        private readonly Mock<IPaymentService> _paymentMockService;

        public PaymentServiceTests()
        {
            _paymentMockService = new Mock<IPaymentService>();
        }
        [Fact]
        public async void GetAll_ShouldBeReturnAListOfPayment_WhenPaymentsExist()
        {
            var paymentList = CreatePaymentList();

            _paymentMockService.Setup(c => c.Get()).ReturnsAsync(paymentList);

            var result = await _paymentMockService.Object.Get();

            Assert.NotNull(result);
            Assert.IsType<List<PaymentDTO>>(result);
        }
        [Fact]
        public async void GetById_ShouldBeReturnAPayment_WhenPaymentsExist()
        {
            var id = 1; // id 1 already exist;

            _paymentMockService.Setup(c => c.GetById(id)).ReturnsAsync(GetPaymentById(id));

            var result = await _paymentMockService.Object.GetById(id);

            Assert.NotNull(result);
            Assert.IsType<PaymentDTO>(result);
        }
        [Fact]
        public async void GetById_ShouldBeReturnNull_WhenPaymentNotExist()
        {
            var id = 2; // id 2 not exist;

            _paymentMockService.Setup(c => c.GetById(id)).ReturnsAsync(GetPaymentById(id));

            var result = await _paymentMockService.Object.GetById(id);

            Assert.Null(result);
        }
        [Fact]
        public async void AddPayment_ShouldBeSuccess_WhenPaymentNotExist()
        {
            var dto = new PaymentDTO
            {
                Id = 2,
                CreditCardNo = "1234567812345678",
                ErrorMessage = "ErrorMessageTest",
                PaymentStatus = true,
                PaymentDate = DateTime.Now,
                UserId = "1"
            };

            _paymentMockService.Setup(c => c.Add(dto)).Returns(AddPayment(dto));
            await _paymentMockService.Object.Add(dto);

        }
        [Fact]
        public async void AddPayment_ShouldBeThrowException_WhenPaymentExist()
        {
            var dto = new PaymentDTO
            {
                Id = 1,
                CreditCardNo = "1234567812345678",
                ErrorMessage = "ErrorMessageTest",
                PaymentStatus = true,
                PaymentDate = DateTime.Now,
                UserId = "1"
            };

            _paymentMockService.Setup(c => c.Add(dto)).Returns(AddPayment(dto));

            await Assert.ThrowsAsync<ArgumentException>(async () => await _paymentMockService.Object.Add(dto));
        }
        [Fact]
        public async void UpdatePayment_ShouldBeSuccess_WhenPaymentUpdated()
        {
            var id = 1;
            var dto = new PaymentDTO
            {
                Id = 1,
                CreditCardNo = "1234567812345678",
                ErrorMessage = "ErrorMessageTest",
                PaymentStatus = true,
                PaymentDate = DateTime.Now,
                UserId = "1"
            };

            _paymentMockService.Setup(c => c.Update(id, dto)).Returns(UpdatePayment(id, dto));
            await _paymentMockService.Object.Update(id, dto);

        }
        [Fact]
        public async void UpdatePayment_ShouldBeThrowException_WhenPaymentNotExist()
        {

            var id = 2;
            var dto = new PaymentDTO
            {
                Id = 2,
                CreditCardNo = "1234567812345678",
                ErrorMessage = "ErrorMessageTest",
                PaymentStatus = true,
                PaymentDate = DateTime.Now,
                UserId = "1"
            };

            _paymentMockService.Setup(c => c.Update(id, dto)).Returns(UpdatePayment(id, dto));

            await Assert.ThrowsAsync<ArgumentException>(async () => await _paymentMockService.Object.Update(id, dto));
        }
        [Fact]
        public async void DeletePayment_ShouldBeSuccess_WhenPaymentExist()
        {
            var id = 1;
            _paymentMockService.Setup(c => c.Delete(id)).Returns(DeletePayment(id));
            await _paymentMockService.Object.Delete(id);

        }
        [Fact]
        public async void DeletePayment_ShouldBeThrowException_WhenPaymentNotExist()
        {

            var id = 2;
            _paymentMockService.Setup(c => c.Delete(id)).Returns(DeletePayment(id));

            await Assert.ThrowsAsync<ArgumentException>(async () => await _paymentMockService.Object.Delete(id));
        }

        private ICollection<PaymentDTO> CreatePaymentList()
        {
            return new List<PaymentDTO>
            {
                new()
                {
                    Id = 1,
                    CreditCardNo = "1234567812345678",
                    ErrorMessage = "ErrorMessageTest",
                    PaymentStatus = true,
                    PaymentDate = DateTime.Now,
                    UserId = "1"
                }
            };
        }

        private PaymentDTO GetPaymentById(int id)
        {
            return CreatePaymentList().FirstOrDefault(c => c.Id == id);
        }

        private async Task AddPayment(PaymentDTO dto)
        {
            if (CreatePaymentList().Any(c => c.Id == dto.Id))
                throw new ArgumentException();
        }
        private async Task UpdatePayment(int id, PaymentDTO dto)
        {
            if (CreatePaymentList().Any(c => c.Id != id))
                throw new ArgumentException();
        }
        private async Task DeletePayment(int id)
        {
            if (CreatePaymentList().Any(c => c.Id != id))
                throw new ArgumentException();
        }
    }
}
