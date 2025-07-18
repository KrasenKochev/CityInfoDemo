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
				new City("Lovech dage")
				{
					Id = 1,
					Description = "The hometown"
				},
				new City("Pleven da-ge")
				{
					Id = 2,
					Description = "The da-ge original city"
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
				new PointOfInterest("zooPark")
				{
					Id = 2,
					CityId =2,
					Description = "The animal zoo"
				}
				);


			base.OnModelCreating(modelBuilder);
		}

		//protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		//{
		//	optionsBuilder.UseSqlite("connectionstring");

		//	base.OnConfiguring(optionsBuilder);
		//}



	}
}
