using AutoMapper;
using CityInfo.API.Services;
using CityInFoAPI.Models;
using CityInFoAPI.Repositoties;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CityInFoAPI.Controllers
{
    //  /api/cities/3/pointsofinterest

    [Route("api/cities/{cityId}/PointsOfInterest")]
    [ApiController]
    public class PointsOfInterestController : ControllerBase
    {
        #region without_sqlite
        //private readonly ILogger<PointsOfInterestController> _logger;
        //private readonly IMailService _localMailService;

        //public PointsOfInterestController(ILogger<PointsOfInterestController> logger, IMailService localMailService)
        //{
        //    this._logger = logger;
        //    this._localMailService = localMailService;
        //}

        //[HttpGet]
        //public ActionResult<IEnumerable<PointOfInterestDto>> GetPointsOfInterest(int cityId)
        //{
        //    var points = CitiesDataStore.current.Cities.FirstOrDefault(x => x.Id == cityId);
        //    if (points == null)
        //    {
        //        _logger.LogInformation($"City with Id={cityId} not found");
        //        return NotFound();
        //    }
        //    return Ok(points);
        //}
        //[HttpGet("{pointOfInterestId}", Name = "GetPointOfInterest")]
        //public ActionResult<PointOfInterestDto> GetPointOfInterest(int cityId, int pointOfInterestId)
        //{
        //    var point = CitiesDataStore.current.Cities.FirstOrDefault(c => c.Id == cityId).PointOfInterest
        //        .FirstOrDefault(p => p.Id == pointOfInterestId);
        //    if (point == null)
        //        return NotFound();
        //    return Ok(point);

        //}

        //[HttpPost]
        //public ActionResult<PointOfInterestDto> CreatePointOfInterest
        //    (int cityId, PointOfInterestForCreationDto pointOfInterestForCreationDto
        //    )
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest();
        //    var city = CitiesDataStore.current.Cities.FirstOrDefault(x => x.Id == cityId);
        //    if (city == null)
        //        return NotFound();
        //    var maxIdOfInterest = CitiesDataStore.current.Cities.SelectMany(c => c.PointOfInterest).Max(p => p.Id);
        //    var newPoint =  new PointOfInterestDto()
        //         {
        //             Id = maxIdOfInterest + 1,
        //             Description = pointOfInterestForCreationDto.Description,
        //             Name = pointOfInterestForCreationDto.Name,
        //         };
        //    city.PointOfInterest.Add( newPoint);

        //    //  return Ok(newPoint);
        //    return CreatedAtAction("GetPointOfInterest",
        //        new
        //        {
        //            cityId = cityId,
        //            pointOfInterestId = newPoint.Id
        //        },
        //        newPoint
        //        );
        //}
        //[HttpPut("{pointOfInterestId}")]
        //public ActionResult UpdatePoinOfInterest(int cityId, int pointOfInterestId,
        //    PointOfInterestForUpdateDto pointOfInterestForUpdateDto)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest();
        //    var city = CitiesDataStore.current.Cities.FirstOrDefault(x => x.Id == cityId);
        //    if (city == null)
        //        return NotFound();
        //    var point = city.PointOfInterest.FirstOrDefault(p => p.Id == pointOfInterestId);
        //    if (point == null)
        //        return NotFound();

        //   point.Name=pointOfInterestForUpdateDto.Name;
        //   point.Description=pointOfInterestForUpdateDto.Description;
        //    return NoContent();
        //}


        //[HttpPatch("{pointOfInterestId}")]
        //public ActionResult PartiallyUpdatePoinOfInterest(int cityId, int pointOfInterestId,
        //    JsonPatchDocument<PointOfInterestForUpdateDto> patchDocument)
        //{

        //    if (!ModelState.IsValid)
        //        return BadRequest();
        //    var city = CitiesDataStore.current.Cities.FirstOrDefault(x => x.Id == cityId);
        //    if (city == null)
        //        return NotFound();
        //    var point = city.PointOfInterest.FirstOrDefault(p => p.Id == pointOfInterestId);
        //    if (point == null)
        //        return NotFound();
        //    var poinOfInterestToPatch = new PointOfInterestForUpdateDto
        //    {
        //        Name = point.Name,
        //        Description = point.Description
        //    };
        //    patchDocument.ApplyTo(poinOfInterestToPatch, (Microsoft.AspNetCore.JsonPatch.Adapters.IObjectAdapter)ModelState);

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest();
        //    }
        //    if (!TryValidateModel(poinOfInterestToPatch))
        //        return BadRequest(modelState: ModelState);


        //    point.Name = poinOfInterestToPatch.Name;
        //    point.Description = poinOfInterestToPatch.Description;

        //    return NoContent();

        //    return null;
        //}

        //[HttpDelete("{pointOfInterestId}")]

        //public ActionResult DeletePoinOfInterest(int cityId, int pointOfInterestId)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest();
        //    var city = CitiesDataStore.current.Cities.FirstOrDefault(x => x.Id == cityId);
        //    if (city == null)
        //        return NotFound();
        //    var point = city.PointOfInterest.FirstOrDefault(p => p.Id == pointOfInterestId);
        //    if (point == null)
        //        return NotFound();
        //    city.PointOfInterest.Remove(point);
        //    LocalMailService.Email("deletion", $"point of interest with Id= {pointOfInterestId} in city with id={cityId} was deleted.", "mehdi.jamali@gmail.com");
        //    return NoContent();
        //}
        #endregion without_sqlite

        private readonly ILogger<PointsOfInterestController> _logger;
        private readonly IMailService _localMailService;

        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;


        private readonly CitiesDataStore citiesDataStore;

        public PointsOfInterestController(ILogger<PointsOfInterestController> loger,
            IMailService localMailService,
            CitiesDataStore citiesDataStore
            , ICityInfoRepository cityInfoRepository, IMapper mapper)
        {
            _logger = loger ?? throw new ArgumentNullException(nameof(loger));
            _localMailService = localMailService ?? throw new ArgumentNullException(nameof(localMailService));

            _cityInfoRepository = cityInfoRepository ??
               throw new ArgumentNullException(nameof(cityInfoRepository));

            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

            this.citiesDataStore = citiesDataStore;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<PointOfInterestDto>>>
            GetPointsOfInterest(int cityId)
        {

            if (await _cityInfoRepository.GetCityAsync(cityId)==null)
            {
                _logger.LogInformation($"{cityId} Not Found ...");
                return NotFound();
            }

            var pointsOfInterestForCity = await _cityInfoRepository
                .GetPointsOfInterestForCityAsync(cityId);

            return Ok(_mapper.Map<IEnumerable<PointOfInterestDto>>(pointsOfInterestForCity));
        }

        [HttpGet("{pointOfInterestId}", Name = "GetPointOfInterest")]
        public  async Task< ActionResult<PointOfInterestDto>> GetPointOfInterest(
            int cityId, int pointOfInterestId
            )
        {
            var city = await _cityInfoRepository.GetCityAsync(cityId);
               
            if (city == null)
            {
                return NotFound();
            }

            var point = await _cityInfoRepository.GetPointOfInterestForCityAsync(cityId,pointOfInterestId);

            if (point == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<PointOfInterestDto>(point));
        }


        #region  Post
        [HttpPost]
        public async Task<ActionResult<PointOfInterestDto>> CreatePointOfInterest(
          int cityId,
          PointOfInterestForCreationDto pointOfInterest
          )
        {
            if (await _cityInfoRepository.GetCityAsync(cityId)==null)
            {
                return NotFound();
            }

            var finalPoint = _mapper.Map<Entities.PointOfInterest>(pointOfInterest);
            await _cityInfoRepository.AddPointOfInterestForCityAsync(
                cityId, finalPoint);
            await _cityInfoRepository.SaveChangesAsync();

            var createdpoint = _mapper.Map<Models.PointOfInterestDto>(finalPoint);

            return CreatedAtRoute("GetPointOfInterest", new
            {
                cityId = cityId,
                pointOfInterestId = createdpoint.Id
            }, createdpoint);
        }
        #endregion


        #region Edit
        [HttpPut("{pontiOfInterestId}")]
        public async Task<ActionResult> UpdatePointOfInterest(int cityId,
            int pontiOfInterestId,
            PointOfInterestForUpdateDto pointOfInterest)
        {

            if (await _cityInfoRepository.GetCityAsync(cityId)==null)
            {
                return NotFound();
            }

            var point = await _cityInfoRepository
                .GetPointOfInterestForCityAsync(cityId, pontiOfInterestId);

            if (point == null)
            {
                return NotFound();
            }

            _mapper.Map(pointOfInterest, point);

            await _cityInfoRepository.SaveChangesAsync();

            return NoContent();

        }
        #endregion



        #region  Edit with patch
        [HttpPatch("{pontiOfInterestid}")]
        public async Task<ActionResult> PartiallyUpdatePointOfOnterest(
            int cityId,
            int pontiOfInterestid,
            JsonPatchDocument<PointOfInterestForUpdateDto> patchDocument
            )
        {
            if (await _cityInfoRepository.GetCityAsync(cityId)==null)
            {
                return NotFound();
            }

            var pointEntity = await _cityInfoRepository
                 .GetPointOfInterestForCityAsync(cityId, pontiOfInterestid);
            if (pointEntity == null)
            {
                return NotFound();
            }

            var pointToPatch = _mapper.Map<PointOfInterestForUpdateDto>
                (pointEntity);

            patchDocument.ApplyTo(pointToPatch, (Microsoft.AspNetCore.JsonPatch.Adapters.IObjectAdapter)ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(pointToPatch))
            {
                return BadRequest(ModelState);
            }
            _mapper.Map(pointToPatch, pointEntity);
            await _cityInfoRepository.SaveChangesAsync();

            return NoContent();
        }

        #endregion

        #region Delete
        [HttpDelete("{pontiOfInterestId}")]
        public async Task<ActionResult> DeletePointOfInterest(
            int cityId,
            int pontiOfInterestId)
        {

            if (await _cityInfoRepository.GetCityAsync(cityId)==null)
            {
                return NotFound();
            }

            var pointOfInterestEntity =
                await _cityInfoRepository
                .GetPointOfInterestForCityAsync(cityId, pontiOfInterestId);

            if (pointOfInterestEntity == null)
            {
                return NotFound();
            }

            _cityInfoRepository.DeletePointOfInterest(pointOfInterestEntity);
            await _cityInfoRepository.SaveChangesAsync();

            _localMailService
                .Send(
                "Point Of intrest deleted",
                $"Point Of Interest {pointOfInterestEntity.Name}with id {pointOfInterestEntity.Id}"
                );

            return NoContent();
        }

        #endregion


    }
}
