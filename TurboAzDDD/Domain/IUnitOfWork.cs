using Domain.Repositories;

namespace Domain
{
	public interface IUnitOfWork : IDisposable
	{
		public IVehicleRepository VehicleRepository { get; }
        public IBrandRepository BrandRepository { get; }
        public IColorRepository ColorRepository { get; }
        public IFuelTypeRepository FuelTypeRepository { get; }
        public ITransmissionRepository TransmissionRepository { get; }
        public IBodyTypeRepository BodyTypeRepository { get; }
        public ISalonRepository SalonRepository { get; }
        public IMarketRepository MarketRepository { get; }
        public IDriveTypeRepository DriveTypeRepository { get; }
        public IModelRepository ModelRepository { get; }
        int Complete();
        Task<int> CompleteAsync();
    }
}