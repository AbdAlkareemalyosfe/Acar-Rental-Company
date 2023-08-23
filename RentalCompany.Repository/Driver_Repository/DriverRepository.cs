using RentalCompany.Models.DriverModel;
using RentalCompany.Repository.BaseRepository;
using RentalCompany.SqlServer;

namespace RentalCompany.Repository.Driver_Repository
{
    public class DriverRepository : RepositoryBase<Driver>, IDriverRepository
    {
        public DriverRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
