using CityInFoAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInFoAPI.DbContexts
{
    public class CityInfoDbContext : DbContext
    {

        public CityInfoDbContext
            (DbContextOptions<CityInfoDbContext> options)
            : base(options)
        {

        }

        public DbSet<City> Cities { get; set; } = null!;
        public DbSet<PointOfInterest> PointsOfInterest { get; set; } = null!;

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite();
        //    base.OnConfiguring(optionsBuilder);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>()
                .HasData(
                new City()
                {
                    Id = 1,
                    Name = "Tehran",
                    Description = "This is Tehran"
                },
                 new City()
                 {
                     Id = 2,
                     Name = "Shiraz",
                     Description = "This is Shiraz"
                 },
                  new City()
                  {
                      Id = 3,
                      Name = "Tabriz",
                      Description = "This is Tabriz"
                  }
                );
            modelBuilder.Entity<PointOfInterest>()
                .HasData(
                new PointOfInterest()
                {
                    Id = 1,
                    CityId = 1,
                    Name = "Azadi Tower",
                    Description = "AZADI       Tower"
                },
                 new PointOfInterest()
                 {
                     Id = 2,
                     CityId = 1,
                     Name = "Shemiran",
                     Description = "This is Shemiran"
                 },
                   new PointOfInterest()
                   {
                       Id = 3,
                       Name = "Meydan ToopKhoone",
                       CityId = 1,
                       Description = "This is ToopKhoone"
                   }
                   );
            base.OnModelCreating(modelBuilder);
        }

    }
}
