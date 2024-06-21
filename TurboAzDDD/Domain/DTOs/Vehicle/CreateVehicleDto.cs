using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Domain.ENUMs;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Annotations;

namespace Domain.DTOs.Vehicle
{
	public class CreateVehicleDto
	{
        [SwaggerSchema(ReadOnly = true)]
        public int Id { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public double Mileage { get; set; }
        public string? Description { get; set; }
        public bool? IsDamaged { get; set; }
        public bool? IsBroken { get; set; }
        public bool? IsColored { get; set; }
        [Required]
        public int YearOfManufacture { get; set; }
        [Required]
        public int PowerOutput { get; set; }
        [Required]
        public int EngineDisplacement { get; set; }
        public bool? IsBarterPossible { get; set; }
        public bool? WithCredit { get; set; }
        public NumberOfOwners? NumberOfOwners { get; set; }
        public NumberOfSeats? NumberOfSeats { get; set; }
        public string? VinCode { get; set; }

        [Required]
        public int BrandId { get; set; }
        [Required]
        public int ModelId { get; set; }
        public int? MarketId { get; set; }
        [Required]
        public int BodyTypeId { get; set; }
        [Required]
        public int FuelTypeId { get; set; }
        public int? TransmissionId { get; set; }
        [Required]
        public int ColorId { get; set; }
        [Required]
        public int DriveTypeId { get; set; }
        public int? SalonId { get; set; }

        public List<int>? TagIds { get; set; }
         
        public IFormFile[]? Photos { get; set; }

    }
}

