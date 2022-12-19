using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using InvoiceManagement.Data.Abstract;
using InvoiceManagement.Domain.Entities;
using InvoiceManagement.Service.Abstracts;
using InvoiceManagement.Service.Concretes;
using InvoiceManagement.Service.DTOs;
using InvoiceManagement.Service.Mapper;
using Moq;
using Xunit;

namespace InvoiceManagementTests.InvoiceManagementServiceTests
{
    public class ApartmentServiceTests
    {
        private readonly Mock<IApartmentService> _apartmentMockService;

        public ApartmentServiceTests()
        {
            _apartmentMockService=new Mock<IApartmentService>();
        }
        [Fact]
        public async void GetAll_ShouldBeReturnAListOfApartment_WhenApartmentsExist()
        {
            var apartmentList = CreateApartmentList();

             _apartmentMockService.Setup(c => c.Get()).ReturnsAsync(apartmentList);
            
            var result = await _apartmentMockService.Object.Get();

            Assert.NotNull(result);
            Assert.IsType<List<ApartmentDTO>>(result);
        }
        [Fact]
        public async void GetById_ShouldBeReturnAApartment_WhenApartmentsExist()
        {
            var id = 1; // id 1 already exist;

             _apartmentMockService.Setup(c => c.GetById(id)).ReturnsAsync(GetApartmentById(id));
            
            var result = await _apartmentMockService.Object.GetById(id);

            Assert.NotNull(result);
            Assert.IsType<ApartmentDTO>(result);
        }
        [Fact]
        public async void GetById_ShouldBeReturnNull_WhenApartmentNotExist()
        {
            var id = 2; // id 2 not exist;

             _apartmentMockService.Setup(c => c.GetById(id)).ReturnsAsync(GetApartmentById(id));
            
            var result = await _apartmentMockService.Object.GetById(id);

            Assert.Null(result);
        }
        [Fact]
        public async void AddApartment_ShouldBeSuccess_WhenApartmentNotExist()
        {
            var dto = new ApartmentDTO
            {
                Id = 2,
                TypeId = 1,
                ApartmentNumber = 1,
                BlockId = 1,
                UserId = Guid.NewGuid().ToString(),
                Status = true,
                Floor = 0
            };

            _apartmentMockService.Setup(c => c.Add(dto)).Returns(AddApartment(dto));
            await _apartmentMockService.Object.Add(dto);

        }
        [Fact]
        public async void AddApartment_ShouldBeThrowException_WhenApartmentExist()
        {
            var dto = new ApartmentDTO
            {
                Id = 1,
                TypeId = 1,
                ApartmentNumber = 1,
                BlockId = 1,
                UserId = Guid.NewGuid().ToString(),
                Status = true,
                Floor = 0
            };

            _apartmentMockService.Setup(c => c.Add(dto)).Returns(AddApartment(dto));

            await Assert.ThrowsAsync<ArgumentException>(async () => await _apartmentMockService.Object.Add(dto));
        }
        [Fact]
        public async void UpdateApartment_ShouldBeSuccess_WhenApartmentUpdated()
        {
            var id = 1;
            var dto = new ApartmentDTO
            {
                Id = 1,
                TypeId = 1,
                ApartmentNumber = 1,
                BlockId = 1,
                UserId = Guid.NewGuid().ToString(),
                Status = true,
                Floor = 0
            };

            _apartmentMockService.Setup(c => c.Update(id,dto)).Returns(UpdateApartment(id,dto));
            await _apartmentMockService.Object.Update(id,dto);

        }
        [Fact]
        public async void UpdateApartment_ShouldBeThrowException_WhenApartmentNotExist()
        {

            var id = 2;
            var dto = new ApartmentDTO
            {
                Id = 2,
                TypeId = 1,
                ApartmentNumber = 1,
                BlockId = 1,
                UserId = Guid.NewGuid().ToString(),
                Status = true,
                Floor = 0
            };

            _apartmentMockService.Setup(c => c.Update(id, dto)).Returns(UpdateApartment(id, dto));

            await Assert.ThrowsAsync<ArgumentException>(async () => await _apartmentMockService.Object.Update(id, dto));
        }
        [Fact]
        public async void DeleteApartment_ShouldBeSuccess_WhenApartmentExist()
        {
            var id = 1;
            _apartmentMockService.Setup(c => c.Delete(id)).Returns(DeleteApartment(id));
            await _apartmentMockService.Object.Delete(id);

        }
        [Fact]
        public async void DeleteApartment_ShouldBeThrowException_WhenApartmentNotExist()
        {

            var id = 2;
            _apartmentMockService.Setup(c => c.Delete(id)).Returns(DeleteApartment(id));

            await Assert.ThrowsAsync<ArgumentException>(async () => await _apartmentMockService.Object.Delete(id));
        }

        private ICollection<ApartmentDTO> CreateApartmentList()
        {
            return new List<ApartmentDTO>
            {
                new()
                {
                    Id = 1,
                    TypeId = 1,
                    ApartmentNumber = 1,
                    BlockId = 1,
                    UserId = Guid.NewGuid().ToString(),
                    Status = true,
                    Floor = 0
                }
            };
        }

        private ApartmentDTO GetApartmentById(int id)
        {
            return CreateApartmentList().FirstOrDefault(c => c.Id == id);
        }

        private async Task AddApartment(ApartmentDTO dto)
        {
            if (CreateApartmentList().Any(c => c.Id == dto.Id))
                throw new ArgumentException();
        }
        private async Task UpdateApartment(int id,ApartmentDTO dto)
        {
            if (CreateApartmentList().Any(c => c.Id != id))
                throw new ArgumentException();
        }
        private async Task DeleteApartment(int id)
        {
            if (CreateApartmentList().Any(c => c.Id != id))
                throw new ArgumentException();
        }
    }
}
