using Domain.DTOs.Transmission;
using Domain.Entities;

namespace UnitTests.Fixtures;

public class TransmissionsFixtures
{
    public static List<Transmission> Transmissions() => new()
    		{
    			new Transmission()
    			{
                    Id =1,
                    TransmissionName ="Avtomat",
                    IsDeleted = false,
                    // CreatedDate = DateTime.UtcNow
                },
    
               new Transmission()
                {
                    Id =2,
                    TransmissionName ="Mexaniki",
                    IsDeleted = false,
                    // CreatedDate = DateTime.UtcNow
                },
               new Transmission()
                {   Id =3,
                    TransmissionName ="Robot",
                    IsDeleted = false,
                    // CreatedDate = DateTime.UtcNow
                },
    
            };
    
            public static CreateTransmissionDto CreateTransmissionDto() => new CreateTransmissionDto()
            {
                TransmissionName = "Avtomat",
            };
            
            public static UpdateTransmissionDto UpdateTransmissionDto() => new UpdateTransmissionDto()
            {
                TransmissionName = "Mexaniki",
                IsDeleted = false
            };
    
}