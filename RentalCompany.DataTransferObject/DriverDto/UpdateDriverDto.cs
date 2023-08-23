using RentalCompany.DataTransferObject.CarDto;

namespace RentalCompany.DataTransferObject.DriverDto
{
    public class UpdateDriverDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public CarResponse car { get; set; }
    }
}
