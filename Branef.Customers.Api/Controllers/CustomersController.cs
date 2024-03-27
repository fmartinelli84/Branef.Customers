using Branef.Customers.Business;
using Branef.Customers.Dtos;
using Branef.Customers.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Branef.Customers.Api.Controllers
{
    [ApiController]
    [Route("customers")]
    public class CustomersController : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto?>> GetByIdAsync(
            [FromRoute] long id, 
            [FromServices] CustomerBusiness customerBusiness)
        {
            return await customerBusiness.GetByIdAsync(id);
        }

        [HttpGet("")]
        public async Task<ActionResult<List<CustomerDto>>> GetAllAsync(
            [FromServices] CustomerBusiness customerBusiness)
        {
            return await customerBusiness.GetAllAsync();
        }

        [HttpPost("")]
        public async Task<ActionResult<CustomerDto?>> CreateAsync(
            [FromBody] CustomerDto customer,
            [FromServices] CustomerBusiness customerBusiness)
        {
            return await customerBusiness.CreateAsync(customer);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CustomerDto?>> UpdateAsync(
            [FromRoute] long id,
            [FromBody] CustomerDto customer,
            [FromServices] CustomerBusiness customerBusiness)
        {
            customer.Id = id;
            return await customerBusiness.UpdateAsync(customer);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CustomerDto?>> DeleteAsync(
            [FromRoute] long id,
            [FromServices] CustomerBusiness customerBusiness)
        {
            return await customerBusiness.DeleteAsync(id);
        }
    }
}
