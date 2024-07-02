namespace UnitTests.Fixtures;

public class BodyTypesFixtures
{
    public static List<BodyType> BodyTypes() => new()
    {
        new BodyType()
        {
            Id =1,
            BodyTypeName ="Avtobus",
            IsDeleted = false,
            // CreatedDate = DateTime.UtcNow
        },

        new BodyType()
        {
            Id =2,
            BodyTypeName ="Fayton",
            IsDeleted = false,
            // CreatedDate = DateTime.UtcNow
        },
        new BodyType()
        {   Id =3,
            BodyTypeName ="Furqon",
            IsDeleted = false,
            // CreatedDate = DateTime.UtcNow
        },

    };
}