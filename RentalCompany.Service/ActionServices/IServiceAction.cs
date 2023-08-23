using RentalCompany.SharedKernel.Operation_Result;

namespace RentalCompany.Service.ActionServices
{
    public interface IServiceAction
    {
        Task<OperationResult<string>> ActionDriverWithCar(Guid IdDriver, Guid IdCar);
        Task<OperationResult<string>> ActioncustomerWithCar(Guid IdCustomer, Guid IdCar);
    }
}
