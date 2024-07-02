using Domain.DTOs.Market;
using Domain.Entities;

namespace UnitTests.Fixtures
{
	public static class MarketsFixtures
	{
		public static List<Market> Markets() => new()
		{
			new Market()
			{
                Id =1,
                MarketName ="Amerika",
                IsDeleted = false,
                // CreatedDate = DateTime.UtcNow
            },

           new Market()
            {
                Id =2,
                MarketName ="Dubay",
                IsDeleted = false,
                // CreatedDate = DateTime.UtcNow
            },
           new Market()
            {   Id =3,
                MarketName ="Koreya",
                IsDeleted = false,
                // CreatedDate = DateTime.UtcNow
            },

        };

    }
}

