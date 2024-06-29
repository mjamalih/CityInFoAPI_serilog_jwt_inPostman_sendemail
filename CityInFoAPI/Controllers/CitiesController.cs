using AutoMapper;
using CityInFoAPI.Models;
using CityInFoAPI.Repositoties;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CityInFoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CitiesController : ControllerBase
    {
        #region without_sqlite
        ////[HttpGet]
        ////public JsonResult GetCities()
        ////{
        ////    return new JsonResult(
        ////        new List<object>
        ////        {
        ////            new {id=1, name="tehran" },
        ////            new {id=2, name="karaj" },
        ////            new {id=3, name="kaki" }
        ////        });
        ////}

        //[HttpGet]
        //public ActionResult<IEnumerable<CityDto>> GetCities()
        //{
        //var cities=CitiesDataStore.current.Cities.ToList();
        //    return Ok(cities);
        //}
        //[HttpGet("{id}")]
        //public ActionResult<CityDto?> GetCities(int id)
        //{
        //    var city= CitiesDataStore.current.Cities.FirstOrDefault(x=>x.Id== id);
        //    if (city == null) 
        //        return NotFound();
        //    else 
        //        return Ok(city);
        //}
        #endregion without_sqlite


        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;

        public CitiesController(ICityInfoRepository cityInfoRepository, IMapper mapper)
        {
            _cityInfoRepository = cityInfoRepository ??
                throw new ArgumentNullException(nameof(cityInfoRepository));

            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityWithoutPointOfInterestDto>>> GetCities()
        {
            var Cities = await _cityInfoRepository.GetCitiesAsync();

            return Ok(
                _mapper.Map<IEnumerable<CityWithoutPointOfInterestDto>>(Cities)
                );

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CityDto>> GetCity(int id, bool includePointsOfInterest=false)
        {
            var city=await _cityInfoRepository.GetCityAsync(id,includePointsOfInterest);
            if (city == null)
                return NotFound();
            if(includePointsOfInterest)
              return Ok( _mapper.Map<CityDto>(city));
            return Ok(_mapper.Map<CityWithoutPointOfInterestDto>(city));


            

        }


    }
}
