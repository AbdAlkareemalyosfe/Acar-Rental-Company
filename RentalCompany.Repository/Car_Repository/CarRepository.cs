using RentalCompany.Models.CarModels;
using RentalCompany.Repository.BaseRepository;
using RentalCompany.SqlServer;

namespace RentalCompany.Repository.Car_Repository
{
    public class CarRepository : RepositoryBase<Car>, ICarRepository
    {
        public CarRepository(ApplicationDbContext context) : base(context)
        {

        }

    }
}
