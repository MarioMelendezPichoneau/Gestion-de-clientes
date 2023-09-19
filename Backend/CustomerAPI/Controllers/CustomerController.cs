using CustomerAPI.CasosDeUsos;
using CustomerAPI.Dtos;
using CustomerAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CustomerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private readonly CustomerDatabaseContext _databaseContext;
        private readonly UpdateCustomerUserCas _updateCustomerUserCas;

        public CustomerController(CustomerDatabaseContext databaseContext, UpdateCustomerUserCas updateCustomerUserCase)
        {
            _databaseContext = databaseContext;
            _updateCustomerUserCas = updateCustomerUserCase;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CustomerDtos>))]
        public async Task<IActionResult> GetCustomer()
        {
            var result = _databaseContext.Customers.Select(c=>c.ToDtos()).ToList();

            return new OkObjectResult(result);
        }





        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(CustomerDtos))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCustomerId( int id)
        {
            CustomerEntity result = await _databaseContext.Get(id);

            return new OkObjectResult(result.ToDtos());
        }





        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var result = await _databaseContext.Delete(id);

            return new OkObjectResult(result);
        }




        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CustomerDtos))]
        public async Task<IActionResult> CreateCustomer(CreateCustomerDtos customer)
        {
            CustomerEntity result = await _databaseContext.Add(customer);

            return new CreatedResult($"https:localhost:7030/api/customer/{result.Id}", null);
        }




        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomerDtos))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCustomer(CustomerDtos customer)
        {
            CustomerDtos? result = await _updateCustomerUserCas.Execute(customer);

            if (result == null)
                return new NotFoundResult();
            return new OkObjectResult(result);
        }




    }
}
