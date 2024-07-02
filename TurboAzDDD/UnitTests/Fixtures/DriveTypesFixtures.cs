using DriveType = Domain.Entities.DriveType;

namespace UnitTests.Fixtures;

public static class DriveTypesFixtures
{
    public static List<DriveType> DriveTypes() => new()
    {
        new DriveType()
        {
            Id =1,
            DriveTypeName ="Arxa",
            IsDeleted = false,
            // CreatedDate = DateTime.UtcNow
        },

        new DriveType()
        {
            Id =2,
            DriveTypeName ="Ön",
            IsDeleted = false,
            // CreatedDate = DateTime.UtcNow
        }

    };
}