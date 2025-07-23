 using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.DbContexts
{
	public class CityInfoContext : DbContext
	{
		public DbSet<City>Cities { get; set; }
		public DbSet<PointOfInterest> PointsOfInterest { get; set; }

		public CityInfoContext(DbContextOptions<CityInfoContext> options)
			: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{


			modelBuilder.Entity<City>()
				.HasData(
				new City("Lovech")
				{
					Id = 1,
					Description = "The hometown"
				},
				new City("Pleven da-ge")
				{
					Id = 2,
					Description = "The da-ge original city"
				},
				new City("Paris")
				{
					Id = 3,
					Description = "The one with that big tower"
				}
				);

			modelBuilder.Entity<PointOfInterest>()
				.HasData(
				new PointOfInterest("Home")
				{
					Id = 1,
					CityId =1,
					Description = "The home"
				},
				new PointOfInterest("Empire State Building")
				{
					Id = 2,
					CityId = 1,
					Description = "A Skycraper"
				},

				new PointOfInterest("zooPark")
				{
					Id = 3,
					CityId =2,
					Description = "The animal zoo"
				},
				new PointOfInterest("Antwerp Central Station")
				{
					Id=4,
					CityId = 2,
					Description = "The finest station"
				},
				new PointOfInterest("Eifell Tower")
				{
					Id=5,
					CityId = 3,
					Description ="The big Tower"
				},
				new PointOfInterest("The Louvre")
				{
					Id = 5,
					CityId = 3,
					Description = "Largest Museum"
				}
				);

			modelBuilder.Entity<City>().HasMany<PointOfInterest>(c => c.PointsOfInterest);
			base.OnModelCreating(modelBuilder);
		}

		//protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		//{
		//	optionsBuilder.UseSqlite("connectionstring");

		//	base.OnConfiguring(optionsBuilder);
		//}



	}
}
