using RentalCompany.Models.Base;
using RentalCompany.Models.CustomerModel;
using RentalCompany.Models.DriverModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentalCompany.Models.CarModels
{
    public class Car : BaseEntity
    {

        public string CarNumber { get; set; }
        public string Type { get; set; }
        public string EngineCapacity { get; set; }
        public string Color { get; set; }
        public decimal DailyFareWithDriver { get; set; }
        public decimal DailyFareWithoutDriver { get; set; }
        public bool WithDriver { get; set; }

        public Driver? driver { get; set; }
        [ForeignKey("DriverId")]
        public Guid? DriverId { get; set; }

        public Customer? customer { get; set; }

        [ForeignKey("CustomerId")]
        public Guid? CustomerId { get; set; }

    }
}
