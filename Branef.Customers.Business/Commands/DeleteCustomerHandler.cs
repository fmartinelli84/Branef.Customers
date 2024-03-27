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
    public class DeleteCustomerHandler : BaseCustomerHandler, IRequestHandler<DeleteCustomerCommand, CustomerDto?>
    {
        public DeleteCustomerHandler(CustomersDbContext dbContext, CustomersMongoDbContext mongoDbContext)
            : base(dbContext, mongoDbContext)
        {
        }

        public async Task<CustomerDto?> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var id = request.Id;

            var currentCustomer = await dbContext.Customers
                .Where(m => m.Id == id)
                .FirstOrDefaultAsync();

            if (currentCustomer != null)
            {
                var customer = await GetByIdFromDbContextAsync(currentCustomer.Id);

                dbContext.Customers.Remove(currentCustomer);

                await dbContext.SaveChangesAsync();

                await mongoDbContext.Customers().DeleteOneAsync(
                    Builders<CustomerDto>.Filter.Eq(c => c.Id, currentCustomer.Id));

                return customer;
            }

            return null;
        }
    }
}
