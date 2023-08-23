using RentalCompany.DataTransferObject.CarDto;
using RentalCompany.SharedKernel.Operation_Result;
using RentalCompany.SharedKernel.OrderingEnum;

namespace RentalCompany.Service.CarService
{
    public interface IServiceCar
    {
        Task<OperationResult<CarResponse>> GetAllCars(bool Available, int pageNumber, int PagSize);
        //    Task<OperationResult<CarResponse>> GetAllCarsWithOrder(int Order);
        Task<OperationResult<CarResponse>> GetCarById(Guid Id);
        Task<OperationResult<string>> DeletACar(Guid Id);
        Task<OperationResult<string>> UpdateACar(UpdateCarDto carDto);
        Task<OperationResult<CarResponse>> FilterCars(CarOrder order, string search = "");
        Task<OperationResult<string>> AddCar(AddCarDto carDto);
        Task<OperationResult<CarResponse>> AddRangCars(IEnumerable<AddCarDto> carDto);



    }
}
