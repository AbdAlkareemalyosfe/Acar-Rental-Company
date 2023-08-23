using Microsoft.Extensions.DependencyInjection;
using RentalCompany.Repository.Seeder;
using RentalCompany.Service.ActionServices;
using RentalCompany.Service.CarService;
using RentalCompany.Service.CustomerService;
using RentalCompany.Service.DriverService;

namespace RentalCompany.Service.ServicesExtinsions
{
    public static class ServicesExtinsions
    {
        public static void AddMyServices(this IServiceCollection services)
        {

            services.AddTransient<IServiceCustomer, ServiceCustomer>();
            services.AddTransient<IServiceDriver, ServiceDriver>();
            services.AddTransient<IServiceCar, ServiceCar>();
            services.AddTransient<IServiceAction, ServiceAction>();

            services.AddScoped<SeedCars>();
        }

    }
}
