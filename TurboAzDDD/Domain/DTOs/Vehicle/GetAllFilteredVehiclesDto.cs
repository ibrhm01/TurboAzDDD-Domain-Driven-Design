using System;
using Domain.ENUMs;

namespace Domain.DTOs.Vehicle
{
	public class GetAllFilteredVehiclesDto
	{
        public double? PriceMin { get; set; }
        public double? PriceMax { get; set; }
        public double? MileageMin { get; set; }
        public double? MileageMax { get; set; }
        public string? Description { get; set; } = null!;
        public bool? IsDamaged { get; set; }
        public bool? IsBroken { get; set; }
        public bool? IsColored { get; set; }
        public int? YearOfManufactureMin { get; set; }
        public int? YearOfManufactureMax { get; set; }
        public int? PowerOutputMin { get; set; }
        public int? PowerOutputMax { get; set; }
        public int? EngineDisplacementMin { get; set; }
        public int? EngineDisplacementMax { get; set; }
        public bool? IsBarterPossible { get; set; }
        public bool? WithCredit { get; set; }
        public NumberOfOwners? NumberOfOwners { get; set; }
        public NumberOfSeats? NumberOfSeats { get; set; }
        public string? VinCode { get; set; } = null!;


        public int? BrandId { get; set; }
        public int? ModelId { get; set; }
        public int? MarketId { get; set; }
        public int? BodyTypeId { get; set; }
        public int? FuelTypeId { get; set; }
        public int? TransmissionId { get; set; }
        public int? ColorId { get; set; }
        public int? DriveTypeId { get; set; }
        public int? SalonId { get; set; }

        public List<int>? TagIds { get; set; }
    }
}

