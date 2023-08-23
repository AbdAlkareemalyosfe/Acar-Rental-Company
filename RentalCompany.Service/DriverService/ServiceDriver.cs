using AutoMapper;
using Microsoft.Extensions.Localization;
using RentalCompany.DataTransferObject.DriverDto;
using RentalCompany.Models.DriverModel;
using RentalCompany.Repository.Driver_Repository;
using RentalCompany.Service.Resources;
using RentalCompany.SharedKernel.Operation_Result;
using RentalCompany.SharedKernel.OrderingEnum;

namespace RentalCompany.Service.DriverService
{
    public class ServiceDriver : IServiceDriver
    {
        #region Fields
        private readonly IDriverRepository _driverRepository;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        #endregion

        #region Ctor
        public ServiceDriver(IDriverRepository driverRepository, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer)
        {
            _driverRepository = driverRepository;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
        }
        #endregion

        #region Action
        public async Task<OperationResult<string>> AddADriver(AddDriverDto driverDto)
        {
            OperationResult<string> operationResult = new OperationResult<string>();
            try
            {
                var Driver = _mapper.Map<Driver>(driverDto);
                var result = await _driverRepository.AddAsync(Driver);
                if (result == null)
                {
                    operationResult.OperationResultType = OperationResultTypes.Failed;
                    operationResult.Message = _stringLocalizer[SharedResourcesKeys.Faild];
                    operationResult.Reslut = null;
                    return operationResult;
                }
                operationResult.OperationResultType = OperationResultTypes.Success;
                operationResult.Message = _stringLocalizer[SharedResourcesKeys.Success];
                operationResult.Reslut = _stringLocalizer[SharedResourcesKeys.Created];


            }
            catch (Exception ex)
            {
                operationResult.OperationResultType = OperationResultTypes.Exception;
                operationResult.Message = ex.Message;
            }
            return operationResult;
        }

        public async Task<OperationResult<DriverReponse>> AddDriveres(IEnumerable<AddDriverDto> carDto)
        {
            OperationResult<DriverReponse> operationResult = new OperationResult<DriverReponse>();
            try
            {
                var Driveres = _mapper.Map<IEnumerable<Driver>>(carDto);
                var result = _driverRepository.AddRangeAsync(Driveres);
                if (result == null)
                {
                    operationResult.OperationResultType = OperationResultTypes.Failed;
                    operationResult.Message = _stringLocalizer[SharedResourcesKeys.Faild];
                    operationResult.Reslut = null;
                    return operationResult;
                }
                var driverresponse = _mapper.Map<IEnumerable<DriverReponse>>(result);
                operationResult.OperationResultType = OperationResultTypes.Success;
                operationResult.Message = _stringLocalizer[SharedResourcesKeys.Created];
                operationResult.RangeResults = driverresponse;


            }
            catch (Exception ex)
            {
                operationResult.OperationResultType = OperationResultTypes.Exception;
                operationResult.Message = ex.Message;
            }
            return operationResult;
        }
        public async Task<OperationResult<string>> DeletADriver(Guid Id)
        {
            OperationResult<string> operationResult = new OperationResult<string>();
            try
            {
                var ISExist = await _driverRepository.ISExist(Id);
                if (!ISExist)
                {
                    operationResult.OperationResultType = OperationResultTypes.Failed;
                    operationResult.Message = _stringLocalizer[SharedResourcesKeys.IsNotExist];
                    operationResult.Reslut = null;
                    return operationResult;
                }
                var driver = await _driverRepository.GetByIdAsync(Id);
                driver.IsDeleted = true;
                driver.DateDeleted = DateTime.Now;
                var result = await _driverRepository.UpdateAsync(driver);
                operationResult.OperationResultType = OperationResultTypes.Success;
                operationResult.Reslut = result.ToString();
                operationResult.Message = _stringLocalizer[SharedResourcesKeys.Success];

            }
            catch (Exception ex)
            {
                operationResult.OperationResultType = OperationResultTypes.Exception;
                operationResult.Message = _stringLocalizer[SharedResourcesKeys.Faild];
                operationResult.Exception = ex;

            }
            return operationResult;
        }

        public async Task<OperationResult<DriverReponse>> FilterDriveresBy(CarOrder order, string search = "")
        {
            OperationResult<DriverReponse> operationResult = new OperationResult<DriverReponse>();
            try
            {
                var drivers = _driverRepository.GetTableNoTracking().AsQueryable();
                if (!string.IsNullOrEmpty(search))
                {
                    drivers = drivers.Where(x => x.Name.Contains(search));
                }
                switch ((int)order)
                {
                    case 1:
                        drivers = drivers.OrderBy(x => x.DateCreated);
                        break;
                    case 2:
                        drivers = drivers.OrderBy(x => x.Name);
                        break;
                    default:
                        drivers = drivers.OrderBy(x => x.Id);
                        break;
                }
                var results = _mapper.Map<IEnumerable<DriverReponse>>(drivers);
                operationResult.OperationResultType = OperationResultTypes.Success;
                operationResult.RangeResults = results;
                operationResult.Message = _stringLocalizer[SharedResourcesKeys.Success];

            }
            catch (Exception ex)
            {
                operationResult.OperationResultType = OperationResultTypes.Exception;
                operationResult.Message = _stringLocalizer[SharedResourcesKeys.Faild];
                operationResult.Exception = ex;

            }
            return operationResult;
        }

