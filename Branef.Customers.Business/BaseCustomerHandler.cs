using Branef.Customers.Data;
using Branef.Customers.Dtos;
using Branef.Customers.Entities;
using Branef.Framework.Exceptions;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Branef.Customers.Business
{
    public abstract class BaseCustomerHandler
    {
        protected readonly CustomersDbContext dbContext;
        protected readonly CustomersMongoDbContext mongoDbContext;

        public BaseCustomerHandler(CustomersDbContext dbContext, CustomersMongoDbContext mongoDbContext)
        {
            this.dbContext = dbContext;
            this.mongoDbContext = mongoDbContext;
        }

        protected void EnsureIsValid(CustomerDto customer)
        {
            if (string.IsNullOrWhiteSpace(customer.Name))
                throw new BusinessException("Name cannot be empty.");

            if (customer.Size != CustomerSize.Small && customer.Size != CustomerSize.Medium && customer.Size != CustomerSize.Big)
                throw new BusinessException("Size must be Small (1), Medium (2) or Big (3).");
        }

        protected async Task<CustomerDto?> GetByIdFromDbContextAsync(long id)
        {
            var customer = await this.dbContext.Customers.AsExpandable()
                .Where(c => c.Id == id)
                .Select(Customer.ToFullDto)
                .FirstOrDefaultAsync();

            return customer;
        }
    }
}
