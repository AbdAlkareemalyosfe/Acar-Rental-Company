using RentalCompany.DataTransferObject.DriverDto;
using RentalCompany.SharedKernel.Operation_Result;
using RentalCompany.SharedKernel.OrderingEnum;

namespace RentalCompany.Service.DriverService
{
    public interface IServiceDriver
    {
        Task<OperationResult<DriverReponse>> GetAllDriveres(int pageNumber, int PagSize);
        // Task<OperationResult<DriverReponse>> GetAllDriveresWithOrdering(int order);
        Task<OperationResult<DriverReponse>> GetDriverById(Guid Id);
        Task<OperationResult<string>> DeletADriver(Guid Id);
        Task<OperationResult<string>> UpdateADriver(UpdateDriverDto carDto);
        Task<OperationResult<string>> AddADriver(AddDriverDto carDto);
        Task<OperationResult<DriverReponse>> AddDriveres(IEnumerable<AddDriverDto> carDto);
        Task<OperationResult<DriverReponse>> FilterDriveresBy(CarOrder order, string search = "");


    }
}
