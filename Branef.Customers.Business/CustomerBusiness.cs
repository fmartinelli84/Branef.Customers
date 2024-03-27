using Branef.Customers.Data;
using Branef.Customers.Dtos;
using Branef.Customers.Entities;
using Branef.Framework.Data;
using Branef.Framework.Exceptions;
using Branef.Framework.Jobs;
using Branef.Framework.Processes;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Branef.Customers.Business
{
    public class CustomerBusiness : BaseBusiness<CustomersDbContext>
    {
        public CustomerBusiness(CustomersDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<CustomerDto?> GetByIdAsync(long id)
        {
            var customer = await this.dbContext.Customers.AsExpandable()
                .Where(c => c.Id == id)
                .Select(Customer.ToFullDto)
                .FirstOrDefaultAsync();

            return customer;
        }

        public async Task<List<CustomerDto>> GetAllAsync()
        {
            var customers = await this.dbContext.Customers.AsExpandable()
                .Select(Customer.ToFullDto)
                .OrderBy(c => c.Id)
                .ToListAsync();

            return customers;
        }

        public async Task<CustomerDto?> CreateAsync(CustomerDto customer)
        {
            this.EnsureIsValid(customer);

            var newCustomer = new Customer(customer, this.dbContext);

            this.dbContext.Customers.Add(newCustomer);

            await this.dbContext.SaveChangesAsync();

            customer.Id = newCustomer.Id;

            return await this.GetByIdAsync(customer.Id);
        }

        public async Task<CustomerDto?> UpdateAsync(CustomerDto movement)
        {
            this.EnsureIsValid(movement);

            var currentMovement = await dbContext.Customers
                .Where(m => m.Id == movement.Id)
                .FirstOrDefaultAsync();

            if (currentMovement != null)
            {
                currentMovement.FromDto(movement, this.dbContext);

                await dbContext.SaveChangesAsync();

                return await this.GetByIdAsync(movement.Id);
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
                var customer = await this.GetByIdAsync(currentCustomer.Id);

                this.dbContext.Customers.Remove(currentCustomer);

                await this.dbContext.SaveChangesAsync();

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
    }
}