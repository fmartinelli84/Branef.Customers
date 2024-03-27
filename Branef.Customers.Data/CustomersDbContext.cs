using Branef.Customers.Entities;
using Branef.Framework.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

namespace Branef.Customers.Data
{
    public class CustomersDbContext : DbContext
    {
        private readonly IServiceProvider serviceProvider;

        public CustomersDbContext(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public CustomersDbContext(DbContextOptions<CustomersDbContext> options, IServiceProvider serviceProvider)
            : base(options)
        {
            this.serviceProvider = serviceProvider;
        }


        public DbSet<Customer> Customers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.ApplyConfiguration(new MovementConfiguration());


            modelBuilder.Model
                .GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys())
                .ToList()
                .ForEach(fk => fk.DeleteBehavior = DeleteBehavior.Restrict);
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            this.FillTrackable();

            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            this.FillTrackable();

            return base.SaveChanges(acceptAllChangesOnSuccess);
        }
    }
}