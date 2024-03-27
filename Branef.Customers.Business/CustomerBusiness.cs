using Branef.Customers.Data;
using Branef.Customers.Dtos;
using Branef.Customers.Entities;
using Branef.Framework.Data;
using Branef.Framework.Exceptions;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Branef.Customers.Business
{
    public class CustomerBusiness : BaseBusiness<CustomersDbContext>
    {
        private readonly CustomersMongoDbContext mongoDbContext;

        public CustomerBusiness(CustomersDbContext dbContext, CustomersMongoDbContext mongoDbContext)
            : base(dbContext)
        {
            this.mongoDbContext = mongoDbContext;
        }

        public async Task<CustomerDto?> GetByIdAsync(long id)
        {
            var customer = await IAsyncCursorSourceExtensions.FirstOrDefaultAsync(
                this.mongoDbContext.Customers().AsQueryable()
                    .Where(c => c.Id == id));

            return customer;
        }

        public async Task<List<CustomerDto>> GetAllAsync()
        {
            var customers = await IAsyncCursorSourceExtensions.ToListAsync(
                this.mongoDbContext.Customers().AsQueryable()
                    .OrderBy(c => c.Id));

            return customers;
        }

        public async Task<CustomerDto?> CreateAsync(CustomerDto customer)
        {
            this.EnsureIsValid(customer);

            var newCustomer = new Customer(customer, this.dbContext);

            this.dbContext.Customers.Add(newCustomer);

            await this.dbContext.SaveChangesAsync();

            customer = (await this.GetByIdFromDbContextAsync(newCustomer.Id))!;

            await this.mongoDbContext.Customers().InsertOneAsync(customer);

            return customer;
        }

        public async Task<CustomerDto?> UpdateAsync(CustomerDto customer)
        {
            this.EnsureIsValid(customer);

            var currentCustomer = await dbContext.Customers
                .Where(m => m.Id == customer.Id)
                .FirstOrDefaultAsync();

            if (currentCustomer != null)
            {
                currentCustomer.FromDto(customer, this.dbContext);

                await dbContext.SaveChangesAsync();

                customer = (await this.GetByIdFromDbContextAsync(currentCustomer.Id))!;

                await this.mongoDbContext.Customers().ReplaceOneAsync(
                    Builders<CustomerDto>.Filter.Eq(c => c.Id, currentCustomer.Id),
                    customer);

                return customer;
            }

            return null;
        }

        public async Task<CustomerDto?> DeleteAsync(long id)
        {
            var currentCustomer = await dbContext.Customers
                .Where(m => m.Id == id)
                .FirstOrDefaultAsync();

            if (currentCustomer != null)
            {
                var customer = await this.GetByIdFromDbContextAsync(currentCustomer.Id);

                this.dbContext.Customers.Remove(currentCustomer);

                await this.dbContext.SaveChangesAsync();

                await this.mongoDbContext.Customers().DeleteOneAsync(
                    Builders<CustomerDto>.Filter.Eq(c => c.Id, currentCustomer.Id));

                return customer;
            }

            return null;
        }

        private void EnsureIsValid(CustomerDto customer)
        {
            if (string.IsNullOrWhiteSpace(customer.Name))
                throw new BusinessException("Name cannot be empty.");

            if (customer.Size != CustomerSize.Small && customer.Size != CustomerSize.Medium && customer.Size != CustomerSize.Big)
                throw new BusinessException("Size must be Small (1), Medium (2) or Big (3).");
        }

        private async Task<CustomerDto?> GetByIdFromDbContextAsync(long id)
        {
            var customer = await this.dbContext.Customers.AsExpandable()
                .Where(c => c.Id == id)
                .Select(Customer.ToFullDto)
                .FirstOrDefaultAsync();

            return customer;
        }
    }
}