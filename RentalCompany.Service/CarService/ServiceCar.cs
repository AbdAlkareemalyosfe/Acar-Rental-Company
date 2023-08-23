using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using RentalCompany.DataTransferObject.CarDto;
using RentalCompany.Models.CarModels;
using RentalCompany.Repository.Car_Repository;
using RentalCompany.Service.Resources;
using RentalCompany.SharedKernel.Operation_Result;
using RentalCompany.SharedKernel.OrderingEnum;

namespace RentalCompany.Service.CarService
{
    public class ServiceCar : IServiceCar
    {
        #region Field
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly ICarRepository _carRepository;
        #endregion

        #region Ctor
        public ServiceCar(IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer, ICarRepository carRepository)
        {
            _carRepository = carRepository;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;

        }
        #endregion

        #region Action
        public async Task<OperationResult<string>> DeletACar(Guid Id)
        {
            OperationResult<string> operation = new OperationResult<string>();
            try
            {
                var IsExist = await _carRepository.ISExist(Id);
                if (!IsExist)
                {
                    operation.OperationResultType = OperationResultTypes.NotExist;
                    operation.Message = _stringLocalizer[SharedResourcesKeys.IsNotExist];
                    operation.Reslut = IsExist.ToString();
                    return operation;
                }
                var car = await _carRepository.GetByIdAsync(Id);

                car.DateDeleted = DateTime.UtcNow;
                car.IsDeleted = true;
                var result = await _carRepository.UpdateAsync(car);
                if (result)
                {
                    operation.OperationResultType = OperationResultTypes.Success;
                    operation.Message = _stringLocalizer[SharedResourcesKeys.Deleted];
                    operation.Reslut = IsExist.ToString();
                }
            }
            catch (Exception ex)
            {
                operation.OperationResultType = OperationResultTypes.Exception;
                operation.Exception = ex;
            }
            return operation;
        }


        public async Task<OperationResult<CarResponse>> GetAllCars(bool Available, int pageNumber, int PagSize)
        {
            OperationResult<CarResponse> operation = new OperationResult<CarResponse>();
            try
            {
                var results = _carRepository.GetTableAsTracking();

                if (results == null)
                {
                    operation.OperationResultType = OperationResultTypes.NoElement;
                    operation.Message = _stringLocalizer[SharedResourcesKeys.Faild];
                    return operation;
                }
                if (Available)
                    results = results.Where(x => !x.CustomerId.HasValue);
                var carMapping = _mapper.Map<IEnumerable<CarResponse>>(results.Include(d => d.driver).Include(c => c.customer));
                operation.OperationResultType = OperationResultTypes.Success;
                operation.Message = _stringLocalizer[SharedResourcesKeys.Success];
                operation.RangeResults = carMapping;
            }
            catch (Exception ex)
            {
                operation.OperationResultType = OperationResultTypes.Exception;
                operation.Exception = ex;
            }
            return operation;

        }
        /*
        public async Task<OperationResult<CarResponse>> GetAllCarsWithOrder(int Order = 0)
        {
            OperationResult<CarResponse> operation = new OperationResult<CarResponse>();
            try
            {
                var results = _carRepository.GetTableAsTracking();
                if (results == null)
                {
                    operation.OperationResultType = OperationResultTypes.NoElement;
                }
                switch (Order)
                {
                    case 1:
                        results = results.OrderBy(x => x.DateCreated); break;
                    case 2:
                        results = results.OrderBy(x => x.DailyFareWithDriver); break;
                    case 3:
                        results = results.OrderBy(x => x.DailyFareWithoutDriver); break;
                    case 4:
                        results = results.OrderBy(x => x.WithDriver); break;
                    default:
                        results = results.OrderBy(x => x.Id); break;


                }
                operation.OperationResultType = OperationResultTypes.Failed;
                operation.Message = _stringLocalizer[SharedResourcesKeys.Faild];

                var carMapping = _mapper.Map<IEnumerable<CarResponse>>(results);
                operation.OperationResultType = OperationResultTypes.Success;
                operation.Message = _stringLocalizer[SharedResourcesKeys.Success];
                operation.RangeResults = carMapping;


            }
            catch (Exception ex)
            {
                operation.OperationResultType = OperationResultTypes.Exception;
                operation.Exception = ex;
            }
            return operation;

        }*/

        public async Task<OperationResult<CarResponse>> GetCarById(Guid Id)
        {
            OperationResult<CarResponse> operation = new OperationResult<CarResponse>();
            try
            {
                var IsExist = await _carRepository.ISExist(Id);
                if (!IsExist)
                {
                    operation.OperationResultType = OperationResultTypes.NotExist;
                    operation.Message = _stringLocalizer[SharedResourcesKeys.IsNotExist];
                    operation.Reslut = null;
                    return operation;
                }
                var result = await _carRepository.GetByIdAsync(Id);

                var carMapping = _mapper.Map<CarResponse>(result);
                operation.OperationResultType = OperationResultTypes.Success;
                operation.Message = _stringLocalizer[SharedResourcesKeys.Success];
                operation.Reslut = carMapping;

            }
            catch (Exception ex)
            {
                operation.OperationResultType = OperationResultTypes.Exception;
                operation.Exception = ex;
            }
            return operation;

        }

