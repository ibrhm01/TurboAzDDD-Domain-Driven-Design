using Application.Exceptions;
using Application.Services;
using AutoMapper;
using Domain;
using Domain.DTOs.Market;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Domain.Services;
using FluentAssertions;
using Infrastructure.Mapper;
using Moq;
using Org.BouncyCastle.Asn1.X509;
using UnitTests.Fixtures;

namespace UnitTests.Services
{
    public class MarketServiceTest
    {
        private IMapper _mapper { get; set; }
        private Mock<IUnitOfWork> _mockUnitOfWork { get; set; }
        private List<Market> _markets { get; set; }


        public MarketServiceTest()
        {
            var mockMapper = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
            var mapper = mockMapper.CreateMapper();
            _mapper = mapper;
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _markets = MarketsFixtures.Markets();

        }

        [Fact]
        public async void GetAllAsync_OnSuccess_ReturnsListOfMarketDtos()
        {
            //Arrange
            _mockUnitOfWork.Setup(u => u.MarketRepository.GetAllAsync()).ReturnsAsync(_markets);
            
            var mockMarketService = new MarketService(_mockUnitOfWork.Object, _mapper);

            //Act

            var result = await mockMarketService.GetAllAsync();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<GetMarketDto>>();
            // result.Should().BeEquivalentTo(marketDtos);
            result.Should().HaveCount(3);

        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void GetOneAsync_OnSuccess_ReturnsMarketDto(int id)
        {
            //Arrange
            var market = _markets.FirstOrDefault(m => m.Id == id);
            
            _mockUnitOfWork.Setup(u => u.MarketRepository.GetByIdAsync(id)).ReturnsAsync(market);
            var mockMarketService = new MarketService(_mockUnitOfWork.Object, _mapper);

            //Act

            var result = await mockMarketService.GetOneAsync(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<GetMarketDto>();
            // result.Should().BeEquivalentTo(marketDtos);
            
        }
        
        [Theory]
        [InlineData(100)]
        [InlineData(102)]
        [InlineData(200)]
        public async void GetOneAsync_OnError_ThrowsEntityNotFoundException(int marketId)
        {
            //Arrange
            var market = _markets.FirstOrDefault(m => m.Id == marketId);
            
            _mockUnitOfWork.Setup(u => u.MarketRepository.GetByIdAsync(marketId)).ReturnsAsync(market);
            var mockMarketService = new MarketService(_mockUnitOfWork.Object, _mapper);

            //Act and Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(async ()=> await mockMarketService.GetOneAsync(marketId));

        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void DeleteAsync_OnSuccess_ReturnsTrue(int marketId)
        {
            //Arrange
            var market = _markets.FirstOrDefault(m => m.Id == marketId);

            _mockUnitOfWork.Setup(u => u.MarketRepository.GetByIdAsync(marketId)).ReturnsAsync(market);
            _mockUnitOfWork.Setup(u => u.MarketRepository.DeleteAsync(market)).Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

            var mockMarketService = new MarketService(_mockUnitOfWork.Object, _mapper);
            //Act

            var result = await mockMarketService.DeleteAsync(marketId);


            //Assert
            result.Should().BeTrue();
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void DeleteAsync_OnFailure_ReturnsFalse(int marketId)
        {
            //Arrange
            var market = _markets.FirstOrDefault(m => m.Id == marketId);

            _mockUnitOfWork.Setup(u => u.MarketRepository.GetByIdAsync(marketId)).ReturnsAsync(market);
            _mockUnitOfWork.Setup(u => u.MarketRepository.DeleteAsync(market)).Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(0);

            var mockMarketService = new MarketService(_mockUnitOfWork.Object, _mapper);
            
            //Act

            var result = await mockMarketService.DeleteAsync(marketId);


            //Assert
            result.Should().BeFalse();
        }
        
        [Theory]
        [InlineData(100)]
        [InlineData(102)]
        [InlineData(200)]
        public async void DeleteAsync_OnError_ThrowsEntityNotFoundException(int marketId)
        {
            //Arrange
            var market = _markets.FirstOrDefault(m => m.Id == marketId);

            _mockUnitOfWork.Setup(u => u.MarketRepository.GetByIdAsync(marketId)).ReturnsAsync(market);
          
            var mockMarketService = new MarketService(_mockUnitOfWork.Object, _mapper);
            
            //Act and Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(async ()=> await mockMarketService.DeleteAsync(marketId));
        }
        
        [Fact]
        public async void CreateAsync_OnSuccess_ReturnsTrue()
        {
            //Arrange
            var createMarketDto = MarketsFixtures.CreateMarketDto();
            var mapped = _mapper.Map<Market>(createMarketDto);
            
            _mockUnitOfWork.Setup(u => u.MarketRepository.IsExistAsync(b => b.MarketName.Trim() == createMarketDto.MarketName.Trim())).ReturnsAsync(false);
            _mockUnitOfWork.Setup(u => u.MarketRepository.CreateAsync(mapped)).Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

            var mockMarketService = new MarketService(_mockUnitOfWork.Object, _mapper);
            
            //Act

            var result = await mockMarketService.CreateAsync(createMarketDto);


            //Assert
            result.Should().BeTrue();
        }
        
        [Fact]
        public async void CreateAsync_OnFailure_ReturnsFalse()
        {
            //Arrange
            var createMarketDto = MarketsFixtures.CreateMarketDto();
            var mapped = _mapper.Map<Market>(createMarketDto);
            
            _mockUnitOfWork.Setup(u => u.MarketRepository.IsExistAsync(b => b.MarketName.Trim() == createMarketDto.MarketName.Trim())).ReturnsAsync(false);
            _mockUnitOfWork.Setup(u => u.MarketRepository.CreateAsync(mapped)).Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(0);

            var mockMarketService = new MarketService(_mockUnitOfWork.Object, _mapper);
            
            //Act

            var result = await mockMarketService.CreateAsync(createMarketDto);


            //Assert
            result.Should().BeFalse();
        }
        
        [Fact]
        public async void CreateAsync_OnError_ThrowsDuplicateNameException()
        {
            //Arrange
            var createMarketDto = MarketsFixtures.CreateMarketDto();
            var mapped = _mapper.Map<Market>(createMarketDto);
            
            _mockUnitOfWork.Setup(u => u.MarketRepository.IsExistAsync(b => b.MarketName.Trim() == createMarketDto.MarketName.Trim())).ReturnsAsync(true);
            
            var mockMarketService = new MarketService(_mockUnitOfWork.Object, _mapper);
            
            //Act and Assert
            await Assert.ThrowsAsync<DuplicateNameException>(async ()=> await mockMarketService.CreateAsync(createMarketDto));
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void UpdateAsync_OnSuccess_ReturnsTrue(int id)
        {
            //Arrange
            var updateMarketDto = MarketsFixtures.UpdateMarketDto();
            var market = _markets.FirstOrDefault(m => m.Id == id);
            var mapped = _mapper.Map(updateMarketDto, market);
            
            _mockUnitOfWork.Setup(u => u.MarketRepository.GetByIdAsyncForAll(id)).ReturnsAsync(market);
            _mockUnitOfWork.Setup(u => u.MarketRepository.IsExistAsync(b => b.MarketName.Trim() == updateMarketDto.MarketName.Trim())).ReturnsAsync(false);
            _mockUnitOfWork.Setup(u => u.MarketRepository.UpdateAsync(mapped)).Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

            var mockMarketService = new MarketService(_mockUnitOfWork.Object, _mapper);
            
            //Act

            var result = await mockMarketService.UpdateAsync(id, updateMarketDto);


            //Assert
            result.Should().BeTrue();
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void UpdateAsync_OnFailure_ReturnsFalse(int id)
        {
            //Arrange
            var updateMarketDto = MarketsFixtures.UpdateMarketDto();
            var market = _markets.FirstOrDefault(m => m.Id == id);
            var mapped = _mapper.Map(updateMarketDto, market);
            
            
            
            _mockUnitOfWork.Setup(u => u.MarketRepository.GetByIdAsyncForAll(id)).ReturnsAsync(market);
            _mockUnitOfWork.Setup(u => u.MarketRepository.IsExistAsync(b => b.MarketName.Trim() == updateMarketDto.MarketName.Trim())).ReturnsAsync(false);
            _mockUnitOfWork.Setup(u => u.MarketRepository.UpdateAsync(mapped)).Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(0);
            
            var mockMarketService = new MarketService(_mockUnitOfWork.Object, _mapper);
            
            //Act

            var result = await mockMarketService.UpdateAsync(id, updateMarketDto);


            //Assert
            result.Should().BeFalse();
        }
        
        [Theory]
        [InlineData(100)]
        [InlineData(102)]
        [InlineData(200)]
        public async void UpdateAsync_OnError_ThrowsEntityNotFoundException(int id)
        {
            //Arrange
            var updateMarketDto = MarketsFixtures.UpdateMarketDto();
            var market = _markets.FirstOrDefault(m => m.Id == id);
            var mapped = _mapper.Map(updateMarketDto, market);
            
            _mockUnitOfWork.Setup(u => u.MarketRepository.GetByIdAsyncForAll(id)).ReturnsAsync(market);
            
            var mockMarketService = new MarketService(_mockUnitOfWork.Object, _mapper);
            
            // Act and  Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(async ()=> await mockMarketService.UpdateAsync(id, updateMarketDto));
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void UpdateAsync_OnError_ThrowsDuplicateNameException(int id)
        {
            //Arrange
            var updateMarketDto = MarketsFixtures.UpdateMarketDto();
            var market = _markets.FirstOrDefault(m => m.Id == id);
            var mapped = _mapper.Map(updateMarketDto, market);
            
            _mockUnitOfWork.Setup(u => u.MarketRepository.GetByIdAsyncForAll(id)).ReturnsAsync(market);
            _mockUnitOfWork.Setup(u => u.MarketRepository.IsExistAsync(b => b.MarketName.Trim() == updateMarketDto.MarketName.Trim())).ReturnsAsync(true);
            var mockMarketService = new MarketService(_mockUnitOfWork.Object, _mapper);
            
            // Act and  Assert
            await Assert.ThrowsAsync<DuplicateNameException>(async ()=> await mockMarketService.UpdateAsync(id, updateMarketDto));
        }
        
    }
}

