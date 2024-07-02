using Domain.DTOs.Transmission;


namespace UnitTests.Services;

public class TransmissionServiceTest
{
     private IMapper _mapper { get; set; }
        private Mock<IUnitOfWork> _mockUnitOfWork { get; set; }
        private List<Transmission> _transmissions { get; set; }


        public TransmissionServiceTest()
        {
            var mockMapper = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
            var mapper = mockMapper.CreateMapper();
            _mapper = mapper;
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _transmissions = TransmissionsFixtures.Transmissions();

        }

        [Fact]
        public async void GetAllAsync_OnSuccess_ReturnsListOfTransmissionDtos()
        {
            //Arrange
            _mockUnitOfWork.Setup(u => u.TransmissionRepository.GetAllAsync()).ReturnsAsync(_transmissions);
            
            var mockTransmissionService = new TransmissionService(_mockUnitOfWork.Object, _mapper);

            //Act

            var result = await mockTransmissionService.GetAllAsync();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<GetTransmissionDto>>();
            // result.Should().BeEquivalentTo(transmissionDtos);
            result.Should().HaveCount(3);

        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void GetOneAsync_OnSuccess_ReturnsTransmissionDto(int id)
        {
            //Arrange
            var transmission = _transmissions.FirstOrDefault(m => m.Id == id);
            
            _mockUnitOfWork.Setup(u => u.TransmissionRepository.GetByIdAsync(id)).ReturnsAsync(transmission);
            var mockTransmissionService = new TransmissionService(_mockUnitOfWork.Object, _mapper);

            //Act

            var result = await mockTransmissionService.GetOneAsync(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<GetTransmissionDto>();
            // result.Should().BeEquivalentTo(transmissionDtos);
            
        }
        
        [Theory]
        [InlineData(100)]
        [InlineData(102)]
        [InlineData(200)]
        public async void GetOneAsync_OnError_ThrowsEntityNotFoundException(int transmissionId)
        {
            //Arrange
            var transmission = _transmissions.FirstOrDefault(m => m.Id == transmissionId);
            
            _mockUnitOfWork.Setup(u => u.TransmissionRepository.GetByIdAsync(transmissionId)).ReturnsAsync(transmission);
            var mockTransmissionService = new TransmissionService(_mockUnitOfWork.Object, _mapper);

            //Act and Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(async ()=> await mockTransmissionService.GetOneAsync(transmissionId));

        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void DeleteAsync_OnSuccess_ReturnsTrue(int transmissionId)
        {
            //Arrange
            var transmission = _transmissions.FirstOrDefault(m => m.Id == transmissionId);

            _mockUnitOfWork.Setup(u => u.TransmissionRepository.GetByIdAsync(transmissionId)).ReturnsAsync(transmission);
            _mockUnitOfWork.Setup(u => u.TransmissionRepository.DeleteAsync(transmission)).Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

            var mockTransmissionService = new TransmissionService(_mockUnitOfWork.Object, _mapper);
            //Act

            var result = await mockTransmissionService.DeleteAsync(transmissionId);


            //Assert
            result.Should().BeTrue();
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void DeleteAsync_OnFailure_ReturnsFalse(int transmissionId)
        {
            //Arrange
            var transmission = _transmissions.FirstOrDefault(m => m.Id == transmissionId);

            _mockUnitOfWork.Setup(u => u.TransmissionRepository.GetByIdAsync(transmissionId)).ReturnsAsync(transmission);
            _mockUnitOfWork.Setup(u => u.TransmissionRepository.DeleteAsync(transmission)).Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(0);

            var mockTransmissionService = new TransmissionService(_mockUnitOfWork.Object, _mapper);
            
            //Act

            var result = await mockTransmissionService.DeleteAsync(transmissionId);


            //Assert
            result.Should().BeFalse();
        }
        
        [Theory]
        [InlineData(100)]
        [InlineData(102)]
        [InlineData(200)]
        public async void DeleteAsync_OnError_ThrowsEntityNotFoundException(int transmissionId)
        {
            //Arrange
            var transmission = _transmissions.FirstOrDefault(m => m.Id == transmissionId);

            _mockUnitOfWork.Setup(u => u.TransmissionRepository.GetByIdAsync(transmissionId)).ReturnsAsync(transmission);
          
            var mockTransmissionService = new TransmissionService(_mockUnitOfWork.Object, _mapper);
            
            //Act and Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(async ()=> await mockTransmissionService.DeleteAsync(transmissionId));
        }
        
        [Fact]
        public async void CreateAsync_OnSuccess_ReturnsTrue()
        {
            //Arrange
            var createTransmissionDto = new CreateTransmissionDto() {TransmissionName = "Reduktor"};
            var mapped = _mapper.Map<Transmission>(createTransmissionDto);
            
            _mockUnitOfWork.Setup(u => u.TransmissionRepository.IsExistAsync(It.IsAny<Expression<Func<Transmission, bool>>>()))
                .ReturnsAsync((Expression<Func<Transmission, bool>> predicate) => _transmissions.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.TransmissionRepository.CreateAsync(mapped))
                .Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.CompleteAsync())
                .ReturnsAsync(1);

            var mockTransmissionService = new TransmissionService(_mockUnitOfWork.Object, _mapper);
            
            //Act

            var result = await mockTransmissionService.CreateAsync(createTransmissionDto);


            //Assert
            result.Should().BeTrue();
        }
        
        [Fact]
        public async void CreateAsync_OnFailure_ReturnsFalse()
        {
            //Arrange
            var createTransmissionDto = new CreateTransmissionDto() {TransmissionName = "Reduktor"};
            var mapped = _mapper.Map<Transmission>(createTransmissionDto);
            
            _mockUnitOfWork.Setup(u => u.TransmissionRepository.IsExistAsync(It.IsAny<Expression<Func<Transmission, bool>>>()))
                .ReturnsAsync((Expression<Func<Transmission, bool>> predicate) => _transmissions.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.TransmissionRepository.CreateAsync(mapped))
                .Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.CompleteAsync())
                .ReturnsAsync(0);

            var mockTransmissionService = new TransmissionService(_mockUnitOfWork.Object, _mapper);
            
            //Act

            var result = await mockTransmissionService.CreateAsync(createTransmissionDto);


            //Assert
            result.Should().BeFalse();
        }
        
        [Fact]
        public async void CreateAsync_OnError_ThrowsDuplicateNameException()
        {
            //Arrange
            var createTransmissionDto = new CreateTransmissionDto() {TransmissionName = "Robot"};
            
            _mockUnitOfWork.Setup(u => u.TransmissionRepository.IsExistAsync(It.IsAny<Expression<Func<Transmission, bool>>>()))
                .ReturnsAsync((Expression<Func<Transmission, bool>> predicate) => _transmissions.Any(predicate.Compile()));
            
            var mockTransmissionService = new TransmissionService(_mockUnitOfWork.Object, _mapper);
            
            //Act and Assert
            await Assert.ThrowsAsync<DuplicateNameException>(async ()=> await mockTransmissionService.CreateAsync(createTransmissionDto));
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void UpdateAsync_OnSuccess_ReturnsTrue(int id)
        {
            //Arrange
            var updateTransmissionDto = new UpdateTransmissionDto() {TransmissionName = "Reduktor", IsDeleted = false};
            var transmission = _transmissions.FirstOrDefault(m => m.Id == id);
            var mapped = _mapper.Map<Transmission>(updateTransmissionDto);

            _mockUnitOfWork.Setup(u => u.TransmissionRepository.GetByIdAsyncForAll(id)).ReturnsAsync(transmission);
            _mockUnitOfWork.Setup(u => u.TransmissionRepository.IsExistAsync(It.IsAny<Expression<Func<Transmission, bool>>>()))
                .ReturnsAsync((Expression<Func<Transmission, bool>> predicate) => _transmissions.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.TransmissionRepository.UpdateAsync(mapped))
                .Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.CompleteAsync())
                .ReturnsAsync(1);

            var mockTransmissionService = new TransmissionService(_mockUnitOfWork.Object, _mapper);
            
            //Act

            var result = await mockTransmissionService.UpdateAsync(id, updateTransmissionDto);


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
            var updateTransmissionDto = new UpdateTransmissionDto() {TransmissionName = "Reduktor", IsDeleted = false};
            var transmission = _transmissions.FirstOrDefault(m => m.Id == id);
            var mapped = _mapper.Map<Transmission>(updateTransmissionDto);
            
            _mockUnitOfWork.Setup(u => u.TransmissionRepository.GetByIdAsyncForAll(id)).ReturnsAsync(transmission);
            _mockUnitOfWork.Setup(u => u.TransmissionRepository.IsExistAsync(It.IsAny<Expression<Func<Transmission, bool>>>()))
                .ReturnsAsync((Expression<Func<Transmission, bool>> predicate) => _transmissions.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.TransmissionRepository.UpdateAsync(mapped)).Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(0);
            
            var mockTransmissionService = new TransmissionService(_mockUnitOfWork.Object, _mapper);
            
            //Act

            var result = await mockTransmissionService.UpdateAsync(id, updateTransmissionDto);


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
            var updateTransmissionDto = new UpdateTransmissionDto() {TransmissionName = "Reduktor", IsDeleted = false};
            var transmission = _transmissions.FirstOrDefault(m => m.Id == id);
            
            _mockUnitOfWork.Setup(u => u.TransmissionRepository.GetByIdAsyncForAll(id))
                .ReturnsAsync(transmission);
            
            var mockTransmissionService = new TransmissionService(_mockUnitOfWork.Object, _mapper);
            
            // Act and  Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(async ()=> await mockTransmissionService.UpdateAsync(id, updateTransmissionDto));
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void UpdateAsync_OnError_ThrowsDuplicateNameException(int id)
        {
            //Arrange
            var updateTransmissionDto = new UpdateTransmissionDto() {TransmissionName = "Robot", IsDeleted = false};
            var transmission = _transmissions.FirstOrDefault(m => m.Id == id);
            
            _mockUnitOfWork.Setup(u => u.TransmissionRepository.GetByIdAsyncForAll(id))
                .ReturnsAsync(transmission);
            _mockUnitOfWork.Setup(u => u.TransmissionRepository.IsExistAsync(It.IsAny<Expression<Func<Transmission, bool>>>()))
                .ReturnsAsync((Expression<Func<Transmission, bool>> predicate) => _transmissions.Any(predicate.Compile()));
            
            var mockTransmissionService = new TransmissionService(_mockUnitOfWork.Object, _mapper);
            
            // Act and  Assert
            await Assert.ThrowsAsync<DuplicateNameException>(async ()=> await mockTransmissionService.UpdateAsync(id, updateTransmissionDto));
        }
}