        public async Task<OperationResult<CarResponse>> FilterCars(CarOrder order, string search = "")
        {
            OperationResult<CarResponse> operation = new OperationResult<CarResponse>();
            try
            {
                var cars = _carRepository.GetTableNoTracking().Include(d => d.driver).Include(c => c.customer).AsQueryable();
                if (!string.IsNullOrEmpty(search))
                {
                    cars = cars.Where(x => x.CarNumber.Contains(search) ||
                                      x.Type.Contains(search) ||
                                      x.EngineCapacity.Contains(search) ||
                                      x.Color.Contains(search)
                                     );
                }
                switch ((int)order)
                {
                    case 1:
                        cars = cars.OrderBy(x => x.DateCreated);
                        break;
                    case 2:
                        cars = cars.OrderBy(x => x.CarNumber);
                        break;
                    case 3:
                        cars = cars.OrderBy(x => x.Color);
                        break;
                    case 4:
                        cars = cars.OrderBy(x => x.DailyFareWithDriver);
                        break;
                    case 5:
                        cars = cars.OrderBy(x => x.DailyFareWithoutDriver);
                        break;
                    case 6:
                        cars = cars.OrderBy(x => x.WithDriver);
                        break;

                    default:
                        cars = cars.OrderBy(x => x.Id);
                        break;
                }
                var results = _mapper.Map<IEnumerable<CarResponse>>(cars);
                operation.OperationResultType = OperationResultTypes.Success;
                operation.RangeResults = results;
                operation.Message = _stringLocalizer[SharedResourcesKeys.Success];

            }
            catch (Exception ex)
            {
                operation.OperationResultType = OperationResultTypes.Exception;
                operation.Exception = ex;
            }
            return operation;

        }

        public async Task<OperationResult<string>> UpdateACar(UpdateCarDto carDto)
        {
            OperationResult<string> operation = new OperationResult<string>();
            try
            {
                var IsExist = await _carRepository.ISExist(carDto.Id);
                if (!IsExist)
                {
                    operation.OperationResultType = OperationResultTypes.Failed;
                    operation.Message = _stringLocalizer[SharedResourcesKeys.Faild];
                    operation.Reslut = IsExist.ToString();
                    return operation;
                }
                var car = _mapper.Map<Car>(carDto);
                var result = await _carRepository.UpdateAsync(car);
                if (result)
                {
                    operation.OperationResultType = OperationResultTypes.Success;
                    operation.Message = _stringLocalizer[SharedResourcesKeys.Updated];
                    return operation;
                }
                operation.OperationResultType = OperationResultTypes.Failed;
                operation.Message = _stringLocalizer[SharedResourcesKeys.Faild];

            }
            catch (Exception ex)
            {
                operation.OperationResultType = OperationResultTypes.Exception;
                operation.Exception = ex;
            }
            return operation;

        }

        public async Task<OperationResult<string>> AddCar(AddCarDto carDto)
        {
            OperationResult<string> operation = new OperationResult<string>();
            try
            {
                var car = _mapper.Map<Car>(carDto);
                var result = _carRepository.AddAsync(car);
                if (result == null)
                {
                    operation.OperationResultType = OperationResultTypes.Failed;
                    operation.Message = _stringLocalizer[SharedResourcesKeys.Faild];
                    operation.Reslut = null;
                    return operation;
                }
                operation.OperationResultType = OperationResultTypes.Success;
                operation.Message = _stringLocalizer[SharedResourcesKeys.Success];
                operation.Reslut = _stringLocalizer[SharedResourcesKeys.Created];

            }
            catch (Exception ex)
            {
                operation.OperationResultType = OperationResultTypes.Exception;
                operation.Exception = ex;

            }
            return operation;
        }

        public async Task<OperationResult<CarResponse>> AddRangCars(IEnumerable<AddCarDto> carDto)
        {
            OperationResult<CarResponse> operation = new OperationResult<CarResponse>();
            try
            {
                var car = _mapper.Map<IEnumerable<Car>>(carDto);
                var carresponce = _mapper.Map<CarResponse>(car);
                var result = _carRepository.AddRangeAsync(car);
                if (result == null)
                {
                    operation.OperationResultType = OperationResultTypes.Failed;
                    operation.Message = _stringLocalizer[SharedResourcesKeys.Faild];
                    operation.Reslut = null;
                    return operation;
                }
                operation.OperationResultType = OperationResultTypes.Success;
                operation.Message = _stringLocalizer[SharedResourcesKeys.Created];
                operation.Reslut = carresponce;

            }
            catch (Exception ex)
            {
                operation.OperationResultType = OperationResultTypes.Exception;
                operation.Exception = ex;

            }
            return operation;
        }

        #endregion
    }
}
