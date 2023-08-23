using AutoMapper;
using Microsoft.Extensions.Localization;
using RentalCompany.DataTransferObject.CustomerDto;
using RentalCompany.Models.CustomerModel;
using RentalCompany.Repository.Pagination;
using RentalCompany.Repository.User_Repository;
using RentalCompany.Service.Resources;
using RentalCompany.SharedKernel.Operation_Result;
using RentalCompany.SharedKernel.OrderingEnum;

namespace RentalCompany.Service.CustomerService
{
    public class ServiceCustomer : IServiceCustomer
    {
        #region Fields
        private readonly ICustomerRepository _customerRepository;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IMapper _mapper;
        #endregion

        #region Ctor
        public ServiceCustomer(ICustomerRepository customerRepository, IStringLocalizer<SharedResources> stringLocalizer, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _stringLocalizer = stringLocalizer;
            _mapper = mapper;
        }
        #endregion

        #region Action 
        public async Task<OperationResult<string>> AddACustomer(AddCustomerDto customerDto)
        {
            OperationResult<string> operationResult = new OperationResult<string>();
            try
            {
                var customer = _mapper.Map<Customer>(customerDto);
                if (customer == null)
                {
                    operationResult.OperationResultType = OperationResultTypes.Failed;
                    operationResult.Message = _stringLocalizer[SharedResourcesKeys.Faild];
                    operationResult.Reslut = null;
                    return operationResult;
                }
                var result = await _customerRepository.AddAsync(customer);
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
                operationResult.Message = _stringLocalizer[SharedResourcesKeys.Faild];
                operationResult.Exception = ex;

            }
            return operationResult;
        }

        public async Task<OperationResult<CustomerResponse>> AddCustomeres(IEnumerable<AddCustomerDto> customerDto)
        {
            OperationResult<CustomerResponse> operationResult = new OperationResult<CustomerResponse>();
            try
            {
                var customeres = _mapper.Map<IEnumerable<Customer>>(customerDto);
                if (customeres == null)
                {
                    operationResult.OperationResultType = OperationResultTypes.Failed;
                    operationResult.Message = _stringLocalizer[SharedResourcesKeys.Faild];
                    operationResult.Reslut = null;
                    return operationResult;
                }
                var result = _customerRepository.AddRangeAsync(customeres);
                if (result == null)
                {
                    operationResult.OperationResultType = OperationResultTypes.Failed;
                    operationResult.Message = _stringLocalizer[SharedResourcesKeys.Faild];
                    operationResult.Reslut = null;
                    return operationResult;
                }
                var customerRespomse = _mapper.Map<IEnumerable<CustomerResponse>>(result);
                operationResult.OperationResultType = OperationResultTypes.Success;
                operationResult.Message = _stringLocalizer[SharedResourcesKeys.Created];
                operationResult.RangeResults = customerRespomse;

            }
            catch (Exception ex)
            {
                operationResult.OperationResultType = OperationResultTypes.Exception;
                operationResult.Message = _stringLocalizer[SharedResourcesKeys.Faild];
                operationResult.Exception = ex;

            }
            return operationResult;
        }

        public async Task<OperationResult<string>> DeletACustomer(Guid Id)
        {
            OperationResult<string> operationResult = new OperationResult<string>();
            try
            {
                var ISExist = await _customerRepository.ISExist(Id);
                if (!ISExist)
                {

                    operationResult.OperationResultType = OperationResultTypes.NotExist;
                    operationResult.Message = _stringLocalizer[SharedResourcesKeys.IsNotExist];
                    operationResult.Reslut = ISExist.ToString();
                    return operationResult;
                }
                var Customer = await _customerRepository.GetByIdAsync(Id);
                Customer.IsDeleted = true;
                Customer.DateDeleted = DateTime.Now;
                var result = await _customerRepository.UpdateAsync(Customer);
                if (!result)
                {
                    operationResult.OperationResultType = OperationResultTypes.Failed;
                    operationResult.Message = _stringLocalizer[SharedResourcesKeys.Faild];
                    operationResult.Reslut = ISExist.ToString();
                    return operationResult;
                }
                operationResult.OperationResultType = OperationResultTypes.Success;
                operationResult.Message = _stringLocalizer[SharedResourcesKeys.Updated];
                operationResult.Reslut = _stringLocalizer[SharedResourcesKeys.Success];
                return operationResult;
            }
            catch (Exception ex)
            {
                operationResult.OperationResultType = OperationResultTypes.Exception;
                operationResult.Message = _stringLocalizer[SharedResourcesKeys.Faild];
                operationResult.Exception = ex;

            }
            return operationResult;
        }

