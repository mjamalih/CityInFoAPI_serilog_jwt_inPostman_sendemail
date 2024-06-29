using CityInFoAPI.DbContexts;
using CityInFoAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInFoAPI.Repositoties
{
    public class CityInfoRepository : ICityInfoRepository
    {
        private readonly CityInfoDbContext _context;
        public CityInfoRepository(CityInfoDbContext context)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
        }

        public async Task AddPointOfInterestForCityAsync(int cityId, PointOfInterest pointOfInterest)
        {
            var city = await GetCityAsync(cityId, false);
            if (city != null)
            {
                city.PointOfInterest.Add(pointOfInterest);
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }

        public void DeletePointOfInterest(PointOfInterest pointOfInterest)
        {
            _context.PointsOfInterest.Remove(pointOfInterest);
        }

        public async Task<IEnumerable<City>> GetCitiesAsync()
        {
            return await _context.Cities
                 .OrderBy(c => c.Name).ToListAsync();
        }

        public async Task<City?> GetCityAsync(int cityId,bool includePointsOfInterest=false)
        {
           if(includePointsOfInterest)
            {
                return await _context.Cities.Include(c=> c.PointOfInterest)
                 .Where(c => c.Id == cityId).FirstOrDefaultAsync();
            }
            return await _context.Cities
                 .Where(c => c.Id == cityId).FirstOrDefaultAsync();
        }

        public async Task<PointOfInterest?> GetPointOfInterestForCityAsync(int cityId,
            int pointOfInterestId)
        {
            return await _context.PointsOfInterest
                .Where(p => p.CityId == cityId && p.Id == pointOfInterestId)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCityAsync(int cityId)
        {
            return await _context.PointsOfInterest
               .Where(p => p.CityId == cityId)
               .ToListAsync();
        }

       
    }
}
