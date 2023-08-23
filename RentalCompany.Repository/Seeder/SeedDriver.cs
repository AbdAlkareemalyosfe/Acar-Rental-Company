using Microsoft.Extensions.DependencyInjection;
using RentalCompany.Models.DriverModel;
using RentalCompany.SqlServer;

namespace RentalCompany.Repository.Seeder
{
    public static class SeedDriver
    {
        public static async Task SeedAsync(IServiceProvider service)
        {
            var context = service.GetService<ApplicationDbContext>();
            if (!context.DriversSet.Any())
            {
                try
                {
                    var drivers = new List<Driver>
                     {
                         new Driver { Name = "Driver 2" },
                         new Driver { Name = "Driver 1" },
                         // Add more drivers as needed
                     };



                    await context.DriversSet.AddRangeAsync(drivers);
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