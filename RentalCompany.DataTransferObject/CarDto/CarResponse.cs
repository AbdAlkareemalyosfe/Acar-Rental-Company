namespace RentalCompany.DataTransferObject.CarDto
{
    public class CarResponse
    {
        public string CarNumber { get; set; }
        public string Type { get; set; }
        public string EngineCapacity { get; set; }
        public string Color { get; set; }
        public decimal DailyFareWithDriver { get; set; }
        public decimal DailyFareWithoutDriver { get; set; }
        public bool WithDriver { get; set; }
        public string DriverName { get; set; }
        public string customerName { get; set; }
    }
}
