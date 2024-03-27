using Branef.Customers.Business;
using Branef.Customers.Dtos;
using Branef.Customers.Dtos.Commands;
using Branef.Customers.Dtos.Queries;
using Branef.Customers.Entities;
using MediatR;
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
            [FromServices] IMediator mediator)
        {
            return await mediator.Send(new GetCustomerByIdQuery() { Id = id });
        }

        [HttpGet("")]
        public async Task<ActionResult<List<CustomerDto>>> GetAllAsync(
            [FromServices] IMediator mediator)
        {
            return await mediator.Send(new GetAllCustomersQuery());
        }

        [HttpPost("")]
        public async Task<ActionResult<CustomerDto?>> CreateAsync(
            [FromBody] CustomerDto customer,
            [FromServices] IMediator mediator)
        {
            return await mediator.Send(new CreateCustomerCommand() { Data = customer });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CustomerDto?>> UpdateAsync(
            [FromRoute] long id,
            [FromBody] CustomerDto customer,
            [FromServices] IMediator mediator)
        {
            customer.Id = id;
            return await mediator.Send(new UpdateCustomerCommand() { Data = customer });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CustomerDto?>> DeleteAsync(
            [FromRoute] long id,
            [FromServices] IMediator mediator)
        {
            return await mediator.Send(new DeleteCustomerCommand() { Id = id });
        }
    }
}
