namespace UnitTests.Fixtures;

public class BrandsFixtures
{
    public static List<Brand> Brands() => new()
    {
        new Brand()
        {
            Id =1,
            BrandName ="Alfa Romeo",
            IsDeleted = false,
            // CreatedDate = DateTime.UtcNow
        },

        new Brand()
        {
            Id =2,
            BrandName ="Aprilia",
            IsDeleted = false,
            // CreatedDate = DateTime.UtcNow
        },
        new Brand()
        {   Id =3,
            BrandName ="Audi",
            IsDeleted = false,
            // CreatedDate = DateTime.UtcNow
        },

    };
}