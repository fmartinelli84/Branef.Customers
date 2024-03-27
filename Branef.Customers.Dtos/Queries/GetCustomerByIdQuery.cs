using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Branef.Customers.Dtos.Queries
{
    public class GetCustomerByIdQuery : IRequest<CustomerDto?>
    {
        public long Id { get; set; }
    }
}
