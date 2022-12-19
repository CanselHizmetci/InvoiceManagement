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
    public class BlockServiceTests
    {
        private readonly Mock<IBlockService> _blockMockService;

        public BlockServiceTests()
        {
            _blockMockService = new Mock<IBlockService>();
        }
        [Fact]
        public async void GetAll_ShouldBeReturnAListOfBlock_WhenBlocksExist()
        {
            var blockList = CreateBlockList();

            _blockMockService.Setup(c => c.Get()).ReturnsAsync(blockList);

            var result = await _blockMockService.Object.Get();

            Assert.NotNull(result);
            Assert.IsType<List<BlockDTO>>(result);
        }
        [Fact]
        public async void GetById_ShouldBeReturnABlock_WhenBlocksExist()
        {
            var id = 1; // id 1 already exist;

            _blockMockService.Setup(c => c.GetById(id)).ReturnsAsync(GetBlockById(id));

            var result = await _blockMockService.Object.GetById(id);

            Assert.NotNull(result);
            Assert.IsType<BlockDTO>(result);
        }
        [Fact]
        public async void GetById_ShouldBeReturnNull_WhenBlockNotExist()
        {
            var id = 2; // id 2 not exist;

            _blockMockService.Setup(c => c.GetById(id)).ReturnsAsync(GetBlockById(id));

            var result = await _blockMockService.Object.GetById(id);

            Assert.Null(result);
        }
        [Fact]
        public async void AddBlock_ShouldBeSuccess_WhenBlockNotExist()
        {
            var dto = new BlockDTO
            {
                Id = 2,
                Title = "TestTitle"
            };

            _blockMockService.Setup(c => c.Add(dto)).Returns(AddBlock(dto));
            await _blockMockService.Object.Add(dto);

        }
        [Fact]
        public async void AddBlock_ShouldBeThrowException_WhenBlockExist()
        {
            var dto = new BlockDTO
            {
                Id = 1,
                Title = "TestTitle"
            };

            _blockMockService.Setup(c => c.Add(dto)).Returns(AddBlock(dto));

            await Assert.ThrowsAsync<ArgumentException>(async () => await _blockMockService.Object.Add(dto));
        }
        [Fact]
        public async void UpdateBlock_ShouldBeSuccess_WhenBlockUpdated()
        {
            var id = 1;
            var dto = new BlockDTO
            {
                Id = 1,
                Title = "TestTitle"
            };

            _blockMockService.Setup(c => c.Update(id, dto)).Returns(UpdateBlock(id, dto));
            await _blockMockService.Object.Update(id, dto);

        }
        [Fact]
        public async void UpdateBlock_ShouldBeThrowException_WhenBlockNotExist()
        {

            var id = 2;
            var dto = new BlockDTO
            {
                Id = 2,
                Title = "TestTitle"
            };

            _blockMockService.Setup(c => c.Update(id, dto)).Returns(UpdateBlock(id, dto));

            await Assert.ThrowsAsync<ArgumentException>(async () => await _blockMockService.Object.Update(id, dto));
        }
        [Fact]
        public async void DeleteBlock_ShouldBeSuccess_WhenBlockExist()
        {
            var id = 1;
            _blockMockService.Setup(c => c.Delete(id)).Returns(DeleteBlock(id));
            await _blockMockService.Object.Delete(id);

        }
        [Fact]
        public async void DeleteBlock_ShouldBeThrowException_WhenBlockNotExist()
        {

            var id = 2;
            _blockMockService.Setup(c => c.Delete(id)).Returns(DeleteBlock(id));

            await Assert.ThrowsAsync<ArgumentException>(async () => await _blockMockService.Object.Delete(id));
        }

        private ICollection<BlockDTO> CreateBlockList()
        {
            return new List<BlockDTO>
            {
                new()
                {
                    Id = 1,
                    Title = "TestTitle"
                }
            };
        }

        private BlockDTO GetBlockById(int id)
        {
            return CreateBlockList().FirstOrDefault(c => c.Id == id);
        }

        private async Task AddBlock(BlockDTO dto)
        {
            if (CreateBlockList().Any(c => c.Id == dto.Id))
                throw new ArgumentException();
        }
        private async Task UpdateBlock(int id, BlockDTO dto)
        {
            if (CreateBlockList().Any(c => c.Id != id))
                throw new ArgumentException();
        }
        private async Task DeleteBlock(int id)
        {
            if (CreateBlockList().Any(c => c.Id != id))
                throw new ArgumentException();
        }
    }
}
