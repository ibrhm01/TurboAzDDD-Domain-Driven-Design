namespace UnitTests.Fixtures;

public class VehiclesFixtures
{
    public static List<Vehicle> Vehicles() => new()
    {
        new Vehicle()
        {
            Id = 1,
            Price = 100000,
            Mileage = 0,
            YearOfManufacture = 2021,
            PowerOutput = 3000,
            EngineDisplacement = 500,
            BodyTypeId = 1,
            FuelTypeId = 1,
            ColorId = 1,
            DriveTypeId = 1,
            ModelId = 21,
            IsDeleted = false,
            // CreatedDate = DateTime.UtcNow
        },

        new Vehicle()
        {
            Id = 2,
            Price = 90000,
            Mileage = 0,
            YearOfManufacture = 2022,
            PowerOutput = 2000,
            EngineDisplacement = 300,
            BodyTypeId = 1,
            FuelTypeId = 1,
            ColorId = 1,
            DriveTypeId = 1,
            ModelId = 21,
            IsDeleted = false,
            // CreatedDate = DateTime.UtcNow
        },
        new Vehicle()
        {
            Id = 3,
            Price = 100000,
            Mileage = 0,
            YearOfManufacture = 2024,
            PowerOutput = 7000,
            EngineDisplacement = 900,
            BodyTypeId = 1,
            FuelTypeId = 1,
            ColorId = 1,
            DriveTypeId = 1,
            ModelId = 21,
            IsDeleted = false,
            // CreatedDate = DateTime.UtcNow
        },

    };
}