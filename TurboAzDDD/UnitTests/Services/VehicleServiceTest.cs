using Domain.DTOs.Vehicle;
using Domain.Services;
using Microsoft.AspNetCore.Hosting;
using DriveType = Domain.Entities.DriveType;



namespace UnitTests.Services;

public class VehicleServiceTest
{
        private IMapper _mapper { get; set; }
        private Mock<IUnitOfWork> _mockUnitOfWork { get; set; }
        private Mock<IImageService> _mockImageService { get; set; }

        private List<Vehicle> _vehicles { get; set; }
        private List<FuelType> _fuelTypes { get; set; }
        private List<DriveType> _driveTypes { get; set; }
        private List<Market> _markets { get; set; }
        private List<BodyType> _bodyTypes { get; set; }
        private List<Brand> _brands { get; set; }
        
        private List<Model> _models { get; set; }
        
        private List<Transmission> _transmissions{ get; set; }

        private List<Color> _colors { get; set; }

        
        
        public VehicleServiceTest()
        {
            var mockMapper = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
            var mapper = mockMapper.CreateMapper();
            _mapper = mapper;
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockImageService = new Mock<IImageService>();
            _vehicles = VehiclesFixtures.Vehicles();
            _bodyTypes = BodyTypesFixtures.BodyTypes();
            _fuelTypes = FuelTypesFixtures.FuelTypes();
            _driveTypes = DriveTypesFixtures.DriveTypes();
            _markets = MarketsFixtures.Markets();
            _models = ModelsFixtures.Models();
            _brands = BrandsFixtures.Brands();
            _transmissions = TransmissionsFixtures.Transmissions();
            _colors = ColorsFixtures.Colors();
        }

