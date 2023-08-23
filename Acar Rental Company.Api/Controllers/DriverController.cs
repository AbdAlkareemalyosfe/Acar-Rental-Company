using Acar_Rental_Company.Api.Helper;
using Microsoft.AspNetCore.Mvc;
using RentalCompany.DataTransferObject.DriverDto;
using RentalCompany.Service.DriverService;
using RentalCompany.SharedKernel.OrderingEnum;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Acar_Rental_Company.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriverController : ControllerBase
    {
        // GET: api/<DriverController>
        private readonly IServiceDriver _serviceDriver;

        public DriverController(IServiceDriver serviceDriver)
        {
            _serviceDriver = serviceDriver;
        }

        // GET: api/<CarsController>
        [HttpGet("GetAllDriveres")]
        public async Task<IActionResult> GetAllDriver(int pageNumber = 0, int PagSize = 0)
        {
            var results = await _serviceDriver.GetAllDriveres(pageNumber, PagSize);

            return results.ToActionResultes();
        }
        // GET: api/<CarsController>
        [HttpGet("FilterDrivers")]
        public async Task<IActionResult> FilterDrivers(CarOrder OrderNum, string? search)
        {
            var results = await _serviceDriver.FilterDriveresBy(OrderNum, search);
            return results.ToActionResultes();
        }
        // GET api/<CarsController>/5
        [HttpGet("GetDriverBy/{id}")]
        public async Task<IActionResult> GetDriverById(Guid id)
        {
            var result = await _serviceDriver.GetDriverById(id);
            return result.ToActionResult();
        }

        // POST api/<CarsController>
        [HttpPost("AddDriver")]
        public async Task<IActionResult> AddDriver([FromBody] AddDriverDto driverDto)
        {
            var result = await _serviceDriver.AddADriver(driverDto);
            return result.ToActionResult();
        }
        [HttpPost("AddRangeDriveres")]
        public async Task<IActionResult> AddRange(IEnumerable<AddDriverDto> driverDtos)
        {
            var result = await _serviceDriver.AddDriveres(driverDtos);
            return result.ToActionResult();
        }
        // PUT api/<CarsController>/5


        // DELETE api/<CarsController>/5
        [HttpDelete("DeletDrriver/{id}")]
        public async Task<IActionResult> DeleteCastomer(Guid id)
        {
            var result = await _serviceDriver.DeletADriver(id);
            return result.ToActionResult();
        }
        [HttpPut("UpdateInfoDriver")]
        public async Task<IActionResult> Update(UpdateDriverDto updateDriver)
        {
            var result = await _serviceDriver.UpdateADriver(updateDriver);
            return result.ToActionResult();
        }
    }
}
