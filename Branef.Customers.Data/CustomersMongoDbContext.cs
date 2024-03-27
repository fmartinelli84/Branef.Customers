using Branef.Customers.Dtos;
using Branef.Framework.Data;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Branef.Customers.Data
{
    public class CustomersMongoDbContext : MongoDbContext
    {
        public CustomersMongoDbContext(string connectionString)
            : base(connectionString, "Customers")
        {
        }

        public IMongoCollection<CustomerDto> Customers() => this.Database.GetCollection<CustomerDto>(nameof(Customers));
    }
}
