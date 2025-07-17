using CityInfo.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
	[Route("api/cities/{cityId}/pointsofinterest")]
	[ApiController]
	public class PointsOfInterestController : ControllerBase
	{
		[HttpGet]
		public ActionResult<IEnumerable<PointsOfInterestDto>> GetPointsOfInterest(int cityId)
		{
			var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

			if (city == null) 
			{
				return NotFound(); 
			}


			return Ok(city.PointsOfInterests);
		}
		[HttpGet("{pointOfinterestId}", Name ="GetPointOfInterest")]
		public ActionResult<PointsOfInterestDto> getPointOfInterest(int cityId, int pointOfinterestId)
		{
			var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
			if (city == null) {
				return NotFound();
			}
			var pointOfInterest = city.PointsOfInterests.FirstOrDefault(c => c.Id == pointOfinterestId);
			if (pointOfInterest == null)
			{
				return NotFound();
			}
			return Ok(pointOfInterest);
			{
				
			}
		}
		[HttpPost]

		public ActionResult<PointsOfInterestDto> CreatePointOfInterest(
			int cityId,
			PointOfInterestForCreationDto pointOfInterest)

		{
			var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
			if (city == null) {
				return NotFound();
			}
			var maxPointsOfInterestId = CitiesDataStore.Current.Cities.SelectMany(c => c.PointsOfInterests).Max(p => p.Id);

			var finalPointOfInterest = new PointsOfInterestDto()
			{
				Id = ++maxPointsOfInterestId,
				Name = pointOfInterest.Name,
				Description = pointOfInterest.Description
			};

			city.PointsOfInterests.Add(finalPointOfInterest);

			return CreatedAtRoute("GetPointOfInterest",
				new
				{
					cityId = cityId,
					pointOfInterestId = finalPointOfInterest.Id
				},finalPointOfInterest);
			
		}
		[HttpPut("{pointofinteresid}")]
		public ActionResult UpdatePointOfInterest(int cityId, int pointofinteresid,
			PointOfInterestForUpdateDto pointOfInterest)
		{
			var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
			if (city == null)
			{
				return NotFound();
			}

			var pointOfInterestFromStore = city.PointsOfInterests.FirstOrDefault(c => c.Id == pointofinteresid);
			if (pointOfInterestFromStore == null)
			{
				return NotFound();
			}

			pointOfInterestFromStore.Name = pointOfInterest.Name;
			pointOfInterestFromStore.Description = pointOfInterest.Description;

			return NoContent();
		}
		[HttpPatch("{pointofinterestid}")]

		public ActionResult PartiallyUpdatedPointOfInterest(
			int cityId, int pointOfInterestId,
			JsonPatchDocument<PointOfInterestForUpdateDto> patchDocument)
		{
			var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
			if (city == null)
			{
				return NotFound();
			}

			var pointOfInterestFromStore = city.PointsOfInterests.FirstOrDefault(c => c.Id == pointOfInterestId);
			if (pointOfInterestFromStore == null)
			{
				return NotFound();
			}

			var pointofInterestToPatch =
				new PointOfInterestForUpdateDto()
				{
					Name = pointOfInterestFromStore.Name,
					Description = pointOfInterestFromStore.Description
				};
			patchDocument.ApplyTo(pointofInterestToPatch, ModelState);

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);

			}
			if(!TryValidateModel(pointofInterestToPatch))
			{
				return BadRequest(ModelState);
			}

			pointOfInterestFromStore.Name = pointofInterestToPatch.Name;
			pointOfInterestFromStore.Description = pointofInterestToPatch.Description;

			return NoContent();
		}
		[HttpDelete("{pointofinterestid}")]

		public ActionResult DeletePointOfInterest ( int cityId, int pointOfInterestId)
		{
			var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
			if (city == null)
			{
				return NotFound();
			}

			var pointOfInterestFromStore = city.PointsOfInterests.FirstOrDefault(c => c.Id == pointOfInterestId);
			if (pointOfInterestFromStore == null)
			{
				return NotFound();
			}

			city.PointsOfInterests.Remove(pointOfInterestFromStore);
			return NoContent();
		}
		
	}
}
