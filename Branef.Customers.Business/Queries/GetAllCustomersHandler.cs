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
    public class GetAllCustomersHandler : BaseCustomerHandler, IRequestHandler<GetAllCustomersQuery, List<CustomerDto>>
    {
        public GetAllCustomersHandler(CustomersDbContext dbContext, CustomersMongoDbContext mongoDbContext)
            : base(dbContext, mongoDbContext)
        {
        }

        public async Task<List<CustomerDto>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            var customers = await IAsyncCursorSourceExtensions.ToListAsync(
                mongoDbContext.Customers().AsQueryable()
                    .OrderBy(c => c.Id));

            return customers;
        }
    }
}
