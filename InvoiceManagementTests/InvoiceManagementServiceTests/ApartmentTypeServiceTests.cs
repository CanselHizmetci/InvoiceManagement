using InvoiceManagement.Service.Abstracts;
using InvoiceManagement.Service.DTOs;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace InvoiceManagementTests.InvoiceManagementServiceTests
{
    public class ApartmentTypeServiceTests
    {
        private readonly Mock<IApartmentTypeService> _apartmentTypeMockService;

        public ApartmentTypeServiceTests()
        {
            _apartmentTypeMockService = new Mock<IApartmentTypeService>();
        }
        [Fact]
        public async void GetAll_ShouldBeReturnAListOfApartmentType_WhenApartmentTypesExist()
        {
            var apartmentTypeList = CreateApartmentTypeList();

            _apartmentTypeMockService.Setup(c => c.Get()).ReturnsAsync(apartmentTypeList);

            var result = await _apartmentTypeMockService.Object.Get();

            Assert.NotNull(result);
            Assert.IsType<List<ApartmentTypeDTO>>(result);
        }
        [Fact]
        public async void GetById_ShouldBeReturnAApartmentType_WhenApartmentTypesExist()
        {
            var id = 1; // id 1 already exist;

            _apartmentTypeMockService.Setup(c => c.GetById(id)).ReturnsAsync(GetApartmentTypeById(id));

            var result = await _apartmentTypeMockService.Object.GetById(id);

            Assert.NotNull(result);
            Assert.IsType<ApartmentTypeDTO>(result);
        }
        [Fact]
        public async void GetById_ShouldBeReturnNull_WhenApartmentTypeNotExist()
        {
            var id = 2; // id 2 not exist;

            _apartmentTypeMockService.Setup(c => c.GetById(id)).ReturnsAsync(GetApartmentTypeById(id));

            var result = await _apartmentTypeMockService.Object.GetById(id);

            Assert.Null(result);
        }
        [Fact]
        public async void AddApartmentType_ShouldBeSuccess_WhenApartmentTypeNotExist()
        {
            var dto = new ApartmentTypeDTO
            {
                Id = 2,
                Title = "TestTitle"
            };

            _apartmentTypeMockService.Setup(c => c.Add(dto)).Returns(AddApartmentType(dto));
            await _apartmentTypeMockService.Object.Add(dto);

        }
        [Fact]
        public async void AddApartmentType_ShouldBeThrowException_WhenApartmentTypeExist()
        {
            var dto = new ApartmentTypeDTO
            {
                Id = 1,
                Title = "TestTitle"
            };

            _apartmentTypeMockService.Setup(c => c.Add(dto)).Returns(AddApartmentType(dto));

            await Assert.ThrowsAsync<ArgumentException>(async () => await _apartmentTypeMockService.Object.Add(dto));
        }
        [Fact]
        public async void UpdateApartmentType_ShouldBeSuccess_WhenApartmentTypeUpdated()
        {
            var id = 1;
            var dto = new ApartmentTypeDTO
            {
                Id = 1,
                Title = "TestTitle"
            };

            _apartmentTypeMockService.Setup(c => c.Update(id, dto)).Returns(UpdateApartmentType(id, dto));
            await _apartmentTypeMockService.Object.Update(id, dto);

        }
        [Fact]
        public async void UpdateApartmentType_ShouldBeThrowException_WhenApartmentTypeNotExist()
        {

            var id = 2;
            var dto = new ApartmentTypeDTO
            {
                Id = 2,
                Title = "TestTitle"
            };

            _apartmentTypeMockService.Setup(c => c.Update(id, dto)).Returns(UpdateApartmentType(id, dto));

            await Assert.ThrowsAsync<ArgumentException>(async () => await _apartmentTypeMockService.Object.Update(id, dto));
        }
        [Fact]
        public async void DeleteApartmentType_ShouldBeSuccess_WhenApartmentTypeExist()
        {
            var id = 1;
            _apartmentTypeMockService.Setup(c => c.Delete(id)).Returns(DeleteApartmentType(id));
            await _apartmentTypeMockService.Object.Delete(id);

        }
        [Fact]
        public async void DeleteApartmentType_ShouldBeThrowException_WhenApartmentTypeNotExist()
        {

            var id = 2;
            _apartmentTypeMockService.Setup(c => c.Delete(id)).Returns(DeleteApartmentType(id));

            await Assert.ThrowsAsync<ArgumentException>(async () => await _apartmentTypeMockService.Object.Delete(id));
        }

        private ICollection<ApartmentTypeDTO> CreateApartmentTypeList()
        {
            return new List<ApartmentTypeDTO>
            {
                new()
                {
                    Id = 1,               
                    Title = "TestTitle"
                }
            };
        }

        private ApartmentTypeDTO GetApartmentTypeById(int id)
        {
            return CreateApartmentTypeList().FirstOrDefault(c => c.Id == id);
        }

        private async Task AddApartmentType(ApartmentTypeDTO dto)
        {
            if (CreateApartmentTypeList().Any(c => c.Id == dto.Id))
                throw new ArgumentException();
        }
        private async Task UpdateApartmentType(int id, ApartmentTypeDTO dto)
        {
            if (CreateApartmentTypeList().Any(c => c.Id != id))
                throw new ArgumentException();
        }
        private async Task DeleteApartmentType(int id)
        {
            if (CreateApartmentTypeList().Any(c => c.Id != id))
                throw new ArgumentException();
        }
    }
}
