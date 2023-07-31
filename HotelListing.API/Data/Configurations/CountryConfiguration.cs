using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelListing.API.Data.Configurations
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasData(
                new Country
                {
                    id = 1,
                    Name = "United States",
                    ShortName = "US"
                },

                new Country
                {
                    id = 2,
                    Name = "United Kingdom",
                    ShortName = "UK"
                },

                new Country
                {
                    id = 3,
                    Name = "Jamaica",
                    ShortName = "JM"
                }

                );
        }
    }
}
