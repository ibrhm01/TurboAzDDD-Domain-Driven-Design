using System;
namespace Domain.DTOs.Market
{
	public class GetMarketDto
	{
        public string MarketName { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}

