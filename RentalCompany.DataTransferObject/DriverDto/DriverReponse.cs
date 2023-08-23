using RentalCompany.DataTransferObject.CarDto;

namespace RentalCompany.DataTransferObject.DriverDto
{
    public class DriverReponse
    {
        public string Name { get; set; }
        public CarResponse car { get; set; }

    }
}
