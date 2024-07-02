using Domain.DTOs.Model;


namespace UnitTests.Services
{
    public class ModelServiceTest
    {
         private IMapper _mapper { get; set; }
        private Mock<IUnitOfWork> _mockUnitOfWork { get; set; }
        private List<Model> _models { get; set; }
        private List<Brand> _brands { get; set; }



        public ModelServiceTest()
        {
            var mockMapper = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
            var mapper = mockMapper.CreateMapper();
            _mapper = mapper;
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _models = ModelsFixtures.Models();
            _brands = BrandsFixtures.Brands();
        }

        [Fact]
        public async void GetAllAsync_OnSuccess_ReturnsListOfModelDtos()
        {
            //Arrange
            _mockUnitOfWork.Setup(u => u.ModelRepository.GetAllAsync()).ReturnsAsync(_models);
            
            var mockModelService = new ModelService(_mockUnitOfWork.Object, _mapper);

            //Act

            var result = await mockModelService.GetAllAsync();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<GetModelDto>>();
            // result.Should().BeEquivalentTo(modelDtos);
            result.Should().HaveCount(3);

        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void GetOneAsync_OnSuccess_ReturnsModelDto(int id)
        {
            //Arrange
            var model = _models.FirstOrDefault(m => m.Id == id);
            
            _mockUnitOfWork.Setup(u => u.ModelRepository.GetByIdAsync(id)).ReturnsAsync(model);
            var mockModelService = new ModelService(_mockUnitOfWork.Object, _mapper);

            //Act

            var result = await mockModelService.GetOneAsync(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<GetModelDto>();
            // result.Should().BeEquivalentTo(modelDtos);
            
        }
        
        [Theory]
        [InlineData(100)]
        [InlineData(102)]
        [InlineData(200)]
        public async void GetOneAsync_OnError_ThrowsEntityNotFoundException(int modelId)
        {
            //Arrange
            var model = _models.FirstOrDefault(m => m.Id == modelId);
            
            _mockUnitOfWork.Setup(u => u.ModelRepository.GetByIdAsync(modelId)).ReturnsAsync(model);
            var mockModelService = new ModelService(_mockUnitOfWork.Object, _mapper);

            //Act and Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(async ()=> await mockModelService.GetOneAsync(modelId));

        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void DeleteAsync_OnSuccess_ReturnsTrue(int modelId)
        {
            //Arrange
            var model = _models.FirstOrDefault(m => m.Id == modelId);

            _mockUnitOfWork.Setup(u => u.ModelRepository.GetByIdAsync(modelId)).ReturnsAsync(model);
            _mockUnitOfWork.Setup(u => u.ModelRepository.DeleteAsync(model)).Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

            var mockModelService = new ModelService(_mockUnitOfWork.Object, _mapper);
            //Act

            var result = await mockModelService.DeleteAsync(modelId);


            //Assert
            result.Should().BeTrue();
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void DeleteAsync_OnFailure_ReturnsFalse(int modelId)
        {
            //Arrange
            var model = _models.FirstOrDefault(m => m.Id == modelId);

            _mockUnitOfWork.Setup(u => u.ModelRepository.GetByIdAsync(modelId)).ReturnsAsync(model);
            _mockUnitOfWork.Setup(u => u.ModelRepository.DeleteAsync(model)).Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(0);

            var mockModelService = new ModelService(_mockUnitOfWork.Object, _mapper);
            
            //Act

            var result = await mockModelService.DeleteAsync(modelId);


            //Assert
            result.Should().BeFalse();
        }
        
        [Theory]
        [InlineData(100)]
        [InlineData(102)]
        [InlineData(200)]
        public async void DeleteAsync_OnError_ThrowsEntityNotFoundException(int modelId)
        {
            //Arrange
            var model = _models.FirstOrDefault(m => m.Id == modelId);

            _mockUnitOfWork.Setup(u => u.ModelRepository.GetByIdAsync(modelId)).ReturnsAsync(model);
          
            var mockModelService = new ModelService(_mockUnitOfWork.Object, _mapper);
            
            //Act and Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(async ()=> await mockModelService.DeleteAsync(modelId));
        }
        
        [Fact]
        public async void CreateAsync_OnSuccess_ReturnsTrue()
        {
            //Arrange
            var createModelDto = new CreateModelDto() {ModelName = "Rapide", BrandId = 1};
            var mapped = _mapper.Map<Model>(createModelDto);
            
            _mockUnitOfWork.Setup(u => u.BrandRepository.IsExistAsync(It.IsAny<Expression<Func<Brand, bool>>>()))
                .ReturnsAsync((Expression<Func<Brand, bool>> predicate) => _brands.Any(predicate.Compile()));
            
            _mockUnitOfWork.Setup(u => u.ModelRepository.IsExistAsync(It.IsAny<Expression<Func<Model, bool>>>()))
                .ReturnsAsync((Expression<Func<Model, bool>> predicate) => _models.Any(predicate.Compile()));
            
            _mockUnitOfWork.Setup(u => u.ModelRepository.CreateAsync(mapped))
                .Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.CompleteAsync())
                .ReturnsAsync(1);

            var mockModelService = new ModelService(_mockUnitOfWork.Object, _mapper);
            
            //Act

            var result = await mockModelService.CreateAsync(createModelDto);


            //Assert
            result.Should().BeTrue();
        }
        
        [Fact]
        public async void CreateAsync_OnFailure_ReturnsFalse()
        {
            //Arrange
            var createModelDto = new CreateModelDto() {ModelName = "Rapide", BrandId = 1};
            var mapped = _mapper.Map<Model>(createModelDto);
            
            _mockUnitOfWork.Setup(u => u.BrandRepository.IsExistAsync(It.IsAny<Expression<Func<Brand, bool>>>()))
                .ReturnsAsync((Expression<Func<Brand, bool>> predicate) => _brands.Any(predicate.Compile()));
            
            _mockUnitOfWork.Setup(u => u.ModelRepository.IsExistAsync(It.IsAny<Expression<Func<Model, bool>>>()))
                .ReturnsAsync((Expression<Func<Model, bool>> predicate) => _models.Any(predicate.Compile()));
            
            _mockUnitOfWork.Setup(u => u.ModelRepository.CreateAsync(mapped))
                .Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.CompleteAsync())
                .ReturnsAsync(0);

            var mockModelService = new ModelService(_mockUnitOfWork.Object, _mapper);
            
            //Act

            var result = await mockModelService.CreateAsync(createModelDto);


            //Assert
            result.Should().BeFalse();
        }
        
        [Fact]
        public async void CreateAsync_OnError_ThrowsDuplicateNameException()
        {
            //Arrange
            var createModelDto = new CreateModelDto() {ModelName = "ILX", BrandId = 2};
            
            _mockUnitOfWork.Setup(u => u.BrandRepository.IsExistAsync(It.IsAny<Expression<Func<Brand, bool>>>()))
                .ReturnsAsync((Expression<Func<Brand, bool>> predicate) => _brands.Any(predicate.Compile()));
            
            _mockUnitOfWork.Setup(u => u.ModelRepository.IsExistAsync(It.IsAny<Expression<Func<Model, bool>>>()))
                .ReturnsAsync((Expression<Func<Model, bool>> predicate) => _models.Any(predicate.Compile()));
            
            var mockModelService = new ModelService(_mockUnitOfWork.Object, _mapper);
            
            //Act and Assert
            await Assert.ThrowsAsync<DuplicateNameException>(async ()=> await mockModelService.CreateAsync(createModelDto));
        }
        
        [Fact]
        public async void CreateAsync_OnError_ThrowsEntityNotFoundException()
        {
            //Arrange
            var createModelDto = new CreateModelDto() {ModelName = "Rapide", BrandId = 100};
            
            _mockUnitOfWork.Setup(u => u.BrandRepository.IsExistAsync(It.IsAny<Expression<Func<Brand, bool>>>()))
                .ReturnsAsync((Expression<Func<Brand, bool>> predicate) => _brands.Any(predicate.Compile()));
            
            var mockModelService = new ModelService(_mockUnitOfWork.Object, _mapper);
            
            //Act and Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(async ()=> await mockModelService.CreateAsync(createModelDto));
        }
        
        
        
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void UpdateAsync_OnSuccess_ReturnsTrue(int id)
        {
            //Arrange
            var updateModelDto = new UpdateModelDto() {ModelName = "Rapide", BrandId = 1 , IsDeleted = false};
            var model = _models.FirstOrDefault(m => m.Id == id);
            var mapped = _mapper.Map<Model>(updateModelDto);

            _mockUnitOfWork.Setup(u => u.ModelRepository.GetByIdAsyncForAll(id)).ReturnsAsync(model);
            
            _mockUnitOfWork.Setup(u => u.ModelRepository.IsExistAsync(It.IsAny<Expression<Func<Model, bool>>>()))
                .ReturnsAsync((Expression<Func<Model, bool>> predicate) => _models.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.BrandRepository.IsExistAsync(It.IsAny<Expression<Func<Brand, bool>>>()))
                .ReturnsAsync((Expression<Func<Brand, bool>> predicate) => _brands.Any(predicate.Compile()));
            
            _mockUnitOfWork.Setup(u => u.ModelRepository.UpdateAsync(mapped))
                .Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.CompleteAsync())
                .ReturnsAsync(1);

            var mockModelService = new ModelService(_mockUnitOfWork.Object, _mapper);
            
            //Act

            var result = await mockModelService.UpdateAsync(id, updateModelDto);


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
            var updateModelDto = new UpdateModelDto() {ModelName = "Rapide", BrandId =1 , IsDeleted = false};
            var model = _models.FirstOrDefault(m => m.Id == id);
            var mapped = _mapper.Map<Model>(updateModelDto);
            
            _mockUnitOfWork.Setup(u => u.ModelRepository.GetByIdAsyncForAll(id)).ReturnsAsync(model);
            
            _mockUnitOfWork.Setup(u => u.ModelRepository.IsExistAsync(It.IsAny<Expression<Func<Model, bool>>>()))
                .ReturnsAsync((Expression<Func<Model, bool>> predicate) => _models.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.BrandRepository.IsExistAsync(It.IsAny<Expression<Func<Brand, bool>>>()))
                .ReturnsAsync((Expression<Func<Brand, bool>> predicate) => _brands.Any(predicate.Compile()));
            
            _mockUnitOfWork.Setup(u => u.ModelRepository.UpdateAsync(mapped)).Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(0);
            
            var mockModelService = new ModelService(_mockUnitOfWork.Object, _mapper);
            
            //Act

            var result = await mockModelService.UpdateAsync(id, updateModelDto);


            //Assert
            result.Should().BeFalse();
        }
        
        [Theory]
        [InlineData(100)]
        [InlineData(102)]
        [InlineData(200)]
        public async void UpdateAsync_OnError_ThrowsEntityNotFoundExceptionForModel(int id)
        {
            //Arrange
            var updateModelDto = new UpdateModelDto() {ModelName = "Rapide", BrandId = 1 , IsDeleted = false};
            var model = _models.FirstOrDefault(m => m.Id == id);
            
            _mockUnitOfWork.Setup(u => u.ModelRepository.GetByIdAsyncForAll(id))
                .ReturnsAsync(model);
            
            var mockModelService = new ModelService(_mockUnitOfWork.Object, _mapper);
            
            // Act and  Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(async ()=> await mockModelService.UpdateAsync(id, updateModelDto));
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void UpdateAsync_OnError_ThrowsEntityNotFoundExceptionForBrand(int id)
        {
            //Arrange
            var updateModelDto = new UpdateModelDto() {ModelName = "Rapide", BrandId = 100 , IsDeleted = false};
            var model = _models.FirstOrDefault(m => m.Id == id);
            
            _mockUnitOfWork.Setup(u => u.ModelRepository.GetByIdAsyncForAll(id))
                .ReturnsAsync(model);
            _mockUnitOfWork.Setup(u => u.BrandRepository.IsExistAsync(It.IsAny<Expression<Func<Brand, bool>>>()))
                .ReturnsAsync((Expression<Func<Brand, bool>> predicate) => _brands.Any(predicate.Compile()));
            
            var mockModelService = new ModelService(_mockUnitOfWork.Object, _mapper);
            
            // Act and  Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(async ()=> await mockModelService.UpdateAsync(id, updateModelDto));
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void UpdateAsync_OnError_ThrowsDuplicateNameException(int id)
        {
            //Arrange
            var updateModelDto = new UpdateModelDto() {ModelName = "ILX", BrandId= 2, IsDeleted = false};
            var model = _models.FirstOrDefault(m => m.Id == id);
            
            _mockUnitOfWork.Setup(u => u.ModelRepository.GetByIdAsyncForAll(id))
                .ReturnsAsync(model);
            
            _mockUnitOfWork.Setup(u => u.BrandRepository.IsExistAsync(It.IsAny<Expression<Func<Brand, bool>>>()))
                .ReturnsAsync((Expression<Func<Brand, bool>> predicate) => _brands.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.ModelRepository.IsExistAsync(It.IsAny<Expression<Func<Model, bool>>>()))
                .ReturnsAsync((Expression<Func<Model, bool>> predicate) => _models.Any(predicate.Compile()));
            
            var mockModelService = new ModelService(_mockUnitOfWork.Object, _mapper);
            
            // Act and  Assert
            await Assert.ThrowsAsync<DuplicateNameException>(async ()=> await mockModelService.UpdateAsync(id, updateModelDto));
        }
        
    }
}

