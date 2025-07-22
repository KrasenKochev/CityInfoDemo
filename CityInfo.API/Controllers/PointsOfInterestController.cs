using AutoMapper;
using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CityInfo.API.Controllers
{
	[Route("api/cities/{cityId}/pointsofinterest")]
	[ApiController]
	public class PointsOfInterestController : ControllerBase
	{
		private readonly ILogger<PointsOfInterestController> _logger;
		private readonly IMailService _mailService;
		private readonly ICityInfoRepository _cityInfoRepository;
		private readonly IMapper _mapper;

		public PointsOfInterestController(ILogger<PointsOfInterestController> logger,
			IMailService MailService,
			ICityInfoRepository cityInfoRepository,
			IMapper mapper)
		{
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
			_mailService = MailService ?? throw new ArgumentNullException(nameof(MailService));
			_cityInfoRepository = cityInfoRepository ?? throw new ArgumentNullException(nameof(cityInfoRepository));
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
		}




		[HttpGet]
		public async  Task< ActionResult<IEnumerable<PointOfInterestDto>>> GetPointsOfInterest(int cityId)
		{
			if (!await _cityInfoRepository.CityExistAsync(cityId))
			{
				_logger.LogInformation($"City with id {cityId} wasnt found when accessing points of interest.");
				return NotFound();
			}

			var pointsOfInterestForCity = await _cityInfoRepository
				.GetPointsOfInterestForCityAsync(cityId);

			return Ok (_mapper.Map<IEnumerable<PointOfInterestDto>>(pointsOfInterestForCity));	
			
		}

		[HttpGet("{pointOfinterestId}", Name ="GetPointOfInterest")]
		public async Task< ActionResult<PointOfInterestDto>> getPointOfInterest(int cityId, int pointOfInterestId)
		{
			if (!await _cityInfoRepository.CityExistAsync(cityId))
			{
				return NotFound();
			}
			var pointOfInterest = await _cityInfoRepository
				.GetPointOfInterestForCityAsync(cityId, pointOfInterestId);

			if(pointOfInterest == null)
			{
				return NotFound();
			}

			return Ok(_mapper.Map<PointOfInterestDto>(pointOfInterest));

		}
		[HttpPost]

		public async Task<ActionResult<PointOfInterestDto>> CreatePointOfInterest(
			int cityId,
			PointOfInterestForCreationDto pointOfInterest)

		{
			if(!await _cityInfoRepository.CityExistAsync(cityId))
			{
				return NotFound();
			}


			var finalPointOfInterest = _mapper.Map<Entities.PointOfInterest>(pointOfInterest);
			
			await _cityInfoRepository.AddPointOfInterestForCityAsync(cityId, finalPointOfInterest);

			await _cityInfoRepository.SaveChangesAsync();	

			var createdPointOfInterestToReturn =
				_mapper.Map<Models.PointOfInterestDto>(finalPointOfInterest);

			return CreatedAtRoute("GetPointOfInterest",
				new
				{
					cityId = cityId,
					pointOfInterestId = createdPointOfInterestToReturn.Id
				}, createdPointOfInterestToReturn);

		}
		[HttpPut("{pointofinteresid}")]
		public async Task< ActionResult> UpdatePointOfInterest(int cityId, int pointofinteresid,
			PointOfInterestForUpdateDto pointOfInterest)
		{
			if (!await _cityInfoRepository.CityExistAsync(cityId))
			{
				return NotFound();
			}

			var pointOfInterestEntity = await _cityInfoRepository
				.GetPointOfInterestForCityAsync(cityId, pointofinteresid);
			if (pointOfInterestEntity == null)
			{
				return NotFound();
			}

			_mapper.Map(pointOfInterest, pointOfInterestEntity);

			await _cityInfoRepository.SaveChangesAsync();

			return NoContent();
		}
		//[HttpPatch("{pointofinterestid}")]

		//public ActionResult PartiallyUpdatedPointOfInterest(
		//	int cityId, int pointOfInterestId,
		//	JsonPatchDocument<PointOfInterestForUpdateDto> patchDocument)
		//{
		//	var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);
		//	if (city == null)
		//	{
		//		return NotFound();
		//	}

		//	var pointOfInterestFromStore = city.PointsOfInterests.FirstOrDefault(c => c.Id == pointOfInterestId);
		//	if (pointOfInterestFromStore == null)
		//	{
		//		return NotFound();
		//	}

		//	var pointofInterestToPatch =
		//		new PointOfInterestForUpdateDto()
		//		{
		//			Name = pointOfInterestFromStore.Name,
		//			Description = pointOfInterestFromStore.Description
		//		};
		//	patchDocument.ApplyTo(pointofInterestToPatch, ModelState);

		//	if (!ModelState.IsValid)
		//	{
		//		return BadRequest(ModelState);

		//	}
		//	if(!TryValidateModel(pointofInterestToPatch))
		//	{
		//		return BadRequest(ModelState);
		//	}

		//	pointOfInterestFromStore.Name = pointofInterestToPatch.Name;
		//	pointOfInterestFromStore.Description = pointofInterestToPatch.Description;

		//	return NoContent();
		//}
		//[HttpDelete("{pointofinterestid}")]

		//public ActionResult DeletePointOfInterest ( int cityId, int pointOfInterestId)
		//{
		//	var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);
		//	if (city == null)
		//	{
		//		return NotFound();
		//	}

		//	var pointOfInterestFromStore = city.PointsOfInterests.FirstOrDefault(c => c.Id == pointOfInterestId);
		//	if (pointOfInterestFromStore == null)
		//	{
		//		return NotFound();
		//	}

		//	city.PointsOfInterests.Remove(pointOfInterestFromStore);

		//	_mailService.Send("Point of interest deleted.", $"Point of interest {pointOfInterestFromStore.Name} " +
		//		$"with id {pointOfInterestFromStore.Id} was deleted");
		//	return NoContent();
		//}

	}
}