        public async Task<OperationResult<DriverReponse>> GetAllDriveres(int pageNumber, int PagSize)

        {
            OperationResult<DriverReponse> operationResult = new OperationResult<DriverReponse>();
            try
            {
                var results = _driverRepository.GetTableAsTracking();
                if (results == null)
                {
                    operationResult.OperationResultType = OperationResultTypes.NoElement;
                    operationResult.Message = _stringLocalizer[SharedResourcesKeys.Faild];
                    return operationResult;
                }
                var Drivers = _mapper.Map<IEnumerable<DriverReponse>>(results);
                operationResult.OperationResultType = OperationResultTypes.Success;
                operationResult.Message = _stringLocalizer[SharedResourcesKeys.Success];
                operationResult.RangeResults = Drivers;
            }
            catch (Exception ex)
            {
                operationResult.OperationResultType = OperationResultTypes.Exception;
                operationResult.Message = _stringLocalizer[SharedResourcesKeys.Faild];
                operationResult.Exception = ex;

            }
            return operationResult;
        }



        public async Task<OperationResult<DriverReponse>> GetDriverById(Guid Id)
        {
            OperationResult<DriverReponse> operationResult = new OperationResult<DriverReponse>();
            try
            {

                var IsExist = await _driverRepository.ISExist(Id);
                if (!IsExist)
                {
                    operationResult.OperationResultType = OperationResultTypes.NotExist;
                    operationResult.Message = _stringLocalizer[SharedResourcesKeys.IsNotExist];
                    operationResult.Reslut = null;
                    return operationResult;
                }
                var result = await _driverRepository.GetByIdAsync(Id);

                var driverResponse = _mapper.Map<DriverReponse>(result);
                operationResult.OperationResultType = OperationResultTypes.Success;
                operationResult.Message = _stringLocalizer[SharedResourcesKeys.Success];
                operationResult.Reslut = driverResponse;



            }
            catch (Exception ex)
            {
                operationResult.OperationResultType = OperationResultTypes.Exception;
                operationResult.Message = _stringLocalizer[SharedResourcesKeys.Faild];
                operationResult.Exception = ex;

            }
            return operationResult;
        }

        /* public async Task<OperationResult<DriverReponse>> OrderDriveresBy(int order)
         {
             OperationResult<DriverReponse> operationResult = new OperationResult<DriverReponse>();
             try
             {
                 var results = _driverRepository.GetTableAsTracking();
                 if (results == null)
                 {
                     switch (order)
                     {
                         case 1:
                             results = results.OrderBy(x => x.DateCreated); break;
                         case 2:
                             results = results.OrderBy(x => x.Name); break;
                         default:
                             results = results.OrderBy(x => x.Id); break;


                     }
                     operationResult.OperationResultType = OperationResultTypes.Failed;
                     operationResult.Message = _stringLocalizer[SharedResourcesKeys.Faild];
                 }
                 var customer = _mapper.Map<IEnumerable<CustomerResponse>>(results);
                 operationResult.OperationResultType = OperationResultTypes.Success;
                 operationResult.Message = _stringLocalizer[SharedResourcesKeys.Success];
                 operationResult.RangeResults = customer;

             }
             catch (Exception ex)
             {
                 operationResult.OperationResultType = OperationResultTypes.Exception;
                 operationResult.Message = _stringLocalizer[SharedResourcesKeys.Faild];
                 operationResult.Exception = ex;

             }
             return operationResult;
         }
        */
        public async Task<OperationResult<string>> UpdateADriver(UpdateDriverDto DriverDto)
        {
            OperationResult<string> operationResult = new OperationResult<string>();
            try
            {
                bool ISExist = await _driverRepository.ISExist(DriverDto.Id);
                var driver = _mapper.Map<Driver>(DriverDto);
                var result = await _driverRepository.UpdateAsync(driver);
                if (!result)
                {
                    operationResult.OperationResultType = OperationResultTypes.Failed;
                    operationResult.Message = _stringLocalizer[SharedResourcesKeys.Faild];
                    operationResult.Reslut = result.ToString();
                    return operationResult;
                }
                operationResult.OperationResultType = OperationResultTypes.Success;
                operationResult.Reslut = result.ToString();
                operationResult.Message = _stringLocalizer[SharedResourcesKeys.Updated];


            }
            catch (Exception ex)
            {
                operationResult.OperationResultType = OperationResultTypes.Exception;
                operationResult.Message = _stringLocalizer[SharedResourcesKeys.Faild];
                operationResult.Exception = ex;

            }
            return operationResult;
        }


        #endregion
    }
}
