using Branef.Customers.Data;
using Branef.Customers.Dtos;
using Branef.Customers.Dtos.Commands;
using Branef.Customers.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Branef.Customers.Business.Commands
{
    public class CreateCustomerHandler : BaseCustomerHandler, IRequestHandler<CreateCustomerCommand, CustomerDto?>
    {
        public CreateCustomerHandler(CustomersDbContext dbContext, CustomersMongoDbContext mongoDbContext)
            : base(dbContext, mongoDbContext)
        {
        }

        public async Task<CustomerDto?> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = request.Data;

            EnsureIsValid(customer);

            var newCustomer = new Customer(customer, dbContext);

            dbContext.Customers.Add(newCustomer);

            await dbContext.SaveChangesAsync();

            customer = (await GetByIdFromDbContextAsync(newCustomer.Id))!;

            await mongoDbContext.Customers().InsertOneAsync(customer);

            return customer;
        }
    }
}
