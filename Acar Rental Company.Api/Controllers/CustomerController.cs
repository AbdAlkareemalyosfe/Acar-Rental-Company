using Acar_Rental_Company.Api.Helper;
using Microsoft.AspNetCore.Mvc;
using RentalCompany.DataTransferObject.CustomerDto;
using RentalCompany.Service.CustomerService;
using RentalCompany.SharedKernel.OrderingEnum;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Acar_Rental_Company.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IServiceCustomer _serviceCustomer;

        public CustomerController(IServiceCustomer serviceCustomer)
        {
            _serviceCustomer = serviceCustomer;
        }

        // GET: api/<CarsController>
        [HttpGet("GetAllCustomeres")]
        public async Task<IActionResult> GetAllCastomerAsTrack(int pageNumber = 0, int PagSize = 0)
        {
            var results = await _serviceCustomer.GetAllCustomeres(pageNumber, PagSize);

            return results.ToActionResultes();
        }
        // GET: api/<CarsController>
        [HttpGet("FilterCustomer")]
        public async Task<IActionResult> FilterCustomer(CarOrder OrderNum, string search = null)
        {
            var results = await _serviceCustomer.FilterCustomeresBy(OrderNum, search);
            return results.ToActionResultes();
        }
        // GET api/<CarsController>/5
        [HttpGet("GetCastomerBy/{id}")]
        public async Task<IActionResult> GetCarById(Guid id)
        {
            var result = await _serviceCustomer.GetCustomerById(id);
            return result.ToActionResult();
        }

        // POST api/<CarsController>
        [HttpPost("AddCastomer")]
        public async Task<IActionResult> AddCar([FromBody] AddCustomerDto customerDto)
        {
            var result = await _serviceCustomer.AddACustomer(customerDto);
            return result.ToActionResult();
        }
        [HttpPost("AddRangeCastomerses")]
        public async Task<IActionResult> AddRange(IEnumerable<AddCustomerDto> customerDto)
        {
            var result = await _serviceCustomer.AddCustomeres(customerDto);
            return result.ToActionResult();
        }
        // PUT api/<CarsController>/5


        // DELETE api/<CarsController>/5
        [HttpDelete("DeletCastomer/{id}")]
        public async Task<IActionResult> DeleteCastomer(Guid id)
        {
            var result = await _serviceCustomer.DeletACustomer(id);
            return result.ToActionResult();
        }
        [HttpPut("UpdatInfoCustomer")]
        public async Task<IActionResult> Updat(UpdateCustomerDto updateCustomer)
        {
            var result = await _serviceCustomer.UpdateACustomer(updateCustomer);
            return result.ToActionResult();
        }
    }
}
