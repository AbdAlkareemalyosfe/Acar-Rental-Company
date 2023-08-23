using RentalCompany.DataTransferObject.CarDto;

namespace RentalCompany.DataTransferObject.CustomerDto
{
    public class UpdateCustomerDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<CarResponse> cars { get; set; }


    }
}
