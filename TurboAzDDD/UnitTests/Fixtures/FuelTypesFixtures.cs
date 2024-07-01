using Domain.DTOs.FuelType;
using Domain.Entities;

namespace UnitTests.Fixtures;

public static class FuelTypesFixtures
{
    public static List<FuelType> FuelTypes() => new()
    {
        new FuelType()
        {
            Id =1,
            FuelTypeName ="Benzin",
            IsDeleted = false,
            // CreatedDate = DateTime.UtcNow
        },

        new FuelType()
        {
            Id =2,
            FuelTypeName ="Dizel",
            IsDeleted = false,
            // CreatedDate = DateTime.UtcNow
        },
        new FuelType()
        {   Id =3,
            FuelTypeName ="Qaz",
            IsDeleted = false,
            // CreatedDate = DateTime.UtcNow
        },

    };

    public static CreateFuelTypeDto CreateFuelTypeDto() => new CreateFuelTypeDto()
    {
        FuelTypeName = "Benzin",
    };
        
    public static UpdateFuelTypeDto UpdateFuelTypeDto() => new UpdateFuelTypeDto()
    {
        FuelTypeName = "Dizel",
        IsDeleted = false
    };
}