using Branef.Customers.Business;
using Branef.Customers.Data;
using Branef.Framework.Api;
using Branef.Framework.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.ConfigureLogging();

builder.Services.AddApi<CustomersDbContext>(builder.Configuration);

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