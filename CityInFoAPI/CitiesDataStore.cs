using CityInFoAPI.Models;

namespace CityInFoAPI
{
    public class CitiesDataStore
    {
        public List<CityDto> Cities { get; set; }

        public static CitiesDataStore current { get; } = new CitiesDataStore();

        public CitiesDataStore()
        {
            Cities = new List<CityDto>()
            {
                new CityDto() { Id = 1, Name = "Tehran",
                Description ="This  is My City" ,
                 PointOfInterest=new List<PointOfInterestDto>()
                 {
                     new PointOfInterestDto()
                     {
                          Id=1,
                          Description="jay didani 1",
                          Name="point of interest 1"
                     },
                      new PointOfInterestDto()
                     {
                          Id=2,
                          Description="jay didani 2",
                          Name="point of interest 2"
                     }
                 }

                },
                 new CityDto() { Id = 2, Name = "Shiraz",
                Description ="This  is My City" },
                  new CityDto() { Id = 3, Name = "Ahwaz",
                Description ="This  is My City" },
            };
        }
    }

}
