using Microsoft.Extensions.DependencyInjection;
using RentalCompany.Repository.BaseRepository;
using RentalCompany.Repository.Car_Repository;
using RentalCompany.Repository.Driver_Repository;
using RentalCompany.Repository.User_Repository;

namespace RentalCompany.Repository.RepositoryExtensions
{
    public static class RepositoryExtensions
    {
        public static void AddMyRepository(this IServiceCollection services)
        {
            services.AddTransient(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
            services.AddTransient<ICarRepository, CarRepository>();
            services.AddTransient<IDriverRepository, DriverRepository>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();


        }

    }
}
