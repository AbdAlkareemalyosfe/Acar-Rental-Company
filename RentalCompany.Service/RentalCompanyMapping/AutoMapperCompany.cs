using AutoMapper;
using RentalCompany.DataTransferObject.CarDto;
using RentalCompany.DataTransferObject.CustomerDto;
using RentalCompany.DataTransferObject.DriverDto;
using RentalCompany.Models.CarModels;
using RentalCompany.Models.CustomerModel;
using RentalCompany.Models.DriverModel;

namespace RentalCompany.Service.RentalCompanyMapping
{
    public class AutoMapperCompany : Profile
    {
        public AutoMapperCompany()
        {
            #region Mapper Car
            CreateMap<Car, CarResponse>()
                .ForMember(dst => dst.customerName, opt => opt.MapFrom(src => src.customer.Name))
                .ForMember(dst => dst.DriverName, opt => opt.MapFrom(src => src.driver.Name));
            CreateMap<AddCarDto, Car>();
            CreateMap<UpdateCarDto, Car>();
            #endregion

            #region Mapper Driver 
            CreateMap<Driver, DriverReponse>();
            CreateMap<AddDriverDto, Driver>();
            CreateMap<UpdateDriverDto, Driver>();
            #endregion

            #region Mapper Customer 
            CreateMap<Customer, CustomerResponse>();
            CreateMap<AddCustomerDto, Customer>();
            CreateMap<UpdateCustomerDto, Customer>();
            #endregion
        }
    }
}
