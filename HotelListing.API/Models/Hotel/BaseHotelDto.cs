﻿using System.ComponentModel.DataAnnotations;

namespace HotelListing.API.Models.Hotel
{
    public class BaseHotelDto
    {
        [Required]
        public string Name { get; set; }
        public string Address { get; set; }
        public double? Rating { get; set; }
        
        [Required]
        [Range(1, int.MaxValue)]
        public int CountryId { get; set; }

        
    }
}
