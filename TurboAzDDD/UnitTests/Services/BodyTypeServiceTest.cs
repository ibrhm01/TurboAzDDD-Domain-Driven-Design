using Domain.DTOs.BodyType;

namespace UnitTests.Services;

public class BodyTypeServiceTest
{
     private IMapper _mapper { get; set; }
        private Mock<IUnitOfWork> _mockUnitOfWork { get; set; }
        private List<BodyType> _bodyTypes { get; set; }


        public BodyTypeServiceTest()
        {
            var mockMapper = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
            var mapper = mockMapper.CreateMapper();
            _mapper = mapper;
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _bodyTypes = BodyTypesFixtures.BodyTypes();

        }

        [Fact]
        public async void GetAllAsync_OnSuccess_ReturnsListOfBodyTypeDtos()
        {
            //Arrange
            _mockUnitOfWork.Setup(u => u.BodyTypeRepository.GetAllAsync()).ReturnsAsync(_bodyTypes);
            
            var mockBodyTypeService = new BodyTypeService(_mockUnitOfWork.Object, _mapper);

            //Act

            var result = await mockBodyTypeService.GetAllAsync();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<GetBodyTypeDto>>();
            // result.Should().BeEquivalentTo(bodyTypeDtos);
            result.Should().HaveCount(3);

        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void GetOneAsync_OnSuccess_ReturnsBodyTypeDto(int id)
        {
            //Arrange
            var bodyType = _bodyTypes.FirstOrDefault(m => m.Id == id);
            
            _mockUnitOfWork.Setup(u => u.BodyTypeRepository.GetByIdAsync(id)).ReturnsAsync(bodyType);
            var mockBodyTypeService = new BodyTypeService(_mockUnitOfWork.Object, _mapper);

            //Act

            var result = await mockBodyTypeService.GetOneAsync(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<GetBodyTypeDto>();
            // result.Should().BeEquivalentTo(bodyTypeDtos);
            
        }
        
        [Theory]
        [InlineData(100)]
        [InlineData(102)]
        [InlineData(200)]
        public async void GetOneAsync_OnError_ThrowsEntityNotFoundException(int bodyTypeId)
        {
            //Arrange
            var bodyType = _bodyTypes.FirstOrDefault(m => m.Id == bodyTypeId);
            
            _mockUnitOfWork.Setup(u => u.BodyTypeRepository.GetByIdAsync(bodyTypeId)).ReturnsAsync(bodyType);
            var mockBodyTypeService = new BodyTypeService(_mockUnitOfWork.Object, _mapper);

            //Act and Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(async ()=> await mockBodyTypeService.GetOneAsync(bodyTypeId));

        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void DeleteAsync_OnSuccess_ReturnsTrue(int bodyTypeId)
        {
            //Arrange
            var bodyType = _bodyTypes.FirstOrDefault(m => m.Id == bodyTypeId);

            _mockUnitOfWork.Setup(u => u.BodyTypeRepository.GetByIdAsync(bodyTypeId)).ReturnsAsync(bodyType);
            _mockUnitOfWork.Setup(u => u.BodyTypeRepository.DeleteAsync(bodyType)).Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

            var mockBodyTypeService = new BodyTypeService(_mockUnitOfWork.Object, _mapper);
            //Act

            var result = await mockBodyTypeService.DeleteAsync(bodyTypeId);


            //Assert
            result.Should().BeTrue();
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void DeleteAsync_OnFailure_ReturnsFalse(int bodyTypeId)
        {
            //Arrange
            var bodyType = _bodyTypes.FirstOrDefault(m => m.Id == bodyTypeId);

            _mockUnitOfWork.Setup(u => u.BodyTypeRepository.GetByIdAsync(bodyTypeId)).ReturnsAsync(bodyType);
            _mockUnitOfWork.Setup(u => u.BodyTypeRepository.DeleteAsync(bodyType)).Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(0);

            var mockBodyTypeService = new BodyTypeService(_mockUnitOfWork.Object, _mapper);
            
            //Act

            var result = await mockBodyTypeService.DeleteAsync(bodyTypeId);


            //Assert
            result.Should().BeFalse();
        }
        
        [Theory]
        [InlineData(100)]
        [InlineData(102)]
        [InlineData(200)]
        public async void DeleteAsync_OnError_ThrowsEntityNotFoundException(int bodyTypeId)
        {
            //Arrange
            var bodyType = _bodyTypes.FirstOrDefault(m => m.Id == bodyTypeId);

            _mockUnitOfWork.Setup(u => u.BodyTypeRepository.GetByIdAsync(bodyTypeId)).ReturnsAsync(bodyType);
          
            var mockBodyTypeService = new BodyTypeService(_mockUnitOfWork.Object, _mapper);
            
            //Act and Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(async ()=> await mockBodyTypeService.DeleteAsync(bodyTypeId));
        }
        
        [Fact]
        public async void CreateAsync_OnSuccess_ReturnsTrue()
        {
            //Arrange
            var createBodyTypeDto = new CreateBodyTypeDto() {BodyTypeName = "Limuzin"};
            var mapped = _mapper.Map<BodyType>(createBodyTypeDto);
            
            _mockUnitOfWork.Setup(u => u.BodyTypeRepository.IsExistAsync(It.IsAny<Expression<Func<BodyType, bool>>>()))
                .ReturnsAsync((Expression<Func<BodyType, bool>> predicate) => _bodyTypes.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.BodyTypeRepository.CreateAsync(mapped))
                .Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.CompleteAsync())
                .ReturnsAsync(1);

            var mockBodyTypeService = new BodyTypeService(_mockUnitOfWork.Object, _mapper);
            
            //Act

            var result = await mockBodyTypeService.CreateAsync(createBodyTypeDto);


            //Assert
            result.Should().BeTrue();
        }
        
        [Fact]
        public async void CreateAsync_OnFailure_ReturnsFalse()
        {
            //Arrange
            var createBodyTypeDto = new CreateBodyTypeDto() {BodyTypeName = "Limuzin"};
            var mapped = _mapper.Map<BodyType>(createBodyTypeDto);
            
            _mockUnitOfWork.Setup(u => u.BodyTypeRepository.IsExistAsync(It.IsAny<Expression<Func<BodyType, bool>>>()))
                .ReturnsAsync((Expression<Func<BodyType, bool>> predicate) => _bodyTypes.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.BodyTypeRepository.CreateAsync(mapped))
                .Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.CompleteAsync())
                .ReturnsAsync(0);

            var mockBodyTypeService = new BodyTypeService(_mockUnitOfWork.Object, _mapper);
            
            //Act

            var result = await mockBodyTypeService.CreateAsync(createBodyTypeDto);


            //Assert
            result.Should().BeFalse();
        }
        
        [Fact]
        public async void CreateAsync_OnError_ThrowsDuplicateNameException()
        {
            //Arrange
            var createBodyTypeDto = new CreateBodyTypeDto() {BodyTypeName = "Avtobus"};
            
            _mockUnitOfWork.Setup(u => u.BodyTypeRepository.IsExistAsync(It.IsAny<Expression<Func<BodyType, bool>>>()))
                .ReturnsAsync((Expression<Func<BodyType, bool>> predicate) => _bodyTypes.Any(predicate.Compile()));
            
            var mockBodyTypeService = new BodyTypeService(_mockUnitOfWork.Object, _mapper);
            
            //Act and Assert
            await Assert.ThrowsAsync<DuplicateNameException>(async ()=> await mockBodyTypeService.CreateAsync(createBodyTypeDto));
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void UpdateAsync_OnSuccess_ReturnsTrue(int id)
        {
            //Arrange
            var updateBodyTypeDto = new UpdateBodyTypeDto() {BodyTypeName = "Limuzin", IsDeleted = false};
            var bodyType = _bodyTypes.FirstOrDefault(m => m.Id == id);
            var mapped = _mapper.Map<BodyType>(updateBodyTypeDto);

            _mockUnitOfWork.Setup(u => u.BodyTypeRepository.GetByIdAsyncForAll(id)).ReturnsAsync(bodyType);
            _mockUnitOfWork.Setup(u => u.BodyTypeRepository.IsExistAsync(It.IsAny<Expression<Func<BodyType, bool>>>()))
                .ReturnsAsync((Expression<Func<BodyType, bool>> predicate) => _bodyTypes.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.BodyTypeRepository.UpdateAsync(mapped))
                .Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.CompleteAsync())
                .ReturnsAsync(1);

            var mockBodyTypeService = new BodyTypeService(_mockUnitOfWork.Object, _mapper);
            
            //Act

            var result = await mockBodyTypeService.UpdateAsync(id, updateBodyTypeDto);


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
            var updateBodyTypeDto = new UpdateBodyTypeDto() {BodyTypeName = "Limuzin", IsDeleted = false};
            var bodyType = _bodyTypes.FirstOrDefault(m => m.Id == id);
            var mapped = _mapper.Map<BodyType>(updateBodyTypeDto);
            
            _mockUnitOfWork.Setup(u => u.BodyTypeRepository.GetByIdAsyncForAll(id)).ReturnsAsync(bodyType);
            _mockUnitOfWork.Setup(u => u.BodyTypeRepository.IsExistAsync(It.IsAny<Expression<Func<BodyType, bool>>>()))
                .ReturnsAsync((Expression<Func<BodyType, bool>> predicate) => _bodyTypes.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.BodyTypeRepository.UpdateAsync(mapped)).Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(0);
            
            var mockBodyTypeService = new BodyTypeService(_mockUnitOfWork.Object, _mapper);
            
            //Act

            var result = await mockBodyTypeService.UpdateAsync(id, updateBodyTypeDto);


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
            var updateBodyTypeDto = new UpdateBodyTypeDto() {BodyTypeName = "Limuzin", IsDeleted = false};
            var bodyType = _bodyTypes.FirstOrDefault(m => m.Id == id);
            
            _mockUnitOfWork.Setup(u => u.BodyTypeRepository.GetByIdAsyncForAll(id))
                .ReturnsAsync(bodyType);
            
            var mockBodyTypeService = new BodyTypeService(_mockUnitOfWork.Object, _mapper);
            
            // Act and  Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(async ()=> await mockBodyTypeService.UpdateAsync(id, updateBodyTypeDto));
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void UpdateAsync_OnError_ThrowsDuplicateNameException(int id)
        {
            //Arrange
            var updateBodyTypeDto = new UpdateBodyTypeDto() {BodyTypeName = "Avtobus", IsDeleted = false};
            var bodyType = _bodyTypes.FirstOrDefault(m => m.Id == id);
            
            _mockUnitOfWork.Setup(u => u.BodyTypeRepository.GetByIdAsyncForAll(id))
                .ReturnsAsync(bodyType);
            _mockUnitOfWork.Setup(u => u.BodyTypeRepository.IsExistAsync(It.IsAny<Expression<Func<BodyType, bool>>>()))
                .ReturnsAsync((Expression<Func<BodyType, bool>> predicate) => _bodyTypes.Any(predicate.Compile()));
            
            var mockBodyTypeService = new BodyTypeService(_mockUnitOfWork.Object, _mapper);
            
            // Act and  Assert
            await Assert.ThrowsAsync<DuplicateNameException>(async ()=> await mockBodyTypeService.UpdateAsync(id, updateBodyTypeDto));
        }
}