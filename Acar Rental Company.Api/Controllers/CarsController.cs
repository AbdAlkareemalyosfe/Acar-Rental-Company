using Acar_Rental_Company.Api.Helper;
using Microsoft.AspNetCore.Mvc;
using RentalCompany.DataTransferObject.CarDto;
using RentalCompany.Service.CarService;
using RentalCompany.SharedKernel.OrderingEnum;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Acar_Rental_Company.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly IServiceCar _serviceCar;

        public CarsController(IServiceCar serviceCar)
        {
            _serviceCar = serviceCar;
        }

        // GET: api/<CarsController>
        [HttpGet("GetAllCar")]
        public async Task<IActionResult> GetAllCarAsTrack(bool AvailablCars = false, int pageNumber = 0, int PagSize = 0)
        {
            var results = await _serviceCar.GetAllCars(AvailablCars, pageNumber, PagSize);

            return results.ToActionResultes();
        }
        // GET: api/<CarsController>
        [HttpGet("FilterCars")]
        public async Task<IActionResult> FilterCars(CarOrder OrderNum, string? search)
        {
            var results = await _serviceCar.FilterCars(OrderNum, search);
            return results.ToActionResultes();
        }
        // GET api/<CarsController>/5
        [HttpGet("GetCarBy/{id}")]
        public async Task<IActionResult> GetCarById(Guid id)
        {
            var result = await _serviceCar.GetCarById(id);
            return result.ToActionResult();
        }

        // POST api/<CarsController>
        [HttpPost("AddCar")]
        public async Task<IActionResult> AddCar([FromBody] AddCarDto carDto)
        {
            var result = await _serviceCar.AddCar(carDto);
            return result.ToActionResult();
        }
        [HttpPost("AddRangeCares")]
        public async Task<IActionResult> AddRange(IEnumerable<AddCarDto> carDtos)
        {
            var result = await _serviceCar.AddRangCars(carDtos);
            return result.ToActionResult();
        }
        // PUT api/<CarsController>/5


        // DELETE api/<CarsController>/5
        [HttpDelete("DeletCar/{id}")]
        public async Task<IActionResult> DeleteCar(Guid id)
        {
            var result = await _serviceCar.DeletACar(id);
            return result.ToActionResult();
        }
        [HttpPut("UpdateInfoCar")]
        public async Task<IActionResult> UpdateAcar(UpdateCarDto carDto)
        {
            var result = await _serviceCar.UpdateACar(carDto);
            return result.ToActionResult();
        }

    }
}
