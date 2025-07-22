using CityInfo.API.Models;

namespace CityInfo.API
{
	public class CitiesDataStore
	{
		public List<CityDto> Cities { get; set; }

		//public static CitiesDataStore Current { get; } = new CitiesDataStore();

		public CitiesDataStore() {

			Cities = new List<CityDto>()
			{
				new CityDto()
				{
					Id = 1,
					Name = "new yourk city",
					Description = "the city with addicts",
					PointsOfInterests = new List<PointOfInterestDto>()
					{
						new PointOfInterestDto()
						{
							Id =1,
							Name = "central Park",
							Description = " Something, something, bla bla"
						},
						new PointOfInterestDto()
						{
							Id =2,
							Name = "central Park number two",
							Description = " Something, something, bla bla, number two"
						}
					}
				},
				new CityDto()
				{
					Id = 2,
					Name = "lovech",
					Description = "the hometown city",
					PointsOfInterests = new List<PointOfInterestDto>()
					{
						new PointOfInterestDto()
						{
							Id =3,
							Name = "My Home",
							Description = "The place I live"
						},
						new PointOfInterestDto()
						{
							Id = 4,
							Name ="My hub",
							Description = "Place to hang out"
						}
					}
				},
				new CityDto()
				{
					Id = 3,
					Name = "bashbunar",
					Description = "the dog city",
					PointsOfInterests = new List<PointOfInterestDto>()
					{
						new PointOfInterestDto()
						{
							Id =5,
							Name = "homeless Dogs",
							Description = "They are hungry and dirty"
						}
					}
				},

			};
		
		}
	}
}
