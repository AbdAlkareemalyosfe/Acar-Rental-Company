using Microsoft.Extensions.DependencyInjection;
using RentalCompany.Models.CarModels;
using RentalCompany.Models.CustomerModel;
using RentalCompany.SqlServer;

namespace RentalCompany.Repository.Seeder
{
    public static class SeedCustomer
    {
        public static async Task SeedAsync(IServiceProvider service)
        {
            var context = service.GetService<ApplicationDbContext>();
            if (!context.CustomersSet.Any())
            {
                try
                {
                    var Customers = new List<Customer>
                    {
                       new Customer
                       {
                           Name = "Customer 1",
                           RentedCars = new List<Car>
                           {
                           }
                       },
                       new Customer
                       {
                           Name = "Customer 2",
                           RentedCars = new List<Car>
                           {
                           }
                       },

                    };
                    await context.CustomersSet.AddRangeAsync(Customers);
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