        public async Task<OperationResult<CustomerResponse>> FilterCustomeresBy(CarOrder order, string search = "")
        {
            OperationResult<CustomerResponse> operationResult = new OperationResult<CustomerResponse>();
            try
            {

                var customers = _customerRepository.GetTableNoTracking().AsQueryable();

                if (!string.IsNullOrEmpty(search))
                {
                    customers = customers.Where(x => x.Name.Contains(search));
                }
                switch ((int)order)
                {
                    case 1:
                        customers = customers.OrderBy(x => x.DateCreated);
                        break;
                    case 2:
                        customers = customers.OrderBy(x => x.Name);
                        break;
                    default:
                        customers = customers.OrderBy(x => x.Id);
                        break;
                }
                var results = _mapper.Map<IEnumerable<CustomerResponse>>(customers);
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

        public async Task<OperationResult<CustomerResponse>> GetAllCustomeres(int pageNumber, int PagSize)
        {
            OperationResult<CustomerResponse> operationResult = new OperationResult<CustomerResponse>();
            try
            {
                var results = _customerRepository.GetTableAsTracking().ToPaginatedListAsync(pageNumber, PagSize);
                if (results == null)
                {
                    operationResult.OperationResultType = OperationResultTypes.NoElement;
                    operationResult.Message = _stringLocalizer[SharedResourcesKeys.Faild];
                    return operationResult;
                }
                var customers = _mapper.Map<IEnumerable<CustomerResponse>>(results);
                operationResult.OperationResultType = OperationResultTypes.Success;
                operationResult.Message = _stringLocalizer[SharedResourcesKeys.Success];
                operationResult.RangeResults = customers;


            }
            catch (Exception ex)
            {
                operationResult.OperationResultType = OperationResultTypes.Exception;
                operationResult.Message = _stringLocalizer[SharedResourcesKeys.Faild];
                operationResult.Exception = ex;

            }
            return operationResult;
        }



        public async Task<OperationResult<CustomerResponse>> GetCustomerById(Guid Id)
        {
            OperationResult<CustomerResponse> operation = new OperationResult<CustomerResponse>();
            try
            {
                var IsExist = await _customerRepository.ISExist(Id);
                if (!IsExist)
                {
                    operation.OperationResultType = OperationResultTypes.NotExist;
                    operation.Message = _stringLocalizer[SharedResourcesKeys.IsNotExist];
                    operation.Reslut = null;
                    return operation;
                }
                var result = await _customerRepository.GetByIdAsync(Id);

                var customerResponse = _mapper.Map<CustomerResponse>(result);
                operation.OperationResultType = OperationResultTypes.Success;
                operation.Message = _stringLocalizer[SharedResourcesKeys.Success];
                operation.Reslut = customerResponse;

            }
            catch (Exception ex)
            {
                operation.OperationResultType = OperationResultTypes.Exception;
                operation.Exception = ex;
            }
            return operation;

        }
        /*
        public async Task<OperationResult<CustomerResponse>> OrderCustomeresBy(int order)
        {
            OperationResult<CustomerResponse> operationResult = new OperationResult<CustomerResponse>();
            try
            {
                var results = _customerRepository.GetTableAsTracking();
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
        }*/

        public async Task<OperationResult<string>> UpdateACustomer(UpdateCustomerDto customerDto)
        {
            OperationResult<string> operationResult = new OperationResult<string>();
            try
            {

                var ISExist = await _customerRepository.ISExist(customerDto.Id);
                if (!ISExist)
                {

                    operationResult.OperationResultType = OperationResultTypes.NotExist;
                    operationResult.Message = _stringLocalizer[SharedResourcesKeys.IsNotExist];
                    operationResult.Reslut = ISExist.ToString();
                    return operationResult;
                }
                var customer = _mapper.Map<Customer>(customerDto);
                var result = await _customerRepository.UpdateAsync(customer);
                if (!result)
                {
                    operationResult.OperationResultType = OperationResultTypes.Failed;
                    operationResult.Message = _stringLocalizer[SharedResourcesKeys.Faild];
                    operationResult.Reslut = ISExist.ToString();
                    return operationResult;
                }
                operationResult.OperationResultType = OperationResultTypes.Success;
                operationResult.Message = _stringLocalizer[SharedResourcesKeys.Updated];
                operationResult.Reslut = _stringLocalizer[SharedResourcesKeys.Success];
                return operationResult;

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
