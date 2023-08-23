using RentalCompany.Models.CustomerModel;
using RentalCompany.Repository.BaseRepository;
using RentalCompany.SqlServer;

namespace RentalCompany.Repository.User_Repository
{
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        public CustomerRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
