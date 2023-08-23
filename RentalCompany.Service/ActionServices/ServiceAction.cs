using Microsoft.Extensions.Localization;
using RentalCompany.Repository.Car_Repository;
using RentalCompany.Service.Resources;
using RentalCompany.SharedKernel.Operation_Result;

namespace RentalCompany.Service.ActionServices
{
    public class ServiceAction : IServiceAction

    {
        private readonly ICarRepository _carRepository;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;


        public ServiceAction(ICarRepository carRepository, IStringLocalizer<SharedResources> stringLocalizer
                           )
        {
            _carRepository = carRepository;
            _stringLocalizer = stringLocalizer;

        }

        public async Task<OperationResult<string>> ActioncustomerWithCar(Guid IdCustomer, Guid IdCar)
        {
            OperationResult<string> operation = new OperationResult<string>();
            try
            {

                var car = await _carRepository.GetByIdAsync(IdCar);
                if (car == null)
                {
                    operation.OperationResultType = OperationResultTypes.NotExist;
                    operation.Message = _stringLocalizer[SharedResourcesKeys.Faild];
                    return operation;
                }
                car.CustomerId = IdCustomer;
                var result = await _carRepository.UpdateAsync(car);
                if (!result)
                {
                    operation.OperationResultType = OperationResultTypes.Failed;
                    operation.Message = _stringLocalizer[SharedResourcesKeys.Faild];
                    operation.Reslut = result.ToString();
                    return operation;
                }
                operation.OperationResultType = OperationResultTypes.Success;
                operation.Message = _stringLocalizer[SharedResourcesKeys.Updated];
                operation.Reslut = result.ToString();
                return operation;


            }
            catch (Exception ex)
            {
                operation.OperationResultType = OperationResultTypes.Exception;
                operation.Message = _stringLocalizer[SharedResourcesKeys.Faild];
                operation.Exception = ex;

            }
            return operation;
        }

        public async Task<OperationResult<string>> ActionDriverWithCar(Guid IdDriver, Guid IdCar)
        {
            OperationResult<string> operation = new OperationResult<string>();
            try
            {
                var car = await _carRepository.GetByIdAsync(IdCar);
                if (car == null)
                {
                    operation.OperationResultType = OperationResultTypes.NotExist;
                    operation.Message = _stringLocalizer[SharedResourcesKeys.Faild];
                    return operation;
                }
                car.DriverId = IdDriver;
                var result = await _carRepository.UpdateAsync(car);
                if (!result)
                {
                    operation.OperationResultType = OperationResultTypes.Failed;
                    operation.Message = _stringLocalizer[SharedResourcesKeys.Faild];
                    operation.Reslut = result.ToString();
                    return operation;
                }
                operation.OperationResultType = OperationResultTypes.Success;
                operation.Message = _stringLocalizer[SharedResourcesKeys.Updated];
                operation.Reslut = result.ToString();
                return operation;

            }
            catch (Exception ex)
            {
                operation.OperationResultType = OperationResultTypes.Exception;
                operation.Message = _stringLocalizer[SharedResourcesKeys.Faild];
                operation.Exception = ex;
            }
            return operation;
        }
    }
}
