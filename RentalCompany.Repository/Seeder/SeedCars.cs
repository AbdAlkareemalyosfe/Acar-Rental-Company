using Microsoft.Extensions.DependencyInjection;
using RentalCompany.Models.CarModels;
using RentalCompany.SqlServer;

namespace RentalCompany.Repository.Seeder
{
    public class SeedCars
    {

        public static async Task SeedAsync(IServiceProvider service)
        {
            var context = service.GetService<ApplicationDbContext>();
            if (!context.CarsSet.Any())
            {
                try
                {
                    var cars = new List<Car>
                 {
                    new Car
                    {
                        CarNumber = "ABC123",
                        Type = "Sedan",
                        EngineCapacity = "2.0L",
                        Color = "Red",
                        DailyFareWithDriver = 50.0m,
                        DailyFareWithoutDriver = 30.0m,
                        WithDriver = true,
                        DriverId = new Guid("9133748A-495E-4A03-A91E-08DBA31D8814"),

                    },
                    new Car
                    {
                        CarNumber = "XYZ789",
                        Type = "SUV",
                        EngineCapacity = "3.5L",
                        Color = "Blue",
                        DailyFareWithDriver = 70.0m,
                        DailyFareWithoutDriver = 50.0m,
                        WithDriver = false,
                        DriverId = new Guid("10C9DE57-6282-49D8-A91F-08DBA31D8814")
                    }


                 };
                    await context.CarsSet.AddRangeAsync(cars);
                    var result = await context.SaveChangesAsync();
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
    }
}