        [Fact]
        public async void GetAllAsync_OnSuccess_ReturnsListOfVehicleDtos()
        {
            //Arrange
            _mockUnitOfWork.Setup(u => u.VehicleRepository.GetAllAsync()).ReturnsAsync(_vehicles);
            
            var vehicleService = new VehicleService(_mockUnitOfWork.Object, _mapper, _mockImageService.Object);

            //Act

            var result = await vehicleService.GetAllAsync();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<GetVehicleDto>>();
            // result.Should().BeEquivalentTo(vehicleDtos);
            result.Should().HaveCount(3);

        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void GetOneAsync_OnSuccess_ReturnsVehicleDto(int id)
        {
            //Arrange
            var vehicle = _vehicles.FirstOrDefault(m => m.Id == id);
            
            _mockUnitOfWork.Setup(u => u.VehicleRepository.GetByIdAsync(id)).ReturnsAsync(vehicle);
            var vehicleService = new VehicleService(_mockUnitOfWork.Object, _mapper, _mockImageService.Object);

            //Act

            var result = await vehicleService.GetOneAsync(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<GetVehicleDto>();
            // result.Should().BeEquivalentTo(vehicleDtos);
            
        }
        
        [Theory]
        [InlineData(100)]
        [InlineData(102)]
        [InlineData(200)]
        public async void GetOneAsync_OnError_ThrowsEntityNotFoundException(int vehicleId)
        {
            //Arrange
            var vehicle = _vehicles.FirstOrDefault(m => m.Id == vehicleId);
            
            _mockUnitOfWork.Setup(u => u.VehicleRepository.GetByIdAsync(vehicleId)).ReturnsAsync(vehicle);
            var vehicleService = new VehicleService(_mockUnitOfWork.Object, _mapper, _mockImageService.Object);

            //Act and Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(async ()=> await vehicleService.GetOneAsync(vehicleId));

        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void DeleteAsync_OnSuccess_ReturnsTrue(int vehicleId)
        {
            //Arrange
            var vehicle = _vehicles.FirstOrDefault(m => m.Id == vehicleId);

            _mockUnitOfWork.Setup(u => u.VehicleRepository.GetByIdAsync(vehicleId)).ReturnsAsync(vehicle);
            _mockUnitOfWork.Setup(u => u.VehicleRepository.DeleteAsync(vehicle)).Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

            var vehicleService = new VehicleService(_mockUnitOfWork.Object, _mapper, _mockImageService.Object);
            //Act

            var result = await vehicleService.DeleteAsync(vehicleId);


            //Assert
            result.Should().BeTrue();
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void DeleteAsync_OnFailure_ReturnsFalse(int vehicleId)
        {
            //Arrange
            var vehicle = _vehicles.FirstOrDefault(m => m.Id == vehicleId);

            _mockUnitOfWork.Setup(u => u.VehicleRepository.GetByIdAsync(vehicleId)).ReturnsAsync(vehicle);
            _mockUnitOfWork.Setup(u => u.VehicleRepository.DeleteAsync(vehicle)).Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(0);

            var vehicleService = new VehicleService(_mockUnitOfWork.Object, _mapper, _mockImageService.Object);
            
            //Act

            var result = await vehicleService.DeleteAsync(vehicleId);


            //Assert
            result.Should().BeFalse();
        }
        
        [Theory]
        [InlineData(100)]
        [InlineData(102)]
        [InlineData(200)]
        public async void DeleteAsync_OnError_ThrowsEntityNotFoundException(int vehicleId)
        {
            //Arrange
            var vehicle = _vehicles.FirstOrDefault(m => m.Id == vehicleId);

            _mockUnitOfWork.Setup(u => u.VehicleRepository.GetByIdAsync(vehicleId)).ReturnsAsync(vehicle);
          
            var vehicleService = new VehicleService(_mockUnitOfWork.Object, _mapper, _mockImageService.Object);
            
            //Act and Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(async ()=> await vehicleService.DeleteAsync(vehicleId));
        }
        
        [Fact]
        public async void CreateAsync_OnSuccess_ReturnsTrue()
        {
            //Arrange
            var createVehicleDto = new CreateVehicleDto()
            {
                Price = 400000,
                Mileage = 0,
                YearOfManufacture = 1998,
                PowerOutput = 2000,
                EngineDisplacement = 200,
                BodyTypeId = 2,
                FuelTypeId = 2,
                ColorId = 2,
                DriveTypeId = 2,
                ModelId = 2,
                BrandId = 1
                
            };
            var mapped = _mapper.Map<Vehicle>(createVehicleDto);
            
            _mockUnitOfWork.Setup(u => u.BodyTypeRepository.IsExistAsync(It.IsAny<Expression<Func<BodyType, bool>>>()))
                .ReturnsAsync((Expression<Func<BodyType, bool>> predicate) => _bodyTypes.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.DriveTypeRepository.IsExistAsync(It.IsAny<Expression<Func<DriveType, bool>>>()))
                .ReturnsAsync((Expression<Func<DriveType, bool>> predicate) => _driveTypes.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.FuelTypeRepository.IsExistAsync(It.IsAny<Expression<Func<FuelType, bool>>>()))
                .ReturnsAsync((Expression<Func<FuelType, bool>> predicate) => _fuelTypes.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.MarketRepository.IsExistAsync(It.IsAny<Expression<Func<Market, bool>>>()))
                .ReturnsAsync((Expression<Func<Market, bool>> predicate) => _markets.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.ModelRepository.IsExistAsync(It.IsAny<Expression<Func<Model, bool>>>()))
                .ReturnsAsync((Expression<Func<Model, bool>> predicate) => _models.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.BrandRepository.IsExistAsync(It.IsAny<Expression<Func<Brand, bool>>>()))
                .ReturnsAsync((Expression<Func<Brand, bool>> predicate) => _brands.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.TransmissionRepository.IsExistAsync(It.IsAny<Expression<Func<Transmission, bool>>>()))
                .ReturnsAsync((Expression<Func<Transmission, bool>> predicate) => _transmissions.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.ColorRepository.IsExistAsync(It.IsAny<Expression<Func<Color, bool>>>()))
                .ReturnsAsync((Expression<Func<Color, bool>> predicate) => _colors.Any(predicate.Compile()));
            
            _mockUnitOfWork.Setup(u => u.VehicleRepository.CreateAsync(mapped))
                .Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.CompleteAsync())
                .ReturnsAsync(1);

            var vehicleService = new VehicleService(_mockUnitOfWork.Object, _mapper, _mockImageService.Object);
            
            //Act

            var result = await vehicleService.CreateAsync(createVehicleDto);


            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async void CreateAsync_OnFailure_ReturnsFalse()
        {
            var createVehicleDto = new CreateVehicleDto()
            {
                Price = 400000,
                Mileage = 0,
                YearOfManufacture = 1998,
                PowerOutput = 2000,
                EngineDisplacement = 200,
                BodyTypeId = 2,
                FuelTypeId = 2,
                ColorId = 2,
                DriveTypeId = 2,
                ModelId = 2,
                BrandId = 1,
            };
            var mapped = _mapper.Map<Vehicle>(createVehicleDto);
            
            _mockUnitOfWork.Setup(u => u.BodyTypeRepository.IsExistAsync(It.IsAny<Expression<Func<BodyType, bool>>>()))
                .ReturnsAsync((Expression<Func<BodyType, bool>> predicate) => _bodyTypes.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.DriveTypeRepository.IsExistAsync(It.IsAny<Expression<Func<DriveType, bool>>>()))
                .ReturnsAsync((Expression<Func<DriveType, bool>> predicate) => _driveTypes.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.FuelTypeRepository.IsExistAsync(It.IsAny<Expression<Func<FuelType, bool>>>()))
                .ReturnsAsync((Expression<Func<FuelType, bool>> predicate) => _fuelTypes.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.MarketRepository.IsExistAsync(It.IsAny<Expression<Func<Market, bool>>>()))
                .ReturnsAsync((Expression<Func<Market, bool>> predicate) => _markets.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.ModelRepository.IsExistAsync(It.IsAny<Expression<Func<Model, bool>>>()))
                .ReturnsAsync((Expression<Func<Model, bool>> predicate) => _models.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.BrandRepository.IsExistAsync(It.IsAny<Expression<Func<Brand, bool>>>()))
                .ReturnsAsync((Expression<Func<Brand, bool>> predicate) => _brands.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.TransmissionRepository.IsExistAsync(It.IsAny<Expression<Func<Transmission, bool>>>()))
                .ReturnsAsync((Expression<Func<Transmission, bool>> predicate) => _transmissions.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.ColorRepository.IsExistAsync(It.IsAny<Expression<Func<Color, bool>>>()))
                .ReturnsAsync((Expression<Func<Color, bool>> predicate) => _colors.Any(predicate.Compile()));
            
            _mockUnitOfWork.Setup(u => u.VehicleRepository.CreateAsync(mapped))
                .Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.CompleteAsync())
                .ReturnsAsync(0);

            var vehicleService = new VehicleService(_mockUnitOfWork.Object, _mapper, _mockImageService.Object);
            
            //Act

            var result = await vehicleService.CreateAsync(createVehicleDto);


            //Assert
            result.Should().BeFalse();
        }
        
        [Fact]
        public async void CreateAsync_OnError_ThrowsEntityNotFoundExceptionForBodyType()
        {
            //Arrange
            var createVehicleDto = new CreateVehicleDto()
            {
                Price = 400000,
                Mileage = 0,
                YearOfManufacture = 1998,
                PowerOutput = 2000,
                EngineDisplacement = 200,
                BodyTypeId = 100,
                FuelTypeId = 2,
                ColorId = 2,
                DriveTypeId = 2,
                ModelId = 2,
                BrandId = 1,
            };            
            _mockUnitOfWork.Setup(u => u.BodyTypeRepository.IsExistAsync(It.IsAny<Expression<Func<BodyType, bool>>>()))
                .ReturnsAsync((Expression<Func<BodyType, bool>> predicate) => _bodyTypes.Any(predicate.Compile()));
            
            var vehicleService = new VehicleService(_mockUnitOfWork.Object, _mapper, _mockImageService.Object);
            
            //Act and Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(async ()=> await vehicleService.CreateAsync(createVehicleDto));
        }
        
        [Fact]
        public async void CreateAsync_OnError_ThrowsEntityNotFoundExceptionForDriveType()
        {
            //Arrange
            var createVehicleDto = new CreateVehicleDto()
            {
                Price = 400000,
                Mileage = 0,
                YearOfManufacture = 1998,
                PowerOutput = 2000,
                EngineDisplacement = 200,
                BodyTypeId = 2,
                FuelTypeId = 2,
                ColorId = 2,
                DriveTypeId = 100,
                ModelId = 2,
                BrandId = 1,
            };            
            _mockUnitOfWork.Setup(u => u.BodyTypeRepository.IsExistAsync(It.IsAny<Expression<Func<BodyType, bool>>>()))
                .ReturnsAsync((Expression<Func<BodyType, bool>> predicate) => _bodyTypes.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.DriveTypeRepository.IsExistAsync(It.IsAny<Expression<Func<DriveType, bool>>>()))
                .ReturnsAsync((Expression<Func<DriveType, bool>> predicate) => _driveTypes.Any(predicate.Compile()));
            
            var vehicleService = new VehicleService(_mockUnitOfWork.Object, _mapper, _mockImageService.Object);
            
            //Act and Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(async ()=> await vehicleService.CreateAsync(createVehicleDto));
        }
        
        [Fact]
        public async void CreateAsync_OnError_ThrowsEntityNotFoundExceptionForFuelType()
        {
            //Arrange
            var createVehicleDto = new CreateVehicleDto()
            {
                Price = 400000,
                Mileage = 0,
                YearOfManufacture = 1998,
                PowerOutput = 2000,
                EngineDisplacement = 200,
                BodyTypeId = 2,
                FuelTypeId = 100,
                ColorId = 2,
                DriveTypeId = 2,
                ModelId = 2,
                BrandId = 1,
            };            
            _mockUnitOfWork.Setup(u => u.BodyTypeRepository.IsExistAsync(It.IsAny<Expression<Func<BodyType, bool>>>()))
                .ReturnsAsync((Expression<Func<BodyType, bool>> predicate) => _bodyTypes.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.DriveTypeRepository.IsExistAsync(It.IsAny<Expression<Func<DriveType, bool>>>()))
                .ReturnsAsync((Expression<Func<DriveType, bool>> predicate) => _driveTypes.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.FuelTypeRepository.IsExistAsync(It.IsAny<Expression<Func<FuelType, bool>>>()))
                .ReturnsAsync((Expression<Func<FuelType, bool>> predicate) => _fuelTypes.Any(predicate.Compile()));
            
            var vehicleService = new VehicleService(_mockUnitOfWork.Object, _mapper, _mockImageService.Object);
            
            //Act and Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(async ()=> await vehicleService.CreateAsync(createVehicleDto));
        }
        
        [Fact]
        public async void CreateAsync_OnError_ThrowsEntityNotFoundExceptionForMarket()
        {
            //Arrange
            var createVehicleDto = new CreateVehicleDto()
            {
                Price = 400000,
                Mileage = 0,
                YearOfManufacture = 1998,
                PowerOutput = 2000,
                EngineDisplacement = 200,
                BodyTypeId = 2,
                FuelTypeId = 2,
                ColorId = 2,
                DriveTypeId = 2,
                ModelId = 2,
                BrandId = 1,
                MarketId = 100,
            };            
            _mockUnitOfWork.Setup(u => u.BodyTypeRepository.IsExistAsync(It.IsAny<Expression<Func<BodyType, bool>>>()))
                .ReturnsAsync((Expression<Func<BodyType, bool>> predicate) => _bodyTypes.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.DriveTypeRepository.IsExistAsync(It.IsAny<Expression<Func<DriveType, bool>>>()))
                .ReturnsAsync((Expression<Func<DriveType, bool>> predicate) => _driveTypes.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.FuelTypeRepository.IsExistAsync(It.IsAny<Expression<Func<FuelType, bool>>>()))
                .ReturnsAsync((Expression<Func<FuelType, bool>> predicate) => _fuelTypes.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.MarketRepository.IsExistAsync(It.IsAny<Expression<Func<Market, bool>>>()))
                .ReturnsAsync((Expression<Func<Market, bool>> predicate) => _markets.Any(predicate.Compile()));
            
            var vehicleService = new VehicleService(_mockUnitOfWork.Object, _mapper, _mockImageService.Object);
            
            //Act and Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(async ()=> await vehicleService.CreateAsync(createVehicleDto));
        }
        
        [Fact]
        public async void CreateAsync_OnError_ThrowsEntityNotFoundExceptionForModel()
        {
            //Arrange
            var createVehicleDto = new CreateVehicleDto()
            {
                Price = 400000,
                Mileage = 0,
                YearOfManufacture = 1998,
                PowerOutput = 2000,
                EngineDisplacement = 200,
                BodyTypeId = 2,
                FuelTypeId = 2,
                ColorId = 2,
                DriveTypeId = 2,
                ModelId = 100,
                BrandId = 1,
                MarketId = 1
            };      
            _mockUnitOfWork.Setup(u => u.BodyTypeRepository.IsExistAsync(It.IsAny<Expression<Func<BodyType, bool>>>()))
                .ReturnsAsync((Expression<Func<BodyType, bool>> predicate) => _bodyTypes.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.DriveTypeRepository.IsExistAsync(It.IsAny<Expression<Func<DriveType, bool>>>()))
                .ReturnsAsync((Expression<Func<DriveType, bool>> predicate) => _driveTypes.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.FuelTypeRepository.IsExistAsync(It.IsAny<Expression<Func<FuelType, bool>>>()))
                .ReturnsAsync((Expression<Func<FuelType, bool>> predicate) => _fuelTypes.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.MarketRepository.IsExistAsync(It.IsAny<Expression<Func<Market, bool>>>()))
                .ReturnsAsync((Expression<Func<Market, bool>> predicate) => _markets.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.ModelRepository.IsExistAsync(It.IsAny<Expression<Func<Model, bool>>>()))
                .ReturnsAsync((Expression<Func<Model, bool>> predicate) => _models.Any(predicate.Compile()));
            
            var vehicleService = new VehicleService(_mockUnitOfWork.Object, _mapper, _mockImageService.Object);
            
            //Act and Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(async ()=> await vehicleService.CreateAsync(createVehicleDto));
        }
        
        [Fact]
        public async void CreateAsync_OnError_ThrowsEntityNotFoundExceptionForBrand()
        {
            //Arrange
            var createVehicleDto = new CreateVehicleDto()
            {
                Price = 400000,
                Mileage = 0,
                YearOfManufacture = 1998,
                PowerOutput = 2000,
                EngineDisplacement = 200,
                BodyTypeId = 2,
                FuelTypeId = 2,
                ColorId = 2,
                DriveTypeId = 2,
                ModelId = 2,
                BrandId = 100,
                MarketId = 1
            };            
            _mockUnitOfWork.Setup(u => u.BodyTypeRepository.IsExistAsync(It.IsAny<Expression<Func<BodyType, bool>>>()))
                .ReturnsAsync((Expression<Func<BodyType, bool>> predicate) => _bodyTypes.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.DriveTypeRepository.IsExistAsync(It.IsAny<Expression<Func<DriveType, bool>>>()))
                .ReturnsAsync((Expression<Func<DriveType, bool>> predicate) => _driveTypes.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.FuelTypeRepository.IsExistAsync(It.IsAny<Expression<Func<FuelType, bool>>>()))
                .ReturnsAsync((Expression<Func<FuelType, bool>> predicate) => _fuelTypes.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.MarketRepository.IsExistAsync(It.IsAny<Expression<Func<Market, bool>>>()))
                .ReturnsAsync((Expression<Func<Market, bool>> predicate) => _markets.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.ModelRepository.IsExistAsync(It.IsAny<Expression<Func<Model, bool>>>()))
                .ReturnsAsync((Expression<Func<Model, bool>> predicate) => _models.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.BrandRepository.IsExistAsync(It.IsAny<Expression<Func<Brand, bool>>>()))
                .ReturnsAsync((Expression<Func<Brand, bool>> predicate) => _brands.Any(predicate.Compile()));
            
            var vehicleService = new VehicleService(_mockUnitOfWork.Object, _mapper, _mockImageService.Object);
            
            //Act and Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(async ()=> await vehicleService.CreateAsync(createVehicleDto));
        }
        
        [Fact]
        public async void CreateAsync_OnError_ThrowsEntityNotFoundExceptionForTransmission()
        {
            //Arrange
            var createVehicleDto = new CreateVehicleDto()
            {
                Price = 400000,
                Mileage = 0,
                YearOfManufacture = 1998,
                PowerOutput = 2000,
                EngineDisplacement = 200,
                BodyTypeId = 2,
                FuelTypeId = 2,
                ColorId = 2,
                DriveTypeId = 2,
                ModelId = 2,
                BrandId = 1,
                MarketId = 1,
                TransmissionId = 100
            };            
            _mockUnitOfWork.Setup(u => u.BodyTypeRepository.IsExistAsync(It.IsAny<Expression<Func<BodyType, bool>>>()))
                .ReturnsAsync((Expression<Func<BodyType, bool>> predicate) => _bodyTypes.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.DriveTypeRepository.IsExistAsync(It.IsAny<Expression<Func<DriveType, bool>>>()))
                .ReturnsAsync((Expression<Func<DriveType, bool>> predicate) => _driveTypes.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.FuelTypeRepository.IsExistAsync(It.IsAny<Expression<Func<FuelType, bool>>>()))
                .ReturnsAsync((Expression<Func<FuelType, bool>> predicate) => _fuelTypes.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.MarketRepository.IsExistAsync(It.IsAny<Expression<Func<Market, bool>>>()))
                .ReturnsAsync((Expression<Func<Market, bool>> predicate) => _markets.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.ModelRepository.IsExistAsync(It.IsAny<Expression<Func<Model, bool>>>()))
                .ReturnsAsync((Expression<Func<Model, bool>> predicate) => _models.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.BrandRepository.IsExistAsync(It.IsAny<Expression<Func<Brand, bool>>>()))
                .ReturnsAsync((Expression<Func<Brand, bool>> predicate) => _brands.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.TransmissionRepository.IsExistAsync(It.IsAny<Expression<Func<Transmission, bool>>>()))
                .ReturnsAsync((Expression<Func<Transmission, bool>> predicate) => _transmissions.Any(predicate.Compile()));
            
            var vehicleService = new VehicleService(_mockUnitOfWork.Object, _mapper, _mockImageService.Object);
            
            //Act and Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(async ()=> await vehicleService.CreateAsync(createVehicleDto));
        }
        
        [Fact]
        public async void CreateAsync_OnError_ThrowsEntityNotFoundExceptionForColor()
        {
            //Arrange
            var createVehicleDto = new CreateVehicleDto()
            {
                Price = 400000,
                Mileage = 0,
                YearOfManufacture = 1998,
                PowerOutput = 2000,
                EngineDisplacement = 200,
                BodyTypeId = 2,
                FuelTypeId = 2,
                ColorId = 100,
                DriveTypeId = 2,
                ModelId = 2,
                BrandId = 100,
                MarketId = 1,
                TransmissionId=1
                
            };            
            _mockUnitOfWork.Setup(u => u.BodyTypeRepository.IsExistAsync(It.IsAny<Expression<Func<BodyType, bool>>>()))
                .ReturnsAsync((Expression<Func<BodyType, bool>> predicate) => _bodyTypes.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.DriveTypeRepository.IsExistAsync(It.IsAny<Expression<Func<DriveType, bool>>>()))
                .ReturnsAsync((Expression<Func<DriveType, bool>> predicate) => _driveTypes.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.FuelTypeRepository.IsExistAsync(It.IsAny<Expression<Func<FuelType, bool>>>()))
                .ReturnsAsync((Expression<Func<FuelType, bool>> predicate) => _fuelTypes.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.MarketRepository.IsExistAsync(It.IsAny<Expression<Func<Market, bool>>>()))
                .ReturnsAsync((Expression<Func<Market, bool>> predicate) => _markets.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.ModelRepository.IsExistAsync(It.IsAny<Expression<Func<Model, bool>>>()))
                .ReturnsAsync((Expression<Func<Model, bool>> predicate) => _models.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.BrandRepository.IsExistAsync(It.IsAny<Expression<Func<Brand, bool>>>()))
                .ReturnsAsync((Expression<Func<Brand, bool>> predicate) => _brands.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.TransmissionRepository.IsExistAsync(It.IsAny<Expression<Func<Transmission, bool>>>()))
                .ReturnsAsync((Expression<Func<Transmission, bool>> predicate) => _transmissions.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.TransmissionRepository.IsExistAsync(It.IsAny<Expression<Func<Transmission, bool>>>()))
                .ReturnsAsync((Expression<Func<Transmission, bool>> predicate) => _transmissions.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.ColorRepository.IsExistAsync(It.IsAny<Expression<Func<Color, bool>>>()))
                .ReturnsAsync((Expression<Func<Color, bool>> predicate) => _colors.Any(predicate.Compile()));
            
            var vehicleService = new VehicleService(_mockUnitOfWork.Object, _mapper, _mockImageService.Object);
            
            //Act and Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(async ()=> await vehicleService.CreateAsync(createVehicleDto));
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void UpdateAsync_OnSuccess_ReturnsTrue(int id)
        {
            //Arrange
            var updateVehicleDto = new UpdateVehicleDto()
            {
                Price = 400000,
                Mileage = 0,
                YearOfManufacture = 1998,
                PowerOutput = 2000,
                EngineDisplacement = 200,
                BodyTypeId = 2,
                FuelTypeId = 2,
                ColorId = 2,
                DriveTypeId = 2,
                ModelId = 2,
                BrandId = 1
                
            };         
            var vehicle = _vehicles.FirstOrDefault(m => m.Id == id);
            var mapped = _mapper.Map<Vehicle>(updateVehicleDto);
        
            _mockUnitOfWork.Setup(u => u.VehicleRepository.GetByIdAsyncForAll(id)).ReturnsAsync(vehicle);
            _mockUnitOfWork.Setup(u => u.BodyTypeRepository.IsExistAsync(It.IsAny<Expression<Func<BodyType, bool>>>()))
                .ReturnsAsync((Expression<Func<BodyType, bool>> predicate) => _bodyTypes.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.DriveTypeRepository.IsExistAsync(It.IsAny<Expression<Func<DriveType, bool>>>()))
                .ReturnsAsync((Expression<Func<DriveType, bool>> predicate) => _driveTypes.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.FuelTypeRepository.IsExistAsync(It.IsAny<Expression<Func<FuelType, bool>>>()))
                .ReturnsAsync((Expression<Func<FuelType, bool>> predicate) => _fuelTypes.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.MarketRepository.IsExistAsync(It.IsAny<Expression<Func<Market, bool>>>()))
                .ReturnsAsync((Expression<Func<Market, bool>> predicate) => _markets.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.ModelRepository.IsExistAsync(It.IsAny<Expression<Func<Model, bool>>>()))
                .ReturnsAsync((Expression<Func<Model, bool>> predicate) => _models.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.BrandRepository.IsExistAsync(It.IsAny<Expression<Func<Brand, bool>>>()))
                .ReturnsAsync((Expression<Func<Brand, bool>> predicate) => _brands.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.TransmissionRepository.IsExistAsync(It.IsAny<Expression<Func<Transmission, bool>>>()))
                .ReturnsAsync((Expression<Func<Transmission, bool>> predicate) => _transmissions.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.ColorRepository.IsExistAsync(It.IsAny<Expression<Func<Color, bool>>>()))
                .ReturnsAsync((Expression<Func<Color, bool>> predicate) => _colors.Any(predicate.Compile()));
            
            _mockUnitOfWork.Setup(u => u.VehicleRepository.UpdateAsync(mapped))
                .Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.CompleteAsync())
                .ReturnsAsync(1);
        
            var vehicleService = new VehicleService(_mockUnitOfWork.Object, _mapper, _mockImageService.Object);
            
            //Act
        
            var result = await vehicleService.UpdateAsync(id, updateVehicleDto);
        
        
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
            var updateVehicleDto = new UpdateVehicleDto()
            {
                Price = 400000,
                Mileage = 0,
                YearOfManufacture = 1998,
                PowerOutput = 2000,
                EngineDisplacement = 200,
                BodyTypeId = 2,
                FuelTypeId = 2,
                ColorId = 2,
                DriveTypeId = 2,
                ModelId = 2,
                BrandId = 1
                
            };         
            var vehicle = _vehicles.FirstOrDefault(m => m.Id == id);
            var mapped = _mapper.Map<Vehicle>(updateVehicleDto);
        
            _mockUnitOfWork.Setup(u => u.VehicleRepository.GetByIdAsyncForAll(id)).ReturnsAsync(vehicle);
            _mockUnitOfWork.Setup(u => u.BodyTypeRepository.IsExistAsync(It.IsAny<Expression<Func<BodyType, bool>>>()))
                .ReturnsAsync((Expression<Func<BodyType, bool>> predicate) => _bodyTypes.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.DriveTypeRepository.IsExistAsync(It.IsAny<Expression<Func<DriveType, bool>>>()))
                .ReturnsAsync((Expression<Func<DriveType, bool>> predicate) => _driveTypes.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.FuelTypeRepository.IsExistAsync(It.IsAny<Expression<Func<FuelType, bool>>>()))
                .ReturnsAsync((Expression<Func<FuelType, bool>> predicate) => _fuelTypes.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.MarketRepository.IsExistAsync(It.IsAny<Expression<Func<Market, bool>>>()))
                .ReturnsAsync((Expression<Func<Market, bool>> predicate) => _markets.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.ModelRepository.IsExistAsync(It.IsAny<Expression<Func<Model, bool>>>()))
                .ReturnsAsync((Expression<Func<Model, bool>> predicate) => _models.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.BrandRepository.IsExistAsync(It.IsAny<Expression<Func<Brand, bool>>>()))
                .ReturnsAsync((Expression<Func<Brand, bool>> predicate) => _brands.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.TransmissionRepository.IsExistAsync(It.IsAny<Expression<Func<Transmission, bool>>>()))
                .ReturnsAsync((Expression<Func<Transmission, bool>> predicate) => _transmissions.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.ColorRepository.IsExistAsync(It.IsAny<Expression<Func<Color, bool>>>()))
                .ReturnsAsync((Expression<Func<Color, bool>> predicate) => _colors.Any(predicate.Compile()));
            
            _mockUnitOfWork.Setup(u => u.VehicleRepository.UpdateAsync(mapped))
                .Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.CompleteAsync())
                .ReturnsAsync(0);
        
            var vehicleService = new VehicleService(_mockUnitOfWork.Object, _mapper, _mockImageService.Object);
            
            //Act
        
            var result = await vehicleService.UpdateAsync(id, updateVehicleDto);
        
        
            //Assert
            result.Should().BeFalse();
        }
        
        [Theory]
        [InlineData(100)]
        [InlineData(102)]
        [InlineData(200)]
        public async void UpdateAsync_OnError_ThrowsEntityNotFoundExceptionForVehicle(int id)
        {
            //Arrange
            var updateVehicleDto = new UpdateVehicleDto()
            {
                Price = 400000,
                Mileage = 0,
                YearOfManufacture = 1998,
                PowerOutput = 2000,
                EngineDisplacement = 200,
                BodyTypeId = 100,
                FuelTypeId = 2,
                ColorId = 2,
                DriveTypeId = 2,
                ModelId = 2,
                BrandId = 1
                
            };     
            var vehicle = _vehicles.FirstOrDefault(m => m.Id == id);
            
            _mockUnitOfWork.Setup(u => u.VehicleRepository.GetByIdAsyncForAll(id))
                .ReturnsAsync(vehicle);
            
            var vehicleService = new VehicleService(_mockUnitOfWork.Object, _mapper, _mockImageService.Object);
            
            // Act and  Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(async ()=> await vehicleService.UpdateAsync(id, updateVehicleDto));
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void UpdateAsync_OnError_ThrowsEntityNotFoundExceptionForBodyType(int id)
        {
            //Arrange
            var updateVehicleDto = new UpdateVehicleDto()
            {
                Price = 400000,
                Mileage = 0,
                YearOfManufacture = 1998,
                PowerOutput = 2000,
                EngineDisplacement = 200,
                BodyTypeId = 100,
                FuelTypeId = 2,
                ColorId = 2,
                DriveTypeId = 2,
                ModelId = 2,
                BrandId = 1
                
            };     
            var vehicle = _vehicles.FirstOrDefault(m => m.Id == id);
            
            _mockUnitOfWork.Setup(u => u.VehicleRepository.GetByIdAsyncForAll(id))
                .ReturnsAsync(vehicle);
            _mockUnitOfWork.Setup(u => u.BodyTypeRepository.IsExistAsync(It.IsAny<Expression<Func<BodyType, bool>>>()))
                .ReturnsAsync((Expression<Func<BodyType, bool>> predicate) => _bodyTypes.Any(predicate.Compile()));
            
            var vehicleService = new VehicleService(_mockUnitOfWork.Object, _mapper, _mockImageService.Object);
            
            // Act and  Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(async ()=> await vehicleService.UpdateAsync(id, updateVehicleDto));
        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void UpdateAsync_OnError_ThrowsEntityNotFoundExceptionForDriveType(int id)
        {
            //Arrange
            var updateVehicleDto = new UpdateVehicleDto()
            {
                Price = 400000,
                Mileage = 0,
                YearOfManufacture = 1998,
                PowerOutput = 2000,
                EngineDisplacement = 200,
                BodyTypeId = 2,
                FuelTypeId = 2,
                ColorId = 2,
                DriveTypeId = 100,
                ModelId = 2,
                BrandId = 1
                
            };     
            var vehicle = _vehicles.FirstOrDefault(m => m.Id == id);
            
            _mockUnitOfWork.Setup(u => u.VehicleRepository.GetByIdAsyncForAll(id))
                .ReturnsAsync(vehicle);
            _mockUnitOfWork.Setup(u => u.BodyTypeRepository.IsExistAsync(It.IsAny<Expression<Func<BodyType, bool>>>()))
                .ReturnsAsync((Expression<Func<BodyType, bool>> predicate) => _bodyTypes.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.DriveTypeRepository.IsExistAsync(It.IsAny<Expression<Func<DriveType, bool>>>()))
                .ReturnsAsync((Expression<Func<DriveType, bool>> predicate) => _driveTypes.Any(predicate.Compile()));
            
            var vehicleService = new VehicleService(_mockUnitOfWork.Object, _mapper, _mockImageService.Object);
            
            // Act and  Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(async ()=> await vehicleService.UpdateAsync(id, updateVehicleDto));
        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void UpdateAsync_OnError_ThrowsEntityNotFoundExceptionForFuelType(int id)
        {
            //Arrange
            var updateVehicleDto = new UpdateVehicleDto()
            {
                Price = 400000,
                Mileage = 0,
                YearOfManufacture = 1998,
                PowerOutput = 2000,
                EngineDisplacement = 200,
                BodyTypeId = 100,
                FuelTypeId = 2,
                ColorId = 2,
                DriveTypeId = 2,
                ModelId = 2,
                BrandId = 1
                
            };     
            var vehicle = _vehicles.FirstOrDefault(m => m.Id == id);
            
            _mockUnitOfWork.Setup(u => u.VehicleRepository.GetByIdAsyncForAll(id))
                .ReturnsAsync(vehicle);
            _mockUnitOfWork.Setup(u => u.BodyTypeRepository.IsExistAsync(It.IsAny<Expression<Func<BodyType, bool>>>()))
                .ReturnsAsync((Expression<Func<BodyType, bool>> predicate) => _bodyTypes.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.DriveTypeRepository.IsExistAsync(It.IsAny<Expression<Func<DriveType, bool>>>()))
                .ReturnsAsync((Expression<Func<DriveType, bool>> predicate) => _driveTypes.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.FuelTypeRepository.IsExistAsync(It.IsAny<Expression<Func<FuelType, bool>>>()))
                .ReturnsAsync((Expression<Func<FuelType, bool>> predicate) => _fuelTypes.Any(predicate.Compile()));
            
            var vehicleService = new VehicleService(_mockUnitOfWork.Object, _mapper, _mockImageService.Object);
            
            // Act and  Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(async ()=> await vehicleService.UpdateAsync(id, updateVehicleDto));
        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void UpdateAsync_OnError_ThrowsEntityNotFoundExceptionForMarket(int id)
        {
            //Arrange
            var updateVehicleDto = new UpdateVehicleDto()
            {
                Price = 400000,
                Mileage = 0,
                YearOfManufacture = 1998,
                PowerOutput = 2000,
                EngineDisplacement = 200,
                BodyTypeId = 100,
                FuelTypeId = 2,
                ColorId = 2,
                DriveTypeId = 2,
                ModelId = 2,
                BrandId = 1
                
            };     
            var vehicle = _vehicles.FirstOrDefault(m => m.Id == id);
            
            _mockUnitOfWork.Setup(u => u.VehicleRepository.GetByIdAsyncForAll(id))
                .ReturnsAsync(vehicle);
            _mockUnitOfWork.Setup(u => u.BodyTypeRepository.IsExistAsync(It.IsAny<Expression<Func<BodyType, bool>>>()))
                .ReturnsAsync((Expression<Func<BodyType, bool>> predicate) => _bodyTypes.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.DriveTypeRepository.IsExistAsync(It.IsAny<Expression<Func<DriveType, bool>>>()))
                .ReturnsAsync((Expression<Func<DriveType, bool>> predicate) => _driveTypes.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.FuelTypeRepository.IsExistAsync(It.IsAny<Expression<Func<FuelType, bool>>>()))
                .ReturnsAsync((Expression<Func<FuelType, bool>> predicate) => _fuelTypes.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.MarketRepository.IsExistAsync(It.IsAny<Expression<Func<Market, bool>>>()))
                .ReturnsAsync((Expression<Func<Market, bool>> predicate) => _markets.Any(predicate.Compile()));
            
            var vehicleService = new VehicleService(_mockUnitOfWork.Object, _mapper, _mockImageService.Object);
            
            // Act and  Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(async ()=> await vehicleService.UpdateAsync(id, updateVehicleDto));
        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void UpdateAsync_OnError_ThrowsEntityNotFoundExceptionForModel(int id)
        {
            //Arrange
            var updateVehicleDto = new UpdateVehicleDto()
            {
                Price = 400000,
                Mileage = 0,
                YearOfManufacture = 1998,
                PowerOutput = 2000,
                EngineDisplacement = 200,
                BodyTypeId = 100,
                FuelTypeId = 2,
                ColorId = 2,
                DriveTypeId = 2,
                ModelId = 2,
                BrandId = 1
                
            };     
            var vehicle = _vehicles.FirstOrDefault(m => m.Id == id);
            
            _mockUnitOfWork.Setup(u => u.VehicleRepository.GetByIdAsyncForAll(id))
                .ReturnsAsync(vehicle);
            
            _mockUnitOfWork.Setup(u => u.BodyTypeRepository.IsExistAsync(It.IsAny<Expression<Func<BodyType, bool>>>()))
                .ReturnsAsync((Expression<Func<BodyType, bool>> predicate) => _bodyTypes.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.DriveTypeRepository.IsExistAsync(It.IsAny<Expression<Func<DriveType, bool>>>()))
                .ReturnsAsync((Expression<Func<DriveType, bool>> predicate) => _driveTypes.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.FuelTypeRepository.IsExistAsync(It.IsAny<Expression<Func<FuelType, bool>>>()))
                .ReturnsAsync((Expression<Func<FuelType, bool>> predicate) => _fuelTypes.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.MarketRepository.IsExistAsync(It.IsAny<Expression<Func<Market, bool>>>()))
                .ReturnsAsync((Expression<Func<Market, bool>> predicate) => _markets.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.ModelRepository.IsExistAsync(It.IsAny<Expression<Func<Model, bool>>>()))
                .ReturnsAsync((Expression<Func<Model, bool>> predicate) => _models.Any(predicate.Compile()));
            
            var vehicleService = new VehicleService(_mockUnitOfWork.Object, _mapper, _mockImageService.Object);
            
            // Act and  Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(async ()=> await vehicleService.UpdateAsync(id, updateVehicleDto));
        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void UpdateAsync_OnError_ThrowsEntityNotFoundExceptionForBrand(int id)
        {
            //Arrange
            var updateVehicleDto = new UpdateVehicleDto()
            {
                Price = 400000,
                Mileage = 0,
                YearOfManufacture = 1998,
                PowerOutput = 2000,
                EngineDisplacement = 200,
                BodyTypeId = 100,
                FuelTypeId = 2,
                ColorId = 2,
                DriveTypeId = 2,
                ModelId = 2,
                BrandId = 1
                
            };     
            var vehicle = _vehicles.FirstOrDefault(m => m.Id == id);
            
            _mockUnitOfWork.Setup(u => u.VehicleRepository.GetByIdAsyncForAll(id))
                .ReturnsAsync(vehicle);
            _mockUnitOfWork.Setup(u => u.BodyTypeRepository.IsExistAsync(It.IsAny<Expression<Func<BodyType, bool>>>()))
                .ReturnsAsync((Expression<Func<BodyType, bool>> predicate) => _bodyTypes.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.DriveTypeRepository.IsExistAsync(It.IsAny<Expression<Func<DriveType, bool>>>()))
                .ReturnsAsync((Expression<Func<DriveType, bool>> predicate) => _driveTypes.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.FuelTypeRepository.IsExistAsync(It.IsAny<Expression<Func<FuelType, bool>>>()))
                .ReturnsAsync((Expression<Func<FuelType, bool>> predicate) => _fuelTypes.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.MarketRepository.IsExistAsync(It.IsAny<Expression<Func<Market, bool>>>()))
                .ReturnsAsync((Expression<Func<Market, bool>> predicate) => _markets.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.ModelRepository.IsExistAsync(It.IsAny<Expression<Func<Model, bool>>>()))
                .ReturnsAsync((Expression<Func<Model, bool>> predicate) => _models.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.BrandRepository.IsExistAsync(It.IsAny<Expression<Func<Brand, bool>>>()))
                .ReturnsAsync((Expression<Func<Brand, bool>> predicate) => _brands.Any(predicate.Compile()));
            
            var vehicleService = new VehicleService(_mockUnitOfWork.Object, _mapper, _mockImageService.Object);
            
            // Act and  Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(async ()=> await vehicleService.UpdateAsync(id, updateVehicleDto));
        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void UpdateAsync_OnError_ThrowsEntityNotFoundExceptionForTransmission(int id)
        {
            //Arrange
            var updateVehicleDto = new UpdateVehicleDto()
            {
                Price = 400000,
                Mileage = 0,
                YearOfManufacture = 1998,
                PowerOutput = 2000,
                EngineDisplacement = 200,
                BodyTypeId = 100,
                FuelTypeId = 2,
                ColorId = 2,
                DriveTypeId = 2,
                ModelId = 2,
                BrandId = 1
                
            };     
            var vehicle = _vehicles.FirstOrDefault(m => m.Id == id);
            
            _mockUnitOfWork.Setup(u => u.VehicleRepository.GetByIdAsyncForAll(id))
                .ReturnsAsync(vehicle);
            
            _mockUnitOfWork.Setup(u => u.BodyTypeRepository.IsExistAsync(It.IsAny<Expression<Func<BodyType, bool>>>()))
                .ReturnsAsync((Expression<Func<BodyType, bool>> predicate) => _bodyTypes.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.DriveTypeRepository.IsExistAsync(It.IsAny<Expression<Func<DriveType, bool>>>()))
                .ReturnsAsync((Expression<Func<DriveType, bool>> predicate) => _driveTypes.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.FuelTypeRepository.IsExistAsync(It.IsAny<Expression<Func<FuelType, bool>>>()))
                .ReturnsAsync((Expression<Func<FuelType, bool>> predicate) => _fuelTypes.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.MarketRepository.IsExistAsync(It.IsAny<Expression<Func<Market, bool>>>()))
                .ReturnsAsync((Expression<Func<Market, bool>> predicate) => _markets.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.ModelRepository.IsExistAsync(It.IsAny<Expression<Func<Model, bool>>>()))
                .ReturnsAsync((Expression<Func<Model, bool>> predicate) => _models.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.BrandRepository.IsExistAsync(It.IsAny<Expression<Func<Brand, bool>>>()))
                .ReturnsAsync((Expression<Func<Brand, bool>> predicate) => _brands.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.TransmissionRepository.IsExistAsync(It.IsAny<Expression<Func<Transmission, bool>>>()))
                .ReturnsAsync((Expression<Func<Transmission, bool>> predicate) => _transmissions.Any(predicate.Compile()));
            
            var vehicleService = new VehicleService(_mockUnitOfWork.Object, _mapper, _mockImageService.Object);
            
            // Act and  Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(async ()=> await vehicleService.UpdateAsync(id, updateVehicleDto));
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void UpdateAsync_OnError_ThrowsEntityNotFoundExceptionForColor(int id)
        {
            //Arrange
            var updateVehicleDto = new UpdateVehicleDto()
            {
                Price = 400000,
                Mileage = 0,
                YearOfManufacture = 1998,
                PowerOutput = 2000,
                EngineDisplacement = 200,
                BodyTypeId = 100,
                FuelTypeId = 2,
                ColorId = 2,
                DriveTypeId = 2,
                ModelId = 2,
                BrandId = 1
                
            };     
            var vehicle = _vehicles.FirstOrDefault(m => m.Id == id);
            
            _mockUnitOfWork.Setup(u => u.VehicleRepository.GetByIdAsyncForAll(id))
                .ReturnsAsync(vehicle);
            
            _mockUnitOfWork.Setup(u => u.BodyTypeRepository.IsExistAsync(It.IsAny<Expression<Func<BodyType, bool>>>()))
                .ReturnsAsync((Expression<Func<BodyType, bool>> predicate) => _bodyTypes.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.DriveTypeRepository.IsExistAsync(It.IsAny<Expression<Func<DriveType, bool>>>()))
                .ReturnsAsync((Expression<Func<DriveType, bool>> predicate) => _driveTypes.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.FuelTypeRepository.IsExistAsync(It.IsAny<Expression<Func<FuelType, bool>>>()))
                .ReturnsAsync((Expression<Func<FuelType, bool>> predicate) => _fuelTypes.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.MarketRepository.IsExistAsync(It.IsAny<Expression<Func<Market, bool>>>()))
                .ReturnsAsync((Expression<Func<Market, bool>> predicate) => _markets.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.ModelRepository.IsExistAsync(It.IsAny<Expression<Func<Model, bool>>>()))
                .ReturnsAsync((Expression<Func<Model, bool>> predicate) => _models.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.BrandRepository.IsExistAsync(It.IsAny<Expression<Func<Brand, bool>>>()))
                .ReturnsAsync((Expression<Func<Brand, bool>> predicate) => _brands.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.TransmissionRepository.IsExistAsync(It.IsAny<Expression<Func<Transmission, bool>>>()))
                .ReturnsAsync((Expression<Func<Transmission, bool>> predicate) => _transmissions.Any(predicate.Compile()));
            _mockUnitOfWork.Setup(u => u.ColorRepository.IsExistAsync(It.IsAny<Expression<Func<Color, bool>>>()))
                .ReturnsAsync((Expression<Func<Color, bool>> predicate) => _colors.Any(predicate.Compile()));
            
            var vehicleService = new VehicleService(_mockUnitOfWork.Object, _mapper, _mockImageService.Object);
            
            // Act and  Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(async ()=> await vehicleService.UpdateAsync(id, updateVehicleDto));
        }
        
        // [Theory]
        // [InlineData(1)]
        // [InlineData(2)]
        // [InlineData(3)]
        // public async void UpdateAsync_OnError_ThrowsDuplicateNameException(int id)
        // {
        //     //Arrange
        //     var updateVehicleDto = new UpdateVehicleDto() {VehicleName = "Robot", IsDeleted = false};
        //     var vehicle = _vehicles.FirstOrDefault(m => m.Id == id);
        //     
        //     _mockUnitOfWork.Setup(u => u.VehicleRepository.GetByIdAsyncForAll(id))
        //         .ReturnsAsync(vehicle);
        //     _mockUnitOfWork.Setup(u => u.VehicleRepository.IsExistAsync(It.IsAny<Expression<Func<Vehicle, bool>>>()))
        //         .ReturnsAsync((Expression<Func<Vehicle, bool>> predicate) => _vehicles.Any(predicate.Compile()));
        //     
        //     var vehicleService = new VehicleService(_mockUnitOfWork.Object, _mapper, _mockImageService.Object);
        //     
        //     // Act and  Assert
        //     await Assert.ThrowsAsync<DuplicateNameException>(async ()=> await vehicleService.UpdateAsync(id, updateVehicleDto));
        // }
}