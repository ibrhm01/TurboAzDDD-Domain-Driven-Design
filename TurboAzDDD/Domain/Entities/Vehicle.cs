using System;
using Domain.ENUMs;

namespace Domain.Entities
{
	public class Vehicle : BaseEntity
    {

        public double Price { get; set; }
        public double Mileage { get; set; }
        public string? Description { get; set; }
        public bool IsDamaged { get; set; }
        public bool IsBroken { get; set; }
        public bool IsColored { get; set; }
        public int YearOfManufacture { get; set; }
        public int PowerOutput { get; set; }
        public bool IsBarterPossible { get; set; }
        public bool WithCredit { get; set; }
        public NumberOfOwners NumberOfOwners { get; set; }
        //public NumberOfSeats NumberOfSeats { get; set; }
        public string? VinCode { get; set; }

        public List<TagVehicle>? TagVehicles { get; set; }
        public int BrandId { get; set; }
        public Brand? Brand { get; set; } 

        public int MarketId { get; set; }
        public Market? Market { get; set; }

        public int BodyTypeId { get; set; }
        public BodyType? BodyType { get; set; }

        public int FuelTypeId { get; set; }
        public FuelType? FuelType { get; set; }

        public int TransmissionId { get; set; }
        public Transmission? Transmission { get; set; }

        public int ColorId { get; set; }
        public Color? Color { get; set; }

        public int DriveTypeId { get; set; }
        public DriveType? DriveType { get; set; }

        public int SalonId { get; set; }
        public Salon? Salon { get; set; }

        public List<Image>? Images { get; set; }

	}
}

