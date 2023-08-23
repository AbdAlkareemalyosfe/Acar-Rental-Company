using Acar_Rental_Company.Api.Helper;
using Microsoft.AspNetCore.Mvc;
using RentalCompany.Service.ActionServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Acar_Rental_Company.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActionController : ControllerBase
    {
        private readonly IServiceAction _serviceAction;
        public ActionController(IServiceAction serviceAction)
        {
            _serviceAction = serviceAction;
        }

        [HttpPost("ActionDriverToCar")]
        public async Task<IActionResult> ActionDriver(Guid driverId, Guid CarId)
        {
            var result = await _serviceAction.ActionDriverWithCar(driverId, CarId);
            return result.ToActionResult();
        }
        [HttpPost("ActionCustomerToCar")]
        public async Task<IActionResult> ActionCustomer(Guid CustomerId, Guid CarId)
        {
            var result = await _serviceAction.ActioncustomerWithCar(CustomerId, CarId);
            return result.ToActionResult();
        }
    }
}
