using Domain.DTOs.Color;
using Domain.Entities;

namespace UnitTests.Fixtures;

public static class ColorsFixtures
{
    public static List<Color> Colors() => new()
    {
        new Color()
        {
            Id =1,
            ColorName ="Boz",
            IsDeleted = false,
            // CreatedDate = DateTime.UtcNow
        },

        new Color()
        {
            Id =2,
            ColorName ="Gümüşü",
            IsDeleted = false,
            // CreatedDate = DateTime.UtcNow
        },
        new Color()
        {   Id =3,
            ColorName ="Qara",
            IsDeleted = false,
            // CreatedDate = DateTime.UtcNow
        },

    };

    public static CreateColorDto CreateColorDto() => new CreateColorDto()
    {
        ColorName = "Benzin",
    };
        
    public static UpdateColorDto UpdateColorDto() => new UpdateColorDto()
    {
        ColorName = "Dizel",
        IsDeleted = false
    };
}