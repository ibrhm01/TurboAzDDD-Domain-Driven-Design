namespace UnitTests.Fixtures;

public class ModelsFixtures
{
    public static List<Model> Models() => new()
    {
        new Model()
        {
            Id =1,
            ModelName = "ILX",
            BrandId = 1,
            IsDeleted = false,
            // CreatedDate = DateTime.UtcNow
        },

        new Model()
        {
            Id =2,
            ModelName = "MDX",
            BrandId = 1,
            IsDeleted = false,
            // CreatedDate = DateTime.UtcNow
        },
        new Model()
        {   Id =3,
            ModelName = "ZDX",
            BrandId = 1,
            IsDeleted = false,
            // CreatedDate = DateTime.UtcNow
        },

    };
}