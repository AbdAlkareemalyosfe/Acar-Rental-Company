using RentalCompany.DataTransferObject.CustomerDto;
using RentalCompany.SharedKernel.Operation_Result;
using RentalCompany.SharedKernel.OrderingEnum;

namespace RentalCompany.Service.CustomerService
{
    public interface IServiceCustomer
    {
        Task<OperationResult<CustomerResponse>> GetAllCustomeres(int pageNumber, int PagSize);
        // Task<OperationResult<CustomerResponse>> GetAllCustomeresWithOrdering(int Order);
        Task<OperationResult<CustomerResponse>> GetCustomerById(Guid Id);
        Task<OperationResult<string>> DeletACustomer(Guid Id);
        Task<OperationResult<string>> UpdateACustomer(UpdateCustomerDto carDto);
        Task<OperationResult<string>> AddACustomer(AddCustomerDto carDto);
        Task<OperationResult<CustomerResponse>> AddCustomeres(IEnumerable<AddCustomerDto> carDto);
        Task<OperationResult<CustomerResponse>> FilterCustomeresBy(CarOrder order, string search = "");

    }
}
