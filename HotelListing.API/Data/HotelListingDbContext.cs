using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Data
{
    public class HotelListingDbContext : DbContext
    {
        public HotelListingDbContext(DbContextOptions options): base(options) 
        { 
        }

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Country>Countries { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Country>().HasData(
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

            modelBuilder.Entity<Hotel>().HasData(

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
