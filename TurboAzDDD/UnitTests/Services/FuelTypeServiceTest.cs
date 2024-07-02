using Domain.DTOs.FuelType;


namespace UnitTests.Services;

public class FuelTypeServiceTest
{
         private IMapper _mapper { get; set; }
        private Mock<IUnitOfWork> _mockUnitOfWork { get; set; }
        private List<FuelType> _fuelTypes { get; set; }


        public FuelTypeServiceTest()
        {
            var mockMapper = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
            var mapper = mockMapper.CreateMapper();
            _mapper = mapper;
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _fuelTypes = FuelTypesFixtures.FuelTypes();

        }

        [Fact]
        public async void GetAllAsync_OnSuccess_ReturnsListOfFuelTypeDtos()
        {
            //Arrange
            _mockUnitOfWork.Setup(u => u.FuelTypeRepository.GetAllAsync()).ReturnsAsync(_fuelTypes);
            
            var mockFuelTypeService = new FuelTypeService(_mockUnitOfWork.Object, _mapper);

            //Act

            var result = await mockFuelTypeService.GetAllAsync();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<GetFuelTypeDto>>();
            // result.Should().BeEquivalentTo(fuelTypeDtos);
            result.Should().HaveCount(3);

        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void GetOneAsync_OnSuccess_ReturnsFuelTypeDto(int id)
        {
            //Arrange
            var fuelType = _fuelTypes.FirstOrDefault(m => m.Id == id);
            
            _mockUnitOfWork.Setup(u => u.FuelTypeRepository.GetByIdAsync(id)).ReturnsAsync(fuelType);
            var mockFuelTypeService = new FuelTypeService(_mockUnitOfWork.Object, _mapper);

            //Act

            var result = await mockFuelTypeService.GetOneAsync(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<GetFuelTypeDto>();
            // result.Should().BeEquivalentTo(fuelTypeDtos);
            
        }
        
        [Theory]
        [InlineData(100)]
        [InlineData(102)]
        [InlineData(200)]
        public async void GetOneAsync_OnError_ThrowsEntityNotFoundException(int fuelTypeId)
        {
            //Arrange
            var fuelType = _fuelTypes.FirstOrDefault(m => m.Id == fuelTypeId);
            
            _mockUnitOfWork.Setup(u => u.FuelTypeRepository.GetByIdAsync(fuelTypeId)).ReturnsAsync(fuelType);
            var mockFuelTypeService = new FuelTypeService(_mockUnitOfWork.Object, _mapper);

            //Act and Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(async ()=> await mockFuelTypeService.GetOneAsync(fuelTypeId));

        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void DeleteAsync_OnSuccess_ReturnsTrue(int fuelTypeId)
        {
            //Arrange
            var fuelType = _fuelTypes.FirstOrDefault(m => m.Id == fuelTypeId);

            _mockUnitOfWork.Setup(u => u.FuelTypeRepository.GetByIdAsync(fuelTypeId)).ReturnsAsync(fuelType);
            _mockUnitOfWork.Setup(u => u.FuelTypeRepository.DeleteAsync(fuelType)).Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

            var mockFuelTypeService = new FuelTypeService(_mockUnitOfWork.Object, _mapper);
            //Act

            var result = await mockFuelTypeService.DeleteAsync(fuelTypeId);


            //Assert
            result.Should().BeTrue();
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void DeleteAsync_OnFailure_ReturnsFalse(int fuelTypeId)
        {
            //Arrange
            var fuelType = _fuelTypes.FirstOrDefault(m => m.Id == fuelTypeId);

            _mockUnitOfWork.Setup(u => u.FuelTypeRepository.GetByIdAsync(fuelTypeId)).ReturnsAsync(fuelType);
            _mockUnitOfWork.Setup(u => u.FuelTypeRepository.DeleteAsync(fuelType)).Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(0);

            var mockFuelTypeService = new FuelTypeService(_mockUnitOfWork.Object, _mapper);
            
            //Act

            var result = await mockFuelTypeService.DeleteAsync(fuelTypeId);


            //Assert
            result.Should().BeFalse();
        }
        
        [Theory]
        [InlineData(100)]
        [InlineData(102)]
        [InlineData(200)]
        public async void DeleteAsync_OnError_ThrowsEntityNotFoundException(int fuelTypeId)
        {
            //Arrange
            var fuelType = _fuelTypes.FirstOrDefault(m => m.Id == fuelTypeId);

            _mockUnitOfWork.Setup(u => u.FuelTypeRepository.GetByIdAsync(fuelTypeId)).ReturnsAsync(fuelType);
          
            var mockFuelTypeService = new FuelTypeService(_mockUnitOfWork.Object, _mapper);
            
            //Act and Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(async ()=> await mockFuelTypeService.DeleteAsync(fuelTypeId));
        }
        
        [Fact]
        public async void CreateAsync_OnSuccess_ReturnsTrue()
        {
            //Arrange
            var createFuelTypeDto = new CreateFuelTypeDto() {FuelTypeName = "Hibrid"};
            var mapped = _mapper.Map<FuelType>(createFuelTypeDto);
            
            _mockUnitOfWork.Setup(u => u.FuelTypeRepository.IsExistAsync(It.IsAny<Expression<Func<FuelType, bool>>>()))
                .ReturnsAsync((Expression<Func<FuelType, bool>> predicate) => _fuelTypes.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.FuelTypeRepository.CreateAsync(mapped))
                .Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.CompleteAsync())
                .ReturnsAsync(1);

            var mockFuelTypeService = new FuelTypeService(_mockUnitOfWork.Object, _mapper);
            
            //Act

            var result = await mockFuelTypeService.CreateAsync(createFuelTypeDto);


            //Assert
            result.Should().BeTrue();
        }
        
        [Fact]
        public async void CreateAsync_OnFailure_ReturnsFalse()
        {
            //Arrange
            var createFuelTypeDto = new CreateFuelTypeDto() {FuelTypeName = "Hibrid"};
            var mapped = _mapper.Map<FuelType>(createFuelTypeDto);
            
            _mockUnitOfWork.Setup(u => u.FuelTypeRepository.IsExistAsync(It.IsAny<Expression<Func<FuelType, bool>>>()))
                .ReturnsAsync((Expression<Func<FuelType, bool>> predicate) => _fuelTypes.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.FuelTypeRepository.CreateAsync(mapped))
                .Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.CompleteAsync())
                .ReturnsAsync(0);

            var mockFuelTypeService = new FuelTypeService(_mockUnitOfWork.Object, _mapper);
            
            //Act

            var result = await mockFuelTypeService.CreateAsync(createFuelTypeDto);


            //Assert
            result.Should().BeFalse();
        }
        
        [Fact]
        public async void CreateAsync_OnError_ThrowsDuplicateNameException()
        {
            //Arrange
            var createFuelTypeDto = new CreateFuelTypeDto() {FuelTypeName = "Benzin"};
            
            _mockUnitOfWork.Setup(u => u.FuelTypeRepository.IsExistAsync(It.IsAny<Expression<Func<FuelType, bool>>>()))
                .ReturnsAsync((Expression<Func<FuelType, bool>> predicate) => _fuelTypes.Any(predicate.Compile()));
            
            var mockFuelTypeService = new FuelTypeService(_mockUnitOfWork.Object, _mapper);
            
            //Act and Assert
            await Assert.ThrowsAsync<DuplicateNameException>(async ()=> await mockFuelTypeService.CreateAsync(createFuelTypeDto));
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void UpdateAsync_OnSuccess_ReturnsTrue(int id)
        {
            //Arrange
            var updateFuelTypeDto = new UpdateFuelTypeDto() {FuelTypeName = "Hibrid", IsDeleted = false};
            var fuelType = _fuelTypes.FirstOrDefault(m => m.Id == id);
            var mapped = _mapper.Map<FuelType>(updateFuelTypeDto);

            _mockUnitOfWork.Setup(u => u.FuelTypeRepository.GetByIdAsyncForAll(id)).ReturnsAsync(fuelType);
            _mockUnitOfWork.Setup(u => u.FuelTypeRepository.IsExistAsync(It.IsAny<Expression<Func<FuelType, bool>>>()))
                .ReturnsAsync((Expression<Func<FuelType, bool>> predicate) => _fuelTypes.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.FuelTypeRepository.UpdateAsync(mapped))
                .Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.CompleteAsync())
                .ReturnsAsync(1);

            var mockFuelTypeService = new FuelTypeService(_mockUnitOfWork.Object, _mapper);
            
            //Act

            var result = await mockFuelTypeService.UpdateAsync(id, updateFuelTypeDto);


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
            var updateFuelTypeDto = new UpdateFuelTypeDto() {FuelTypeName = "Hibrid", IsDeleted = false};
            var fuelType = _fuelTypes.FirstOrDefault(m => m.Id == id);
            var mapped = _mapper.Map<FuelType>(updateFuelTypeDto);
            
            _mockUnitOfWork.Setup(u => u.FuelTypeRepository.GetByIdAsyncForAll(id)).ReturnsAsync(fuelType);
            _mockUnitOfWork.Setup(u => u.FuelTypeRepository.IsExistAsync(It.IsAny<Expression<Func<FuelType, bool>>>()))
                .ReturnsAsync((Expression<Func<FuelType, bool>> predicate) => _fuelTypes.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.FuelTypeRepository.UpdateAsync(mapped)).Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(0);
            
            var mockFuelTypeService = new FuelTypeService(_mockUnitOfWork.Object, _mapper);
            
            //Act

            var result = await mockFuelTypeService.UpdateAsync(id, updateFuelTypeDto);


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
            var updateFuelTypeDto = new UpdateFuelTypeDto() {FuelTypeName = "Hibrid", IsDeleted = false};
            var fuelType = _fuelTypes.FirstOrDefault(m => m.Id == id);
            
            _mockUnitOfWork.Setup(u => u.FuelTypeRepository.GetByIdAsyncForAll(id))
                .ReturnsAsync(fuelType);
            
            var mockFuelTypeService = new FuelTypeService(_mockUnitOfWork.Object, _mapper);
            
            // Act and  Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(async ()=> await mockFuelTypeService.UpdateAsync(id, updateFuelTypeDto));
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void UpdateAsync_OnError_ThrowsDuplicateNameException(int id)
        {
            //Arrange
            var updateFuelTypeDto = new UpdateFuelTypeDto() {FuelTypeName = "Benzin", IsDeleted = false};
            var fuelType = _fuelTypes.FirstOrDefault(m => m.Id == id);
            
            _mockUnitOfWork.Setup(u => u.FuelTypeRepository.GetByIdAsyncForAll(id))
                .ReturnsAsync(fuelType);
            _mockUnitOfWork.Setup(u => u.FuelTypeRepository.IsExistAsync(It.IsAny<Expression<Func<FuelType, bool>>>()))
                .ReturnsAsync((Expression<Func<FuelType, bool>> predicate) => _fuelTypes.Any(predicate.Compile()));
            
            var mockFuelTypeService = new FuelTypeService(_mockUnitOfWork.Object, _mapper);
            
            // Act and  Assert
            await Assert.ThrowsAsync<DuplicateNameException>(async ()=> await mockFuelTypeService.UpdateAsync(id, updateFuelTypeDto));
        }
}