using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.Pkcs;

namespace HotelListing.API.Models.Hotel
{
    public class HotelDto : BaseHotelDto
    {

        public int Id { get; set; }
        //public string Name { get; set; }
        //public string Address { get; set; }
        //public double Rating { get; set; }

        //[ForeignKey(nameof(CountryId))]
        //public int CountryId { get; set; }
       

    }
}