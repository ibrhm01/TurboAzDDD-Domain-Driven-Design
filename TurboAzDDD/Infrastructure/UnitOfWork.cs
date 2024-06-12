using Domain;
using Domain.Repositories;
using Infrastructure.Data.Context;
using Infrastructure.Data.Repositories;

namespace Infrastructure.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _appDbContext;

        public UnitOfWork(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public IVehicleRepository? _vehicleRepository;

        public IVehicleRepository VehicleRepository => _vehicleRepository ??= new VehicleRepository(_appDbContext);

        private IBrandRepository? _brandRepository;

        public IBrandRepository BrandRepository => _brandRepository ??= new BrandRepository(_appDbContext);

        private IColorRepository? _colorRepository;

        public IColorRepository ColorRepository => _colorRepository ??= new ColorRepository(_appDbContext);

        private IFuelTypeRepository? _fuelTypeRepository;

        public IFuelTypeRepository FuelTypeRepository => _fuelTypeRepository ??= new FuelTypeRepository(_appDbContext);

        public ITransmissionRepository? _transmissionRepository;

        public ITransmissionRepository TransmissionRepository => _transmissionRepository ??= new TransmissionRepository(_appDbContext);

        public IBodyTypeRepository? _bodyTypeRepository;

        public IBodyTypeRepository BodyTypeRepository => _bodyTypeRepository ??= new BodyTypeRepository(_appDbContext);

        public int Complete()
        {
            return _appDbContext.SaveChanges();
        }
        public async Task<int> CompleteAsync()
        {
            return await _appDbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _appDbContext.Dispose();

        }
    }
}

