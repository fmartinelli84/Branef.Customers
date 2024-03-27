using Branef.Customers.Dtos;
using Branef.Framework.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Branef.Customers.Entities
{
    public class Customer : BaseTrackableEntity, IMapFromDto<CustomerDto, Customer>
    {
        public Customer()
        {
        }

        public Customer(CustomerDto dto, DbContext dbContext)
        {
            this.FromDto(dto, dbContext);
        }

        public long Id { get; set; }

        [MaxLength(256)]
        public string Name { get; set; } = null!;

        public CustomerSize Size { get; set; }




        public static readonly Expression<Func<Customer, CustomerDto>> ToFullDto =
            m => new CustomerDto()
            {
                Id = m.Id,
                Name = m.Name,
                Size = m.Size,
                CreatedAtDate = m.CreatedAtDate,
                ModifiedAtDate = m.ModifiedAtDate
            };


        public Customer FromDto(CustomerDto dto, DbContext dbContext)
        {
            this.Id = dto.Id;
            this.Name = dto.Name.Trim();
            this.Size = dto.Size;

            return this;
        }
    }

    public class MovementConfiguration : BaseEntityTypeConfiguration<Customer>
    {
        public override void Configure(EntityTypeBuilder<Customer> builder)
        {
            
        }
    }
}