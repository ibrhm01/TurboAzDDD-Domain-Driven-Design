using System;
using Domain.ENUMs;

namespace Domain.DTOs.Vehicle
{
	public class GetVehicleDto
	{
        public double Price { get; set; }
        public double Mileage { get; set; }
        public string? Description { get; set; }
        public bool? IsDamaged { get; set; }
        public bool? IsBroken { get; set; }
        public bool? IsColored { get; set; }
        public int YearOfManufacture { get; set; }
        public int PowerOutput { get; set; }
        public int EngineDisplacement { get; set; }
        public bool? IsBarterPossible { get; set; }
        public bool? WithCredit { get; set; }
        public NumberOfOwners? NumberOfOwners { get; set; }
        public NumberOfSeats? NumberOfSeats { get; set; }
        public string? VinCode { get; set; }


        public int BrandId { get; set; }
        public int ModelId { get; set; }
        public int? MarketId { get; set; }
        public int BodyTypeId { get; set; }
        public int FuelTypeId { get; set; }
        public int? TransmissionId { get; set; }
        public int ColorId { get; set; }
        public int DriveTypeId { get; set; }
        public int? SalonId { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}

