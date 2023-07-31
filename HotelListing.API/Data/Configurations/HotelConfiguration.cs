using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace HotelListing.API.Data.Configurations
{
    public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.HasData(

                new Hotel
                {
                    Id = 1,
                    Name = "Kirk Kingdom",
                    Address = "71 Homestead Dr",
                    Rating = 5.0,
                    CountryId = 1

                },
                new Hotel
                {
                    Id = 2,
                    Name = "Motel Monty Python",
                    Address = "100 Holy Grail Ave",
                    Rating = 4.7,
                    CountryId = 2

                },
                new Hotel
                {
                    Id = 3,
                    Name = "Marley Mansion",
                    Address = "1 Spleef St",
                    Rating = 4.9,
                    CountryId = 3
                },
                new Hotel
                {
                    Id = 4,
                    Name = "Weir Here",
                    Address = "10 Garcia Cr",
                    Rating = 3.7,
                    CountryId = 1

                }
                );
        }
    }
}
