using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.Market
{
	public class CreateMarketDto
	{
        [Required]
        public string MarketName { get; set; } = null!;
    }
}

