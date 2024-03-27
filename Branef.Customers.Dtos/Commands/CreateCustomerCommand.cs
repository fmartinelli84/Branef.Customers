using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Branef.Customers.Dtos.Commands
{
    public class CreateCustomerCommand : IRequest<CustomerDto?>
    {
        public CustomerDto Data { get; set; } = null!;
    }
}
