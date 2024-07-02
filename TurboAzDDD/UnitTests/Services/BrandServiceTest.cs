using Domain.DTOs.Brand;


namespace UnitTests.Services;

public class BrandServiceTest
{
        private IMapper _mapper { get; set; }
        private Mock<IUnitOfWork> _mockUnitOfWork { get; set; }
        private List<Brand> _brands { get; set; }


        public BrandServiceTest()
        {
            var mockMapper = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
            var mapper = mockMapper.CreateMapper();
            _mapper = mapper;
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _brands = BrandsFixtures.Brands();

        }

        [Fact]
        public async void GetAllAsync_OnSuccess_ReturnsListOfBrandDtos()
        {
            //Arrange
            _mockUnitOfWork.Setup(u => u.BrandRepository.GetAllAsync()).ReturnsAsync(_brands);
            
            var mockBrandService = new BrandService(_mockUnitOfWork.Object, _mapper);

            //Act

            var result = await mockBrandService.GetAllAsync();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<GetBrandDto>>();
            // result.Should().BeEquivalentTo(brandDtos);
            result.Should().HaveCount(3);

        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void GetOneAsync_OnSuccess_ReturnsBrandDto(int id)
        {
            //Arrange
            var brand = _brands.FirstOrDefault(m => m.Id == id);
            
            _mockUnitOfWork.Setup(u => u.BrandRepository.GetByIdAsync(id)).ReturnsAsync(brand);
            var mockBrandService = new BrandService(_mockUnitOfWork.Object, _mapper);

            //Act

            var result = await mockBrandService.GetOneAsync(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<GetBrandDto>();
            // result.Should().BeEquivalentTo(brandDtos);
            
        }
        
        [Theory]
        [InlineData(100)]
        [InlineData(102)]
        [InlineData(200)]
        public async void GetOneAsync_OnError_ThrowsEntityNotFoundException(int brandId)
        {
            //Arrange
            var brand = _brands.FirstOrDefault(m => m.Id == brandId);
            
            _mockUnitOfWork.Setup(u => u.BrandRepository.GetByIdAsync(brandId)).ReturnsAsync(brand);
            var mockBrandService = new BrandService(_mockUnitOfWork.Object, _mapper);

            //Act and Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(async ()=> await mockBrandService.GetOneAsync(brandId));

        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void DeleteAsync_OnSuccess_ReturnsTrue(int brandId)
        {
            //Arrange
            var brand = _brands.FirstOrDefault(m => m.Id == brandId);

            _mockUnitOfWork.Setup(u => u.BrandRepository.GetByIdAsync(brandId)).ReturnsAsync(brand);
            _mockUnitOfWork.Setup(u => u.BrandRepository.DeleteAsync(brand)).Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

            var mockBrandService = new BrandService(_mockUnitOfWork.Object, _mapper);
            //Act

            var result = await mockBrandService.DeleteAsync(brandId);


            //Assert
            result.Should().BeTrue();
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void DeleteAsync_OnFailure_ReturnsFalse(int brandId)
        {
            //Arrange
            var brand = _brands.FirstOrDefault(m => m.Id == brandId);

            _mockUnitOfWork.Setup(u => u.BrandRepository.GetByIdAsync(brandId)).ReturnsAsync(brand);
            _mockUnitOfWork.Setup(u => u.BrandRepository.DeleteAsync(brand)).Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(0);

            var mockBrandService = new BrandService(_mockUnitOfWork.Object, _mapper);
            
            //Act

            var result = await mockBrandService.DeleteAsync(brandId);


            //Assert
            result.Should().BeFalse();
        }
        
        [Theory]
        [InlineData(100)]
        [InlineData(102)]
        [InlineData(200)]
        public async void DeleteAsync_OnError_ThrowsEntityNotFoundException(int brandId)
        {
            //Arrange
            var brand = _brands.FirstOrDefault(m => m.Id == brandId);

            _mockUnitOfWork.Setup(u => u.BrandRepository.GetByIdAsync(brandId)).ReturnsAsync(brand);
          
            var mockBrandService = new BrandService(_mockUnitOfWork.Object, _mapper);
            
            //Act and Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(async ()=> await mockBrandService.DeleteAsync(brandId));
        }
        
        [Fact]
        public async void CreateAsync_OnSuccess_ReturnsTrue()
        {
            //Arrange
            var createBrandDto = new CreateBrandDto() {BrandName = "ATV"};
            var mapped = _mapper.Map<Brand>(createBrandDto);
            
            _mockUnitOfWork.Setup(u => u.BrandRepository.IsExistAsync(It.IsAny<Expression<Func<Brand, bool>>>()))
                .ReturnsAsync((Expression<Func<Brand, bool>> predicate) => _brands.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.BrandRepository.CreateAsync(mapped))
                .Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.CompleteAsync())
                .ReturnsAsync(1);

            var mockBrandService = new BrandService(_mockUnitOfWork.Object, _mapper);
            
            //Act

            var result = await mockBrandService.CreateAsync(createBrandDto);


            //Assert
            result.Should().BeTrue();
        }
        
        [Fact]
        public async void CreateAsync_OnFailure_ReturnsFalse()
        {
            //Arrange
            var createBrandDto = new CreateBrandDto() {BrandName = "ATV"};
            var mapped = _mapper.Map<Brand>(createBrandDto);
            
            _mockUnitOfWork.Setup(u => u.BrandRepository.IsExistAsync(It.IsAny<Expression<Func<Brand, bool>>>()))
                .ReturnsAsync((Expression<Func<Brand, bool>> predicate) => _brands.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.BrandRepository.CreateAsync(mapped))
                .Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.CompleteAsync())
                .ReturnsAsync(0);

            var mockBrandService = new BrandService(_mockUnitOfWork.Object, _mapper);
            
            //Act

            var result = await mockBrandService.CreateAsync(createBrandDto);


            //Assert
            result.Should().BeFalse();
        }
        
        [Fact]
        public async void CreateAsync_OnError_ThrowsDuplicateNameException()
        {
            //Arrange
            var createBrandDto = new CreateBrandDto() {BrandName = "Audi"};
            
            _mockUnitOfWork.Setup(u => u.BrandRepository.IsExistAsync(It.IsAny<Expression<Func<Brand, bool>>>()))
                .ReturnsAsync((Expression<Func<Brand, bool>> predicate) => _brands.Any(predicate.Compile()));
            
            var mockBrandService = new BrandService(_mockUnitOfWork.Object, _mapper);
            
            //Act and Assert
            await Assert.ThrowsAsync<DuplicateNameException>(async ()=> await mockBrandService.CreateAsync(createBrandDto));
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void UpdateAsync_OnSuccess_ReturnsTrue(int id)
        {
            //Arrange
            var updateBrandDto = new UpdateBrandDto() {BrandName = "ATV", IsDeleted = false};
            var brand = _brands.FirstOrDefault(m => m.Id == id);
            var mapped = _mapper.Map<Brand>(updateBrandDto);

            _mockUnitOfWork.Setup(u => u.BrandRepository.GetByIdAsyncForAll(id)).ReturnsAsync(brand);
            _mockUnitOfWork.Setup(u => u.BrandRepository.IsExistAsync(It.IsAny<Expression<Func<Brand, bool>>>()))
                .ReturnsAsync((Expression<Func<Brand, bool>> predicate) => _brands.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.BrandRepository.UpdateAsync(mapped))
                .Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.CompleteAsync())
                .ReturnsAsync(1);

            var mockBrandService = new BrandService(_mockUnitOfWork.Object, _mapper);
            
            //Act

            var result = await mockBrandService.UpdateAsync(id, updateBrandDto);


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
            var updateBrandDto = new UpdateBrandDto() {BrandName = "ATV", IsDeleted = false};
            var brand = _brands.FirstOrDefault(m => m.Id == id);
            var mapped = _mapper.Map<Brand>(updateBrandDto);
            
            _mockUnitOfWork.Setup(u => u.BrandRepository.GetByIdAsyncForAll(id)).ReturnsAsync(brand);
            _mockUnitOfWork.Setup(u => u.BrandRepository.IsExistAsync(It.IsAny<Expression<Func<Brand, bool>>>()))
                .ReturnsAsync((Expression<Func<Brand, bool>> predicate) => _brands.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.BrandRepository.UpdateAsync(mapped)).Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(0);
            
            var mockBrandService = new BrandService(_mockUnitOfWork.Object, _mapper);
            
            //Act

            var result = await mockBrandService.UpdateAsync(id, updateBrandDto);


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
            var updateBrandDto = new UpdateBrandDto() {BrandName = "ATV", IsDeleted = false};
            var brand = _brands.FirstOrDefault(m => m.Id == id);
            
            _mockUnitOfWork.Setup(u => u.BrandRepository.GetByIdAsyncForAll(id))
                .ReturnsAsync(brand);
            
            var mockBrandService = new BrandService(_mockUnitOfWork.Object, _mapper);
            
            // Act and  Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(async ()=> await mockBrandService.UpdateAsync(id, updateBrandDto));
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void UpdateAsync_OnError_ThrowsDuplicateNameException(int id)
        {
            //Arrange
            var updateBrandDto = new UpdateBrandDto() {BrandName = "Audi", IsDeleted = false};
            var brand = _brands.FirstOrDefault(m => m.Id == id);
            
            _mockUnitOfWork.Setup(u => u.BrandRepository.GetByIdAsyncForAll(id))
                .ReturnsAsync(brand);
            _mockUnitOfWork.Setup(u => u.BrandRepository.IsExistAsync(It.IsAny<Expression<Func<Brand, bool>>>()))
                .ReturnsAsync((Expression<Func<Brand, bool>> predicate) => _brands.Any(predicate.Compile()));
            
            var mockBrandService = new BrandService(_mockUnitOfWork.Object, _mapper);
            
            // Act and  Assert
            await Assert.ThrowsAsync<DuplicateNameException>(async ()=> await mockBrandService.UpdateAsync(id, updateBrandDto));
        }
}