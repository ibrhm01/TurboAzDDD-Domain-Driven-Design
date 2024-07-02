using Domain.DTOs.DriveType;
using DriveType = Domain.Entities.DriveType;


namespace UnitTests.Services;

public class DriveTypeServiceTest
{
        private IMapper _mapper { get; set; }
        private Mock<IUnitOfWork> _mockUnitOfWork { get; set; }
        private List<DriveType> _driveTypes { get; set; }


        public DriveTypeServiceTest()
        {
            var mockMapper = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
            var mapper = mockMapper.CreateMapper();
            _mapper = mapper;
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _driveTypes = DriveTypesFixtures.DriveTypes();

        }

        [Fact]
        public async void GetAllAsync_OnSuccess_ReturnsListOfDriveTypeDtos()
        {
            //Arrange
            _mockUnitOfWork.Setup(u => u.DriveTypeRepository.GetAllAsync()).ReturnsAsync(_driveTypes);
            
            var mockDriveTypeService = new DriveTypeService(_mockUnitOfWork.Object, _mapper);

            //Act

            var result = await mockDriveTypeService.GetAllAsync();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<GetDriveTypeDto>>();
            // result.Should().BeEquivalentTo(driveTypeDtos);
            result.Should().HaveCount(2);

        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async void GetOneAsync_OnSuccess_ReturnsDriveTypeDto(int id)
        {
            //Arrange
            var driveType = _driveTypes.FirstOrDefault(m => m.Id == id);
            
            _mockUnitOfWork.Setup(u => u.DriveTypeRepository.GetByIdAsync(id)).ReturnsAsync(driveType);
            var mockDriveTypeService = new DriveTypeService(_mockUnitOfWork.Object, _mapper);

            //Act

            var result = await mockDriveTypeService.GetOneAsync(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<GetDriveTypeDto>();
            // result.Should().BeEquivalentTo(driveTypeDtos);
            
        }
        
        [Theory]
        [InlineData(100)]
        [InlineData(102)]
        [InlineData(200)]
        public async void GetOneAsync_OnError_ThrowsEntityNotFoundException(int driveTypeId)
        {
            //Arrange
            var driveType = _driveTypes.FirstOrDefault(m => m.Id == driveTypeId);
            
            _mockUnitOfWork.Setup(u => u.DriveTypeRepository.GetByIdAsync(driveTypeId)).ReturnsAsync(driveType);
            var mockDriveTypeService = new DriveTypeService(_mockUnitOfWork.Object, _mapper);

            //Act and Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(async ()=> await mockDriveTypeService.GetOneAsync(driveTypeId));

        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async void DeleteAsync_OnSuccess_ReturnsTrue(int driveTypeId)
        {
            //Arrange
            var driveType = _driveTypes.FirstOrDefault(m => m.Id == driveTypeId);

            _mockUnitOfWork.Setup(u => u.DriveTypeRepository.GetByIdAsync(driveTypeId)).ReturnsAsync(driveType);
            _mockUnitOfWork.Setup(u => u.DriveTypeRepository.DeleteAsync(driveType)).Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

            var mockDriveTypeService = new DriveTypeService(_mockUnitOfWork.Object, _mapper);
            //Act

            var result = await mockDriveTypeService.DeleteAsync(driveTypeId);


            //Assert
            result.Should().BeTrue();
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async void DeleteAsync_OnFailure_ReturnsFalse(int driveTypeId)
        {
            //Arrange
            var driveType = _driveTypes.FirstOrDefault(m => m.Id == driveTypeId);

            _mockUnitOfWork.Setup(u => u.DriveTypeRepository.GetByIdAsync(driveTypeId)).ReturnsAsync(driveType);
            _mockUnitOfWork.Setup(u => u.DriveTypeRepository.DeleteAsync(driveType)).Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(0);

            var mockDriveTypeService = new DriveTypeService(_mockUnitOfWork.Object, _mapper);
            
            //Act

            var result = await mockDriveTypeService.DeleteAsync(driveTypeId);


            //Assert
            result.Should().BeFalse();
        }
        
        [Theory]
        [InlineData(100)]
        [InlineData(102)]
        [InlineData(200)]
        public async void DeleteAsync_OnError_ThrowsEntityNotFoundException(int driveTypeId)
        {
            //Arrange
            var driveType = _driveTypes.FirstOrDefault(m => m.Id == driveTypeId);

            _mockUnitOfWork.Setup(u => u.DriveTypeRepository.GetByIdAsync(driveTypeId)).ReturnsAsync(driveType);
          
            var mockDriveTypeService = new DriveTypeService(_mockUnitOfWork.Object, _mapper);
            
            //Act and Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(async ()=> await mockDriveTypeService.DeleteAsync(driveTypeId));
        }
        
        [Fact]
        public async void CreateAsync_OnSuccess_ReturnsTrue()
        {
            //Arrange
            var createDriveTypeDto = new CreateDriveTypeDto() {DriveTypeName = "Tam"};
            var mapped = _mapper.Map<DriveType>(createDriveTypeDto);
            
            _mockUnitOfWork.Setup(u => u.DriveTypeRepository.IsExistAsync(It.IsAny<Expression<Func<DriveType, bool>>>()))
                .ReturnsAsync((Expression<Func<DriveType, bool>> predicate) => _driveTypes.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.DriveTypeRepository.CreateAsync(mapped))
                .Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.CompleteAsync())
                .ReturnsAsync(1);

            var mockDriveTypeService = new DriveTypeService(_mockUnitOfWork.Object, _mapper);
            
            //Act

            var result = await mockDriveTypeService.CreateAsync(createDriveTypeDto);


            //Assert
            result.Should().BeTrue();
        }
        
        [Fact]
        public async void CreateAsync_OnFailure_ReturnsFalse()
        {
            //Arrange
            var createDriveTypeDto = new CreateDriveTypeDto() {DriveTypeName = "Tam"};
            var mapped = _mapper.Map<DriveType>(createDriveTypeDto);
            
            _mockUnitOfWork.Setup(u => u.DriveTypeRepository.IsExistAsync(It.IsAny<Expression<Func<DriveType, bool>>>()))
                .ReturnsAsync((Expression<Func<DriveType, bool>> predicate) => _driveTypes.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.DriveTypeRepository.CreateAsync(mapped))
                .Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.CompleteAsync())
                .ReturnsAsync(0);

            var mockDriveTypeService = new DriveTypeService(_mockUnitOfWork.Object, _mapper);
            
            //Act

            var result = await mockDriveTypeService.CreateAsync(createDriveTypeDto);


            //Assert
            result.Should().BeFalse();
        }
        
        [Fact]
        public async void CreateAsync_OnError_ThrowsDuplicateNameException()
        {
            //Arrange
            var createDriveTypeDto = new CreateDriveTypeDto() {DriveTypeName = "Arxa"};
            
            _mockUnitOfWork.Setup(u => u.DriveTypeRepository.IsExistAsync(It.IsAny<Expression<Func<DriveType, bool>>>()))
                .ReturnsAsync((Expression<Func<DriveType, bool>> predicate) => _driveTypes.Any(predicate.Compile()));
            
            var mockDriveTypeService = new DriveTypeService(_mockUnitOfWork.Object, _mapper);
            
            //Act and Assert
            await Assert.ThrowsAsync<DuplicateNameException>(async ()=> await mockDriveTypeService.CreateAsync(createDriveTypeDto));
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async void UpdateAsync_OnSuccess_ReturnsTrue(int id)
        {
            //Arrange
            var updateDriveTypeDto = new UpdateDriveTypeDto() {DriveTypeName = "Tam", IsDeleted = false};
            var driveType = _driveTypes.FirstOrDefault(m => m.Id == id);
            var mapped = _mapper.Map<DriveType>(updateDriveTypeDto);

            _mockUnitOfWork.Setup(u => u.DriveTypeRepository.GetByIdAsyncForAll(id)).ReturnsAsync(driveType);
            _mockUnitOfWork.Setup(u => u.DriveTypeRepository.IsExistAsync(It.IsAny<Expression<Func<DriveType, bool>>>()))
                .ReturnsAsync((Expression<Func<DriveType, bool>> predicate) => _driveTypes.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.DriveTypeRepository.UpdateAsync(mapped))
                .Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.CompleteAsync())
                .ReturnsAsync(1);

            var mockDriveTypeService = new DriveTypeService(_mockUnitOfWork.Object, _mapper);
            
            //Act

            var result = await mockDriveTypeService.UpdateAsync(id, updateDriveTypeDto);


            //Assert
            result.Should().BeTrue();
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async void UpdateAsync_OnFailure_ReturnsFalse(int id)
        {
            //Arrange
            var updateDriveTypeDto = new UpdateDriveTypeDto() {DriveTypeName = "Tam", IsDeleted = false};
            var driveType = _driveTypes.FirstOrDefault(m => m.Id == id);
            var mapped = _mapper.Map<DriveType>(updateDriveTypeDto);
            
            _mockUnitOfWork.Setup(u => u.DriveTypeRepository.GetByIdAsyncForAll(id)).ReturnsAsync(driveType);
            _mockUnitOfWork.Setup(u => u.DriveTypeRepository.IsExistAsync(It.IsAny<Expression<Func<DriveType, bool>>>()))
                .ReturnsAsync((Expression<Func<DriveType, bool>> predicate) => _driveTypes.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.DriveTypeRepository.UpdateAsync(mapped)).Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(0);
            
            var mockDriveTypeService = new DriveTypeService(_mockUnitOfWork.Object, _mapper);
            
            //Act

            var result = await mockDriveTypeService.UpdateAsync(id, updateDriveTypeDto);


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
            var updateDriveTypeDto = new UpdateDriveTypeDto() {DriveTypeName = "Tam", IsDeleted = false};
            var driveType = _driveTypes.FirstOrDefault(m => m.Id == id);
            
            _mockUnitOfWork.Setup(u => u.DriveTypeRepository.GetByIdAsyncForAll(id))
                .ReturnsAsync(driveType);
            
            var mockDriveTypeService = new DriveTypeService(_mockUnitOfWork.Object, _mapper);
            
            // Act and  Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(async ()=> await mockDriveTypeService.UpdateAsync(id, updateDriveTypeDto));
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async void UpdateAsync_OnError_ThrowsDuplicateNameException(int id)
        {
            //Arrange
            var updateDriveTypeDto = new UpdateDriveTypeDto() {DriveTypeName = "Arxa", IsDeleted = false};
            var driveType = _driveTypes.FirstOrDefault(m => m.Id == id);
            
            _mockUnitOfWork.Setup(u => u.DriveTypeRepository.GetByIdAsyncForAll(id))
                .ReturnsAsync(driveType);
            _mockUnitOfWork.Setup(u => u.DriveTypeRepository.IsExistAsync(It.IsAny<Expression<Func<DriveType, bool>>>()))
                .ReturnsAsync((Expression<Func<DriveType, bool>> predicate) => _driveTypes.Any(predicate.Compile()));
            
            var mockDriveTypeService = new DriveTypeService(_mockUnitOfWork.Object, _mapper);
            
            // Act and  Assert
            await Assert.ThrowsAsync<DuplicateNameException>(async ()=> await mockDriveTypeService.UpdateAsync(id, updateDriveTypeDto));
        }
}