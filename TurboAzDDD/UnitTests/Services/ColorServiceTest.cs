using Application.Exceptions;
using Application.Services;
using AutoMapper;
using Domain;
using Domain.DTOs.Color;
using Domain.Entities;
using Domain.Exceptions;
using FluentAssertions;
using Infrastructure.Mapper;
using Moq;
using UnitTests.Fixtures;

namespace UnitTests.Services;

public class ColorServiceTest
{
        private IMapper _mapper { get; set; }
        private Mock<IUnitOfWork> _mockUnitOfWork { get; set; }
        private List<Color> _colors { get; set; }


        public ColorServiceTest()
        {
            var mockMapper = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
            var mapper = mockMapper.CreateMapper();
            _mapper = mapper;
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _colors = ColorsFixtures.Colors();

        }

        [Fact]
        public async void GetAllAsync_OnSuccess_ReturnsListOfColorDtos()
        {
            //Arrange
            _mockUnitOfWork.Setup(u => u.ColorRepository.GetAllAsync()).ReturnsAsync(_colors);
            
            var mockColorService = new ColorService(_mockUnitOfWork.Object, _mapper);

            //Act

            var result = await mockColorService.GetAllAsync();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<GetColorDto>>();
            // result.Should().BeEquivalentTo(colorDtos);
            result.Should().HaveCount(3);

        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void GetOneAsync_OnSuccess_ReturnsColorDto(int id)
        {
            //Arrange
            var color = _colors.FirstOrDefault(m => m.Id == id);
            
            _mockUnitOfWork.Setup(u => u.ColorRepository.GetByIdAsync(id)).ReturnsAsync(color);
            var mockColorService = new ColorService(_mockUnitOfWork.Object, _mapper);

            //Act

            var result = await mockColorService.GetOneAsync(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<GetColorDto>();
            // result.Should().BeEquivalentTo(colorDtos);
            
        }
        
        [Theory]
        [InlineData(100)]
        [InlineData(102)]
        [InlineData(200)]
        public async void GetOneAsync_OnError_ThrowsEntityNotFoundException(int colorId)
        {
            //Arrange
            var color = _colors.FirstOrDefault(m => m.Id == colorId);
            
            _mockUnitOfWork.Setup(u => u.ColorRepository.GetByIdAsync(colorId)).ReturnsAsync(color);
            var mockColorService = new ColorService(_mockUnitOfWork.Object, _mapper);

            //Act and Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(async ()=> await mockColorService.GetOneAsync(colorId));

        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void DeleteAsync_OnSuccess_ReturnsTrue(int colorId)
        {
            //Arrange
            var color = _colors.FirstOrDefault(m => m.Id == colorId);

            _mockUnitOfWork.Setup(u => u.ColorRepository.GetByIdAsync(colorId)).ReturnsAsync(color);
            _mockUnitOfWork.Setup(u => u.ColorRepository.DeleteAsync(color)).Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

            var mockColorService = new ColorService(_mockUnitOfWork.Object, _mapper);
            //Act

            var result = await mockColorService.DeleteAsync(colorId);


            //Assert
            result.Should().BeTrue();
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void DeleteAsync_OnFailure_ReturnsFalse(int colorId)
        {
            //Arrange
            var color = _colors.FirstOrDefault(m => m.Id == colorId);

            _mockUnitOfWork.Setup(u => u.ColorRepository.GetByIdAsync(colorId)).ReturnsAsync(color);
            _mockUnitOfWork.Setup(u => u.ColorRepository.DeleteAsync(color)).Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(0);

            var mockColorService = new ColorService(_mockUnitOfWork.Object, _mapper);
            
            //Act

            var result = await mockColorService.DeleteAsync(colorId);


            //Assert
            result.Should().BeFalse();
        }
        
        [Theory]
        [InlineData(100)]
        [InlineData(102)]
        [InlineData(200)]
        public async void DeleteAsync_OnError_ThrowsEntityNotFoundException(int colorId)
        {
            //Arrange
            var color = _colors.FirstOrDefault(m => m.Id == colorId);

            _mockUnitOfWork.Setup(u => u.ColorRepository.GetByIdAsync(colorId)).ReturnsAsync(color);
          
            var mockColorService = new ColorService(_mockUnitOfWork.Object, _mapper);
            
            //Act and Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(async ()=> await mockColorService.DeleteAsync(colorId));
        }
        
        [Fact]
        public async void CreateAsync_OnSuccess_ReturnsTrue()
        {
            //Arrange
            var createColorDto = ColorsFixtures.CreateColorDto();
            var mapped = _mapper.Map<Color>(createColorDto);
            
            _mockUnitOfWork.Setup(u => u.ColorRepository.IsExistAsync(b => b.ColorName.Trim() == createColorDto.ColorName.Trim())).ReturnsAsync(false);
            _mockUnitOfWork.Setup(u => u.ColorRepository.CreateAsync(mapped)).Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

            var mockColorService = new ColorService(_mockUnitOfWork.Object, _mapper);
            
            //Act

            var result = await mockColorService.CreateAsync(createColorDto);


            //Assert
            result.Should().BeTrue();
        }
        
        [Fact]
        public async void CreateAsync_OnFailure_ReturnsFalse()
        {
            //Arrange
            var createColorDto = ColorsFixtures.CreateColorDto();
            var mapped = _mapper.Map<Color>(createColorDto);
            
            _mockUnitOfWork.Setup(u => u.ColorRepository.IsExistAsync(b => b.ColorName.Trim() == createColorDto.ColorName.Trim())).ReturnsAsync(false);
            _mockUnitOfWork.Setup(u => u.ColorRepository.CreateAsync(mapped)).Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(0);

            var mockColorService = new ColorService(_mockUnitOfWork.Object, _mapper);
            
            //Act

            var result = await mockColorService.CreateAsync(createColorDto);


            //Assert
            result.Should().BeFalse();
        }
        
        [Fact]
        public async void CreateAsync_OnError_ThrowsDuplicateNameException()
        {
            //Arrange
            var createColorDto = ColorsFixtures.CreateColorDto();
            var mapped = _mapper.Map<Color>(createColorDto);
            
            _mockUnitOfWork.Setup(u => u.ColorRepository.IsExistAsync(b => b.ColorName.Trim() == createColorDto.ColorName.Trim())).ReturnsAsync(true);
            
            var mockColorService = new ColorService(_mockUnitOfWork.Object, _mapper);
            
            //Act and Assert
            await Assert.ThrowsAsync<DuplicateNameException>(async ()=> await mockColorService.CreateAsync(createColorDto));
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void UpdateAsync_OnSuccess_ReturnsTrue(int id)
        {
            //Arrange
            var updateColorDto = ColorsFixtures.UpdateColorDto();
            var color = _colors.FirstOrDefault(m => m.Id == id);
            var mapped = _mapper.Map(updateColorDto, color);
            
            _mockUnitOfWork.Setup(u => u.ColorRepository.GetByIdAsyncForAll(id)).ReturnsAsync(color);
            _mockUnitOfWork.Setup(u => u.ColorRepository.IsExistAsync(b => b.ColorName.Trim() == updateColorDto.ColorName.Trim())).ReturnsAsync(false);
            _mockUnitOfWork.Setup(u => u.ColorRepository.UpdateAsync(mapped)).Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

            var mockColorService = new ColorService(_mockUnitOfWork.Object, _mapper);
            
            //Act

            var result = await mockColorService.UpdateAsync(id, updateColorDto);


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
            var updateColorDto = ColorsFixtures.UpdateColorDto();
            var color = _colors.FirstOrDefault(m => m.Id == id);
            var mapped = _mapper.Map(updateColorDto, color);
            
            
            
            _mockUnitOfWork.Setup(u => u.ColorRepository.GetByIdAsyncForAll(id)).ReturnsAsync(color);
            _mockUnitOfWork.Setup(u => u.ColorRepository.IsExistAsync(b => b.ColorName.Trim() == updateColorDto.ColorName.Trim())).ReturnsAsync(false);
            _mockUnitOfWork.Setup(u => u.ColorRepository.UpdateAsync(mapped)).Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(0);
            
            var mockColorService = new ColorService(_mockUnitOfWork.Object, _mapper);
            
            //Act

            var result = await mockColorService.UpdateAsync(id, updateColorDto);


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
            var updateColorDto = ColorsFixtures.UpdateColorDto();
            var color = _colors.FirstOrDefault(m => m.Id == id);
            var mapped = _mapper.Map(updateColorDto, color);
            
            _mockUnitOfWork.Setup(u => u.ColorRepository.GetByIdAsyncForAll(id)).ReturnsAsync(color);
            
            var mockColorService = new ColorService(_mockUnitOfWork.Object, _mapper);
            
            // Act and  Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(async ()=> await mockColorService.UpdateAsync(id, updateColorDto));
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void UpdateAsync_OnError_ThrowsDuplicateNameException(int id)
        {
            //Arrange
            var updateColorDto = ColorsFixtures.UpdateColorDto();
            var color = _colors.FirstOrDefault(m => m.Id == id);
            var mapped = _mapper.Map(updateColorDto, color);
            
            _mockUnitOfWork.Setup(u => u.ColorRepository.GetByIdAsyncForAll(id)).ReturnsAsync(color);
            _mockUnitOfWork.Setup(u => u.ColorRepository.IsExistAsync(b => b.ColorName.Trim() == updateColorDto.ColorName.Trim())).ReturnsAsync(true);
            var mockColorService = new ColorService(_mockUnitOfWork.Object, _mapper);
            
            // Act and  Assert
            await Assert.ThrowsAsync<DuplicateNameException>(async ()=> await mockColorService.UpdateAsync(id, updateColorDto));
        }
}