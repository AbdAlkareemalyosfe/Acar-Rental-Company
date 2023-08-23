using RentalCompany.Models.Base;
using RentalCompany.Models.CarModels;

namespace RentalCompany.Models.CustomerModel
{
    public class Customer : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<Car> RentedCars { get; set; }
    }
}
