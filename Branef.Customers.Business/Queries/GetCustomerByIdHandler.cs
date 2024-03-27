using Branef.Customers.Data;
using Branef.Customers.Dtos;
using Branef.Customers.Dtos.Queries;
using Branef.Customers.Entities;
using Branef.Framework.Data;
using Branef.Framework.Exceptions;
using LinqKit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Branef.Customers.Business.Queries
{
    public class GetCustomerByIdHandler : BaseCustomerHandler, IRequestHandler<GetCustomerByIdQuery, CustomerDto?>
    {
        public GetCustomerByIdHandler(CustomersDbContext dbContext, CustomersMongoDbContext mongoDbContext)
            : base(dbContext, mongoDbContext)
        {
        }

        public async Task<CustomerDto?> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var id = request.Id;

            var customer = await IAsyncCursorSourceExtensions.FirstOrDefaultAsync(
                mongoDbContext.Customers().AsQueryable()
                    .Where(c => c.Id == id));

            return customer;
        }
    }
}
