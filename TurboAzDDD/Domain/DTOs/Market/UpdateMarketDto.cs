using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.Market
{
	public class UpdateMarketDto
	{
        [Required]
        public string MarketName { get; set; } = null!;
        [Required]
        public bool IsDeleted { get; set; }
    }
}

