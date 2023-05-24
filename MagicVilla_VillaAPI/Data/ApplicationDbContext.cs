using MagicVilla_VillaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {

        }
        public DbSet<Villa> Villas { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(
                new Villa()
                {
                    Id = 1,
                    Name = "Happy Villa",
                    Details = "Enjoy more in the villa",
                    ImageUrl = "https://ibb.co/09YGpdT",
                    Occupancy = 5,
                    Rate = 2000,
                    Sqft = 500,
                    Amenity = "",
                    CreatedDate = DateTime.Now,
                },
                new Villa()
                {
                    Id = 2,
                    Name = "Smile Villa",
                    Details = "Be happy more in the villa",
                    ImageUrl = "https://ibb.co/kBMTWMM",
                    Occupancy = 8,
                    Rate = 400,
                    Sqft = 600,
                    Amenity = "",
                    CreatedDate = DateTime.Now,
                },
                new Villa()
                {
                    Id = 3,
                    Name = "Royal Villa",
                    Details = "Royality style more in the villa",
                    ImageUrl = "https://ibb.co/fpbjXMd",
                    Occupancy = 3,
                    Rate = 6000,
                    Sqft = 80,
                    Amenity = "",
                    CreatedDate = DateTime.Now,
                },
                new Villa()
                {
                    Id = 4,
                    Name = "Mountain Villa",
                    Details = "Nature visible more in the villa",
                    ImageUrl = "https://ibb.co/T2F02vY",
                    Occupancy = 50,
                    Rate = 9000,
                    Sqft = 10,
                    Amenity = "",
                    CreatedDate = DateTime.Now,
                },
                new Villa()
                {
                    Id = 5,
                    Name = "Beautiful Villa",
                    Details = "Pleasent more in the villa",
                    ImageUrl = "https://ibb.co/d6qLsXT",
                    Occupancy = 11,
                    Rate = 700,
                    Sqft = 50,
                    Amenity = "",
                    CreatedDate = DateTime.Now,
                }
                );
        }
    }
}
