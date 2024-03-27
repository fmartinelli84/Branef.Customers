using Branef.Customers.Business;
using Branef.Customers.Data;
using Branef.Framework.Api;
using Branef.Framework.Data;
using Branef.Framework.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.ConfigureLogging();

builder.Services.AddApi<CustomersDbContext>(builder.Configuration);
builder.Services.AddMongoDbContext(builder.Configuration, connectionString => new CustomersMongoDbContext(connectionString));

builder.Services.AddBusiness(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseApi(app.Environment);

// Create a migrate database (for test only)
using var scope = app.Services.CreateScope();
scope.ServiceProvider
    .GetRequiredService<CustomersDbContext>()
    .Database
    .Migrate();

app.Run();