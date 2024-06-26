﻿using Branef.Framework.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Branef.Framework.Data
{
    public static class DataExtensions
    {
        public static IServiceCollection AddDbContext<TDbContext>(this IServiceCollection services, IConfiguration configuration, string connectionName = "DefaultConnection")
            where TDbContext : DbContext
        {
            services.AddDbContext<TDbContext, TDbContext>(configuration, connectionName);

            return services;
        }

        public static IServiceCollection AddDbContext<TDbContextService, TDbContextImplementation>(this IServiceCollection services, IConfiguration configuration, string connectionName = "DefaultConnection")
            where TDbContextImplementation : DbContext, TDbContextService
        {
            services.AddDbContext<TDbContextService, TDbContextImplementation>(
                optionsAction: options =>
                {
                    options.ConfigureWarnings(w => w.Default(WarningBehavior.Throw));
                    options.ConfigureWarnings(w => w.Ignore(SqlServerEventId.DecimalTypeKeyWarning));
                    options.ConfigureWarnings(w => w.Ignore(RelationalEventId.MultipleCollectionIncludeWarning));
                    options.ConfigureWarnings(w => w.Ignore(CoreEventId.FirstWithoutOrderByAndFilterWarning));
                    options.ConfigureWarnings(w => w.Ignore(CoreEventId.RowLimitingOperationWithoutOrderByWarning));
                    options.ConfigureWarnings(w => w.Ignore(CoreEventId.SensitiveDataLoggingEnabledWarning));

                    options.EnableSensitiveDataLogging();

                    options.UseSqlServer(configuration.GetConnectionString(connectionName), sqlOptions =>
                    {
                        sqlOptions.CommandTimeout(60);
                        sqlOptions.EnableRetryOnFailure();
                    });
                });

            return services;
        }


        public static IServiceCollection AddMongoDbContext<TMongoDbContext>(this IServiceCollection services, IConfiguration configuration, Func<string, TMongoDbContext> implementationFactory, string connectionName = "MongoDbConnection")
            where TMongoDbContext : MongoDbContext
        {
            services.AddMongoDbContext<TMongoDbContext, TMongoDbContext>(configuration, implementationFactory, connectionName);

            return services;
        }

        public static IServiceCollection AddMongoDbContext<TMongoDbContextService, TMongoDbContextImplementation>(this IServiceCollection services, IConfiguration configuration, Func<string, TMongoDbContextImplementation> implementationFactory, string connectionName = "MongoDbConnection")
            where TMongoDbContextService : class
            where TMongoDbContextImplementation : MongoDbContext, TMongoDbContextService
        {
            services.AddScoped<TMongoDbContextService, TMongoDbContextImplementation>(options => implementationFactory(configuration.GetConnectionString(connectionName)!));

            return services;
        }
    }
}
