using Branef.Customers.Data;
using Branef.Customers.Dtos;
using Branef.Customers.Dtos.Commands;
using Branef.Customers.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Branef.Customers.Business.Commands
{
    public class UpdateCustomerHandler : BaseCustomerHandler, IRequestHandler<UpdateCustomerCommand, CustomerDto?>
    {
        public UpdateCustomerHandler(CustomersDbContext dbContext, CustomersMongoDbContext mongoDbContext)
            : base(dbContext, mongoDbContext)
        {
        }

        public async Task<CustomerDto?> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = request.Data;

            EnsureIsValid(customer);

            var currentCustomer = await dbContext.Customers
                .Where(m => m.Id == customer.Id)
                .FirstOrDefaultAsync();

            if (currentCustomer != null)
            {
                currentCustomer.FromDto(customer, dbContext);

                await dbContext.SaveChangesAsync();

                customer = (await GetByIdFromDbContextAsync(currentCustomer.Id))!;

                await mongoDbContext.Customers().ReplaceOneAsync(
                    Builders<CustomerDto>.Filter.Eq(c => c.Id, currentCustomer.Id),
                    customer);

                return customer;
            }

            return null;
        }
    }
}
