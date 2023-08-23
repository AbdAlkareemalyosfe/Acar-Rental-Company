using RentalCompany.Models.Base;
using RentalCompany.Models.CarModels;

namespace RentalCompany.Models.DriverModel
{
    public class Driver : BaseEntity
    {
        public string Name { get; set; }

        public Car? Car { get; set; }


    }
}
